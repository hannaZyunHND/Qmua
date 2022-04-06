using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class Product
    {
        public int LangCount;
        public IEnumerable<KeyValuePair<string, string>> ListUrl;
        public Product()
        {
            this.ProductInArticle = new List<ProductInArticle>();
            this.ProductInLanguage = new List<ProductInLanguage>();
            this.ProductInZone = new List<ProductInZone>();
            this.ProductInPromotion = new List<ProductInPromotion>();
            this.ProductSpecifications = new List<ProductSpecifications>();
            this.ProductInRegion = new List<ProductInRegion>();
            this.Status = 2;
            this.MaterialType = 0;
            this.DiscountPercent = 0;
            this.ViewCount = 0;
            this.ExprirePromotion = DateTime.MinValue;
            this.Vat = false;
            this.SortOrder = 999;
            this.ArticleId = string.Empty;
        }
        public virtual ICollection<ProductInArticle> ProductInArticle { get; set; }
        public virtual ICollection<ProductInLanguage> ProductInLanguage { get; set; }
        public virtual ICollection<ProductInZone> ProductInZone { get; set; }
        public virtual ICollection<ProductInPromotion> ProductInPromotion { get; set; }
        public virtual ICollection<ProductSpecifications> ProductSpecifications { get; set; }
        public virtual ICollection<ProductInRegion> ProductInRegion { get; set; }


    }
}
