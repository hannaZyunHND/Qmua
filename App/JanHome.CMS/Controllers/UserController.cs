using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.Extensions;
using JanHome.CMS.Filter;
using MI.Dapper.Data.Constant;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, IUserRepository userRepository,
            IPermissionRepository permissionRepository, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _roleManager = roleManager;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.GetUserId();
            var currentUser = new CurrentUserViewModel();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var roleNames = await _userManager.GetRolesAsync(user);
                var roles = string.Join(";", await _userManager.GetRolesAsync(user));
                var functionPermissions = new List<FunctionViewModel>();
                foreach (var item in roleNames)
                {
                    functionPermissions.AddRange(
                        await _permissionRepository.GetAllFunctionByPermissionCurrentUser(item, userId));
                }

                var listFunctionString = functionPermissions.Select(item => item.Url).ToList();
                listFunctionString.Insert(0, "/admin/profile");
                listFunctionString.Insert(1, "/admin/profile/change-password");
                var functionPermission = string.Join(";", listFunctionString);
                var result = await _permissionRepository.GetPermissionByUserId(userId.ToString());
                var permissions = string.Join(";", result);
                currentUser = new CurrentUserViewModel()
                {
                    Email = user.Email,
                    Id = user.Id.ToString(),
                    Permissions = permissions,
                    Roles = roles,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    PermissionUrl = functionPermission,
                    Avatar = user.Avatar,
                    Address = user.Address
                };
            }

            return Ok(currentUser);
        }

        [HttpGet("getall")]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, ActionCode.VIEW)]
        public async Task<ResponseData> Get()
        {
            var responseData = new ResponseData();
            var result = await _userRepository.GetAll();
            responseData.Success = true;
            responseData.Message = "Thành công";
            responseData.ListData = result.ToList<object>();
            return responseData;
        }

        [HttpGet("getRoleNameByUserId")]
        public async Task<IActionResult> GetRoleNameByUserId(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var roleName = await _userManager.GetRolesAsync(user);
                var roleIds = new List<string>();
                foreach (var item in roleName)
                {
                    roleIds.Add(_roleManager.FindByNameAsync(item).Result.Id.ToString());
                }

                // var result = string.Join(',', roleName);
                return Ok(roleIds);
            }

            return Ok(new List<string>());
        }

        [HttpPost("lockUser")]
        public async Task<IActionResult> LockUser(string id)
        {
            var responseData = new ResponseData();
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await _userRepository.UpdateStatus(user.Id,false);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Thất bại";
                }

                return Ok(responseData);
            }

            return Ok(responseData);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("getpage")]
        public async Task<IActionResult> GetPage([FromQuery] FilterQuery filter)
        {
            var result = await _userRepository.GetAllPaging(filter);
            return Ok(result);
        }

        [HttpPost("add")]
        [ValidateModel]
        [ClaimRequirement(FunctionCode.SYSTEM_USER, ActionCode.CREATE)]
        public async Task<IActionResult> Post([FromBody] AppUser user)
        {
            var responseData = new ResponseData();
            if (user != null)
            {
                user.InActive = true;
            }

            var result = await _userManager.CreateAsync(user, SystemConstants.PassworDefault);
            if (result.Succeeded)
            {
                responseData.Success = true;
                responseData.Message = "Thành công";
                return Ok(responseData);
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return BadRequest(responseData);
        }


        [HttpPost("updateProfile")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProfile([FromBody] AppUser user)
        {
            var responseData = new ResponseData();
            var userDb = await _userManager.FindByIdAsync(user.Id.ToString());
            var findByEmail = await _userManager.FindByEmailAsync(user.Email);
            if (!userDb.Email.Equals(user.Email) && findByEmail != null)
            {
                responseData.Success = false;
                responseData.Message = "Thất bại";
                return BadRequest(responseData);
            }

            user.PasswordHash = userDb.PasswordHash;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                responseData.Success = true;
                responseData.Message = "Thành công";
                return Ok(responseData);
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return BadRequest(responseData);
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            var responseData = new ResponseData();
            var user = await _userManager.FindByIdAsync(changePasswordViewModel.UserId);
            var checkPassword = await _userManager.CheckPasswordAsync(user, changePasswordViewModel.OldPassword);

            if (checkPassword == false)
            {
                responseData.Success = false;
                responseData.Message = "Thất bại";
                return BadRequest(responseData);
            }
            else
            {
                var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword,
                    changePasswordViewModel.Password);
                if (result.Succeeded)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    return Ok(responseData);
                }
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return BadRequest(responseData);
        }


        [HttpPut("assigntoroles")]
        public async Task<IActionResult> AssignToRoles([Required] Guid id, [Required] string roleName)
        {
            var responseData = new ResponseData();
            var result = await _userRepository.AssignToRoles(id, roleName);
            if (result)
            {
                responseData.Success = true;
                responseData.Message = "Thành công";
                return Ok(responseData);
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return BadRequest(responseData);
        }

        [HttpDelete("removeroles")]
        public async Task<IActionResult> RemoveRoleToUser([Required] Guid id, [Required] string roleName)
        {
            var responseData = new ResponseData();
            var result = await _userRepository.RemoveRoleToUser(id, roleName);
            if (result)
            {
                responseData.Success = true;
                responseData.Message = "Thành công";
                return Ok(responseData);
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return BadRequest(responseData);
        }

        [HttpGet("getuserbyroles")]
        public async Task<IActionResult> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var model = await _userManager.GetRolesAsync(user);
            return Ok(model);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var responseData = new ResponseData();
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                responseData.Success = true;
                responseData.Message = "Thành công";
                return Ok(responseData);
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return BadRequest(responseData);
        }
    }
}
