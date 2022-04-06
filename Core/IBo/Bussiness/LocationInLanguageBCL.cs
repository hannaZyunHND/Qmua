using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class LocationInLanguageBCL:Base<LocationInLanguage>
    {
        public LocationInLanguageBCL()
        {
            
        }
        public List<LocationInLanguage> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            total = 0;

            return new List<LocationInLanguage>();
        }

        public bool Merge(LocationInLanguage entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<LocationInLanguage>(new List<LocationInLanguage> { entity });
                    //_context.BulkMerge(new List<ZoneInLanguage> { entity });

                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool Delete(int locationId)
        {
            bool result = false;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.LocationInLanguage.Where(x => x.LocationId == locationId);
                    db.Set<LocationInLanguage>().RemoveRange(data);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
