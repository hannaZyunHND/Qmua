using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Article.Repository;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;

namespace TemplateKAO.Controllers
{
    public class FilterArticleController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;

        public FilterArticleController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;

        }
        public IActionResult FilterArticleByTag(string tag, int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 10;
            var total = 0;
            var model = _articleRepository.GetArticlesSameTag(tag, CurrentLanguageCode, pageIndex, pageSize, out total);
            ViewBag.Total = total;
            ViewBag.Tag = tag;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            return View(model);
        }
    }
}