using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.BannerAds.Repository;
using TemplateKAO.Services.Order.Repository;
using TemplateKAO.Services.Order.ViewModal;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Promotion.Repository;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Utility;
using Utils;

namespace TemplateKAO.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IBannerAdsRepository _bannerAdsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICookieUtility _cookieUtility;
        private readonly IHostingEnvironment _env;

        public OrderController(IBannerAdsRepository bannerAdsRepository, IZoneRepository zoneRepository, IProductRepository productRepository, IPromotionRepository promotionRepository, IOrderRepository orderRepository, ICookieUtility cookieUtility, IHostingEnvironment env)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _promotionRepository = promotionRepository;
            _orderRepository = orderRepository;
            _cookieUtility = cookieUtility;
            _bannerAdsRepository = bannerAdsRepository;
            _env = env;
        }
        public IActionResult Orders()
        {
            //Lay danh sach san pham theo list product_id
            //var current_location_id = int.Parse(Request.Cookies[CookieLocationId]);
            //var total = 0;
            //var model = _productRepository.GetProductInListProductsMinify(product_ids, current_location_id, CurrentLanguageCode, 1, 10, out total);
            //var list_promotion_item = new List<int>();

            ////v2: Lay tat ca khuyen mai, sau nay co the cache cai nay
            //var promotions = _promotionRepository.GetAllPromotions(CurrentLanguageCode);
            //ViewBag.Promotions = promotions;
            return View();
        }
        [HttpPost]
        public IActionResult GetQuanHuyen(string locationType, string parent)
        {
            var provinces_result = new Dictionary<string, QuanHuyen>();
            var provinces_json_part = _env.WebRootFileProvider.GetFileInfo("hanhchinhvn-master/dist/" + locationType + ".json")?.PhysicalPath;
            if (provinces_json_part != null)
            {
                var file_content = System.IO.File.ReadAllText(provinces_json_part);
                if (file_content != null)
                {
                    provinces_result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, QuanHuyen>>(file_content);

                    var result = provinces_result.Where(r => r.Value.parent_code.Equals(parent));
                    var result_cooked = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    return Ok(result_cooked);
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel order)
        {
            var result = _orderRepository.CreateOrderInWebsite(order);
            var list_mail_receivers = _bannerAdsRepository.GetConfigByName(CurrentLanguageCode, "MailManager");

            if (list_mail_receivers != null)
            {
                var x = list_mail_receivers.Split(",");
                if (x.Count() > 0)
                {
                    var reciver = x[0];
                    var bccer = new List<string>();
                    bccer.Add(reciver);
                    Email.Send(reciver, "THÔNG BÁO ĐƠN HÀNG", order.MailBody, bccer);
                }
            }
            /*Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"","\"").Replace("\"{","{").Replace("}\"","}"))*/

            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateOrderStringtify(string order)
        {
            var serialized = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderViewModel>(order);
            var result = _orderRepository.CreateOrderInWebsite(serialized);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult LoadOrderDetail(string product_ids)
        {
            return ViewComponent("OrderDetail", new { product_ids = product_ids });

        }
        [HttpPost]
        public IActionResult LoadOrderDetailJson(string product_ids)
        {
            var total = 0;
            var result = _productRepository.GetProductInListProductsMinify(product_ids, _cookieUtility.SetCookieDefault().LocationId, CurrentLanguageCode, 1, 1000, out total);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult LoadDropdonwCart(string product_ids)
        {
            return ViewComponent("DropdownCart", new { product_ids = product_ids });

        }
        [HttpPost]
        public IActionResult GetOrderCode()
        {
            var result = _orderRepository.GetOrderCode();
            return Ok(result + 1);
            //return ViewComponent("DropdownCart", new { product_ids = product_ids });
        }
        [HttpGet]
        public IActionResult GetCouPonByCode(string code = "")
        {
            if (string.IsNullOrEmpty(code)) code = "";
            var result = _orderRepository.GetCouponChildByCode(code);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult PreMenu()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MenuOrder()
        {
            return View();
        }
        public IActionResult PreMenu1()
        {
            return View();
        }
    }
    public class MetaDataOrderController
    {

        /*"{\"IsNgoiTaiQuan\":\"1\",\"SoBan\":\"ádasd\",\"GhiChu\":\"áda\",\"HHThanhToan\":\"\",\"ThoiGianGiao\":\"2021-04-08\",\"MaVoucher\":\"\",\"GiaTriVoucher\":0}"*/
        public string IsNgoiTaiQuan { get; set; }
        public string SoBan { get; set; }
        public string GhiChu { get; set; }
        public string HHThanhToan { get; set; }
        public string ThoiGianGiao { get; set; }
        public string MaVoucher { get; set; }
        public int GiaTriVoucher { get; set; }
        public int TongTien { get; set; }

    }
}