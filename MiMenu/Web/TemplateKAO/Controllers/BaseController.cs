using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.BannerAds.Repository;
using TemplateKAO.Services.BannerAds.ViewModel;
using TemplateKAO.Utility;

namespace TemplateKAO.Controllers
{
    public class BaseController : Controller
    {
        public const string CookieLocationId = "_LocationId";
        public const string CookieLocationName = "_LocationName";

        private string _currentLanguage;
        private string _currentLanguageCode;
        public string CurrentLanguage
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguage))
                    return _currentLanguage;

                if (string.IsNullOrEmpty(_currentLanguage))
                {
                    var feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguage = feature.RequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
                }

                return _currentLanguage;
            }
        }
        public string CurrentLanguageCode
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguageCode))
                    return _currentLanguageCode;

                if (string.IsNullOrEmpty(_currentLanguageCode))
                {
                    IRequestCultureFeature feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguageCode = feature.RequestCulture.Culture.ToString();
                }

                return _currentLanguageCode;
            }


        }

        //public List<BannerAdsViewModel> ListBannerAds()
        //{
        //    List<BannerAdsViewModel> lstData = new List<BannerAdsViewModel>();
        //    try
        //    {
        //        lstData = _bannerAdsRepository.GetBannerAds();
        //        if (lstData.Count() > 0)
        //        {
        //            HttpContext.Session.SetComplexData(SessionBannerAds, lstData);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return lstData;
        //}
    }
}