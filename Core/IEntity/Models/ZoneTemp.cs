using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ZoneTemp
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
        public byte? Type { get; set; }
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
    }
}
