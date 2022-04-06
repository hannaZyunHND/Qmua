using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class CouponChild
    {
        public CouponChild()
        {
            this.CreateBy = "admin";
            this.CreateDate = DateTime.Now;
            this.ExprireDate = DateTime.Today.AddDays(3);
            this.Ma = string.Empty;
            this.Name = string.Empty;
            this.NumberUseCode = 0;
            this.Parrent = 0;
            this.Status = 1;
        }
    }
}
