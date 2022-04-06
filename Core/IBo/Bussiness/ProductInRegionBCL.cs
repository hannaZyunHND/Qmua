using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ProductInRegionBCL : Base<ProductInRegion>
    {
        public ProductInRegionBCL()
        {

        }

        public bool Add(int zoneId, List<ProductInRegion> lstObj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    if (zoneId > 0)
                    {
                        _context.ProductInRegion.RemoveRange(_context.ProductInRegion.Where(d => d.ZoneId == zoneId));
                        _context.ProductInRegion.AddRange(lstObj);
                    }
                    //else
                    //{
                    //    _context.ProductInRegion.RemoveRange(_context.ProductInRegion.Where(d => d.Region.Trim().Equals(keyword) && d.ZoneId == zoneId));
                    //    _context.ProductInRegion.AddRange(lstObj);
                    //}
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
