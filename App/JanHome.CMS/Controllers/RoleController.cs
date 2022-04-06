using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.Extensions;
using JanHome.CMS.Filter;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleController(RoleManager<AppRole> roleManager, IRoleRepository roleRepository,
            UserManager<AppUser> userManager, IPermissionRepository permissionRepository)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _userManager = userManager;
            _permissionRepository = permissionRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleRepository.GetAll();
            return Ok(result);
        }


        [HttpGet("getalloptions")]
        public async Task<IActionResult> GetAllOptions()
        {
            var listRoles = await _roleRepository.GetAll();
            var result=new List<AppRoleOptionsViewModel>();
            foreach (var item in listRoles)
            {
                result.Add(new AppRoleOptionsViewModel()
                {
                    Id = item.Id.ToString(),
                    Label = item.Name
                });
            }
            return Ok(result);
        }

        [HttpGet("getallfunctions")]
        public async Task<IActionResult> GetAllFunctions()
        {
            var result = await _roleRepository.GetAllFunction();
            return Ok(result);
        }

        [HttpGet("getallactions")]
        public async Task<IActionResult> GetAllActions(string roleid)
        {
            try
            {
                var permissions = (await _permissionRepository.GetAllRolePermissions(new Guid(roleid)));
                var result = await _roleRepository.GetAllAction();
                foreach (var item in result)
                {
                    foreach (var itemPermission in permissions)
                    {
                        if (item.Id == itemPermission.ActionId)
                        {
                            item.Functions.Add(itemPermission.FunctionId);
                        }
                    }
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("getallpaging")]
        public async Task<IActionResult> GetPaging([FromQuery] FilterQuery filterQuery)
        {
            var result = await _roleRepository.GetAllPaging(filterQuery);
            return Ok(result);
        }

        [HttpPost("create")]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody] AppRole role)
        {
            ResponseData responseData = new ResponseData();
            var result = await _roleManager.CreateAsync(role);
            var roleNew = await _roleManager.FindByNameAsync(role.Name);
            var permissionViewModel = new List<PermissionViewModel>()
            {
                new PermissionViewModel()
                {
                    ActionId = "VIEW",
                    FunctionId = "HOME"
                }
            };
            if (roleNew.Id != null) await _permissionRepository.SavePermissions(roleNew.Id.Value, permissionViewModel);
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

        [HttpPut("update")]
        public async Task<IActionResult> Put([Required] string id, [FromBody] AppRole role)
        {
            ResponseData responseData = new ResponseData();
            role.Id = new Guid(id);
            var result = await _roleManager.UpdateAsync(role);
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            ResponseData responseData = new ResponseData();
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
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
