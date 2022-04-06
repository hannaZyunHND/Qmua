using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MI.Dapper.Data.Models;
using Utils;

namespace MI.Dapper.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetAll();
        Task<PagedResult<AppUser>> GetAllPaging(FilterQuery filterQuery);
        Task<bool> AssignToRoles(Guid id, string roleName);
        Task<bool> UpdateStatus(Guid id, bool acticve);
        Task<bool> RemoveRoleToUser(Guid id, string roleName);
    }
}