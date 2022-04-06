using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Order.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Promotion.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class OrderDetailViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICookieUtility _cookieUnitily;
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
        public OrderDetailViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IPromotionRepository promotionRepository, IOrderRepository orderRepository, ICookieUtility cookieUtility)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
            _orderRepository = orderRepository;
            _cookieUnitily = cookieUtility;
        }
        public IViewComponentResult Invoke(string product_ids)
        {
            //Lay danh sach san pham theo list product_id
            var cookie_location = _cookieUnitily.SetCookieDefault();
            var total = 0;
            var model = _productRepository.GetProductInListProductsMinify(product_ids, cookie_location.LocationId, CurrentLanguageCode, 1, 10, out total);
            var list_promotion_item = new List<int>();

            //v2: Lay tat ca khuyen mai, sau nay co the cache cai nay
            var promotions = _promotionRepository.GetAllPromotions(CurrentLanguageCode);
            ViewBag.Promotions = promotions;
            return View(model);
        }
    }
}
