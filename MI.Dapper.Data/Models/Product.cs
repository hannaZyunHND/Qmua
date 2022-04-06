using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Dapper.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = 0;
            this.Status = 1;
            this.Url = "";
            this.Avatar = "";
            this.AvatarArray = "";
            this.Price = 0;
            this.DiscountPrice = 0;
            this.Warranty = "";
            this.ManufacturerId = 0;
            this.Code = "";
            this.CreatedDate = DateTime.Now;
            this.CreatedBy = "";
            this.ModifyDate = DateTime.Now;
            this.ModifyBy = "";
            this.Unit = "";
            this.Quantity = 0;
            this.PropertyId = "";
        }

        public int Id { get; set; }
        public int? Status { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
        public string AvatarArray { get; set; }
        public double? Price { get; set; }
        public double? DiscountPrice { get; set; }
        public string Warranty { get; set; }
        public int? ManufacturerId { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public string Unit { get; set; }
        public int? Quantity { get; set; }
        public string PropertyId { get; set; }
    }
}