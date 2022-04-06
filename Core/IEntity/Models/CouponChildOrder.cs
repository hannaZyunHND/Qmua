using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class CouponChildOrder
    {
        public string Voucher { get; set; }
        public decimal? LogPrice { get; set; }
        public string Status { get; set; }
    }
}
