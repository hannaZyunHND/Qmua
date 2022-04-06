using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class RedirectBCL : Base<Redirect>
    {
        public RedirectBCL()
        {

        }
        public List<Redirect> Find(Utils.FilterQuery filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var colors = _context.Redirect.AsQueryable();
                    //colors = colors.Where(x => x.LanguageCode.Trim().Equals(filter.languageCode));
                    if (!String.IsNullOrEmpty(filter.keyword))
                        colors = colors.Where(x => x.UrlOld.ToString().Contains(filter.keyword.ToLower()) || x.UrlNew.ToLower().ToString().Contains(filter.keyword.ToLower()));
                    total = colors.Count();
                    return colors.OrderBy(x => x.UrlNew).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                }
            }
            catch
            {

            }
            return new List<Redirect>();
        }

        public bool Merge(Redirect obj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<Redirect>(new List<Redirect> { obj });
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
