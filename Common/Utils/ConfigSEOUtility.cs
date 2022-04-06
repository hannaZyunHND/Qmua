using Enterbuy.Data.SqlServer.Dao.Interfaces;
using Enterbuy.Data.SqlServer.ModelDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Utils
{
    public interface IConfigSEOUtilityy
    {
        string ConfigWebsite(string domain, string page_name);
        string ConfigMeta(string title, string description, string keyword);
        string ConfigSocialMeta(string fbAppId = "default fbAddId", string url = "default url", string title = "default title", string description = "default description", string image = "default image");
        string ConfigShemaOrg(string domain_full, string title, string avatar, DateTime createdDate, DateTime modifiedDate, string metaTitle, string web_name, string logo, string description);
        string ConfigShemaOrgProduct(ProductDetail product);
        string ConfigShemaOrgArticle(ArticleDetail article, string domain_full, string domain);
    }
    public class ConfigSEOUtility : IConfigSEOUtilityy
    {
        private readonly IBannerAdsDao _bannerAds;
        private IHttpContextAccessor _httpContext;
        public ConfigSEOUtility(IBannerAdsDao bannerAds, IHttpContextAccessor httpContext)
        {
            _bannerAds = bannerAds;
            _httpContext = httpContext;
        }
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
                    var feature = _httpContext.HttpContext.Features.Get<IRequestCultureFeature>();
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
                    IRequestCultureFeature feature = _httpContext.HttpContext.Features.Get<IRequestCultureFeature>();
                    _currentLanguageCode = feature.RequestCulture.Culture.ToString();
                }

                return _currentLanguageCode;
            }


        }

        public string ConfigMeta(string title, string description, string keyword)
        {

            var r = "";
            r += "<title>" + title + "</title>";
            r += "<meta name=\"descripttion\" content=\"" + description + "\">";
            r += "<meta name=\"keywords\" content=\"" + keyword + "\">";
            return r;
        }

        public string ConfigShemaOrg(string domain_full, string title, string avatar, DateTime createdDate, DateTime modifiedDate, string metaTitle, string web_name, string logo, string description)
        {
            var r = "";
            r += "<script type=\"application/ld+json\">";
            r += "{";
            r += "\"@context\":\"http://schema.org\",";
            r += "\"@type\":\"Article\",";
            r += "\"mainEntityOfPage\":{\"@type\":\"WebPage\",\"@id\":\"" + domain_full + "\"},";
            r += "\"headline\":\"" + title + "\",";
            r += "\"image\":{\"@type\":\"ImageObject\",\"url\":\"" + avatar + "\",\"height\":600,\"width\":800},";
            r += "\"datePublished\":\"" + createdDate + "\",";
            r += "\"dateModified\":\"" + modifiedDate + "\",";
            r += "\"author\":{  \"@type\":\"Person\",\"name\":\"" + web_name + "\"},";
            r += "\"publisher\":{\"@type\":\"Organization\",\"name\":\"" + web_name + "\",\"logo\":{\"@type\":\"ImageObject\",\"url\":\"" + logo + "\",\"width\":35,\"height\":34}},";
            r += "\"description\":\"" + description + "\"";
            r += "}";
            r += "</script>";
            return r;
        }
        public string ConfigShemaOrgProduct(ProductDetail product)
        {
            var r = "";
            r += "<script type=\"application/ld+json\">";
            r += "{";
            r += "\"@context\":\"http://schema.org\",";
            r += "\"@type\":\"Product\",";
            r += "\"name\":\"" + product.Title + "\",";
            r += "\"image\":\"" + UIHelper.StoreFilePath(product.Avatar) + "\",";
            r += "\"description\":\"" + product.Description + "\",";
            r += "\"mpn\":\"" + product.Id + "\",";
            r += "\"brand\":{\"@type\":\"Thing\",\"name\":\"" + product.Title + "\"},";
           //   r += "\"aggregateRating\":{\"@type\":\"AggregateRating\",\"ratingValue\":\"" + Math.Round(product.RateAVG, 1) + "\", \"reviewCount\":\"" + product.TotalRate + "\"},";
           // r += "\"offers\":{\"@type\":\"Offer\",\"priceCurrency\":\"VND\",\"price\":\"" + product. + "\",\"priceValidUntil\":\"" + product.DiscountPrice + "\",\"itemCondition\":\"http://schema.org/UsedCondition \",\"availability\":\"http://schema.org/InStock \",\"logo\":{\"@type\":\"Organization\",\"name\":\"Executive Objects\"}},";
            r += "}";
            r += "</script>";
            return r;
        }
        public string ConfigShemaOrgArticle(ArticleDetail article, string domain_full, string domain)
        {
            var r = "";
            r += "<script type=\"application/ld+json\">";
            r += "{";
            r += "\"@context\":\"http://schema.org\",";
            r += "\"@type\":\"Article\",";
            r += "\"mainEntityOfPage\":{\"@type\":\"WebPage\",\"@id\":\"" + domain_full + "\"},";
            r += "\"headline\":\"" + article.Title + "\",";
            r += "\"image\":{\"@type\":\"ImageObject\",\"url\":\"" + UIHelper.StoreFilePath(article.Avatar) + "\",\"height\":600,\"width\":800},";
            r += "\"datePublished\":\"" + article.CreatedDate + "\",";
            r += "\"dateModified\":\"" + article.CreatedDate + "\",";
            r += "\"author\":{  \"@type\":\"Person\",\"name\":\"" + article.Author + "\"},";
            r += "\"publisher\":{\"@type\":\"Organization\",\"name\":\"" + domain + "\",\"logo\":{\"@type\":\"ImageObject\",\"url\":\"" + UIHelper.StoreFilePath(article.Avatar) + "\",\"width\":35,\"height\":34}},";
            r += "\"description\":\"" + article.Description + "\"";
            r += "}";
            r += "</script>";
            return r;
        }

        public string ConfigSocialMeta(string fbAppId = "default fbAddId", string url = "default url", string title = "default title", string description = "default description", string image = "default image")
        {
            var r = "";
            r += "<meta property=\"fb:app_id\" content=\"" + fbAppId + "\" />\r\n";
            r += "<meta property=\"og:url\" content=\"" + url + "\" />\r\n";
            r += "<meta property=\"og:type\" content=\"article\" />\r\n";
            r += "<meta property=\"og:title\" content=\"" + title + "\" />\r\n";
            r += "<meta property=\"og:description\" content=\"" + description + "\" />\r\n";
            r += "<meta property=\"og:image\" content=\"" + image + "\" />\r\n";
            return r;
        }

        public string ConfigWebsite(string domain, string page_name)
        {
            var favicon = _bannerAds.GetConfigByName(CurrentLanguageCode, "Favicon");
            var r = "";
            r += "<meta charset=\"utf - 8\">\r\n";
            r += "<meta name=\"viewport\" content=\"width = device-width, initial-scale = 1\">\r\n";
            r += "<meta charset=\"utf-8\" />\r\n";
            r += "<meta charset=\"utf-8\" />\r\n";
            r += "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n";
            r += "<meta name=\"theme-color\" content=\"#081b62\" />\r\n";
            r += "<meta name=\"msapplication-navbutton-color\" content=\"#081b62\">\r\n";
            r += "<meta name=\"apple-mobile-web-app-capable\" content=\"yes\">\r\n";
            r += "<meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\">\r\n";
            r += "<meta http-equiv=\"Content-Language\" content=\"vi\" />\r\n";
            r += "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n";
            r += "<meta http-equiv=\"audience\" content=\"general\" />\r\n";
            r += "<meta name=\"resource-type\" content=\"document\" />\r\n";
            r += "<meta name=\"abstract\" content=\"" + page_name + "\" />\r\n";
            r += "<meta name=\"classification\" content=\"" + page_name + "\" />\r\n";
            r += "<meta name=\"area\" content=\"" + page_name + "\" />\r\n";
            r += "<meta name=\"placename\" content=\"Việt Nam\" />\r\n";
            r += "<meta name=\"author\" content=\"" + domain + "\" />\r\n";
            r += "<meta name=\"copyright\" content=\"©2020" + domain + "\" />\r\n";
            r += "<meta name=\"owner\" content=\"" + domain + "\" />\r\n";
            r += "<meta name=\"distribution\" content=\"Global\" />\r\n";
            r += "<meta name=\"robots\" content=\"index,follow,all\" />\r\n";
            r += "<link rel=\"alternate\" href=\"" + domain + "\" />\r\n";
            r += "<link rel=\"canonical\" href=\"" + domain + "\" />\r\n";
            r += "<link rel=\"shortcut icon\" type=\"image / png\" href=\"" + UIHelper.StoreFilePath(favicon, false) + "\"/>";
            return r;
        }


        public string ConfigSeoBreadcrumb(ZoneByTreeViewMinify bread, string page)
        {
            var r = "";

            return r;
        }
    }
}
