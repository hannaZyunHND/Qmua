using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateKAO.Services.Zone.ViewModal
{
    public class ZoneByTreeViewMinify
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int SortOrder { get; set; }
        public string LanguageCode { get; set; }
        public string Icon { get; set; }
        public int root { get; set; }
        public string Url { get; set; }
        public int level { get; set; }
        public string order { get; set; }
        public int IsShowMenu { get; set; }
        public string Banner { get; set; }
        public int Type { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string BannerLink { get; set; }
        public string UrlOld { get; set; }
        public string Filter { get; set; }
        public string ManufacturerId { get; set; }
        public int ZoneSearchType { get; set; }
        //public string Filter { get; set; }

    }

    public class ZoneToRedirect
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int isHaveChild { get; set; }
    }

    public class ZoneSugget
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }


    public class ZoneDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int? SortOrder { get; set; }
        public int? ParentId { get; set; }
        public byte Status { get; set; }
        public string Avatar { get; set; }
        public string Background { get; set; }
        public int Type { get; set; }
        public string Banner { get; set; }
        public string BannerLink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string LanguageCode { get; set; }
        public decimal Rate { get; set; }
        public string UrlOld { get; set; }
        public string Filter { get; set; }
    }
}
