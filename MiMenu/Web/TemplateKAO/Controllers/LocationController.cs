using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Locations.Repository;

namespace TemplateKAO.Controllers
{
    public class LocationController : BaseController
    {
        //private readonly ILocationsRepository _locationsRepository;
        //private readonly IStringLocalizer<HomeController> _localizer;

        public IActionResult Index()
        {
            return View();
        }
    }
}