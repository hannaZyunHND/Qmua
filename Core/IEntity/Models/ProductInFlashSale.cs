using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductInFlashSale
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? FlashSaleId { get; set; }
        public decimal? ProductPriceInFlashSale { get; set; }
        public int? Quantity { get; set; }
    }
}
