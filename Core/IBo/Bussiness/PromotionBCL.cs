

using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace MI.Bo.Bussiness
{
    public class PromotionBCL : Base<Promotion>
    {
        public PromotionBCL()
        {

        }
        public List<Promotion> GetName()
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {

                    return _context.Promotion.Where(x => x.Status != (byte)MI.Entity.Enums.StatusPromotion.Deleted).Select(x => new { x.Id, x.Name }).ToList().Select(x => new Promotion { Id = x.Id, Name = x.Name }).ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return new List<Promotion>();
        }
        public List<Promotion> Get(Utils.FilterPromotion filter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var promotion = _context.Promotion.AsQueryable();
                promotion = promotion.Include(x => x.PromotionInLanguage);




                if (!String.IsNullOrEmpty(filter.keyword))
                {
                    promotion = promotion.Where(x => x.PromotionInLanguage.Any(d => d.Name.ToLower().Contains(filter.keyword.ToLower())));
                }

                if (filter.status != Entity.Enums.StatusPromotion.All)
                {
                    promotion = promotion.Where(x => x.Status == (byte)filter.status);
                }
                else
                {
                    if (filter.status != Entity.Enums.StatusPromotion.Deleted)
                    {
                        promotion = promotion.Where(x => x.Status != (byte)Entity.Enums.StatusProduct.Deleted);
                    }
                }

                //if (filter.sortDir == "asc")
                //{
                //    promotion = promotion.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                //}
                //else
                //{
                //    promotion = promotion.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                //}

                total = promotion.Count();

                return promotion.OrderByDescending(d => d.Name).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).Select(x => new
                {
                    x.Id,
                    x.IsDiscountPrice,
                    x.Name,
                    LangCount = x.PromotionInLanguage.Count,
                    x.Type,
                    x.Value,
                    x.Status,
                    x.StartDate,
                    x.EndDate
                }).ToList().Select(x => new Promotion
                {
                    Id = x.Id,
                    IsDiscountPrice = x.IsDiscountPrice,
                    LangCount = x.LangCount,
                    Name = x.Name,
                    Type = x.Type,
                    Value = x.Value,
                    Status = x.Status,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();
            }
        }

        public KeyValuePair<bool, string> UpdateStatus(Promotion entity)
        {

            var result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(x => x.Status).IsModified = true;
                    _context.SaveChanges();
                    result = new KeyValuePair<bool, string>(true, "Thành công");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, ex.Message);
            }
            return result;
        }

        public List<Promotion> GetAll(Utils.FilterQuery filter, out int total)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var promotion = _context.Promotion.AsQueryable();
                    //promotion = promotion.Include(x => x.PromotionInLanguage);
                    //promotion = promotion.Where(x => x.PromotionInLanguage.Any(d => d.LanguageCode.Trim().Equals(filter.languageCode.Trim())) || x.PromotionInLanguage.Count <= 0);

                    if (!String.IsNullOrEmpty(filter.keyword))
                    {
                        promotion = promotion.Where(x =>
                            x.PromotionInLanguage.Any(d => d.Name.ToLower().Contains(filter.keyword.ToLower())));
                    }
                    promotion = promotion.Where(x => x.Status != 3);

                    total = promotion.Count();
                    var result = promotion.Select(x => new Promotion() { Id = x.Id, Name = x.Name });
                    result = result.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize);
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int CreateProduct(Promotion promotion)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Promotion.FirstOrDefault(n => n.Id == promotion.Id);
                    if (kH == null)
                    {
                        db.Set<Promotion>().Add(promotion);

                        db.SaveChanges();
                        result = promotion.Id;
                    }
                    else
                        result = -1; // đã tồn tại
                }

            }
            catch (Exception ex)
            {
                return -99;
            }
            return result;
        }

    }
}
