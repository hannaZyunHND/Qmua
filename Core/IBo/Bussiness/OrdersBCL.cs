using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class OrdersBCL : Base<Orders>
    {
        public OrdersBCL()
        {

        }

        public List<Orders> Get(Utils.FilterOrders filter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var orders = _context.Orders.AsQueryable();
                orders = orders.Include(x => x.OrderDetail);
                orders = orders.Include(x => x.Customer);

                if (!String.IsNullOrEmpty(filter.keyword))
                {
                    orders = orders.Where(x => x.OrderCode == filter.keyword || x.Id.ToString() == filter.keyword);
                }
                if (!String.IsNullOrEmpty(filter.orderStatus))
                {
                    orders = orders.Where(x => x.Status == filter.orderStatus);
                }

                if (filter.startDate != null)
                {
                    orders = orders.Where(x => x.CreatedDate >= filter.startDate);
                }
                if (filter.endDate != null)
                {
                    orders = orders.Where(x => x.CreatedDate <= filter.endDate);
                }

                if (filter.orderType != Entity.Enums.OrderType.All)
                {
                    orders = orders.Where(x => x.OrderSourceType == (byte)filter.orderType);
                }
                ////location = location.Where(x => x.LocationInLanguage.Any(d => d.LanguageCode.Trim().Equals(filter.languageCode.Trim())) || x.LocationInLanguage.Count <= 0);

                //if (!String.IsNullOrEmpty(filter.keyword))
                //{
                //    location = location.Where(x => x.OrderDetail.Any(d => d.Name.ToLower().Contains(filter.keyword.ToLower())));
                //}
                //if (filter.sortDir == "asc")
                //{
                //    orders = orders.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                //}
                //else
                //{
                //    orders = orders.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                //}
                total = orders.Count();
                var lstData = orders.OrderByDescending(x => x.CreatedDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                return lstData;
            }
        }

        public List<Location> GetById(Utils.FilterQuery filter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var location = _context.Location.AsQueryable();
                location = location.Include(x => x.LocationInLanguage);
                //location = location.Where(x => x.LocationInLanguage.Any(d => d.LanguageCode.Trim().Equals(filter.languageCode.Trim())) || x.LocationInLanguage.Count <= 0);

                if (!String.IsNullOrEmpty(filter.keyword))
                {
                    location = location.Where(x => x.LocationInLanguage.Any(d => d.Name.ToLower().Contains(filter.keyword.ToLower())));
                }
                if (filter.sortDir == "asc")
                {
                    location = location.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                }
                else
                {
                    location = location.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                }

                var lstData = location.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                total = lstData.Count();
                return lstData;
            }
        }
        //public Dictionary<int,Product> GetListProduct(int orderId)
        //{
        //    Dictionary<int, Product> lstProduct = new Dictionary<int, Product>()
        //    try
        //    {
        //        using (IDbContext db = new IDbContext())
        //        {
        //            var data = db.OrderDetail.Where(n => n.OrderId == orderId).Join(db.Product, orderDetail => orderDetail.ProductId, product => product.Id, (od, pt) => new { OrderList = od, ProductList = pt }).ToList();

        //            lstProduct = data.ToDictionary(x=>x.OrderList.Id,x=>x.)
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return lstProduct;
        //}

        public List<OrderPromotionDetail> GetListOrderPromotionDetail(int orderId)
        {
            List<OrderPromotionDetail> lstData = new List<OrderPromotionDetail>();
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.OrderDetail.Where(n => n.OrderId == orderId).Join(db.OrderPromotionDetail, orderDetail => orderDetail.Id, orderPromotionDetail => orderPromotionDetail.OrderDetailId, (od, odpromotion) => new { OrderList = od, OrderPromotion = odpromotion }).ToList();
                    foreach (var item in data)
                    {
                        item.OrderPromotion.OrderDetail = null;
                        lstData.Add(item.OrderPromotion);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return lstData;
        }

        private static Dictionary<OrderDetail, OrderPromotionDetail> DicOrderPromotionDetail()
        {
            Dictionary<OrderDetail, OrderPromotionDetail> keyValuePairs = new Dictionary<OrderDetail, OrderPromotionDetail>();
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.OrderDetail.Join(db.OrderPromotionDetail, orderDetail => orderDetail.Id, orderPromotionDetail => orderPromotionDetail.OrderDetailId, (od, odDetail) => new { OrderList = od, OrderDetailPromotion = odDetail }).ToList();
                    foreach (var item in data)
                    {
                        keyValuePairs.Add(item.OrderList, item.OrderDetailPromotion);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return keyValuePairs;
        }
        private static Dictionary<OrderDetail, Product> DicProduct()
        {
            Dictionary<OrderDetail, Product> dicData = new Dictionary<OrderDetail, Product>();
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.OrderDetail.Join(db.Product, orderDetail => orderDetail.ProductId, product => product.Id, (od, pt) => new { OrderList = od, ProductList = pt }).ToList();
                    foreach (var item in data)
                    {
                        dicData.Add(item.OrderList, item.ProductList);
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return dicData;
        }
        public bool UpdateTrangThai(Orders entity)
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
    }

}
