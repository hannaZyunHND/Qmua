using MI.Entity.CustomClass;
using System;
using System.Collections.Generic;

namespace MI.Dapper.Data.ViewModels
{
    public class ArticleInLanguageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public int? WordCount { get; set; }
        public int? ViewCount { get; set; }
        public string Url { get; set; }
        public string UrlOld { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }
        public string MetaNoIndex { get; set; }
        public string MetaCanonical { get; set; }
        public string MetaTitle { get; set; }
        public bool? IsAllowComment { get; set; }
        public string SocialTitle { get; set; }
        public string SocialDescription { get; set; }
        public string SocialImage { get; set; }
        public string LanguageCode { get; set; }
        public int ArticleId { get; set; }
        public string MetaData { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Count { get; set; }
        public string Salary { get; set; }
        public string ttkh { get; set; }
        public string ttdc { get; set; }
        public string ttlv { get; set; }
        public string ttdv { get; set; }
        public List<MoRong> phanMoRong { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tags { get; set; }
        public string FileAttachment { get; set; }
        public string FileAttachmentUrl { get; set; }
        public string Indexing { get; set; }
        public string MetaWebPage { get; set; }
        public List<TagsViewModel> TagsViewModels { get; set; } = new List<TagsViewModel>();
        public string MoHinh { get; set; }
        public string ThiTruong { get; set; }
        public string MucDichDauTu { get; set; }
        public string DuAn { get; set; }
    }
}