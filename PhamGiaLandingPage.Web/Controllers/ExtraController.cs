using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.BannerAds.Repository;
using PhamGiaLandingPage.Web.Services.Extra.Repository;
using PhamGiaLandingPage.Web.Services.Extra.ViewModel;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace PhamGiaLandingPage.Web.Controllers
{
    public class ExtraController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IExtraRepository _extraRepository;
        private readonly IBannerAdsRepository _bannerAdsRepository;

        public ExtraController(IExtraRepository extraRepository, IZoneRepository zoneRepository, IBannerAdsRepository bannerAdsRepository)
        {
            _extraRepository = extraRepository;
            _zoneRepository = zoneRepository;
            _bannerAdsRepository = bannerAdsRepository;
        }

        public IActionResult RedirectToParentZone(string alias, int parent_id)
        {
            var list_parents = _zoneRepository.GetListZoneByParentId((int)TypeZone.AllButProduct, CurrentLanguageCode);
            var type_target = list_parents.Where(r => r.Id == parent_id).FirstOrDefault();
            if (type_target != null)
            {
                switch (type_target.Type)
                {
                    case (int)TypeZone.Article:
                        //return RedirectToAction("BlogList1", "Blog");
                        return RedirectToRoute("Blogs", new { alias = alias, zone_id = parent_id });
                    case (int)TypeZone.Promotion:
                        return RedirectToRoute("Promotions");
                    case (int)TypeZone.Quotation:
                        return RedirectToRoute("quotation");
                    case (int)TypeZone.Recruitment:
                        return RedirectToRoute("Recruitment");
                    case (int)TypeZone.Categories:
                        return RedirectToRoute("DownloadCategory");
                    default:
                        return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult GetPropertyDetails()
        {
            var result = _extraRepository.GetPropertyDetails(CurrentLanguageCode);
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }
        [HttpPost]
        public IActionResult CreateRating(int objectId, int objectType, int rate)
        {
            var result = _extraRepository.CreateRating(objectId, objectType, rate);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateViewCount(int objectId, string type)
        {
            var result = _extraRepository.CreateViewCount(objectId, type);
            return Ok(result);
        }
        public IActionResult CreateComment(int objectId, int objectType, string name, string phoneOrEmail, string avatar, string content, string type, int rate, int parentId = 0)
        {
            var result = _extraRepository.CreateComment(objectId, objectType, name, phoneOrEmail, avatar, content, type, rate, CurrentLanguageCode, parentId);

            return Ok(result);
        }

        public IActionResult CreateServiceTicket(ServiceTicket ticket)
        {

            var result = _extraRepository.CreateContact(ticket);


            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateServiceTicketWBookingTime(ServiceTicket ticket)
        {

            var result = _extraRepository.CreateContactwBookingTime(ticket);


            return Ok(result);
        }

        [HttpPost]
        public IActionResult SendMail(string type, string body)
        {
            var list_mail_receivers = _bannerAdsRepository.GetConfigByName(CurrentLanguageCode, "MailManager");

            if (type == "order")
            {
                //var list_mail_receivers = _bannerAdsRepository.GetConfigByName(CurrentLanguageCode,"MailManager");
                if (list_mail_receivers != null)
                {
                    var x = list_mail_receivers.Split(",");
                    foreach (var item in x)
                        Email.Send(item, "THÔNG BÁO ĐƠN HÀNG", body, null);
                }
            }
            if (type == "comment")
            {
                if (list_mail_receivers != null)
                {
                    var x = list_mail_receivers.Split(",");
                    foreach (var item in x)
                        Email.Send(item, "THÔNG BÁO COMMENT", body, null);
                }
            }
            if (type == "rating")
            {
                if (list_mail_receivers != null)
                {
                    var x = list_mail_receivers.Split(",");
                    foreach (var item in x)
                        Email.Send(item, "THÔNG BÁO RATING", body, null);
                }
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult GetCommentList(int object_id, int object_type, int? pageIndex)
        {
            pageIndex = pageIndex ?? 1;
            return ViewComponent("Comment", new { object_id = object_id, object_type = object_type, pageIndex = pageIndex });
        }
        [HttpPost]
        public IActionResult GetReplyComment(int id, int obj_id, int obj_type)
        {
            if (obj_id > 0)
            {
                ViewBag.Id = id;
                ViewBag.ObjId = obj_id;
                ViewBag.ObjType = obj_type;
                return View();
            }
            return BadRequest();

        }
        public IActionResult SiteMapXml()
        {
            var sitemapNodes = _bannerAdsRepository.GetConfigByName(CurrentLanguageCode, "SiteMap");
            var url = Utils.UIHelper.StoreFilePath(sitemapNodes, false);
            var textFromFile = (new WebClient()).DownloadString(url);
            return this.Content(textFromFile, "text/xml", Encoding.UTF8);
        }

        public IActionResult SiteMapGenerate()
        {

            return View();
        }

        public IActionResult RobotsTxT()
        {
            var sitemapNodes = _bannerAdsRepository.GetConfigByName(CurrentLanguageCode, "Robotxt");
            var url = Utils.UIHelper.StoreFilePath(sitemapNodes, false);
            var textFromFile = (new WebClient()).DownloadString(url);
            return this.Content(textFromFile, "text/plain", Encoding.UTF8);
        }
        [HttpPost]
        public IActionResult GetBannerAdsByCode(string lang_code, string banner_code)
        {
            var result = _bannerAdsRepository.GetBannerAds_By_Code(lang_code, banner_code);

            return PartialView((object)result.MetaData);
        }
    }
}