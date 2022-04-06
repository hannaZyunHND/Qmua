using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Article.Repository;
using TemplateKAO.Services.Article.ViewModel;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Services.Zone.ViewModal;

//using MI.Cache;
//using StackExchange.Redis;

namespace TemplateKAO.Controllers
{
    public class BlogController : Controller
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        const string CookieLocationId = "_LocationId";
        const string CookieLocationName = "_LocationName";
        private string _currentLanguage;
        private string _currentLanguageCode;

        //cache 
        //private readonly IDistributedCache _distributedCache;
        //private readonly IConfiguration _configuration;
        //private readonly IConnectionMultiplexer _multiplexer;
        // cache end
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
        //public BlogController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository, IDistributedCache distributedCache, IConfiguration configuration, IConnectionMultiplexer multiplexer)
        public BlogController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
            //cache
            //_distributedCache = distributedCache;
            //_configuration = configuration;
            //_multiplexer = multiplexer;
            // cache end
        }

        //[HttpGet("getalltagv2")]
        //public async Task<IActionResult> GetAlltagCache()
        //{
        //    var tags = await _productRepository.getxxxx();

        //    var keyCache = string.Format("", 1, 2, 2);
        //    var result = _distributedCache.GetOrSetCache(keyCache, () => tags, _configuration);
        //    return Ok(result);
        //}

        //public bool RemoveCache(string key)
        //{
        //    RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, key);
        //    return true;
        //}

        public IActionResult BlogList1(string alias, int zone_id)
        {
            //Get ra list zone bai viet
            var list_zone_blog = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Article, CurrentLanguageCode, 0);
            //var list_zone_promotions = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Promotion, CurrentLanguageCode, 0);
            var zone_blog_parent = list_zone_blog.Where(r => r.ParentId == 0 && r.Id == zone_id).FirstOrDefault();
            if (zone_blog_parent != null)
            {
                ViewBag.ZoneParent = zone_blog_parent;
                var list_blog_childs = list_zone_blog.Where(r => r.ParentId == zone_blog_parent.Id).ToList();
                if (list_blog_childs != null)
                    ViewBag.ZoneChilds = list_blog_childs;
                else
                    ViewBag.ZoneChilds = new List<ZoneByTreeViewMinify>();
            }
            return View();
        }

        public IActionResult AMPRedirectAction(string alias)
        {
            alias = alias.Replace(".amp.html", "");
            var zone_tar = _articleRepository.GetObjectByAlias(alias, CurrentLanguageCode);
            if (zone_tar != null)
            {
                ViewBag.ZoneId = zone_tar.ObjectId;
                ViewBag.Type = zone_tar.ObjectType;
                //ViewBag.Parent = zone_tar.ParentId;
            }
            return View();
        }

        public IActionResult RedirectAction(string alias)
        {
            alias = alias.Replace(".html", "");
            var zone_tar = _articleRepository.GetObjectByAlias(alias, CurrentLanguageCode);
            if (zone_tar != null)
            {
                ViewBag.ZoneId = zone_tar.ObjectId;
                ViewBag.Type = zone_tar.ObjectType;
                //ViewBag.Parent = zone_tar.ParentId;
                return View();
            }
            return View("~/Views/P404/P404.cshtml");

        }

        public IActionResult BlogList2(string alias, int zoneId, int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 5;
            //Get ra zone by Id
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageSize = pageSize;
            if (zoneId > 0)
            {
                var zone_target = _zoneRepository.GetZoneDetail(zoneId, CurrentLanguageCode);
                if (zone_target != null)
                {

                    ViewBag.ZoneTarget = zone_target;
                }
            }
            return View();
        }

        public IActionResult BlogDetail(string alias, int articleId)
        {
            ArticleDetail articleDetail = new ArticleDetail();
            try
            {
                articleDetail = _articleRepository.GetArticleDetail(articleId, CurrentLanguageCode);
            }
            catch (Exception ex)
            {

            }
            ViewBag.Detail = articleDetail;
            return View();
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
        //ProductListInArticle
        [HttpPost]
        public IActionResult MoreBlogs(int zone_id, int type, string filter, int page_index, int page_size)
        {
            if (string.IsNullOrEmpty(filter))
                filter = "";
            return ViewComponent("MoreBlog", new { zone_id = zone_id, type = type, filter = filter, page_index = page_index, page_size = page_size });
        }
        [HttpPost]
        public IActionResult ProductsInArticle(string product_ids, int location_id)
        {
            return ViewComponent("ProductListInArticle", new { product_ids = product_ids, location_id = location_id });
        }
    }
}