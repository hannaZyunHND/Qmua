using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Utils;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<AppUser>> GetAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    await conn.OpenAsync();

                var paramaters = new DynamicParameters();
                var result = await conn.QueryAsync<AppUser>("Get_User_All", paramaters, null, null,
                    System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<PagedResult<AppUser>> GetAllPaging(FilterQuery filterQuery)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    await conn.OpenAsync();

                var paramaters = new DynamicParameters();
                paramaters.Add("@keyword", filterQuery.keyword);
                paramaters.Add("@pageIndex", filterQuery.pageIndex);
                paramaters.Add("@pageSize", filterQuery.pageSize);
                paramaters.Add("@totalRow", dbType: System.Data.DbType.Int32,
                    direction: System.Data.ParameterDirection.Output);

                var result = await conn.QueryAsync<AppUser>("Get_User_AllPaging", paramaters, null, null,
                    System.Data.CommandType.StoredProcedure);

                int totalRow = paramaters.Get<int>("@totalRow");

                var pagedResult = new PagedResult<AppUser>()
                {
                    Items = result.ToList(),
                    TotalRow = totalRow,
                    PageIndex = filterQuery.pageIndex,
                    PageSize = filterQuery.pageSize
                };
                return pagedResult;
            }
        }

        public async Task<bool> AssignToRoles(Guid id, string roleName)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var roleIds = roleName.Split(",");

                    await connection.ExecuteAsync(
                           $"DELETE FROM [AspNetUserRoles] WHERE [UserId] = @userId ",
                            new { userId = user.Id });

                    foreach (var item in roleIds)
                    {
                        await connection.ExecuteAsync(
                            $"INSERT INTO [AspNetUserRoles]([UserId], [RoleId]) VALUES(@userId, @{nameof(item)})",
                            new { userId = user.Id, item });
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRoleToUser(Guid id, string roleName)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var roleId = await connection.ExecuteScalarAsync<Guid?>(
                        "SELECT [Id] FROM [AspNetRoles] WHERE [NormalizedName] = @normalizedName",
                        new { normalizedName = roleName.ToUpper() });
                    if (roleId.HasValue)
                        await connection.ExecuteAsync(
                            $"DELETE FROM [AspNetUserRoles] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                            new { userId = user.Id, roleId });
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatus(Guid id, bool acticve)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync(
                    $"Update [AspNetUsers] set InActive = @acticve where Id= @userId ",
                    new { userId = id, acticve });
                return true;
            }
        }
    }
}