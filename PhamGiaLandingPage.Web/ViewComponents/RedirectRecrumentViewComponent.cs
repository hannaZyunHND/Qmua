using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Article.ViewModel;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Services.Zone.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class RedirectRecrumentViewComponent : ViewComponent
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IProductRepository _productRepository;
        private readonly IArticleRepository _articleRepository;
        const string CookieLocationId = "_LocationId";
        const string CookieLocationName = "_LocationName";
        private string _currentLanguage;
        private string _currentLanguageCode;
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

        //public IProductRepository ProductRepository => _productRepository;

        public RedirectRecrumentViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        public IViewComponentResult Invoke(ZoneDetail zoneDetail, int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 1;
            pageSize = pageSize ?? 10;
            int total = 0;
            string culture_code = CurrentLanguageCode;
            if (zoneDetail != null)
            {
                List<ZoneByTreeViewMinify> zoneByTreeViewMinifies = new List<ZoneByTreeViewMinify>();
                var list_blog_childs = new List<ZoneByTreeViewMinify>();
                var zone_blog_parent = new ZoneByTreeViewMinify();
                var top_newest = new List<ArticleMinify>();

                try
                {
                    zoneByTreeViewMinifies = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Recruitment, CurrentLanguageCode, 0);
                    if (zoneByTreeViewMinifies.Count() > 0)
                    {
                        zone_blog_parent = zoneByTreeViewMinifies.Where(r => r.ParentId == 0).FirstOrDefault();
                    }

                    if (zone_blog_parent.Id > 0)
                    {

                        list_blog_childs = zoneByTreeViewMinifies.Where(r => r.ParentId == zone_blog_parent.Id).ToList();
                        if (zone_blog_parent.Id == zoneDetail.Id)
                        {
                            top_newest = _articleRepository.GetArticlesInZoneId_Minify(0, (int)TypeZone.Recruitment, culture_code, "", pageIndex, pageSize, out total);
                        }
                        if (zone_blog_parent.Id == zoneDetail.ParentId)
                        {
                            top_newest = _articleRepository.GetArticlesInZoneId_Minify(zoneDetail.Id, (int)TypeZone.Recruitment, culture_code, "", pageIndex, pageSize, out total);
                        }
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
                ViewBag.Detail = zoneDetail;
                return View();
            }
            return null;
        }
    }
}
