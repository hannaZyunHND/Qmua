using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.Extensions;
using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.Store;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly UserManager<AppUser> _userManager;

        public PermissionController(IPermissionRepository permissionRepository, UserManager<AppUser> userManager)
        {
            _permissionRepository = permissionRepository;
            _userManager = userManager;
        }

        [HttpGet("function-actions")]
        public async Task<IActionResult> GetAllWithPermission()
        {
            var result = await _permissionRepository.GetAllWithPermission();
            return Ok(result);
        }

        [HttpGet("role-permissions")]
        public async Task<IActionResult> GetAllRolePermissions(Guid role)
        {
            var result = await _permissionRepository.GetAllRolePermissions(role);
            return Ok(result);
        }

        [HttpPost("save-permissions")]
        public async Task<IActionResult> SavePermissions(Guid role, [FromBody] List<ActionViewModel> actionViewModels)
        {
            ResponseData responseData = new ResponseData();
            var permissions = new List<PermissionViewModel>();
            foreach (var item in actionViewModels)
            {
                if (item.Functions.Count > 0)
                {
                    foreach (var itemFunction in item.Functions)
                    {
                        permissions.Add(new PermissionViewModel()
                        {
                            ActionId = item.Id,
                            FunctionId = itemFunction
                        });
                    }
                }
            }

            var result = await _permissionRepository.SavePermissions(role, permissions);
            if (result > 0)
            {
                responseData.Success = true;
                responseData.Message = "Thành công";
                return Ok(responseData);
            }

            responseData.Success = false;
            responseData.Message = "Thất bại";
            return Ok(responseData);
        }


        [HttpGet("functions-view")]
        public async Task<IActionResult> GetAllFunctionByPermission()
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
            var roles = await _userManager.GetRolesAsync(user);
            var roleByUser = string.Join(";", roles);
            var result = await _permissionRepository.GetAllFunctionByPermission(roleByUser, User.GetUserId());
            return Ok(result);
        }
    }
}
