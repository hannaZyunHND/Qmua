using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool? IsDiscountPrice { get; set; }
        public decimal? Value { get; set; }
        public byte? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
