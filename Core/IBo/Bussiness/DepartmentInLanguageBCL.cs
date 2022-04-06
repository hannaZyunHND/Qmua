using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class DepartmentInLanguageBCL : Base<DepartmentInLanguage>
    {
        public DepartmentInLanguageBCL()

        {
        }

        public List<DepartmentInLanguage> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            total = 0;

            return new List<DepartmentInLanguage>();
        }
        public bool Merge(DepartmentInLanguage department)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<DepartmentInLanguage>(new List<DepartmentInLanguage> { department });
                    return true;

                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool Delete(int Id)
        {
            bool result = false;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.DepartmentInLanguage.Where(x => x.DepartmentId == Id);
                    db.Set<DepartmentInLanguage>().RemoveRange(data);
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
