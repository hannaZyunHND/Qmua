using System;
using System.Collections.Generic;
using System.Linq;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using Utils;

namespace MI.Bo.Bussiness
{
    public class LuckySpinBCL: Base<LuckySpin>
    {
        public bool Add(LuckySpin obj)
        {
            Guid result;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    db.LuckySpin.Add(obj);
                    db.SaveChanges();
                    result = obj.Id;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return result!=null;
        }
        public bool Update(LuckySpin obj)
        {
            Guid result ;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    db.LuckySpin.Update(obj);
                    db.SaveChanges();
                    result = obj.Id;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return result != null;
        }
        public bool Delete(Guid id)
        {
            Guid result ;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var obj = db.LuckySpin.FirstOrDefault(it => it.Id == id);
                    db.LuckySpin.Remove(obj);
                    db.SaveChanges();
                    result = obj.Id;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            return result != null;
        }
        public LuckySpin Get(Guid id)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.LuckySpin.FirstOrDefault(it=>it.Id==id);
            }
        }

    }
}
