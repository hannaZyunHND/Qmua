using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;

namespace TemplateKAO.ViewComponents
{
    public class ComboInProductByLocationViewComponent : ViewComponent
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
        public ComboInProductByLocationViewComponent(IProductRepository productRepository, IStringLocalizer<ProductListViewComponent> localizer)
        {
            _localizer = localizer;
            _productRepository = productRepository;
        }
        public IViewComponentResult Invoke(int product_id, int location_id)
        {
            var total_row_combo = 0;
            var model = _productRepository.GetProductsInProductById(product_id, "com-bo", location_id, CurrentLanguageCode, 1, 4, out total_row_combo);
            return View(model);
        }
    }
}
