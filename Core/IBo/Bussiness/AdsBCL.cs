using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Utils;

namespace MI.Bo.Bussiness
{
    public class AdsBCL : Base<Ads>
    {
        public AdsBCL()
        {

        }
        public List<Ads> Get(FilterQuery filter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var Ads = _context.Ads.AsQueryable();
                if (!String.IsNullOrEmpty(filter.keyword))
                    Ads = Ads.Where(x => x.Name.ToLower().Contains(filter.keyword.ToLower()) || x.Id.ToString().Contains(filter.keyword.ToLower()));
                if (filter.sortDir == "asc")
                    Ads = Ads.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                else
                    Ads = Ads.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                total = Ads.Count();
                return Ads.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
            }
        }
    }
}
