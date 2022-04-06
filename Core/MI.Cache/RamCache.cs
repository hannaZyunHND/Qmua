using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Cache
{
    public class RamCache
    {
        private const int timeCache = 2;

        #region  Coupon

        public static Dictionary<int, MI.Entity.Models.Coupon> _DicCoupon { get; set; }
        public static DateTime LastestLoadCoupon = new DateTime(2000, 01, 01);

        public static Dictionary<int, MI.Entity.Models.Coupon> DicCoupon
        {
            get
            {
                if (_DicCoupon == null || _DicCoupon.Count <= 0 || LastestLoadCoupon.AddDays(timeCache) < DateTime.Now)
                {
                    _DicCoupon = new MI.Bo.Bussiness.CouponBCL().FindAll().ToDictionary(p => p.Id, p => p);
                    if (_DicCoupon == null)
                    {
                        _DicCoupon = new Dictionary<int, Entity.Models.Coupon>();
                    }
                    LastestLoadCoupon = DateTime.Now;
                }

                return _DicCoupon;
            }
        }

        #endregion Coupon

        #region Zone

        public static Dictionary<int, MI.Entity.Models.Zone> _DicZone { get; set; }
        public static DateTime LastestLoadZone = new DateTime(2000, 01, 01);

        public static Dictionary<int, MI.Entity.Models.Zone> DicZone
        {
            get
            {
                if (_DicZone == null || _DicZone.Count <= 0 || LastestLoadZone.AddDays(timeCache) < DateTime.Now)
                {
                    _DicZone = new MI.Bo.Bussiness.ZoneBCL().FindAll(x => x.Status != 3).ToDictionary(p => p.Id, p => p);
                    if (_DicZone == null)
                    {
                        _DicZone = new Dictionary<int, Entity.Models.Zone>();
                    }
                    LastestLoadZone = DateTime.Now;
                }

                return _DicZone;
            }
        }

        #endregion Zone
        #region Product

        public static Dictionary<int, string> _DicProduct { get; set; }
        public static DateTime LastestLoadProduct = new DateTime(2000, 01, 01);

        public static Dictionary<int, string> DicProduct
        {
            get
            {
                if (_DicProduct == null || _DicProduct.Count <= 0 || LastestLoadProduct.AddDays(timeCache) < DateTime.Now)
                {
                    _DicProduct = new MI.Bo.Bussiness.ProductBCL().GetAllName();
                    if (_DicProduct == null)
                    {
                        _DicProduct = new Dictionary<int, string>();
                    }
                    LastestLoadProduct = DateTime.Now;
                }

                return _DicProduct;
            }
        }

        #endregion Product
        #region Product

        public static Dictionary<int, string> _DicArticle { get; set; }
        public static DateTime LastestLoadArticle = new DateTime(2000, 01, 01);

        public static Dictionary<int, string> DicArticle
        {
            get
            {
                if (_DicArticle == null || _DicArticle.Count <= 0 || LastestLoadArticle.AddDays(timeCache) < DateTime.Now)
                {
                    _DicArticle = new MI.Bo.Bussiness.ArticleBCL().GetAllName();
                    if (_DicArticle == null)
                    {
                        _DicArticle = new Dictionary<int, string>();
                    }
                    LastestLoadArticle = DateTime.Now;
                }

                return _DicArticle;
            }
        }

        #endregion Product
    }
}
