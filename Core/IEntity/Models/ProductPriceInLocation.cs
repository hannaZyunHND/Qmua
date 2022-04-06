using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductPriceInLocation
    {
        public int Id { get; set; }
        public double? Price { get; set; }
        public double? SalePrice { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public double DiscountPercent { get; set; }
    }
}
