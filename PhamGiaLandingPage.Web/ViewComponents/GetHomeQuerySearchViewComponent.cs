using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Product.ViewModel;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class GetHomeQuerySearchViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ICookieUtility _locationUtility;
        //const string CookieLocationId = "_LocationId";
        //const string CookieLocationName = "_LocationName";
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
                    IRequestCultureFeature feature = HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguageCode = feature.RequestCulture.Culture.ToString();
                }

                return _currentLanguageCode;
            }


        }
        public GetHomeQuerySearchViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, ICookieUtility locationUtility)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _locationUtility = locationUtility;
        }
        public IViewComponentResult Invoke(FilterProductBySpectification fp)
        {
            if (fp != null)
            {
                var total = 0;
                var result = _productRepository.FilterProductBySpectificationsInZone(fp, out total);
                var zone_id = fp.parentId;
                if (zone_id > 0)
                {
                    var zone_tar = _zoneRepository.GetZoneDetail(zone_id, CurrentLanguageCode);
                    ViewBag.KeyWord = zone_tar.Name;
                }
                else
                {
                    ViewBag.KeyWord = "Tất cả danh mục";
                }


                ViewBag.Total = total;
                ViewBag.Index = fp.pageNumber;
                ViewBag.Size = fp.pageSize;
                //ViewBag.KeyWord = fp.filter;
                return View(result);
            }
            return null;
        }
    }
}
