using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace MI.Bo.Bussiness
{
    public class RatingBCL : Base<Rating>
    {
        public RatingBCL()
        {

        }

        public List<Department> Get(FilterDepartment fillter, out int total)
        {
            //using (IDbContext _context = new IDbContext())
            //{
            //    var Department = _context.Department.AsQueryable();
            //    if (!String.IsNullOrEmpty(fillter.keyword))
            //        //Department = Department.Where(x => x.Name.ToLower().Equals(fillter.keyword.ToLower()) || x.Id.ToString().Equals(fillter.keyword.ToLower()));
            //        if (fillter.locationId > 0)
            //            Department = Department.Where(x => x.LocationId == fillter.locationId);
            //    if (!String.IsNullOrEmpty(fillter.languageCode))
            //        //Department = Department.Where(x => x.LanguageCode == fillter.languageCode);
            //        if (fillter.sortDir == "asc")
            //            Department = Department.OrderBy(x => x.GetType().GetProperty(fillter.sortBy).GetValue(x, null));
            //        else
            //            Department = Department.OrderByDescending(x => x.GetType().GetProperty(fillter.sortBy).GetValue(x, null));
            //    total = Department.Count();
            //    return Department.Skip((fillter.pageIndex - 1) * fillter.pageSize).Take(fillter.pageSize).ToList();
            //}
            total = 0;
            return new List<Department>();
        }
    }
}
