using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ProductInPromotionBCL : Base<ProductInPromotion>
    {
        public ProductInPromotionBCL()
        {

        }
        public bool Add(int idProduct, List<ProductInPromotion> entitys)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.ProductInPromotion.RemoveRange(_context.ProductInPromotion.Where(x => x.ProductId == idProduct));
                    _context.ProductInPromotion.AddRange(entitys);
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
