using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class BannerAdsBCL : Base<BannerAds>
    {
        public BannerAdsBCL()
        {

        }

        public List<KeyValuePair<string, string>> GetAllCode()
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.BannerAds.Where(x => x.Type != 3).Select(x => new { x.Code }).Distinct().ToList().Select(x => new KeyValuePair<string, string>(x.Code, x.Code)).ToList();
                }
            }
            catch (Exception)
            {

            }
            return new List<KeyValuePair<string, string>>();
        }



        public List<KeyValuePair<string, string>> GetGuarantee()
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.BannerAds.Where(x => x.Type == 3).GroupBy(x => x.Code).Select(d => new KeyValuePair<string, string>(d.Key, d.Key)).ToList();

            }
        }

        public List<KeyValuePair<string, int>> Find(string tuKhoa)
        {
            using (IDbContext _context = new IDbContext())
            {
                if (String.IsNullOrEmpty(tuKhoa))
                {
                    return _context.BannerAds.Where(x => x.Type == 3).GroupBy(x => x.Code).Select(d => new KeyValuePair<string, int>(d.Key, d.Count())).ToList();
                }
                else
                {

                }
                return _context.BannerAds.Where(x => x.Type == 3 && x.Code.ToLower().Contains(tuKhoa.ToLower())).GroupBy(x => x.Code).Select(d => new KeyValuePair<string, int>(d.Key, d.Count())).ToList();


            }
        }

        public KeyValuePair<bool, string> AddOrUpdate(BannerAds obj)
        {
            var res = new KeyValuePair<bool, string>(false, "Bản ghi đã tồn tại");
            try
            {

                using (IDbContext _context = new IDbContext())
                {
                    if (_context.BannerAds.Any(d => d.LanguageCode.Trim() == obj.LanguageCode.Trim() && d.Code.Trim().Equals(obj.Code.Trim()) && d.Id != obj.Id))
                    {
                        res = new KeyValuePair<bool, string>(false, "Bản ghi đã tồn tại");
                    }
                    else
                    {
                        _context.BulkInsertOrUpdate(new List<BannerAds> { obj });
                        res = new KeyValuePair<bool, string>(true, "Thành công");
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return res;
        }

        public KeyValuePair<bool, string> Remove(string key)
        {
            var res = new KeyValuePair<bool, string>(false, "Bản ghi đã tồn tại");
            try
            {

                using (IDbContext _context = new IDbContext())
                {
                    _context.BannerAds.RemoveRange(_context.BannerAds.Where(x => x.Code == key));
                    _context.SaveChanges();
                    res = new KeyValuePair<bool, string>(true, "Thành công");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return res;
        }
    }
}
