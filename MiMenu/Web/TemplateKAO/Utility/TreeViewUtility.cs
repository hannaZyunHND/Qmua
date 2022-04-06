using MI.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateKAO.Services.Zone.ViewModal;
using Utils;

namespace TemplateKAO.Utility
{
    public static class TreeviewHelper
    {
        public static string RenderTag(List<ZoneByTreeViewMinify> list, int level, int parentId)
        {
            if (list.Count > 0)
            {
                var max_leaf = list.Max(r => r.level);
                var result = "";
                var _af = list.Where(r => r.level >= level && r.ParentId == parentId).ToList();
                foreach (var item in _af)
                {
                    var link_tar = string.Format("/{0}", item.Url);
                    var p = item.Id;
                    var _tt1 = level + 1;
                    var _tt2 = level + 2;
                    result += "<li class=\"li-tree-lv-" + _tt1 + "\">";
                    result += string.Format("<span class=\"span-tree-node tree-lv-{2}\" data-url=\"{0}\" data-sp=" + 1 + ">{1}</span>", link_tar, UIHelper.TrimByWord(item.Name, 6, "..."), level + 1);
                    result += "<ul class=\"ul-tree-lv-" + _tt2 + "\">";
                    result += RenderTag(list, level + 1, p);
                    result += "</ul>";
                    result += "</li>";

                    //return result;
                }
                return result;


            }
            return "";
        }
        public static string RenderTagWithMaxLevel(List<ZoneByTreeViewMinify> list, int level, int parentId, int maxLevel, string link_root)
        {

            if (list.Count > 0 && level <= maxLevel)
            {

                var max_leaf = list.Max(r => r.level);
                var result = "";
                var _af = list.Where(r => r.level >= level && r.ParentId == parentId).OrderBy(r => r.SortOrder).ToList();
                foreach (var item in _af)
                {
                    var link_tar = string.Format("/{0}", item.Url);
                    var b = false;
                    var ftype = 0;
                    var fval = "";
                    if (item.ZoneSearchType == (int)ZoneSearchType.Manufacture || item.ZoneSearchType == (int)ZoneSearchType.Price)
                    {
                        var parent = list.Where(r => r.Id == item.ParentId).FirstOrDefault();
                        if (parent != null)
                        {
                            link_tar = LinkRedirectUrlUtility.ProductCategoryUrl(parent.Url);
                            ftype = item.ZoneSearchType;
                            if (ftype == (int)ZoneSearchType.Manufacture)
                                fval = item.ManufacturerId;
                            if (ftype == (int)ZoneSearchType.Price)
                                fval = item.Filter;
                            b = true;
                        }
                    }
                    var p = item.Id;
                    var _tt1 = level + 1;
                    var _tt2 = level + 2;
                    result += "<li class=\"li-tree-lv-" + _tt1 + "\">";
                    if (!b)
                        result += string.Format("<a class=\"span-tree-node tree-lv-{2}\" href=\"{0}\" data-url=\"{0}\">{1}</a>", link_tar, UIHelper.TrimByWord(item.Name, 8, "..."), level + 1);
                    else
                        result += string.Format("<a class=\"span-tree-node tree-lv-{2} zone-have-filter\" href=\"javascript:void(0)\" data-tarurl=\"{0}\" data-ftype=\"{3}\" data-fval=\"{4}\" data-url=\"{0}\">{1}</a>", link_tar, UIHelper.TrimByWord(item.Name, 6, "..."), level + 1, ftype, fval);
                    result += "<ul class=\"ul-tree-lv-" + _tt2 + "\">";
                    result += RenderTagWithMaxLevel(list, level + 1, p, maxLevel, link_root);
                    result += "</ul>";
                    result += "</li>";
                    //return result;
                }
                return result;
            }
            return "";
        }
        public static string ReturnHTMLTree(List<ZoneByTreeViewMinify> list, int parentId)
        {
            var result = "";
            try
            {
                result += RenderTag(list, 0, parentId);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            return result;
        }
    }
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Root { get; set; }
        public int Level { get; set; }
        public int ParentId { get; set; }
    }
}
