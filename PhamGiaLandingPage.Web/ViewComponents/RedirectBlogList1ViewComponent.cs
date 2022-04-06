using MI.Entity.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Article.Repository;
using PhamGiaLandingPage.Web.Services.Product.Repository;
using PhamGiaLandingPage.Web.Services.Zone.Repository;
using PhamGiaLandingPage.Web.Services.Zone.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.ViewComponents
{
    public class RedirectBlogList1ViewComponent : ViewComponent
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
        public RedirectBlogList1ViewComponent(IZoneRepository zoneRepository, IProductRepository productRepository, IArticleRepository articleRepository)
        {
            _zoneRepository = zoneRepository;
            _productRepository = productRepository;
            _articleRepository = articleRepository;
        }
        public IViewComponentResult Invoke(List<ZoneByTreeViewMinify> zones, int id)
        {
            if (zones != null && id > 0)
            {
                var zone_id = id;
                //var list_zone_blog = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Article, CurrentLanguageCode, 0);
                var list_zone_blog = zones;
                //var list_zone_promotions = _zoneRepository.GetZoneByTreeViewMinifies((int)TypeZone.Promotion, CurrentLanguageCode, 0);
                var zone_blog_parent = list_zone_blog.Where(r => (r.ParentId == 0 && r.Id == zone_id)
                                                            || (r.ParentId > 0 && r.Id == zone_id)).FirstOrDefault();
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
            return null;
        }
    }
}
