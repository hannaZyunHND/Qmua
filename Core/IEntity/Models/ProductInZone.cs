using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductInZone
    {
        public int ZoneId { get; set; }
        public int ProductId { get; set; }
        public bool IsPrimary { get; set; }
        public bool? IsHot { get; set; }
        public string BigThumb { get; set; }

        public Product Product { get; set; }
        public Zone Zone { get; set; }
    }
}
