using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Extra.Repository;
using TemplateKAO.Services.Extra.ViewModel;

namespace TemplateKAO.Utility
{
    public interface ISiteMapUtility
    {
        string GenerateSiteMap(string domain);
    }
    public class SiteMapUtility : ISiteMapUtility
    {
        private IExtraRepository _extra;
        public SiteMapUtility(IExtraRepository extra)
        {
            _extra = extra;
        }
        private string GenerateSiteMapNote(string domain, SiteMapViewModel item)
        {
            var result = "";
            var u = "";
            //var d = new DateTime();
            //d = item.Modified == null ? (item.Create == null ? DateTime.Now : item.Create) : item.Modified;
            var x = item.Create == null ? DateTime.Now : item.Create;
            var d = item.Modified == null ? x : item.Modified;
            switch (item.Type)
            {
                case "zone":
                    u = domain + "/" + item.Url;
                    break;
                case "product":
                    u = domain + "/" + item.Url + ".html";
                    break;
                case "article":
                    u = domain + "/" + item.Url + ".html";
                    break;
                case "region":
                    u = domain + "/danh-muc/" + item.Url;
                    break;
                case "tag":
                    u = domain + "/the/" + item.Url;
                    break;
            }
            result += "<url>";
            result += "<loc>" + u + "</loc>";
            result += "<lastmod>" + d.ToString("yyyy-MM-dd") + "</lastmod>";
            result += "<changefreq>" + "daily" + "</changefreq>";
            result += "<priority>0.9</priority>";
            result += "</url>";
            return result;
        }
        public string GenerateSiteMap(string domain)
        {
            var result = "";
            var l = _extra.GetSiteMapUrl();
            foreach (var item in l)
            {
                result += GenerateSiteMapNote(domain, item);
            }
            //Parallel.ForEach(l, it =>
            //{
            //   result += GenerateSiteMapNote(domain, it);
            //});
            return result;
            //Cac link san pham
            //Cac link bai viet
            //Cac link phu

        }
    }
}
