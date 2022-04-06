using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace MI.Bo.Bussiness
{
    public partial class LocationBCL : Base<Location>
    {
        public LocationBCL()
        {

        }
        public List<Location> GetAll()
        {
            using (IDbContext _context = new IDbContext())
            {
                var location = _context.Location.AsQueryable();
                location = location.Include(x => x.LocationInLanguage);
                //location = location.Where(x => x.LocationInLanguage.Any(d => d.LanguageCode.Trim().Equals(filter.languageCode.Trim())) || x.LocationInLanguage.Count <= 0);
                return location.ToList();
            }
        }
        public List<Location> Get(Utils.FilterQuery filter, out int total)
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
                total = location.Count();
                return location.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize)
                .Select(x => new { x.Code, x.Id,  x.Name, x.Note, x.ParentId, LangCount = x.LocationInLanguage.Count }).ToList()
                .Select(x => new Location { LangCount = x.LangCount, Id = x.Id, Code = x.Code, Name = x.Name })
               .ToList();
            }
        }
        public int Create(Location Property)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Location.FirstOrDefault(n => n.Id == Property.Id);
                    if (kH == null)
                    {
                        db.Set<Location>().Add(Property);
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
