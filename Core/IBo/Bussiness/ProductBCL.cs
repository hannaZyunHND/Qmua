using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class ProductBCL : Base<Product>
    {
        public ProductBCL()
        {

        }
        public List<Product> Get(int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var Product = _context.Product.AsQueryable();
                if (sortDir == "asc")
                {
                    Product = Product.OrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
                }
                else
                {
                    Product = Product.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
                }

                total = Product.Count();

                return Product.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public KeyValuePair<bool, string> UpdateVoucher(Dictionary<int, string> dicVoucher)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    foreach (var item in dicVoucher)
                    {
                        var entity = new Product { Id = item.Key, Voucher = item.Value };
                        var entry = _context.Entry(entity);
                        entry.Property(p => p.Voucher).IsModified = true;
                    }
                    _context.SaveChanges();
                    return new KeyValuePair<bool, string>(true, "Thành công");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "Thất bại");
            }


        }

        public Dictionary<int, string> GetVoucherById(List<int> lstId)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.Product.Where(x => x.Status != 3 && lstId.Contains(x.Id)).Select(x => new { x.Id, x.Voucher }).ToList().ToDictionary(d => d.Id, d => d.Voucher);
            }
        }

        public Dictionary<int, string> GetAllName()
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.Product.Where(x => x.Status != 3).Select(x => new { x.Id, x.Name }).ToList().ToDictionary(d => d.Id, d => d.Name);
            }
        }
        public void Test()
        {
            Console.WriteLine(1);
        }
        public List<Product> Search(string productName, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var Product = _context.Product.AsQueryable();

                if (sortDir == "asc")
                {
                    Product = Product.OrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
                }
                else
                {
                    Product = Product.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
                }

                total = Product.Count();

                return Product.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int CreateProduct(Product product)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Product.FirstOrDefault(n => n.Code.Trim() == product.Code.Trim());
                    if (kH == null)
                    {
                        db.Set<Product>().Add(product);

                        db.SaveChanges();
                        result = product.Id;
                    }
                    else
                        result = -1; // Sản phẩm đã tồn tại
                }

            }
            catch (Exception ex)
            {
                return -99;
            }
            return result;
        }

        public Dictionary<int, Product> GetByOrderDetail()
        {
            using (IDbContext _context = new IDbContext())
            {
                var query = _context.OrderDetail.
                    Join(_context.Product,
                    order => order.ProductId,
                    product => product.Id,
                    (od, pt) => new { Oder = od, Product = pt }).ToList();
                return new Dictionary<int, Product>();
            }

        }


        public int Add(Product product, List<ProductInZone> lstObj)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Product.FirstOrDefault(n => n.Code.Trim() == product.Code.Trim());
                    if (kH == null)
                    {
                        db.ProductInZone.RemoveRange(db.ProductInZone.Where(x => x.ProductId == product.Id));
                        db.ProductInZone.AddRange(lstObj);
                        db.Product.Add(product);
                        db.SaveChanges();
                        result = product.Id;
                    }
                    else
                        result = -1; // Sản phẩm đã tồn tại
                }

            }
            catch (Exception ex)
            {
                return -99;
            }
            return result;
        }
        public int Update(Product product, List<ProductInZone> lstObj)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Product.Any(n => n.Code.Trim() == product.Code.Trim() && n.Id != product.Id);
                    if (!kH)
                    {
                        db.ProductInZone.RemoveRange(db.ProductInZone.Where(x => x.ProductId == product.Id));
                        db.ProductInZone.AddRange(lstObj);
                        db.Product.Update(product);
                        db.SaveChanges();
                        result = product.Id;
                    }
                    else
                        result = -1; // Sản phẩm đã tồn tại
                }

            }
            catch (Exception ex)
            {
                return -99;
            }
            return result;
        }
        public bool UpdateTrangThai(Product entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.Status).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool UpdateSort(Product entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.SortOrder).IsModified = true;
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
