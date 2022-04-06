
using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Extra.Repository;
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
    public class RedirectProductDetailViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IExtraRepository _extraRepository;
        private readonly ICookieUtility _cookieUtility;

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
        public RedirectProductDetailViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, IExtraRepository extraRepository, ICookieUtility cookieUtility)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _extraRepository = extraRepository;
            _cookieUtility = cookieUtility;
        }
        public IViewComponentResult Invoke(ProductDetail p_detail)
        {
            if (p_detail != null)
            {
                var product_id = p_detail.Id;
                var product_infomatin_detail = p_detail;
                if (product_infomatin_detail != null)
                {
                    var product_spectification_detail = _productRepository.GetProductSpectificationDetail(product_id, CurrentLanguageCode);
                    var product_price_detail = _productRepository.GetProductPriceInLocationDetail(product_id, CurrentLanguageCode);
                    var current_location_id = _cookieUtility.SetCookieDefault().LocationId;
                    var current_location_name = _cookieUtility.SetCookieDefault().LocationName;
                    var total_row_combo = 0;
                    var products_in_product_combo = _productRepository.GetProductsInProductById(product_id, "com-bo", current_location_id, CurrentLanguageCode, 1, 4, out total_row_combo);
                    var promotions_in_product = _productRepository.GetPromotionInProduct(product_id, CurrentLanguageCode);
                    var location_id = _cookieUtility.SetCookieDefault().LocationId;
                    var same_zone_total = 0;
                    var product_same_zone = _productRepository.GetProductInZoneByZoneIdMinify(product_infomatin_detail.ZoneId, location_id, CurrentLanguageCode, 1, 6, out same_zone_total);
                    ViewBag.Infomation = product_infomatin_detail;
                    ViewBag.Zone = product_infomatin_detail.ZoneId;
                    ViewBag.ZoneUrl = product_infomatin_detail.ZoneUrl;
                    ViewBag.Spectification = product_spectification_detail;
                    ViewBag.SameZone = product_same_zone;
                    ViewBag.SameTotal = same_zone_total;

                    ViewBag.DefaultLocation = current_location_name;
                    var default_price_item = product_price_detail.Where(r => r.LocationId == current_location_id).FirstOrDefault();
                    if (default_price_item != null)
                    {
                        ViewBag.DefaultLocationPrice = default_price_item;
                    }
                    ViewBag.ListLocation = product_price_detail;
                    ViewBag.Combo = products_in_product_combo;
                    ViewBag.Promotion = promotions_in_product;

                    var list_comment = _extraRepository.GetCommentPublisedByObjectId(product_id, (int)CommentType.Product);
                    ViewBag.Comments = list_comment;
                }

                //ViewBag.DefaultPrice = product_price_detail.Where(r => r.)
                return View();
            }
            return null;
        }
    }
}
