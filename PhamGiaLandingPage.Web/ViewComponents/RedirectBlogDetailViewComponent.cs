using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Article.ViewModel;
using PhamGiaLandingPage.Web.Services.Extra.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class RedirectBlogDetailViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IExtraRepository _extraRepository;

        const string CookieLocationId = "_LocationId";
        const string CookieLocationName = "_LocationName";
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
        public RedirectBlogDetailViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, IExtraRepository extraRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _extraRepository = extraRepository;
        }
        public IViewComponentResult Invoke(ArticleDetail a_detail)
        {
            if (a_detail != null)
            {
                ArticleDetail articleDetail = new ArticleDetail();
                try
                {
                    articleDetail = a_detail;
                }
                catch (Exception ex)
                {

                }
                ViewBag.Detail = articleDetail;
                return View();
            }
            return null;
        }
    }
}
