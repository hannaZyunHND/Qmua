using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class GetElasticAllViewComponent : ViewComponent
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
        public GetElasticAllViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, ICookieUtility locationUtility)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _locationUtility = locationUtility;
        }
        public IViewComponentResult Invoke(string keyword, int index, int size)
        {
            long total = 0;
            var rx = MI.ES.BCLES.AutocompleteService.SuggestAsyncAll(keyword, CurrentLanguageCode, out total);
            var ress = rx.Suggests.Skip((index - 1) * size).Take(size);
            var ls_prod = new List<int>();
            //Parallel.ForEach(rx.Suggests, item =>
            //{
            //    ls_prod.Add(item.Id);
            //});
            foreach (var item in ress)
            {
                ls_prod.Add(item.Id);
            }
            var ls_string = string.Join(',', ls_prod);
            int total_row = 0;
            var result = _productRepository.GetProductInListProductsMinify(ls_string, _locationUtility.SetCookieDefault().LocationId, CurrentLanguageCode, 1, size, out total_row);
            ViewBag.Total = total;
            ViewBag.Index = index;
            ViewBag.Size = size;
            ViewBag.KeyWord = keyword;
            return View(result);
        }
    }
}
