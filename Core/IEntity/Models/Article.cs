using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleInLanguage = new HashSet<ArticleInLanguage>();
            ArticlesInZone = new HashSet<ArticlesInZone>();
            ProductInArticle = new HashSet<ProductInArticle>();
        }

        public int Id { get; set; }
        public string Avatar { get; set; }
        public byte Status { get; set; }
        public byte? Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string PublishedBy { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public bool? IsShowHome { get; set; }
        public int? ViewCount { get; set; }

        public ICollection<ArticleInLanguage> ArticleInLanguage { get; set; }
        public ICollection<ArticlesInZone> ArticlesInZone { get; set; }
        public ICollection<ProductInArticle> ProductInArticle { get; set; }
    }
}
