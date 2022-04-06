using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.ViewModels;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<string>> GetPermissionByUserId(string userId);
        Task<List<FunctionActionViewModel>> GetAllWithPermission();
        Task<List<FunctionPermissionViewModel>> GetAllFunctionByPermission(string roleName, Guid id);
        Task<List<FunctionViewModel>> GetAllFunctionByPermissionCurrentUser(string roleName, Guid id);
        Task<List<PermissionViewModel>> GetAllRolePermissions(Guid id);
        Task<int> SavePermissions(Guid role, List<PermissionViewModel> permissions);
    }
}