using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Store.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IStoreRepository _storeRepository;

        public ContactController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, IStoreRepository storeRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            _storeRepository = storeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetDepartments(int loc_id)
        {
            if (loc_id > 0)
            {
                var model = _storeRepository.GetDepartmentByLocationID(CurrentLanguageCode, loc_id);
                return View(model);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
