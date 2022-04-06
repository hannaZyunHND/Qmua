using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.ES
{

    public class InfoES
    {
        public string Lang { get; set; }
        public string Name { get; set; }
        public string NameNorm { get; set; }
        public string Url { get; set; }
    }
    public class PriceES
    {
        public int LocationId { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public PriceES()
        {
            this.LocationId = 0;
            this.Price = 0;
            this.SalePrice = 0;
        }
    }
    public class ProductES
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public double DiscountPercent { get; set; }
        public string Unit { get; set; }
        public int ManufacturerId { get; set; }
        public List<PriceES> Prices { get; set; }
        public string Spectifications { get; set; }
        public List<InfoES> Info { get; set; }
        public CompletionField Suggest { get; set; }
        public double Rate { get; set; }
        public double Evaluate { get; set; }
        public string Properties { get; set; }
        public string ArticleId { get; set; }
        public ProductES()
        {
            this.Id = 0;
            this.Avatar = string.Empty;
            this.Code = string.Empty;
            this.Price = 0;
            this.DiscountPrice = 0;
            this.DiscountPercent = 0;
            this.ManufacturerId = 0;
            this.Prices = new List<PriceES>();
            this.Spectifications = string.Empty;
            this.Unit = "/mm2";
            this.Info = new List<InfoES>();
            this.Rate = 0;
            this.Evaluate = 0;
            this.Properties = string.Empty;
            this.ArticleId = string.Empty;
        }
    }

    public class ProductSuggestResponse
    {
        public IEnumerable<ProductSuggest> Suggests { get; set; }
    }

    public class ProductSuggest
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string DiscountPrice { get; set; }
        public double DiscountPercent { get; set; }
        public string Unit { get; set; }
        public double Score { get; set; }
        public string Spectificat { get; set; }
        public double Rate { get; set; }
        public double Evaluate { get; set; }
        public string Properties { get; set; }
    }
}
