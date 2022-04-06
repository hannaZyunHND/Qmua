using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MI.Dapper.Data.Repositories.Impl
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly string _connectionString;

        public LanguageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Languages>> GetAllLanguage()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                var result = await conn.QueryAsync<Languages>("select languagecode as Id,name as Label from language",
                    null, null, null,
                    CommandType.Text);
                var listLannguage = result.ToList();
                return listLannguage;
            }
        }
    }
}