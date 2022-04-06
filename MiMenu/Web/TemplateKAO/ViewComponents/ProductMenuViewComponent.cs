using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Zone.Repository;

namespace TemplateKAO.ViewComponents
{
    //[ResponseCache(Duration =3600)]
    public class ProductMenuViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IStringLocalizer<ProductMenuViewComponent> _localizer;
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
        public ProductMenuViewComponent(IZoneRepository zoneRepository, IStringLocalizer<ProductMenuViewComponent> localizer)
        {
            _localizer = localizer;
            _zoneRepository = zoneRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = _zoneRepository.GetZoneByTreeViewMinifies(1, CurrentLanguageCode, 0);
            return View(model);
        }
    }
}
