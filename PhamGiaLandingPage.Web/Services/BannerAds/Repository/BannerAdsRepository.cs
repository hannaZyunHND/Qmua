using Dapper;
using Mi.MemoryCache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using PhamGiaLandingPage.Web.ExecuteCommand;
using PhamGiaLandingPage.Web.Services.BannerAds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.BannerAds.Repository
{
    public interface IBannerAdsRepository
    {
        List<BannerAdsViewModel> GetBannerAds(string langCode);
        BannerAdsViewModel GetBannerAds_By_Code(string langCode, string code);
        string GetConfigByName(string lang_code, string name);
    }
    public class BannerAdsRepository : IBannerAdsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        //cache 
        // cache end

        //memory cache
        private ICacheController _memory;
        //end memory cache
        public BannerAdsRepository(IConfiguration configuration, IExecuters executers, ICacheController memory)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
            _memory = memory;

        }

        //public BannerAdsRepository(IConfiguration configuration, IExecuters executers)
        //{
        //    _configuration = configuration;
        //    _connStr = _configuration.GetConnectionString("DefaultConnection");
        //    _executers = executers;
        //    //_distributedCache = distributedCache;
        //    //_multiplexer = multiplexer;

        //}

        public List<BannerAdsViewModel> GetBannerAds(string langCode)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_Get_BannerAds";
            p.Add("@langCode", langCode);

            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<BannerAdsViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            //var keyCache = string.Format("", 1, 2, 2);
            //var result_after_cached = _distributedCache.GetOrSetCache(keyCache, () => result, _configuration);
            return result;
        }
        public BannerAdsViewModel GetBannerAds_By_Code(string langCode, string code)
        {
            var keyCache = string.Format("banner_{0}_{1}", code, langCode);
            var r = new BannerAdsViewModel();

            //Get in memory cache

            var m = _memory.Get(keyCache);
            if (!string.IsNullOrEmpty(m))
            {
                r = Newtonsoft.Json.JsonConvert.DeserializeObject<BannerAdsViewModel>(m);
            }
            else
            {
                var p = new DynamicParameters();
                var commandText = "usp_Web_Get_BannerAds_By_Code";
                p.Add("@langCode", langCode);
                p.Add("@code", code);
                r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<BannerAdsViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
                //Add cache
                var add_to_cache = Newtonsoft.Json.JsonConvert.SerializeObject(r);
                _memory.Create(keyCache, add_to_cache);
            }

            //End get in memory cache

            //Get in cache
            //var result_after_cache = _distributedCache.Get(keyCache);
            //if (result_after_cache != null)
            //{
            //    r = Newtonsoft.Json.JsonConvert.DeserializeObject<BannerAdsViewModel>(Encoding.UTF8.GetString(result_after_cache));
            //}
            //if (result_after_cache == null)
            //{
            //    var p = new DynamicParameters();
            //    var commandText = "usp_Web_Get_BannerAds_By_Code";
            //    p.Add("@langCode", langCode);
            //    p.Add("@code", code);
            //    r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<BannerAdsViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            //    //Add cache
            //    var add_to_cache = Newtonsoft.Json.JsonConvert.SerializeObject(r);
            //    result_after_cache = Encoding.UTF8.GetBytes(add_to_cache);
            //    var cache_options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)).SetAbsoluteExpiration(DateTime.Now.AddMinutes(int.Parse(_configuration["Redis:CachingExpireMinute"])));
            //    _distributedCache.Set(keyCache, result_after_cache, cache_options);
            //}
            //var p = new DynamicParameters();
            //var commandText = "usp_Web_Get_BannerAds_By_Code";
            //p.Add("@langCode", langCode);
            //p.Add("@code", code);
            //var r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<BannerAdsViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));

            return r;
        }

        public string GetConfigByName(string lang_code, string name)
        {
            try
            {
                var keyCache = string.Format("config_{0}_{1}", name, lang_code);
                var r = ""; ;
                //Get in cache               
                var result_after_cache = _memory.Get(keyCache);
                if (result_after_cache != null)
                {
                    r = result_after_cache;
                }
                if (result_after_cache == null)
                {
                    var p = new DynamicParameters();
                    var commandText = "usp_Web_GetConfigByName";
                    p.Add("@lang_code", lang_code);
                    p.Add("@configName", name);
                    r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<string>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
                    //Add cache
                    result_after_cache = r;

                    _memory.Create(keyCache, result_after_cache);
                }
                return r;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
