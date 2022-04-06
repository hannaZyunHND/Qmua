
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class ConfigBCL : Base<Config>
    {
        public ConfigBCL()
        {

        }

        public List<Config> Get(int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var Property = _context.Config.AsQueryable();
                if (sortDir == "asc")
                {
                    Property = Property.OrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
                }
                else
                {
                    Property = Property.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
                }
                var lstData = Property.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                //foreach (var item in lstData)
                //{
                //    string nameTemp = EnumHelper.GetDescription((GroupProp)item.GroupId);
                //    lstData.Add(new Property() {
                //        GroupName = nameTemp
                //    });
                //}

                total = lstData.Count();
                return lstData;
            }
        }
        public int CreateProduct(Config Property)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Config.FirstOrDefault(n => n.ConfigGroupKey == Property.ConfigGroupKey);
                    if (kH == null)
                    {
                        db.Set<Config>().Add(Property);
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
        public bool Delete(string configGroupKey)
        {
            bool result = false;
            try
            {
                using (IDbContext db = new IDbContext())
                {
                    var data = db.Config.FirstOrDefault(x => x.ConfigGroupKey == configGroupKey);
                    db.Set<Config>().Remove(data);
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
