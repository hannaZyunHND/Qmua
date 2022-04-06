using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    [ViewComponent(Name = "ProductList")]
    public class ProductListViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<ProductListViewComponent> _localizer;
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
        public ProductListViewComponent(IProductRepository productRepository, IStringLocalizer<ProductListViewComponent> localizer)
        {
            _localizer = localizer;
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke(int zone_parent_id, int locationId, int pageIndex, int pageSize)
        {
            var total = 0;
            var model = _productRepository.GetProductMinifiesTreeViewByZoneParentId(zone_parent_id, CurrentLanguageCode, locationId, pageIndex, pageSize, out total);
            ViewBag.Total = total;
            ViewBag.Id = zone_parent_id;
            ViewBag.Index = pageIndex;
            ViewBag.Size = pageSize;
            return View(model);
        }
    }
}
