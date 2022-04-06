using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class CouponChild
    {
        public string Ma { get; set; }
        public int? Parrent { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public byte? Status { get; set; }
        public DateTime? ExprireDate { get; set; }
        public int? NumberUseCode { get; set; }
        public string Email { get; set; }
    }
}
