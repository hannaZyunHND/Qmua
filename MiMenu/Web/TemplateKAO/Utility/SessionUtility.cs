using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateKAO.Utility
{
    public class SessionUtility
    {
        public const string SessionBannerAds = "SessionBannerAds";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Set Session syntax

        public void SetSession(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);

        }

        //Get Session syntax

        public string GetSession(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);

        }
    }
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
