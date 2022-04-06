using Dapper;
using MI.Entity.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.Zone.ViewModal;
using static Dapper.SqlMapper;

namespace TemplateKAO.Services.Zone.Repository
{
    public interface IZoneRepository
    {
        List<ZoneByTreeViewMinify> GetZoneByTreeViewMinifies(int type, string lang_code, int parentId);
        List<ZoneByTreeViewMinify> GetBreadcrumbByZoneId(int zone_id, string lang_code);
        List<ZoneByTreeViewMinify> GetListZoneByParentId(int type, string lang_code);
        ZoneToRedirect GetZoneByAlias(string url, string lang_code);
        List<ZoneSugget> GetZoneSugget(string lang_code);
        ZoneDetail GetZoneDetail(int zoneId, string lang_code);
        List<ZoneByTreeViewMinify> GetZoneByTreeViewShowMenuMinifies(int type, string lang_code, int parentId, int isShowMenu);

        ZoneByTreeViewMinify GetFirstZoneInType(int type, string lang_code, int parentId, int isShowMenu);
    }
    public class ZoneRepository : IZoneRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        //cache 

        // cache end
        public ZoneRepository(IConfiguration configuration, IExecuters executers)
        {
            _configuration = configuration;
            _connStr = _configuration.GetConnectionString("DefaultConnection");
            _executers = executers;
        }

        public List<ZoneByTreeViewMinify> GetBreadcrumbByZoneId(int zone_id, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_BreadcrumbByZoneId";
            p.Add("@zone_id", zone_id);
            p.Add("@lang_code", lang_code);

            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ZoneByTreeViewMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public ZoneByTreeViewMinify GetFirstZoneInType(int type, string lang_code, int parentId, int isShowMenu)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetZoneByTreeView_Minify_ShowMenu_First";
            p.Add("@type", type);
            p.Add("@lang_code", lang_code);
            p.Add("@parentId", parentId);
            p.Add("@isShowMenu", isShowMenu);
            try
            {
                var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ZoneByTreeViewMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

        public List<ZoneByTreeViewMinify> GetListZoneByParentId(int type, string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetZoneParentByType";
            p.Add("@type", type);
            p.Add("@lang_code", lang_code);

            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ZoneByTreeViewMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }

        public ZoneToRedirect GetZoneByAlias(string url, string lang_code)
        {
            //Dapper
            var result = new ZoneToRedirect();
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetZoneByAlias";
            p.Add("@url", url);
            p.Add("@lang_code", lang_code);
            result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ZoneToRedirect>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }

        public List<ZoneByTreeViewMinify> GetZoneByTreeViewMinifies(int type, string lang_code, int parentId)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetZoneByTreeView_Minify_v1";
            p.Add("@type", type);
            p.Add("@lang_code", lang_code);
            p.Add("@parentId", parentId);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ZoneByTreeViewMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
        public List<ZoneByTreeViewMinify> GetZoneByTreeViewShowMenuMinifies(int type, string lang_code, int parentId, int isShowMenu)
        {
            var keyCache = string.Format("{0}_{1}_{2}_{3}_{4}", "zone", lang_code, type, parentId, isShowMenu);
            var r = new List<ZoneByTreeViewMinify>();
            //get in cache
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetZoneByTreeView_Minify_ShowMenu";
            p.Add("@type", type);
            p.Add("@lang_code", lang_code);
            p.Add("@parentId", parentId);
            p.Add("@isShowMenu", isShowMenu);
            r = _executers.ExecuteCommand(_connStr, conn => conn.Query<ZoneByTreeViewMinify>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return r;
        }

        public ZoneDetail GetZoneDetail(int zoneId, string lang_code)
        {
            var result = new ZoneDetail();
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetZoneDetail";
            p.Add("@id", zoneId);
            p.Add("@lang_code", lang_code);
            result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ZoneDetail>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }

        public List<ZoneSugget> GetZoneSugget(string lang_code)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_Get_ZoneSugget";
            p.Add("@lang_code", lang_code);

            var result = _executers.ExecuteCommand(_connStr, conn => conn.Query<ZoneSugget>(commandText, p, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            return result;
        }
    }
}
