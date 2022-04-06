using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class CouponInProduct
    {
        public int ProductId { get; set; }
        public string CouponChildMa { get; set; }
        public double? Value { get; set; }
        public byte? Type { get; set; }
        public int CouponId { get; set; }
    }
}
