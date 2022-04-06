using Dapper;
using MI.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.BannerAds.ViewModel;

namespace TemplateKAO.Services.BannerAds.Repository
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
        public BannerAdsRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;

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
            //Get in cache
            var p = new DynamicParameters();
            var commandText = "usp_Web_Get_BannerAds_By_Code";
            p.Add("@langCode", langCode);
            p.Add("@code", code);
            r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<BannerAdsViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
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
                var p = new DynamicParameters();
                var commandText = "usp_Web_GetConfigByName";
                p.Add("@lang_code", lang_code);
                p.Add("@configName", name);
                r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<string>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
                return r;
            }

            //try
            //{
            //    var p = new DynamicParameters();
            //    var commandText = "usp_Web_GetConfigByName";
            //    p.Add("@lang_code", lang_code);
            //    p.Add("@configName", name);
            //    var r = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<string>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            //    if(r == null)
            //    {
            //        r = "";
            //    }
            //    return r;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "";
            }
        }
    }
}
