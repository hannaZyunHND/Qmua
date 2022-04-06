using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class MoreBlogViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
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
        public MoreBlogViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        //articleRepository.GetArticlesInZoneId_Minify(zone_target.Id, (int)TypeArticle.Blog, culture_code, "", 1, page_size, out total); }
        public IViewComponentResult Invoke(int zone_id, int type, string filter, int page_index, int page_size)
        {
            var total = 0;
            var model = _articleRepository.GetArticlesInZoneId_Minify(zone_id, type, CurrentLanguageCode, filter, page_index, page_size, out total);
            if (page_index == 1)
                model = model.Skip(3).ToList();
            return View(model);
        }
    }
}
