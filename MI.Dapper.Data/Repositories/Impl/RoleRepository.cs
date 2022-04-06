using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Utils;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly string _connectionString;

        public RoleRepository(RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<AppRole>> GetAll()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    await conn.OpenAsync();

                var paramaters = new DynamicParameters();
                var result = await conn.QueryAsync<AppRole>("Get_Role_All", paramaters, null, null,
                    System.Data.CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<PagedResult<AppRole>> GetAllPaging(FilterQuery filterQuery)
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

                var result = await conn.QueryAsync<AppRole>("Get_Role_AllPaging", paramaters, null, null,
                    System.Data.CommandType.StoredProcedure);

                int totalRow = paramaters.Get<int>("@totalRow");

                var pagedResult = new PagedResult<AppRole>()
                {
                    Items = result.ToList(),
                    TotalRow = totalRow,
                    PageIndex = filterQuery.pageIndex,
                    PageSize = filterQuery.pageSize
                };
                return pagedResult;
            }
        }

        public async Task<List<FunctionViewModel>> GetAllFunction()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = "select * from Functions";
                var result = await conn.QueryAsync<FunctionViewModel>(sql, null, null, null,
                    System.Data.CommandType.Text);
                return result.ToList();
            }
        }

        public async Task<List<ActionViewModel>> GetAllAction()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    await conn.OpenAsync();
                var sql = "select * from Actions";
                var result = await conn.QueryAsync<ActionViewModel>(sql, null, null, null,
                    System.Data.CommandType.Text);
                return result.ToList();
            }
        }
    }
}