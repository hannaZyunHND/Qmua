using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ProductSpecificationsBCL : Base<MaintainSpectificatinInProduct>
    {
        public ProductSpecificationsBCL()
        {

        }

        public bool Add(int idProduct, List<MaintainSpectificatinInProduct> entitys)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.MaintainSpectificatinInProduct.RemoveRange(_context.MaintainSpectificatinInProduct.Where(x => x.ProductId == idProduct ));
                    _context.MaintainSpectificatinInProduct.AddRange(entitys);
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
