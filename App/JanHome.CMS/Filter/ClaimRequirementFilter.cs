using System.Linq;
using MI.Dapper.Data.Constant;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JanHome.CMS.Filter
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly FunctionCode _function;
        private readonly ActionCode _action;
        private readonly IPermissionRepository _permissionRepository;
        private readonly UserManager<AppUser> _userManager;

        public ClaimRequirementFilter(FunctionCode function, ActionCode action,
            IPermissionRepository permissionRepository, UserManager<AppUser> userManager)
        {
            _function = function;
            _action = action;
            _permissionRepository = permissionRepository;
            _userManager = userManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId =
                context.HttpContext.User.Claims.SingleOrDefault(c => c.Type == SystemConstants.UserClaim.Id);
            if (userId != null)
            {
                var user = _userManager.FindByIdAsync(userId.Value).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                var rolesClaim = string.Join(";", roles);
                if (rolesClaim.Contains("Admin"))
                {
                    return;
                }

                var permissionsClaim = _permissionRepository.GetPermissionByUserId(userId.Value).Result;
                if (permissionsClaim != null)
                {
                    var functionArr = _function.ToString().Split("_");
                    var functionId = string.Join(".", functionArr);
                    if (!permissionsClaim.Contains(functionId + "_" + _action))
                    {
                        context.Result = new ForbidResult();
                    }
                }
                else
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
