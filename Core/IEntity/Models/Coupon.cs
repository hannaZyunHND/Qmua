using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Coupon
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? DiscountOption { get; set; }
        public int? NumberOfUsed { get; set; }
        public int? QuantityDiscount { get; set; }
        public bool? Locked { get; set; }
        public string ValueDiscount { get; set; }
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
