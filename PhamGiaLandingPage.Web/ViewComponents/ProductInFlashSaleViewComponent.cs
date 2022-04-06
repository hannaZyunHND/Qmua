using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.FlashSale.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class ProductInFlashSaleViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly ICookieUtility _cookieUtility;
        private readonly IFlashSaleRepository _flashSaleRepository;
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
        public ProductInFlashSaleViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, ICookieUtility cookieUtility, IFlashSaleRepository flashSaleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _cookieUtility = cookieUtility;
            _flashSaleRepository = flashSaleRepository;
        }
        public IViewComponentResult Invoke(int fSaleId, int pageIndex, int pageSize)
        {
            var total = 0;
            var model = _flashSaleRepository.GetProductInFlashSale(fSaleId, _cookieUtility.SetCookieDefault().LocationId, CurrentLanguageCode, pageIndex, pageSize, out total);
            ViewBag.Total = total;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            return View(model);
        }
    }
}
