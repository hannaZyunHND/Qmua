using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Article.Repository;
using TemplateKAO.Services.Extra.Repository;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Services.Zone.ViewModal;

namespace TemplateKAO.Controllers
{
    public class ZoneController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IExtraRepository _extratRepository;
        private readonly IArticleRepository _articleRepository;
        //private readonly IStringLocalizer<HomeController> _localizer;

        public ZoneController(IZoneRepository zoneRepository, IProductRepository productRepository, IExtraRepository extraRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _extratRepository = extraRepository;
            _articleRepository = articleRepository;
        }

        public IActionResult RedirectAction(string alias, int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 12;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            var zone_tar = _zoneRepository.GetZoneByAlias(alias, CurrentLanguageCode);
            if (zone_tar != null)
            {
                ViewBag.ZoneId = zone_tar.Id;
                ViewBag.Type = zone_tar.Type;
                ViewBag.Parent = zone_tar.ParentId;

                ViewBag.IsHaveChild = zone_tar.isHaveChild;
                //return View();
            }
            //return View("~/Views/P404/P404.cshtml");
            return View();
        }


        public List<ZoneSugget> GetZoneSugget()
        {
            var zone_tar = _zoneRepository.GetZoneSugget(CurrentLanguageCode);
            return zone_tar;
        }

    }
}