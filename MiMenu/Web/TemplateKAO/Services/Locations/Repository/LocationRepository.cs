using Dapper;
using MI.Entity.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.Locations.ViewModal;

namespace TemplateKAO.Services.Locations.Repository
{
    public interface ILocationsRepository
    {
        List<LocationViewModel> GetLocations(string lang_code);
        LocationViewModel GetLocationFirst(string lang_code);
    }
    public class LocationsRepository : ILocationsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        //cache 
        // cache end

        public LocationsRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;

        }

        public LocationViewModel GetLocationFirst(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetLocations";
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<LocationViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }

        public List<LocationViewModel> GetLocations(string lang_code)
        {
            var keyCache = string.Format("{0}_{1}", "location", lang_code);
            var r = new List<LocationViewModel>();
            //get in cache
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetLocations";
            p.Add("@lang_code", lang_code);
            r = _executers.ExecuteCommand(_connStr, conn => conn.Query<LocationViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            //Add cache
            return r;

        }



    }
}
