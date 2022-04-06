using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductInPromotion
    {
        public int Id { get; set; }
        public int? PromotionId { get; set; }
        public int? ProductId { get; set; }
        public int? LocationId { get; set; }

        public Product Product { get; set; }
    }
}
