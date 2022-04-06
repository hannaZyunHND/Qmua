using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductInLanguage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int? ViewCount { get; set; }
        public string Url { get; set; }
        public string PromotionInfo { get; set; }
        public string Catalog { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string SocialTitle { get; set; }
        public string SocialDescription { get; set; }
        public string SocialImage { get; set; }
        public string LanguageCode { get; set; }
        public int ProductId { get; set; }
        public string Id { get; set; }
        public string MetaWebPage { get; set; }
        public string UrlOld { get; set; }
        public string MetaNoIndex { get; set; }
        public string MetaCanonical { get; set; }

        public Product Product { get; set; }
    }
}
