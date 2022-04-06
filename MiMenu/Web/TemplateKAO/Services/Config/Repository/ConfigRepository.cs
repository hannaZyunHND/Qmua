using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.ExecuteCommand;
using TemplateKAO.Services.Config.ViewModel;

namespace TemplateKAO.Services.Config.Repository
{
    public class ConfigRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connStr;
        private readonly IExecuters _executers;

        public ConfigViewModel GetArticlesInZoneId_Minify(string configName, string langCode)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetConfigByName";
            p.Add("@configName", configName);
            p.Add("@lang_code", langCode);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ConfigViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            return result;
        }
        public ConfigViewModel GetConfigByName(string configName, string langCode)
        {
            var p = new DynamicParameters();
            var commandText = "usp_Web_GetConfigByName";
            p.Add("@configName", configName);
            p.Add("@lang_code", langCode);
            var result = _executers.ExecuteCommand(_connStr, conn => conn.QueryFirstOrDefault<ConfigViewModel>(commandText, p, commandType: System.Data.CommandType.StoredProcedure));
            if (result != null)
            {
                return result;
            }

            return new ConfigViewModel();
        }
    }


}
