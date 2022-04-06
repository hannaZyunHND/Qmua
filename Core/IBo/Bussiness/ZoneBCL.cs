using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using EFCore.BulkExtensions;
namespace MI.Bo.Bussiness
{
    public partial class ZoneBCL : Base<Zone>
    {
        public ZoneBCL()
        {

        }

        public List<Zone> Get(FilterZone filter, out int total)
        {
            total = 1;
            return new List<Zone>();
        }
        public List<Zone> GetById(List<int> lstId, string languageCode = "vn")
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.Zone.Where(x => lstId.Contains(x.Id) ).Select(x => new { x.Id, x.Name }).ToList().Select(x => new Zone { Id = x.Id, Name = x.Name }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<Zone>();
        }
        public Zone AddReturnObj(Zone entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Add(entity);
                    _context.SaveChanges();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);


            }
            return new Zone();
        }
        public bool UpdateTrangThai(Zone entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.Status).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);


            }
            return false;
        }

        public bool UpdateSort(Zone entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {


                    _context.Attach(entity);
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.SortOrder).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);


            }
            return false;
        }
        public bool UpdateShowLayout(Zone entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.Attach(entity);
                    //_context.Zone.ToList().ForEach(x => x.IsShowHome = false);
                    //entity.IsShowHome = true;
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.IsShowHome).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);


            }
            return false;
        }
        public bool UpdateShowSearch(Zone entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {

                    _context.Attach(entity);
                    
                    var entry = _context.Entry(entity);
                    entry.Property(p => p.IsShowSearch).IsModified = true;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);


            }
            return false;
        }
    }
}
