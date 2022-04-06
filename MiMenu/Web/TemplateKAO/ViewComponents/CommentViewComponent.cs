using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Article.Repository;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;

namespace TemplateKAO.ViewComponents
{
    public class CommentViewComponent : ViewComponent
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
        public CommentViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        public IViewComponentResult Invoke(int object_id, int object_type, int? pageIndex)
        {
            pageIndex = pageIndex ?? 1;
            var pageSize = 10;
            ViewBag.Index = pageIndex.Value;
            ViewBag.Size = pageSize;
            ViewBag.ObjId = object_id;
            ViewBag.ObjType = object_type;

            return View();
        }
    }
}
