using Dapper;
using Mi.MemoryCache;
using MI.Entity.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using PhamGiaLandingPage.Web.ExecuteCommand;
using PhamGiaLandingPage.Web.Services.Locations.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Locations.Repository
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
        private ICacheController _memory;

        public LocationsRepository(IConfiguration configuration, IExecuters executers, ICacheController memory)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
            _memory = memory;
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
            var result_after_cache = _memory.Get(keyCache);
            if (result_after_cache != null)
            {
                r = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocationViewModel>>(result_after_cache);
            }
            if (result_after_cache == null)
            {
                var p = new DynamicParameters();
                var commandText = "usp_Web_GetLocations";
                p.Add("@lang_code", lang_code);
                r = _executers.ExecuteCommand(_connStr, conn => conn.Query<LocationViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                //Add cache
                var add_to_cache = Newtonsoft.Json.JsonConvert.SerializeObject(r);
                result_after_cache = add_to_cache;

                _memory.Create(keyCache, add_to_cache);
            }

            return r;

        }



    }
}
