using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TemplateKAO.Services.Locations.Repository;
using Utils;

namespace TemplateKAO.Utility
{
    public interface ICookieUtility
    {
        CookieLocation SetCookieDefault();
    }
    public class CookieUtility : ICookieUtility
    {
        private readonly ILocationsRepository _locationsRepository;
        private readonly IActionContextAccessor _accessor;
        const string CookieLocationId = "_LocationId";
        const string CookieLocationName = "_LocationName";
        private IHttpContextAccessor _httpContextAccessor;

        private string _currentLanguage;
        private string _currentLanguageCode;
        public CookieUtility(ILocationsRepository locationsRepository, IHttpContextAccessor httpContextAccessor, IActionContextAccessor accessor)
        {
            _locationsRepository = locationsRepository;
            _httpContextAccessor = httpContextAccessor;
            _accessor = accessor;
        }
        private string CurrentLanguage
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguage))
                    return _currentLanguage;

                if (string.IsNullOrEmpty(_currentLanguage))
                {
                    var feature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }

                return _currentLanguage;
            }
        }
        private string CurrentLanguageCode
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguageCode))
                    return _currentLanguageCode;

                if (string.IsNullOrEmpty(_currentLanguageCode))
                {
                    var feature = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguageCode = feature.RequestCulture.Culture.ToString();
                }

                return _currentLanguageCode;
            }


        }

        public CookieLocation SetCookieDefault()
        {
            CookieOptions cookie = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                Secure = false,
                IsEssential = true,
                Expires = DateTime.Now.AddDays(7),
                SameSite = SameSiteMode.None
            };
            if (_httpContextAccessor.HttpContext.Request.Cookies[CookieLocationId] == null)
            {
                //Lay IP
                var customer_ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
                var demo_ip = "27.76.210.115";
                //Lay thong tin location
                using (var _http = new HttpClient())
                {
                    var url_request = "https://freegeoip.app/json/" + customer_ip;
                    var res = _http.GetAsync(url_request).Result;
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var x = res.Content.ReadAsStringAsync().Result;
                        var ip_info = Newtonsoft.Json.JsonConvert.DeserializeObject<FreeGeoIPInfo>(x);
                        if (!string.IsNullOrEmpty(ip_info.region_name) || ip_info.region_code != "null")
                        {
                            var n = ip_info.region_name.Replace("Tinh", "").TrimStart();

                            //Kiem tra region 
                            var location_affected = _locationsRepository.GetLocations(CurrentLanguageCode).Where(r => UIHelper.ChuyenCoDauThanhKhongCoDau(r.Name).Contains(n)).FirstOrDefault();
                            if (location_affected != null)
                            {
                                cookie.Expires = DateTime.Now.AddDays(7);
                                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationId, location_affected.Id.ToString(), cookie);
                                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationName, location_affected.Name, cookie);
                                var result = new CookieLocation() { LocationId = location_affected.Id, LocationName = location_affected.Name };
                                return result;
                            }
                            else
                            {
                                var location_default = _locationsRepository.GetLocationFirst(CurrentLanguageCode);
                                if (location_default != null)
                                {
                                    cookie.Expires = DateTime.Now.AddDays(7);
                                    _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationId, location_default.Id.ToString(), cookie);
                                    _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationName, location_default.Name, cookie);
                                    var result = new CookieLocation() { LocationId = location_default.Id, LocationName = location_default.Name };
                                    return result;

                                }
                            }
                        }

                    }
                    else
                    {
                        var location_default = _locationsRepository.GetLocationFirst(CurrentLanguageCode);
                        cookie.Expires = DateTime.Now.AddDays(7);
                        _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationId, location_default.Id.ToString(), cookie);
                        _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationName, location_default.Name, cookie);
                        var result = new CookieLocation() { LocationId = location_default.Id, LocationName = location_default.Name };
                        return result;
                    }

                }
            }
            else
            {
                var result = new CookieLocation() { LocationId = int.Parse(_httpContextAccessor.HttpContext.Request.Cookies[CookieLocationId]), LocationName = _httpContextAccessor.HttpContext.Request.Cookies[CookieLocationName] };
                return result;
            }
            return null;
        }
    }

    public class CookieLocation
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
