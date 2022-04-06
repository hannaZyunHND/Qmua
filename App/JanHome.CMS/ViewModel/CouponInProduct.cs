using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.ViewModel
{
    public class CouponInProductVM
    {
        public List<CouponInProduct> lstObj { get; set; }
        public int id { get; set; }
        public CouponInProductVM()
        {
            this.id = 0;
            this.lstObj = new List<CouponInProduct>();
        }

    }
}
