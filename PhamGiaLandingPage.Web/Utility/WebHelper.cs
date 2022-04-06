using HtmlAgilityPack;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace PhamGiaLandingPage.Web.Utility
{
    public class WebHelper
    {
        public static IConfigurationRoot Configuration { get; set; }
        static WebHelper()
        {
            Configuration = ConfigurationHelper.Init();

        }

        public static string Version()
        {

            try
            {

                return Configuration["AppSettings:Version"];
            }
            catch (Exception e)
            {

            }

            return "1.0.0";



        }

        public static string RenderLazyLoadBody(string body)
        {
            HtmlDocument doc = new HtmlDocument();
            if (!string.IsNullOrEmpty(body))
            {
                doc.LoadHtml(body);
                var imgs = doc.DocumentNode.SelectNodes("//img");
                if (imgs != null)
                {
                    foreach (var item in imgs)
                    {
                        if (item != null)
                        {

                            var origin = item.GetAttributeValue("src", null);
                            item.SetAttributeValue("src", "~/images/gray.jpg");

                            if (!string.IsNullOrEmpty(origin) && (origin.StartsWith("/uploads/") || origin.StartsWith("https://janhome.vn/wp-content")))
                            {
                                var domain_img_store = Configuration["AppSettings:FoderImg"];
                                origin = domain_img_store + origin.Replace("https://janhome.vn/wp-content", "");
                            }
                            item.SetAttributeValue("data-src", origin);
                            item.SetAttributeValue("srcset", origin);
                            item.AddClass("lazy");
                        }
                    }
                }
                var figures = doc.DocumentNode.SelectNodes("//figure");
                if (figures != null)
                {

                    foreach (var item in figures)
                    {
                        var _img = item.SelectSingleNode(".//img");
                        var _a = item.SelectSingleNode(".//a");
                        if (_img != null)
                        {
                            item.SetAttributeValue("style", "");
                            _img.AddClass("cust-ag");
                            //var url = _img.GetAttributeValue("data-src", null);


                        }
                        if (_a != null)
                        {
                            var url = _a.GetAttributeValue("href", null);
                            _a.SetAttributeValue("href", "javascript:void(0)");
                            _a.SetAttributeValue("data-url", url);
                        }

                    }

                }

                var uls = doc.DocumentNode.SelectNodes("//ul");
                if (uls != null)
                {
                    foreach (var item in uls)
                    {
                        item.AddClass("maintain-ul");

                        var ul_smaller = item.SelectNodes(".//ul");
                        if (ul_smaller != null)
                        {
                            foreach (var it in ul_smaller)
                            {
                                it.RemoveClass("maintain-ul");
                                it.AddClass("maintain-ul-smaller");
                            }
                        }
                    }
                }
                //var ul_parents = doc.DocumentNode.SelectNodes("//ul[contains(@class, 'toc_list')]").FirstOrDefault();
                var p_link = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'toc_title')]");
                if (p_link != null)
                {
                    var text = p_link.InnerText;
                    p_link.RemoveAllChildren();
                    p_link.Name = "a";
                    p_link.SetAttributeValue("href", "javascript:void(0)");
                    p_link.SetAttributeValue("style", "color: inherit;");
                    p_link.InnerHtml = text;
                    HtmlDocument fa = new HtmlDocument();
                    fa.LoadHtml("<i class=\"fas fa-angle-down\"></i>");
                    var f = fa.DocumentNode.SelectSingleNode("//i");
                    if (f != null)
                    {
                        p_link.ChildNodes.Add(f);
                    }

                }
                return doc.DocumentNode.OuterHtml;
            }
            return "";


        }

        public static string IndexingCss(string body)
        {
            HtmlDocument doc = new HtmlDocument();
            if (!string.IsNullOrEmpty(body))
            {
                doc.LoadHtml(body);

                var ul_parents = doc.DocumentNode.SelectNodes("//ul[contains(@class, 'toc_list')]").FirstOrDefault();
                if (ul_parents != null)
                {
                    var ul_childs = ul_parents.SelectNodes(".//ul");
                    if (ul_childs != null)
                    {
                        foreach (var item in ul_childs)
                            item.AddClass("pl-2");
                    }

                }

                var p_link = doc.DocumentNode.SelectSingleNode("//p");
                if (p_link != null)
                {
                    var text = p_link.InnerText;
                    p_link.RemoveAllChildren();
                    p_link.Name = "a";
                    p_link.SetAttributeValue("href", "javascript:void(0)");
                    p_link.InnerHtml = text;
                    HtmlDocument fa = new HtmlDocument();
                    fa.LoadHtml("<i class=\"fas fa-angle-down\"></i>");
                    var f = fa.DocumentNode.SelectSingleNode("//i");
                    if (f != null)
                    {
                        p_link.ChildNodes.Add(f);
                    }

                }
                return doc.DocumentNode.OuterHtml;
            }
            return "";
        }

        public static string GetFirstImage(string body)
        {
            HtmlDocument doc = new HtmlDocument();
            if (!string.IsNullOrEmpty(body))
            {
                doc.LoadHtml(body);

                var firstImg = doc.DocumentNode.SelectSingleNode("//img");
                if (firstImg != null)
                {
                    var r = firstImg.GetAttributeValue("src", "").ToString();
                    r = r.Replace("https://janhome.vn/wp-content/uploads", "");
                    return r;
                }
            }
            return "";
        }
    }
}
