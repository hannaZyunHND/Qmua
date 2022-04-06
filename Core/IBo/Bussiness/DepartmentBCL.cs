using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace MI.Bo.Bussiness
{
    public partial class DepartmentBCL : Base<Department>
    {
        public DepartmentBCL()
        {

        }

        public List<Department> Get(FilterDepartment fillter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var departments = _context.Department.AsQueryable();
                departments = departments.Include(x => x.DepartmentInLanguage);
                if (!String.IsNullOrEmpty(fillter.keyword))
                    departments = departments.Where(x => x.DepartmentInLanguage.Any(d => d.Name.ToLower().Contains(fillter.keyword.ToLower())));
                if (fillter.locationId > 0)
                    departments = departments.Where(x => x.LocationId == fillter.locationId);

                if (fillter.sortDir == "asc")
                    departments = departments.OrderBy(x => x.GetType().GetProperty(fillter.sortBy).GetValue(x, null));
                else
                    departments = departments.OrderByDescending(x => x.GetType().GetProperty(fillter.sortBy).GetValue(x, null));
                total = departments.Count();
                return departments.Skip((fillter.pageIndex - 1) * fillter.pageSize).Take(fillter.pageSize)
                    .Select(x => new { x.Id, LangCount = x.DepartmentInLanguage.Count, x.Latitude, x.LocationId, x.Longitude, x.SortOrder, x.Name }).ToList()
                    .Select(x => new Department
                    {
                        Id = x.Id,
                        LangCount = x.LangCount,
                        Latitude = x.Latitude,
                        LocationId = x.LocationId,
                        Longitude = x.Longitude,
                        SortOrder = x.SortOrder,
                        Name = x.Name
                    }).ToList();
            }
        }
        public int Create(Department department)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Department.FirstOrDefault(n => n.Id == department.Id);
                    if (kH == null)
                    {
                        db.Set<Department>().Add(department);
                        db.SaveChanges();
                        result = 1;
                    }
                    else
                    {
                        result = -1; // đã tồn tại
                    }
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
