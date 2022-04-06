using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class BaseBA
    {
        public static string UrlCategory(string cat, string domain = "")
        {
            if (!String.IsNullOrEmpty(cat))
            {
                if (cat.StartsWith(@"/"))
                {
                    return $"{domain}{cat}.html";
                }
                else
                {
                    return $"{domain}/{cat}.html";
                }
            }
            return cat;
        }
        public static string UrlCategoryJanHome(string cat, string domain = "")
        {
            if (!String.IsNullOrEmpty(cat))
            {
                if (cat.StartsWith(@"/"))
                {
                    return $"{domain}/{cat}";
                }
                else
                {
                    return $"{domain}/{cat}";
                }
            }
            return cat;
        }
        public static string UrlRegion(string cat, string domain = "")
        {
            if (!String.IsNullOrEmpty(cat))
            {
                if (cat.StartsWith(@"/"))
                {
                    return $"{domain}/danh-muc{cat}.html";
                }
                else
                {
                    return $"{domain}/danh-muc/{cat}.html";
                }
            }
            return cat;
        }
        public static string UrlBase(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                url = Settings.AppSettings.BaseDomain + url;
            }
            return url;
        }

        public static string UrlCategoryNews(string cat, string domain = "", bool extention = true)
        {
            string extent = "";
            if (extention)
            {
                extent = ".html";
            }
            if (!String.IsNullOrEmpty(cat))
            {
                if (cat.StartsWith(@"/"))
                {
                    return $"{domain}{cat}{extent}";
                }
                else
                {
                    return $"{domain}/news/{cat}{extent}";
                }
            }
            return cat;
        }
        public static string UrlNews(string cat, string news, int id = 0, string domain = "")
        {
            //if (id > 0)
            //    return $"{domain}/tin-tuc/{news}-{id}.html";
            //else
            return $"{domain}/tin-tuc/{news}.html";

        }
        public static string UrlNews2(string url, int id, string domain = "")
        {
            return $"{domain}/tin-tuc/{url}-{id}.html";

        }
        public static string UrlNewsJanhome(string cat, string news, int id, string domain = "")
        {
            if (Settings.AppSettings.GetByKey("et") == "1")
            {
                return $"{domain}/tin-tuc/{news}.html";
            }
            else
            {
                return $"{domain}/{news}.html";
            }
           
        }

        public static string UrlProduct(string cat, string pro, string domain = "")
        {

            string extent = ".html";
            if (pro.EndsWith(".html"))
            {
                extent = "";
            }

            if (!String.IsNullOrEmpty(cat) && cat.StartsWith(@"/"))
            {
                return $"{domain}{pro}{extent}";
            }
            else
            {
                return $"{domain}/{pro}{extent}";
            }
        }
        public static string UrlProductJanhome(string cat, string pro, string domain = "")
        {
            if (Settings.AppSettings.GetByKey("et") == "1")
            {
                string extent = ".html";
                if (pro.EndsWith(".html"))
                {
                    extent = "";
                }

                if (!String.IsNullOrEmpty(cat) && cat.StartsWith(@"/"))
                {
                    return $"{domain}san-pham/{pro}{extent}";
                }
                else
                {
                    return $"{domain}/san-pham/{pro}{extent}";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(cat))
                {
                    cat = "kxd";
                }
                if (cat.StartsWith(@"/"))
                {
                    return $"{domain}/{pro}.html";
                }
                else
                {
                    return $"{domain}/{pro}.html";
                }
            }
              
        }
        public static string UrlImstallment(string pro, int id, string domain = "")
        {
            return $"{domain}/tra-gop/{pro}-p{id}.html";
        }
    }
}
