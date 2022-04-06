using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class SwitchRegionViewComponent : ViewComponent
    {
        private readonly IProductRepository _productRepository;
        private readonly IZoneRepository _zoneRepository;
        private string _currentLanguage;
        private string _currentLanguageCode;
        private string CurrentLanguage
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
        private string CurrentLanguageCode
        {
            get
            {
                if (!string.IsNullOrEmpty(_currentLanguageCode))
                    return _currentLanguageCode;

                if (string.IsNullOrEmpty(_currentLanguageCode))
                {
                    var feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguageCode = feature.RequestCulture.Culture.ToString();
                }

                return _currentLanguageCode;
            }


        }
        public SwitchRegionViewComponent(IProductRepository productRepository, IZoneRepository zoneRepository)
        {
            _productRepository = productRepository;
            _zoneRepository = zoneRepository;
        }
        public IViewComponentResult Invoke(int region_id)
        {
            ViewBag.Region = region_id;
            var rg = _zoneRepository.GetZoneDetail(region_id, CurrentLanguageCode);
            if (rg != null)
            {
                ViewBag.UrlOld = rg.UrlOld;
                ViewBag.Url = rg.Url;
            }
            return View();
        }
    }
}
