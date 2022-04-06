using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.Store.ViewModel;

namespace TemplateKAO.Services.Store.Repository
{
    public interface IStoreRepository
    {
        List<StoreResponse> GetNearLocation(string Longitude, string Latitude, int distance, string langCode, int sortOrder, out int totalRows);
        List<StoreResponse> GetAllDepartment(string langCode);
        List<StoreResponse> GetDepartmentByLocationID(string langCode, int locationId);
    }
    public class StoreRepository : IStoreRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;

        public StoreRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;

        }
        public List<StoreResponse> GetNearLocation(string Longitude, string Latitude, int distance, string langCode, int sortOrder, out int totalRows)
        {
            var p = new DynamicParameters();
            var commandText = "NearLocationFullProperty";
            p.Add("@Longitude", Longitude);
            p.Add("@Latitude", Latitude);
            p.Add("@distance", distance);
            p.Add("@LanguageCode", langCode);
            p.Add("@SortOrder", sortOrder);
            p.Add("@TotalRows", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<StoreResponse>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            totalRows = p.Get<int>("@TotalRows");
            return result;
        }
        public List<StoreResponse> GetAllDepartment(string langCode)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetAllLocationDepartment";
            p.Add("@lang_code", langCode);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<StoreResponse>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
        public List<StoreResponse> GetDepartmentByLocationID(string langCode, int locationId)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetLocationDepartment";
            p.Add("@lang_code", langCode);
            p.Add("@locationId", locationId);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<StoreResponse>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
    }
}
