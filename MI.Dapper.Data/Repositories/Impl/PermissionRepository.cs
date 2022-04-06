using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Extensions;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using Microsoft.Extensions.Configuration;
using MoreLinq.Extensions;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly string _connectionString;

        public PermissionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<string>> GetPermissionByUserId(string userId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var paramaters = new DynamicParameters();
                paramaters.Add("@userId", userId);

                var result = await conn.QueryAsync<string>("Get_Permission_ByUserId", paramaters, null, null,
                    CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<FunctionActionViewModel>> GetAllWithPermission()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    await conn.OpenAsync();

                var result = await conn.QueryAsync<FunctionActionViewModel>("Get_Function_WithActions", null, null,
                    null, CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task<List<FunctionPermissionViewModel>> GetAllFunctionByPermission(string roleName, Guid id)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    List<FunctionViewModel> listFunction;
                    List<FunctionPermissionViewModel> listFunctionResult = new List<FunctionPermissionViewModel>();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("@userId", id);
                    if (roleName.Contains("Admin"))
                    {
                        var resultFunctionByAdmin = await conn.QueryAsync<FunctionViewModel>(
                            "select f.Id,f.Name,f.Url,f.ParentId,f.IsActive,f.SortOrder,f.CssClass from Functions f",
                            null,
                            null, null, CommandType.Text);
                        listFunction = resultFunctionByAdmin.OrderBy(x => x.SortOrder).ToList();
                        foreach (var item in listFunction)
                        {
                            listFunctionResult.Add(new FunctionPermissionViewModel()
                            {
                                Id = item.Id,
                                Icon = item.CssClass,
                                Name = item.Name,
                                Url = item.Url,
                                ParentId = item.ParentId
                            });
                        }

                        return listFunctionResult.BuildTree().ToList();
                    }

                    var result = await conn.QueryAsync<FunctionViewModel>("Get_Function_ByPermission", paramaters, null,
                        null, CommandType.StoredProcedure);
                    listFunction = result.OrderBy(x => x.SortOrder).ToList();
                    foreach (var item in listFunction)
                    {
                        listFunctionResult.Add(new FunctionPermissionViewModel()
                        {
                            Id = item.Id,
                            Icon = item.CssClass,
                            Name = item.Name,
                            Url = item.Url,
                            ParentId = item.ParentId
                        });
                    }

                    return listFunctionResult.BuildTree().ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<FunctionViewModel>> GetAllFunctionByPermissionCurrentUser(string roleName, Guid id)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    List<FunctionViewModel> listFunction;
                    List<FunctionPermissionViewModel> listFunctionResult = new List<FunctionPermissionViewModel>();
                    var paramaters = new DynamicParameters();
                    paramaters.Add("@userId", id);
                    if (roleName.Contains("Admin"))
                    {
                        var resultFunctionByAdmin = await conn.QueryAsync<FunctionViewModel>(
                            "select f.Id,f.Name,f.Url,f.ParentId,f.IsActive,f.SortOrder,f.CssClass from Functions f",
                            null,
                            null, null, CommandType.Text);


                        return resultFunctionByAdmin.ToList();
                    }

                    var result = await conn.QueryAsync<FunctionViewModel>("Get_Function_ByPermission", paramaters, null,
                        null, CommandType.StoredProcedure);

                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<PermissionViewModel>> GetAllRolePermissions(Guid id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var paramaters = new DynamicParameters();
                paramaters.Add("@roleId", id);

                var result = await conn.QueryAsync<PermissionViewModel>("Get_Permission_ByRoleId", paramaters, null,
                    null, CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> SavePermissions(Guid role, List<PermissionViewModel> permissions)
        {
            try
            {
                var listFunctionString = new List<string>();
                var listPermissionNew = new List<PermissionViewModel>();
                foreach (var item in permissions)
                {
                    listFunctionString.Add(item.FunctionId);
                }

                foreach (var item in permissions)
                {
                    if (item.FunctionId.Contains("."))
                    {
                        var separatorIndex = item.FunctionId.IndexOf(".", StringComparison.Ordinal);
                        if (separatorIndex >= 0)
                        {
                            var isEqual = false;
                            var result = item.FunctionId.Substring(0, separatorIndex);
                            foreach (var itemj in listFunctionString)
                            {
                                if (itemj.Equals(result))
                                {
                                    isEqual = true;
                                    break;
                                }
                            }

                            if (isEqual == false)
                            {
                                listPermissionNew.Add(new PermissionViewModel()
                                {
                                    ActionId = "VIEW",
                                    FunctionId = result
                                });
                            }
                        }
                    }
                }

                var resultListPermissionFinal = listPermissionNew.DistinctBy(x => x.FunctionId).ToList();
                permissions.AddRange(resultListPermissionFinal);

                using (var conn = new SqlConnection(_connectionString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var dt = new DataTable();
                    dt.Columns.Add("RoleId", typeof(Guid));
                    dt.Columns.Add("FunctionId", typeof(string));
                    dt.Columns.Add("ActionId", typeof(string));
                    foreach (var item in permissions)
                    {
                        dt.Rows.Add(role, item.FunctionId, item.ActionId);
                    }

                    var paramaters = new DynamicParameters();
                    paramaters.Add("@roleId", role);
                    paramaters.Add("@permissions", dt.AsTableValuedParameter("dbo.Permission"));
                    await conn.ExecuteAsync("Create_Permission", paramaters, null, null,
                        CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}