using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ArticleInLanguage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public int? WordCount { get; set; }
        public int? ViewCount { get; set; }
        public string Url { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool? IsAllowComment { get; set; }
        public string SocialTitle { get; set; }
        public string SocialDescription { get; set; }
        public string SocialImage { get; set; }
        public string LanguageCode { get; set; }
        public int ArticleId { get; set; }
        public string Metadata { get; set; }
        public string FileAttachment { get; set; }
        public string Tags { get; set; }
        public string FileAttachmentUrl { get; set; }
        public string Indexing { get; set; }
        public string MetaWebPage { get; set; }
        public string FileAttachmentMinify { get; set; }
        public string UrlOld { get; set; }
        public string MetaNoIndex { get; set; }
        public string MetaCanonical { get; set; }
        public string ThiTruong { get; set; }
        public string MoHinh { get; set; }
        public string DuAn { get; set; }
        public string MucDichDauTu { get; set; }

        public Article Article { get; set; }
        public Language LanguageCodeNavigation { get; set; }
    }
}
