using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class PropertyLanguageBCL : Base<PropertyLanguage>
    {
        public PropertyLanguageBCL()
        {

        }
        public List<PropertyLanguage> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            total = 0;

            return new List<PropertyLanguage>();
        }
        public bool Delete(int propertyId)
        {
            bool result = false;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.PropertyLanguage.Where(x => x.PropertyId == propertyId);
                    db.Set<PropertyLanguage>().RemoveRange(data);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public bool Add(List<PropertyLanguage> lst)
        {
            bool result = false;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    db.BulkInsertOrUpdate<PropertyLanguage>(lst);
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
