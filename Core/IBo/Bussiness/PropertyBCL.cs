using MI.Dal.IDbContext;
using MI.Entity;
using MI.Entity.Enums;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Bo.Bussiness
{
    public class PropertyBCL : Base<Property>
    {
        public PropertyBCL()
        {

        }
        public List<Property> Get(Utils.FilterQuery filter, out int total)
        {
            using (IDbContext _context = new IDbContext())
            {
                var property = _context.Property.AsQueryable();
                property = property.Include(x => x.PropertyLanguage);

                //property = property.Where(x => x.PropertyLanguage.Any(d => d.LanguageCode.Trim().Equals(filter.languageCode.Trim())) || x.PropertyLanguage.Count <= 0);

                if (!String.IsNullOrEmpty(filter.keyword))
                {
                    property = property.Where(x => x.PropertyLanguage.Any(d => d.Name.ToLower().Contains(filter.keyword.ToLower())));
                }

                if (filter.sortDir == "asc")
                {
                    property = property.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                }
                else
                {
                    property = property.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                }
                var lstData = property.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize)
                    .Select(x => new { x.Id, x.GroupId, x.Thumb, x.Position, LangCount = x.PropertyLanguage.Count, x.Name })
                    .Select(x => new Property
                    {
                        GroupId = x.GroupId,
                        Id = x.Id,
                        LangCount = x.LangCount,
                        Name = x.Name,
                        Position = x.Position,
                        Thumb = x.Thumb
                    })
                    .ToList();
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
        public int CreateProduct(Property Property)
        {
            int result = 0;
            try
            {
                using (IDbContext db = new IDbContext())
                {

                    var kH = db.Property.FirstOrDefault(n => n.Id == Property.Id);
                    if (kH == null)
                    {
                        if (Property.Id == 0)
                        {
                            try
                            {
                                Property.Id = db.Property.Max(x => x.Id);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        db.Set<Property>().Add(Property);
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
    }
}
