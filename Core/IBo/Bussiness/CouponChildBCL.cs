using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Utils;

namespace MI.Bo.Bussiness
{
    public class CouponChildBCL : Base<CouponChild>
    {
        public CouponChildBCL()
        {

        }
        public List<CouponChildOrder> GetCouponChildOrder(List<string> vouchers)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var result = (from x in _context.OrderDetail
                                  join y in _context.Orders on x.OrderId equals y.Id
                                  select new CouponChildOrder { Voucher = x.Voucher, LogPrice = x.LogPrice, Status = y.Status }).Where(o => o.Status == "THANH_CONG"
                                  && vouchers.Contains(o.Voucher)).ToList();
                    return result;
                }
            }
            catch (Exception ex) { }
            return new List<CouponChildOrder>();
        }
        public List<CouponChild> FindAll(FilterCouponChild filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<CouponChild> items = _context.CouponChild.AsQueryable();

                    if (!String.IsNullOrEmpty(filter.keyword))
                    {
                        filter.keyword = filter.keyword.ToLower();
                        items = items.Where(x => x.Name.ToLower().Contains(filter.keyword) || x.Ma.ToLower().Contains(filter.keyword));
                    }
                    if (filter.Parrent > 0)
                        items = items.Where(x => x.Parrent == filter.Parrent);
                    if (filter.Status > 0)
                        items = items.Where(x => x.Status == filter.Status);
                    if (filter.EndDateFrom != null && filter.EndDateTo != null)
                        items = items.Where(x => x.ExprireDate <= filter.EndDateTo && x.ExprireDate >= filter.EndDateFrom);
                    total = items.Count();
                    filter.pageIndex = filter.pageIndex == 0 ? 1 : filter.pageIndex;
                    return items.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<CouponChild>();
        }

        public bool Merge(CouponChild entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<CouponChild>(new List<CouponChild> { entity });
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public KeyValuePair<bool, string> UpdateStatus(CouponChild entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {

                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.Status).IsModified = true;
                    _context.SaveChanges();
                    return new KeyValuePair<bool, string>(true, "Thành công");
                }
            }
            catch (Exception ex)
            {

            }
            return new KeyValuePair<bool, string>(false, "Thất bại");
        }
        public bool Merge(List<CouponChild> entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<CouponChild>(entity);
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public Dictionary<string, string> GetByParrentId(int parrentId)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.CouponChild.Where(x => x.Parrent == parrentId).Select(x => new { x.Ma, x.Name }).ToDictionary(x => x.Ma, x => x.Name);
                }
            }
            catch (Exception ex)
            {

            }
            return new Dictionary<string, string>();
        }
    }
}
