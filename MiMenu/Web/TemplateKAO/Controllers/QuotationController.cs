using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Article.Repository;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Services.Zone.ViewModal;

namespace TemplateKAO.Controllers
{
    public class QuotationController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        public QuotationController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        public IActionResult QuotationList(int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 6;
            int total = 0;
            string culture_code = "vi-VN";
            List<ZoneByTreeViewMinify> zoneByTreeViewMinifies = new List<ZoneByTreeViewMinify>();
            var list_blog_childs = new List<ZoneByTreeViewMinify>();
            var zone_blog_parent = new ZoneByTreeViewMinify();
            var top_newest = _articleRepository.GetArticlesInZoneId_Minify(0, (int)TypeZone.Quotation, culture_code, "", pageIndex, pageSize, out total);
            try
            {
                zoneByTreeViewMinifies = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Quotation, CurrentLanguageCode, 0);
                if (zoneByTreeViewMinifies.Count() > 0)
                {
                    zone_blog_parent = zoneByTreeViewMinifies.Where(r => r.ParentId == 0).FirstOrDefault();
                }

                if (zone_blog_parent.Id > 0)
                {
                    list_blog_childs = zoneByTreeViewMinifies.Where(r => r.ParentId == zone_blog_parent.Id).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.TotalPage = total;
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            ViewBag.ZoneParent = zone_blog_parent;
            ViewBag.ZoneChilds = list_blog_childs;
            ViewBag.Top_newest = top_newest;
            return View();
        }
        public IActionResult QuotationDetail(int Id)
        {
            List<ZoneByTreeViewMinify> zoneByTreeViewMinifies = new List<ZoneByTreeViewMinify>();
            var list_blog_childs = new List<ZoneByTreeViewMinify>();
            var zone_blog_parent = new ZoneByTreeViewMinify();
            try
            {
                zoneByTreeViewMinifies = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Quotation, CurrentLanguageCode, 0);
                if (zoneByTreeViewMinifies.Count() > 0)
                {
                    zone_blog_parent = zoneByTreeViewMinifies.Where(r => r.ParentId == 0).FirstOrDefault();
                }

                if (zone_blog_parent.Id > 0)
                {
                    list_blog_childs = zoneByTreeViewMinifies.Where(r => r.ParentId == zone_blog_parent.Id).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.ZoneParent = zone_blog_parent;
            ViewBag.ZoneChilds = list_blog_childs;
            return View();
        }
    }
}