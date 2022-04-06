using Enterbuy.Data.SqlServer.Dao.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public interface ICookieLocationUtility
    {
        CookieLocation SetCookieDefault();
    }
    public class CookieUtility : ICookieLocationUtility
    {
        private readonly ILocationDao _locationsRepository;
        const string CookieLocationId = "_LocationId";
        const string CookieLocationName = "_LocationName";
        private IHttpContextAccessor _httpContextAccessor;

        private string _currentLanguage;
        private string _currentLanguageCode;
        public CookieUtility(ILocationDao locationsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _locationsRepository = locationsRepository;
            _httpContextAccessor = httpContextAccessor;
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
                    if (feature != null)
                        _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                    else
                        _currentLanguage = "vi-VN";
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
                    if (feature != null)
                        _currentLanguageCode = feature.RequestCulture.Culture.ToString();
                    else
                        _currentLanguageCode = "vi-VN";

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
                //IsEssential = true,
                Expires = DateTime.Now.AddDays(7),
                SameSite = SameSiteMode.None
            };
            if (_httpContextAccessor.HttpContext.Request.Cookies[CookieLocationId] == null)
            {
                cookie.Expires = DateTime.Now.AddDays(7);
                var location_default = _locationsRepository.GetLocationFirst(CurrentLanguageCode);
                if (location_default != null)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationId, location_default.Id.ToString(), cookie);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieLocationName, location_default.Name, cookie);
                }
                var result = new CookieLocation() { LocationId = location_default.Id, LocationName = location_default.Name };
                return result;
            }
            else
            {
                var result = new CookieLocation() { LocationId = int.Parse(_httpContextAccessor.HttpContext.Request.Cookies[CookieLocationId]), LocationName = _httpContextAccessor.HttpContext.Request.Cookies[CookieLocationName] };
                return result;
            }
        }
    }

    public class CookieLocation
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}
