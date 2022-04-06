using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class ProductPriceInLocationBCL : Base<ProductPriceInLocation>
    {
        public ProductPriceInLocationBCL()
        {

        }

        public bool Add(int idProduct, List<ProductPriceInLocation> entitys)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.ProductPriceInLocation.RemoveRange(_context.ProductPriceInLocation.Where(x => x.ProductId == idProduct));
                    _context.ProductPriceInLocation.AddRange(entitys);
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
