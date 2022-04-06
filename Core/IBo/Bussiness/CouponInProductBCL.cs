using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class CouponInProductBCL : Base<CouponInProduct>
    {
        public CouponInProductBCL()
        {

        }

        public bool Merge(List<CouponInProduct> entity, int id)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.CouponInProduct.RemoveRange(_context.CouponInProduct.Where(x => x.CouponId == id));
                    _context.CouponInProduct.AddRange(entity);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

    }
}
