using MI.Entity.CustomClass;
using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Article.Repository;
using TemplateKAO.Services.Article.ViewModel;
using TemplateKAO.Services.Product.Repository;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Services.Zone.ViewModal;

namespace TemplateKAO.Controllers
{
    public class RecruitmentController : BaseController
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;

        public RecruitmentController(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        public IActionResult RecruitmentList(int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 6;
            int total = 0;
            //string culture_code = "vi-VN";
            List<ArticleMinify> articleMinifies = new List<ArticleMinify>();
            List<ZoneByTreeViewMinify> zoneByTreeViewMinifies = new List<ZoneByTreeViewMinify>();
            var list_blog_childs = new List<ZoneByTreeViewMinify>();
            var zone_blog_parent = new ZoneByTreeViewMinify();
            try
            {
                zoneByTreeViewMinifies = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Recruitment, CurrentLanguageCode, 0);
                articleMinifies = _articleRepository.GetArticlesInZoneId_Minify(0, (int)TypeZone.Recruitment, CurrentLanguageCode, "", pageIndex, pageSize, out total);
                if (articleMinifies.Count() > 0)
                {
                    for (int i = 0; i < articleMinifies.Count(); i++)
                    {
                        articleMinifies[i].ArticleRecruitment = JsonConvert.DeserializeObject<ArticleRecruitment>(articleMinifies[i].Metadata);
                    }
                }
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
            ViewBag.Top_newest = articleMinifies;
            return View();
        }
        public IActionResult RecruitmentDetail(string alias, int articleId)
        {
            ArticleDetail article_detail = new ArticleDetail();
            try
            {
                article_detail = _articleRepository.GetArticleDetail(articleId, CurrentLanguageCode);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            ViewBag.Detail = article_detail;
            return View();
        }
    }
}