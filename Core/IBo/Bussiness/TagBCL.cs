using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class TagBCL : Base<Tag>
    {
        IDbContext _context = new IDbContext();
        public TagBCL() { }


        public List<string> GetAllNameTags()
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.Tag.Select(x => new { x.Name }).ToList().Select(x => x.Name).ToList();
            }
        }
        public List<Tag> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            var result = _context.Tag.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                result = result.Where(r => r.Name.ToLower().Contains(keyword.ToLower()));
            if (sortDir == "asc")
            {
                result = result.OrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
            }
            else
            {
                result = result.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
            }
            total = result.Count();

            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<int> MergeTag(List<string> lstTag)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var data = _context.Tag.FromSql("EXEC dbo.Sp_MergeTag @Tag={0}", String.Join(",", lstTag));
                    return data.Select(x => new { x.Id }).ToList().Select(x => x.Id).ToList();
                }

            }
            catch (Exception ex)
            {

            }
            return new List<int>();
        }
    }
}
