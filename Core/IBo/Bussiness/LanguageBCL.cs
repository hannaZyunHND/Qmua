using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace MI.Bo.Bussiness
{
    public partial class LanguageBCL : Base<Language>
    {
        public LanguageBCL()
        {

        }
        public List<Language> Get(FilterQuery filter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var Language = _context.Language.AsQueryable();

                if (filter.sortDir == "asc")
                {
                    Language = Language.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                }
                else
                {
                    Language = Language.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                }
                total = Language.Count();
                return Language.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
            }
        }
    }
}
