using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Extensions;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class ZoneArticleRepository : IZoneArticleRepository
    {
        private readonly string _connectionString;
        private ILogger<ZoneArticleRepository> _logger;

        public ZoneArticleRepository(ILogger<ZoneArticleRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<List<Group>> GetAllZoneArticles()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var result = await conn.QueryAsync<Group>(
                    "select Id,name as Label,ParentId from Zone where Type != 1 and Type != 7 and Type != 0 and Status != 3",
                    null, null, null,
                    CommandType.Text);
                var listZoneArticle = result.BuildTree().ToList();
                return listZoneArticle;
            }
        }
    }
}