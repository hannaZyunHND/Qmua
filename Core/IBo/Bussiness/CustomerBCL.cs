using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class CustomerBCL : Base<Customer>
    {
        public CustomerBCL()
        {

        }

        public Dictionary<int, string> GetName(List<int> lstId)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.Customer.Where(x => lstId.Contains(x.Id)).Select(x => new { x.Id, x.Name }).ToList().ToDictionary(x => x.Id, x => x.Name);
                }
            }
            catch
            {

            }
            return new Dictionary<int, string>();
        }

        public bool Merge(Customer entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<Customer>(new List<Customer> { entity });
                    return true;
                }
            }
            catch
            {

            }
            return false;

        }
        public List<Customer> Find(Utils.FilterCustomer filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var customer = _context.Customer.AsQueryable();
                    if (!String.IsNullOrEmpty(filter.keyword))
                        customer = customer.Where(x => x.Id.ToString().Equals(filter.keyword) || x.Name.ToLower().ToString().Contains(filter.keyword.ToLower()));

                    if (filter.source != Entity.Enums.SourceCustomer.All)
                    {
                        customer = customer.Where(x => x.Source == (byte)filter.source);
                    }
                    if (filter.type != Entity.Enums.TypeCustomer.All)
                    {
                        customer = customer.Where(x => x.Type == (byte)filter.type);
                    }
                    //if (filter.sortDir == "asc")
                    //    customer = customer.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                    //else
                    //    customer = customer.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                    total = customer.Count();
                    return customer.OrderByDescending(x=>x.CreatedDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                }
            }
            catch
            {

            }
            return new List<Customer>();
        }
    }
}
