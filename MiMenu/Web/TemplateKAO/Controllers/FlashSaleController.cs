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
    public class FlashSaleController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;

        public FlashSaleController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        [HttpPost]
        public IActionResult SwitchFlashSaleActive(int fSaleId, int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 1;
            return ViewComponent("ProductInFlashSale", new { fSaleId = fSaleId, pageIndex = pageIndex, pageSize = pageSize });
        }

        public IActionResult FlashSaleList()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}