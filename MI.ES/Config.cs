using Elasticsearch.Net;
using Nest;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace MI.ES
{
    public class ConfigES
    {

        protected static int blockBulk = 500;
        protected static string strNameIndex_HeThong = "";
        protected static ElasticClient client_HeThong;
        public const string formatDatePattern = "dd-MM-yyyy HH:mm";
        public static bool Start()
        {
            try
            {
                var Check = Utils.Settings.AppSettings.GetByKey("ESEnable").ToLower();
                if (Check == "True".ToLower())
                {
                    Uri node_HeThong = new Uri(Utils.Settings.AppSettings.GetByKey("NodeES"));
                    strNameIndex_HeThong = Utils.Settings.AppSettings.GetByKey("IndexES");
                    var pool = new SingleNodeConnectionPool(node_HeThong);
                    ConnectionSettings settings_HeThong = new ConnectionSettings(pool).DefaultIndex(strNameIndex_HeThong);
                    client_HeThong = new ElasticClient(settings_HeThong);
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static string EscapeTerm(string term)
        {
            string[] rmChars = new string[] { "+", "=", "&&", "||", ">", "<", "!", "(", ")", "{", "}", "[", "]", "^", "~", ":", "\\", "/", "-", "'" };
            foreach (string item in rmChars)
            {
                term = term.Replace(item, " ");
            }

            return term.ToLower();
        }
    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd-MM-yyyy HH:mm";
        }
    }
}
