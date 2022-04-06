using MI.Entity.CustomClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Article.ViewModel
{
    public class ArticleMinify
    {
        public ArticleMinify()
        {
            ArticleRecruitment = new ArticleRecruitment();
        }
        public int Id { get; set; }
        public string Avatar { get; set; }
        public int Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public string ZoneUrl { get; set; }
        public string ZoneType { get; set; }
        public string Metadata { get; set; }
        public ArticleRecruitment ArticleRecruitment { get; set; }
        public string UrlFileDownload { get; set; }
        public string UrlFileDownloadMinify { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
    }
    public class ArticleRecruitment1
    {
        public string Position { get; set; }
        public string Address { get; set; }
        public string Count { get; set; }
        public string Salary { get; set; }

        public string ttkh { get; set; }
        public string ttdc { get; set; }
        public string ttlv { get; set; }
        public string ttdv { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AvatarArray { get; set; } = "";
    }

    public class RedirechArticle
    {
        public int ObjectId { get; set; }
        public int ObjectType { get; set; }
        public string ObjectUrl { get; set; }
        public string ObjectTitle { get; set; }
    }

    public class ArticleDetail
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int IsAllowComment { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string Title { get; set; }
        public int ViewCount { get; set; }
        public string SocialTitle { get; set; }
        public string SocialImage { get; set; }
        public string SocialDescription { get; set; }
        public int ZoneId { get; set; }
        public string ZoneBanner { get; set; }
        public string ZoneName { get; set; }
        public string Metadata { get; set; }
        public string Tags { get; set; }
        public ArticleRecruitment ArticleRecruitment { get; set; }
        public string Indexing { get; set; }
        public int Type { get; set; }
        public decimal RateAVG { get; set; }
        public int TotalRate { get; set; }
        public int Five_Star { get; set; }
        public int Four_Star { get; set; }
        public int Three_Star { get; set; }
        public int Two_Star { get; set; }
        public int One_Star { get; set; }
        public int CommentCount { get; set; }
    }
}
