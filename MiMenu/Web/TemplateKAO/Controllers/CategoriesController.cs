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
    public class CategoriesController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository; //Khai baos noi ham
        private readonly IArticleRepository _articleRepository;

        public CategoriesController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }

        public IActionResult CategoriesList()
        {
            return View();
        }

        public IActionResult CategoriesList1(int zoneId, int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 10;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            ViewBag.CateId = zoneId;
            _zoneRepository.GetZoneByAlias("1", CurrentLanguageCode);
            return View();
        }
    }
}