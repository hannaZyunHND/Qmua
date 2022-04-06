using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class RegionInLanguage
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string BannerLink { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string LanguageCode { get; set; }
        public int RegionId { get; set; }
    }
}
