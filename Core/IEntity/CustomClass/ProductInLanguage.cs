using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class ProductInLanguage
    {
        public List<string> ListTagName;
        public ProductInLanguage()
        {
            ListTagName = new List<string>();
            this.Catalog = string.Empty;
            this.Content = string.Empty;
            this.Description = string.Empty;
            this.Id = string.Empty;
            this.LanguageCode = string.Empty;

            this.ListTagName = new List<string>();
            this.MetaDescription = string.Empty;
            this.MetaKeyword = string.Empty;
            this.MetaTitle = string.Empty;
            this.Product = new Product();
            this.ProductId = 0;
            this.PromotionInfo = string.Empty;
            this.SocialDescription = string.Empty;
            this.SocialImage = string.Empty;
            this.SocialTitle = string.Empty;
            this.Title = string.Empty;
            this.Url = string.Empty;
            this.ViewCount = 0;
        }
        public ProductInLanguage(ProductInLanguage obj, List<string> lstTags)
        {
            this.Catalog = obj.Catalog;
            this.Content = obj.Content ?? "";
            this.Description = obj.Description ?? "";
            this.Id = obj.Id;
            this.LanguageCode = obj.LanguageCode;

            this.ListTagName = lstTags;
            this.MetaDescription = obj.MetaDescription;
            this.MetaKeyword = obj.MetaKeyword;
            this.MetaTitle = obj.MetaTitle;
            this.MetaNoIndex = obj.MetaNoIndex;
            this.MetaCanonical = obj.MetaCanonical;
            this.Product = obj.Product;
            this.ProductId = obj.ProductId;
            this.PromotionInfo = obj.PromotionInfo;
            this.SocialDescription = obj.SocialDescription;
            this.SocialImage = obj.SocialImage;
            this.SocialTitle = obj.SocialTitle;
            this.Title = obj.Title;
            this.Url = obj.Url;
            this.ViewCount = obj.ViewCount;
            this.MetaWebPage = obj.MetaWebPage;
            this.UrlOld = obj.UrlOld;
        }
    }
}
