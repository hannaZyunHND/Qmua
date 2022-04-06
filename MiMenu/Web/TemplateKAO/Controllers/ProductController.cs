using MI.Entity.Enums;
using MI.ES;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Extra.Repository;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Product.ViewModel;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Utility;

namespace TemplateKAO.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IExtraRepository _extratRepository;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ICookieUtility _cookieUtility;

        public ProductController(IZoneRepository zoneRepository, IProductRepository productRepository, IStringLocalizer<HomeController> localizer, IExtraRepository extraRepository, ICookieUtility cookieUtility)
        {
            _localizer = localizer;
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _extratRepository = extraRepository;
            _cookieUtility = cookieUtility;

            Task.Run(() =>
            {
                if (Utils.Utility.DateMerge < DateTime.Now)
                {
                    var Check = Utils.Settings.AppSettings.GetByKey("ESEnable").ToLower();
                    if (Check == "True".ToLower())
                    {
                        MI.Service.SyncProductToES.Run();
                        Utils.Utility.DateMerge = DateTime.Now.AddHours(2);
                    }
                    else
                    {
                        Utils.Utility.DateMerge = DateTime.Now.AddHours(2);
                    }
                }
            });

        }

        //[Route("/{alias}-c{zoneId}")]
        public IActionResult ProductList(string alias, int zoneId)
        {
            var zone_selected_with_treeview = _zoneRepository.GetZoneByTreeViewMinifies(1, CurrentLanguageCode, zoneId);
            var zone_details = _zoneRepository.GetZoneDetail(zoneId, CurrentLanguageCode);
            ViewBag.ZoneTreeView = zone_selected_with_treeview;
            ViewBag.ZoneDetail = zone_details;

            return View();
        }

        [HttpGet]
        public ProductSuggestResponse Get(string keyword)
        {
            //MI.Service.SyncProductToES.Run();
            var Check = Utils.Settings.AppSettings.GetByKey("ESEnable").ToLower();
            if (Check == "True".ToLower())
            {
                long total = 0;
                return MI.ES.BCLES.AutocompleteService.SuggestAsync(keyword, CurrentLanguageCode, 0, 10, out total);
            }
            else return new ProductSuggestResponse();

        }
        [HttpGet]
        public ProductSuggestResponse GetElasticAll(string keyword, int index, int size)
        {
            //MI.Service.SyncProductToES.Run();
            var Check = Utils.Settings.AppSettings.GetByKey("ESEnable").ToLower();
            if (Check == "True".ToLower())
            {
                long total = 0;
                return MI.ES.BCLES.AutocompleteService.SuggestAsync(keyword, CurrentLanguageCode, index, size, out total);
            }
            else return new ProductSuggestResponse();

        }
        [HttpGet]
        public IActionResult Merge()
        {
            MI.Service.SyncProductToES.Run();
            return Ok("OK");
        }
        public IActionResult ProductFilterInHome()
        {
            //var total = 0;
            //var result = _productRepository.FilterProductBySpectificationsInZone(fp, out total);
            return View();
        }

        public IActionResult ESSearchResult()
        {
            //ViewBag.Index = index;
            //ViewBag.Size = size;
            //ViewBag.KeyWord = keyword;
            return View();
        }

        public IActionResult ESSeachResultComponent(string keyword, int index, int size)
        {
            return ViewComponent("GetElasticAll", new { keyword = keyword, index = index, size = size });
        }
        [HttpPost]
        public IActionResult HomeQuerySearchComponent(FilterProductBySpectification fp)
        {
            return ViewComponent("GetHomeQuerySearch", new { fp = fp });
        }
        //[HttpPost]
        public IActionResult ProductDetail(string alias, int product_id)
        {
            //   ViewData["Title"] = "chi tiet ";

            var product_infomatin_detail = _productRepository.GetProductInfomationDetail(product_id, CurrentLanguageCode);
            if (product_infomatin_detail != null)
            {
                var product_spectification_detail = _productRepository.GetProductSpectificationDetail(product_id, CurrentLanguageCode);
                var product_price_detail = _productRepository.GetProductPriceInLocationDetail(product_id, CurrentLanguageCode);
                var current_location_id = _cookieUtility.SetCookieDefault().LocationId;
                var current_location_name = _cookieUtility.SetCookieDefault().LocationName;
                var total_row_combo = 0;
                var products_in_product_combo = _productRepository.GetProductsInProductById(product_id, "com-bo", current_location_id, CurrentLanguageCode, 1, 4, out total_row_combo);
                var promotions_in_product = _productRepository.GetPromotionInProduct(product_id, CurrentLanguageCode);
                var location_id = Request.Cookies[CookieLocationId] == null ? product_price_detail.FirstOrDefault().LocationId : int.Parse(Request.Cookies[CookieLocationId]);
                var same_zone_total = 0;
                var product_same_zone = _productRepository.GetProductInZoneByZoneIdMinify(product_infomatin_detail.ZoneId, location_id, CurrentLanguageCode, 1, 6, out same_zone_total);
                ViewBag.Infomation = product_infomatin_detail;
                ViewBag.Zone = product_infomatin_detail.ZoneId;
                ViewBag.ZoneUrl = product_infomatin_detail.ZoneUrl;
                ViewBag.Spectification = product_spectification_detail;
                ViewBag.SameZone = product_same_zone;
                ViewBag.SameTotal = same_zone_total;

                ViewBag.DefaultLocation = current_location_name;
                var default_price_item = product_price_detail.Where(r => r.LocationId == current_location_id).FirstOrDefault();

                ViewBag.DefaultLocationPrice = default_price_item ?? new ProductPriceInLocationDetail();

                ViewBag.ListLocation = product_price_detail;
                ViewBag.Combo = products_in_product_combo;
                ViewBag.Promotion = promotions_in_product;

                var list_comment = _extratRepository.GetCommentPublisedByObjectId(product_id, (int)CommentType.Product);
                ViewBag.Comments = list_comment;
            }

            //ViewBag.DefaultPrice = product_price_detail.Where(r => r.)
            return View();
        }
        [HttpPost]
        public IActionResult GetComboByLocationId(int product_id, int location_id)
        {
            return ViewComponent("ComboInProductByLocation", new { product_id = product_id, location_id = location_id });
        }

        [HttpPost]
        public IActionResult GetProductByZoneId(int zone_Id, int location_id)
        {
            return ViewComponent("SwitchProductList", new { zone_parent_id = zone_Id, locationId = location_id });
        }
        [HttpPost]
        public IActionResult ViewMore(int zone_parent_id, int locationId, int skip, int size)
        {
            return ViewComponent("ViewMore", new { zone_parent_id = zone_parent_id, locationId = locationId, skip = skip, size = size });
        }
        [HttpPost]
        public IActionResult VLastSeen(string product_ids)
        {
            return ViewComponent("ProductLastSeen", new { product_ids = product_ids });
        }
        //FilterSpectificationInZoneFilterSpectificationInZone
        [HttpPost]
        public IActionResult FilterSpectificationInZone(FilterProductBySpectification fp)
        {
            return ViewComponent("FilterSpectificationInZone", new { fp = fp });
        }
        [HttpPost]
        public IActionResult FilterSpectificationInZoneListProductList(FilterProductBySpectification fp)
        {
            return ViewComponent("FilterSpectificationInZoneProductList", new { fp = fp });
        }
        [HttpPost]
        public IActionResult FilterProductByNameAutocomplete(string filter, int pageSize)
        {
            return null;
        }
    }
}