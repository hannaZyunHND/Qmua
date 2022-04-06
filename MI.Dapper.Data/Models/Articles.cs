using System;
using System.Collections.Generic;
using MI.Entity.CustomClass;

namespace MI.Dapper.Data.Models
{
    public class Articles
    {
        public Articles()
        {
            this.Id = 0;
            this.Avatar = string.Empty;
            this.Status = 0;
            this.Type = 0;
            this.CreatedDate = DateTime.Now;
            this.LastModifiedDate = DateTime.Now;
            this.DistributionDate = DateTime.Now;
            this.CreatedBy = string.Empty;
            this.LastModifiedBy = string.Empty;
            this.PublishedBy = string.Empty;
            this.Url = string.Empty;
            this.IsAllowComment = false;
            this.Description = string.Empty;
            this.Body = string.Empty;
            this.Count = "0";
            this.ViewCount = 0;
            this.WordCount = 0;
            this.ZoneIds = string.Empty;
            this.UrlOld = string.Empty;
            this.MetaWebPage = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public byte Status { get; set; }
        public byte? Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string PublishedBy { get; set; }
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
        public string MetaNoIndex { get; set; }
        public string MetaCanonical { get; set; }
        public bool? IsAllowComment { get; set; }
        public bool? IsShowHome { get; set; }
        public string SocialTitle { get; set; }
        public string SocialDescription { get; set; }
        public string SocialImage { get; set; }
        public string LanguageCode { get; set; }
        public string ZoneIds { get; set; }
        public string ProductIds { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Count { get; set; }
        public string Salary { get; set; }
        public string TagNames { get; set; }
        public string FileAttachment { get; set; }
        public string FileAttachmentMinify { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Indexing { get; set; }
        public string FileAttachmentUrl { get; set; }
        public string MetaWebPage { get; set; }
        public string UrlOld { get; set; }
        public string ttkh { get; set; }
        public string ttdc { get; set; }
        public string ttlv { get; set; }
        public string ttdv { get; set; }
        public string avatarArray { get; set; }
        public string ThiTruong { get; set; }
        public string MoHinh { get; set; }
        public string DuAn { get; set; }
        public string MucDichDauTu { get; set; }
        //public string MyProperty { get; set; }
        public List<MoRong> phanMoRong { get; set; } = new List<MoRong>();
    }

    
}

/*
 name: "",
                        sub: [
                            {
                                title: "",
                                avatar: "",
                                description: "",
                                sort: 0,
                                url: ""
                            }
                        ]*/