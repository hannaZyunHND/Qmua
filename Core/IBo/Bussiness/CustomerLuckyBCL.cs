using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MI.Dal.IDbContext;
using MI.Entity.Models;

namespace MI.Bo.Bussiness
{
   public class CustomerLuckyBCL: Base<CustomLucky>
    {
        public static List<CustomLucky> Find(string phone, int pageIndex,int pageSize, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var data = _context.CustomLucky.AsQueryable();

                    //if (!string.IsNullOrEmpty(phone))
                    //{
                    //    data = data.Where(x => x.PhoneNumber == phone);
                    //}

                    total = data.Count();
                    return data.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch
            {

            }
            return new List<CustomLucky>();
        }
    }
}
