using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ColorsBCL : Base<Colors>
    {
        public ColorsBCL()
        {

        }
        public List<Colors> Find(Utils.FilterQuery filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var colors = _context.Colors.AsQueryable();
                    colors = colors.Where(x => x.LanguageCode.Trim().Equals(filter.languageCode));
                    if (!String.IsNullOrEmpty(filter.keyword))
                        colors = colors.Where(x => x.Code.ToString().Equals(filter.keyword) || x.Name.ToLower().ToString().Contains(filter.keyword.ToLower()));

                    total = colors.Count();
                    return colors.OrderBy(x => x.CreateDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                }
            }
            catch
            {

            }
            return new List<Colors>();
        }

        public bool Merge(Colors obj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<Colors>(new List<Colors> { obj });
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
