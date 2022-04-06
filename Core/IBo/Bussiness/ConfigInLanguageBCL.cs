using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ConfigInLanguageBCL : Base<ConfigInLanguage>
    {
        public ConfigInLanguageBCL()
        {

        }
        public List<ConfigInLanguage> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            total = 0;

            return new List<ConfigInLanguage>();
        }
        public bool Delete(string configName)
        {
            bool result = false;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.ConfigInLanguage.Where(x => x.ConfigName == configName);
                    db.Set<ConfigInLanguage>().RemoveRange(data);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public bool Update(List<ConfigInLanguage> lst)
        {
            bool result = false;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate(lst);
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
