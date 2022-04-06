using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.Article.ViewModel;

namespace TemplateKAO.Services.Article.Repository
{
    public interface IArticleRepository
    {

        List<ArticleMinify> GetArticlesInZoneId_Minify(int zoneId, int zone_type, string lang_code, string filter, int? pageIndex, int? pageSize, out int totalRow);
        List<ArticleMinify> GetArticlesInZoneId_Minify_FullFilter(int zoneId, int zone_type, int article_type, int? isHot, string lang_code, string filter, int? pageIndex, int? pageSize, out int totalRow);
        List<ArticleMinify> GetArticlesInZoneId_Minify_AddFilterHot(int zoneId, int zone_type, int? isHot, string lang_code, string filter, int? pageIndex, int? pageSize, out int totalRow);
        List<ArticleMinify> GetArticlesSameTag(string tag, string lang_code, int? pageIndex, int? pageSize, out int total);
        List<ArticleMinify> GetArticlesInZoneParent(int zoneId, string lang_code, int pageIndex, int pageSize, out int total);
        ArticleDetail GetArticleDetail(int article_id, string lang_code);
        RedirechArticle GetObjectByAlias(string url, string lang_code);
    }
    public class ArticleRepository : IArticleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        //cache 

        // cache end

        public ArticleRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;

        }

        public ArticleDetail GetArticleDetail(int article_id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetArticleById";
            p.Add("@id", article_id);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ArticleDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }

        public List<ArticleMinify> GetArticlesInZoneId_Minify(int zoneId, int zone_type, string lang_code, string filter, int? pageIndex, int? pageSize, out int totalRow)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetArticlesInZoneId_Minify";
            p.Add("@zoneId", zoneId);
            p.Add("@Type", zone_type);
            p.Add("@lang_code", lang_code);
            p.Add("@filter", filter);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@totalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ArticleMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            totalRow = p.Get<int>("@totalRow");
            return result;
        }

        public List<ArticleMinify> GetArticlesInZoneId_Minify_AddFilterHot(int zoneId, int zone_type, int? isHot, string lang_code, string filter, int? pageIndex, int? pageSize, out int totalRow)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetArticlesInZoneId_Minify_AddFilterHot";
            p.Add("@zoneId", zoneId);
            p.Add("@Type", zone_type);
            p.Add("@isHot", isHot);
            p.Add("@lang_code", lang_code);
            p.Add("@filter", filter);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@totalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ArticleMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            totalRow = p.Get<int>("@totalRow");
            return result;
        }
        public List<ArticleMinify> GetArticlesInZoneId_Minify_FullFilter(int zoneId, int zone_type, int article_type, int? isHot, string lang_code, string filter, int? pageIndex, int? pageSize, out int totalRow)
        {
            totalRow = pageSize ?? 10;
            var keyCache = string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}", "articleFilter", zoneId, article_type, lang_code, pageIndex.Value, pageSize.Value, zone_type);
            var r = new List<ArticleMinify>();
            //get in cache
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetArticlesInZoneId_Minify_Full";
            p.Add("@zoneId", zoneId);
            p.Add("@Type", zone_type);
            p.Add("@Article_type", article_type);
            p.Add("@isHot", isHot);
            p.Add("@lang_code", lang_code);
            p.Add("@filter", filter);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@totalRow", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            r = _executers.ExecuteCommand(_connStr, conn => conn.Query<ArticleMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            totalRow = p.Get<int>("@totalRow");
            return r;



        }

        public List<ArticleMinify> GetArticlesInZoneParent(int zoneId, string lang_code, int pageIndex, int pageSize, out int total)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetArticleInParentZone_FullFilter";
            p.Add("@zone_id", zoneId);
            p.Add("@lang_code", lang_code);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var r = _executers.ExecuteCommand(_connStr, conn => conn.Query<ArticleMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return r;
        }

        public List<ArticleMinify> GetArticlesSameTag(string tag, string lang_code, int? pageIndex, int? pageSize, out int total)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetArticleSameTag";
            p.Add("@tag", tag);
            p.Add("@lang_code", lang_code);
            p.Add("@pageIndex", pageIndex);
            p.Add("@pageSize", pageSize);
            p.Add("@total", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ArticleMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            total = p.Get<int>("@total");
            return result;
        }

        public RedirechArticle GetObjectByAlias(string url, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetObjectByAlias";
            p.Add("@url", url);
            p.Add("@lang_code", lang_code);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<RedirechArticle>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }
    }
}
