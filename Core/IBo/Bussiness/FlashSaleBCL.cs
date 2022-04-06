using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class FlashSaleBCL : Base<FlashSale>
    {
        public FlashSaleBCL()
        {

        }
        public List<FlashSale> Get(Utils.FilterQuery fillter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var flashSale = _context.FlashSale.AsQueryable();

                if (!String.IsNullOrEmpty(fillter.keyword))
                    flashSale = flashSale.Where(x => x.Name.ToLower().Contains(fillter.keyword));

                total = flashSale.Count();
                return flashSale.Skip((fillter.pageIndex - 1) * fillter.pageSize).Take(fillter.pageSize)
                    .Select(x => new { x.Id, x.Name, x.StartTime, x.EndTime, x.Status }).ToList()
                    .Select(x => new FlashSale
                    {
                        Id = x.Id,
                        Name = x.Name,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        Status = x.Status
                    }).ToList();
            }
        }


        public List<object> GetProduct(int Id)
        {
            using (IDbContext _context = new IDbContext())
            {
                var lstObj = _context.ProductInFlashSale.Join(_context.Product,
                    x => x.ProductId,
                    x => x.Id, (pf, p) =>
                   new
                   {
                       Fls = pf,
                       pro = p
                   }).Where(x => x.Fls.FlashSaleId == Id).Select(x => new
                   {
                       x.Fls.FlashSaleId,
                       x.Fls.ProductId,
                       x.Fls.ProductPriceInFlashSale,
                       x.Fls.Quantity,
                       x.pro.Name,
                       x.pro.Avatar,
                       x.pro.Code
                   }).ToList<object>();

                return lstObj;
            }
        }
        public KeyValuePair<bool, string> UpdateStatus(FlashSale entity)
        {
            KeyValuePair<bool, string> respone = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.Status).IsModified = true;
                    _context.SaveChanges();
                    respone = new KeyValuePair<bool, string>(true, "Thành công");
                }
            }
            catch (Exception ex)
            {

            }
            return respone;
        }
        public KeyValuePair<bool, string> Merge(FlashSale flashSale, List<ProductInFlashSale> productInFS)
        {
            KeyValuePair<bool, string> respone = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    if (flashSale.Id > 0)
                    {
                        flashSale.ModifiedDate = DateTime.Now;
                        _context.FlashSale.Update(flashSale);
                        _context.RemoveRange(_context.ProductInFlashSale.Where(x => x.FlashSaleId == flashSale.Id));
                        _context.ProductInFlashSale.AddRange(productInFS);
                        _context.SaveChanges();
                        respone = new KeyValuePair<bool, string>(true, "Thành công");
                    }
                    else
                    {
                        flashSale.Status = 1;
                        flashSale.CreatedDate = DateTime.Now;
                        flashSale.ModifiedDate = DateTime.Now;
                        flashSale.CreatedBy = flashSale.ModifiedBy;
                        _context.FlashSale.Add(flashSale);
                        foreach (var i in productInFS)
                            i.FlashSaleId = flashSale.Id;
                        _context.ProductInFlashSale.AddRange(productInFS);
                        _context.SaveChanges();
                        respone = new KeyValuePair<bool, string>(true, "Thành công");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return respone;
        }
    }
}
