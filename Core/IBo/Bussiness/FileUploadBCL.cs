using MI.Dal.IDbContext;
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MI.Bo.Bussiness
{
    public partial class FileUploadBCL : Base<FileUpload>
    {
        public FileUploadBCL()
        {

        }

        public List<FileUpload> Find(string keyword, int skip, int take, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var colors = _context.FileUpload.AsQueryable();

                    if (!String.IsNullOrEmpty(keyword))
                        colors = colors.Where(x => x.Name.ToString().Equals(keyword) || x.Name.ToLower().ToString().Contains(keyword));


                    total = colors.Count();
                    return colors.OrderByDescending(x => x.UploadedDate).Skip(skip).Take(take).ToList();
                }
            }
            catch
            {

            }
            return new List<FileUpload>();
        }

        public bool ExistFile(string fileName)
        {

            try
            {
                using (IDbContext _context = new IDbContext())
                {
                   return  _context.FileUpload.Any(x => x.Name.Equals(fileName));
                }
            }
            catch
            {

            }
            return false;
        }
    }
}
