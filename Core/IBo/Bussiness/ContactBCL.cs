using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ContactBCL : Base<Contact>
    {
        public ContactBCL()
        {

        }

        public bool UpdateStatus(Contact obj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(obj);
                    var entry = _context.Entry(obj);
                    entry.Property(x => x.Status).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool UpdateNote(Contact obj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(obj);
                    var entry = _context.Entry(obj);
                    entry.Property(x => x.ModifiedDate).IsModified = true;
                    entry.Property(x => x.ModifiedBy).IsModified = true;
                    entry.Property(x => x.Status).IsModified = true;
                    entry.Property(x => x.Note).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public List<Contact> Find(Utils.FilterContact filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<Contact> items = _context.Set<Contact>();

                    if (!String.IsNullOrEmpty(filter.keyword))
                        items = items.Where(x => x.Id.ToString() == filter.keyword || x.Title.Contains(filter.keyword) || x.Name.Contains(filter.keyword));

                    if (filter.status != Entity.Enums.StatusContact.All)
                    {
                        items = items.Where(x => x.Status == (byte)filter.status);
                    }
                    if (filter.type != Entity.Enums.TypeContact.All)
                    {
                        items = items.Where(x => x.Type == (byte)filter.type);
                    }
                    //if (filter.sortDir == "asc")
                    //{
                    //    items = items.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                    //}
                    //else
                    //{
                    //    items = items.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                    //}
                    total = items.Count();
                    return items.OrderByDescending(x => x.CreatedDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<Contact>();
        }
    }
}
