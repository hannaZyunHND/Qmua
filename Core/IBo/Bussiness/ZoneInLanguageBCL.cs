using MI.Dal.IDbContext;
using MI.Entity.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class ZoneInLanguageBCL : Base<ZoneInLanguage>
    {
        public ZoneInLanguageBCL()
        {

        }
        public List<Zone> FindAll(string tuKhoa, MI.Entity.Enums.StatusZone trangThai, MI.Entity.Enums.TypeZone loai, string languageCode = "vi-VN")
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<Zone> items = _context.Zone.AsQueryable();

                    if (!String.IsNullOrEmpty(tuKhoa))
                    {
                        items = items.Where(x => x.ZoneInLanguage.Any(d => d.Name.ToLower().Contains(tuKhoa.ToLower())) || x.Id.ToString() == tuKhoa);
                    }
                    if (trangThai != Entity.Enums.StatusZone.All)
                    {
                        items = items.Where(x => x.Status == (byte)trangThai);
                    }
                    else
                    {
                        if (trangThai != Entity.Enums.StatusZone.Delete)
                        {
                            items = items.Where(x => x.Status != (byte)Entity.Enums.StatusZone.Delete);
                        }
                    }
                    if (loai != Entity.Enums.TypeZone.All)
                    {
                        items = items.Where(x => x.Type == (byte)loai);
                    }
                    items = items.Include(x => x.ZoneInLanguage);
                    return items.Select(x => new { x.Id, x.ZoneInLanguage, x.IsShowHome, x.IsShowSearch,x.IsShowMenu, x.ParentId, x.SortOrder, x.Name }).OrderBy(x => x.SortOrder).ToList().Select(d => new Zone { Id = d.Id, IsShowSearch = d.IsShowSearch,IsShowMenu=d.IsShowMenu ,Name = d.Name, IsShowHome = d.IsShowHome, ZoneInLanguage = d.ZoneInLanguage, ParentId = d.ParentId, SortOrder = d.SortOrder }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<Zone>();
        }
        public Dictionary<int, string> GetURLById(List<int> lstId)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.ZoneInLanguage.Where(x => lstId.Contains(x.ZoneId) && x.LanguageCode.Trim() == Utils.Utility.DefaultLang).Select(x => new { x.ZoneId, x.Url }).ToList().ToDictionary(d => d.ZoneId, d => d.Url);
            }
        }
        public List<ZoneInLanguage> GetById(List<int> lstId, string languageCode = "vn")
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.ZoneInLanguage.Where(x => lstId.Contains(x.ZoneId) && x.LanguageCode.Trim().Equals(languageCode)).Select(x => new { x.ZoneId, x.Name }).ToList().Select(x => new ZoneInLanguage { ZoneId = x.ZoneId, Name = x.Name }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<ZoneInLanguage>();
        }
        public Dictionary<int, string> GetByName(Expression<Func<Zone, bool>> predicate)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.Zone
                .Where(predicate).Select(x => new { x.Id, x.Name }).ToDictionary(x => x.Id, x => x.Name);
            }
        }

        public List<ZoneInLanguage> FindAll(Expression<Func<ZoneInLanguage, bool>> predicate)
        {
            using (IDbContext _context = new IDbContext())
            {
                IQueryable<ZoneInLanguage> items = _context.ZoneInLanguage.AsQueryable();
                items = items.Include(x => x.Zone);

                items = items.Where(predicate);
                items = items.Where(x => x.Zone.Status != (byte)MI.Entity.Enums.StatusZone.Delete);

                return items.Select(x => new { x.ZoneId, x.Name, x.Zone.ParentId, x.Zone.Type }).ToList().Select(x => new ZoneInLanguage { ZoneId = x.ZoneId, Name = x.Name, Zone = new Zone { ParentId = x.ParentId, Type = x.Type } }).ToList();
            }
        }
        public bool ExistUrl(string url, int id)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.ZoneInLanguage.Any(x => x.Url == url && x.ZoneId != id);
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool Merge(ZoneInLanguage entity)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<ZoneInLanguage>(new List<ZoneInLanguage> { entity });
                    //_context.BulkMerge(new List<ZoneInLanguage> { entity });

                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
