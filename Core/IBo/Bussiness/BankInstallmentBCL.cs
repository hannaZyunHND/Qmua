using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class BankInstallmentBCL : Base<BankInstallment>
    {
        public BankInstallmentBCL()
        {

        }

        public List<BankInstallment> Find(string tuKhoa)
        {
            using (IDbContext _context = new IDbContext())
            {
                if (String.IsNullOrEmpty(tuKhoa))
                {
                    return _context.BankInstallment.ToList();
                }
                else
                {
                    return _context.BankInstallment.Where(x => x.Name.ToUpper().Contains(tuKhoa)).ToList();
                }

            }
        }

        public KeyValuePair<bool, string> AddOrUpdate(BankInstallment obj)
        {
            var res = new KeyValuePair<bool, string>(false, "Bản ghi đã tồn tại");
            try
            {

                using (IDbContext _context = new IDbContext())
                {
                    //if (_context.BankInstallment.Any(d=>d.Id != obj.Id))
                    //{
                    //    res = new KeyValuePair<bool, string>(false, "Không tồn tại bản ghi.");
                    //}
                    //else
                    //{
                    _context.BulkInsertOrUpdate(new List<BankInstallment> { obj });
                    res = new KeyValuePair<bool, string>(true, "Thành công");
                    //}

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return res;
        }

        public KeyValuePair<bool, string> Remove(int Id)
        {
            var res = new KeyValuePair<bool, string>(false, "Bản ghi đã tồn tại");
            try
            {

                using (IDbContext _context = new IDbContext())
                {
                    _context.BankInstallment.RemoveRange(_context.BankInstallment.Where(x => x.Id == Id).FirstOrDefault());
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
