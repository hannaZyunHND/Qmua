using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class CouponInProduct
    {
        public CouponInProduct()
        {
            this.CouponChildMa = string.Empty;
            this.CouponId = 0;
            this.ProductId = 0;
            this.Type = 2;
            this.Value = 0;
        }
    }
}
