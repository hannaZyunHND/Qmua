using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Product
    {
       

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
        public string Color { get; set; }
        public string Name { get; set; }
        public string Guarantee { get; set; }
        public int? MaterialType { get; set; }
        public double DiscountPercent { get; set; }
        public string MetaFile { get; set; }
        public string Voucher { get; set; }
        public int? ViewCount { get; set; }
        public DateTime? ExprirePromotion { get; set; }
        public bool? IsInstallment { get; set; }
        public bool? Vat { get; set; }
        public int SortOrder { get; set; }
        public string ArticleId { get; set; }
        public int? ProductComboParentId { get; set; }


    }
}
