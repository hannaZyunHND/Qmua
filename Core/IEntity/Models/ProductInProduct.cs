using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductInProduct
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? ProductItemId { get; set; }
        public string Type { get; set; }
        public int? SortOrder { get; set; }
        public decimal ConfigPrice { get; set; }
        public string ConfigNote { get; set; }
    }
}
