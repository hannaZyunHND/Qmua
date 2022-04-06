using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Utils;

namespace MI.Bo.Bussiness
{
    public class ProductInLanguageBCL : Base<ProductInLanguage>
    {
        IDbContext _context = new IDbContext();
        public ProductInLanguageBCL()
        {

        }
        public List<ProductInLanguage> Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir, out int total)
        {
            //var result = _context.ProductInLanguage.AsQueryable();

            //if (!string.IsNullOrEmpty(keyword))
            //    result = result.Where(r => r.Title.ToLower().Contains(keyword.ToLower()));
            //if (sortDir == "asc")
            //{
            //    result = result.OrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
            //}
            //else
            //{
            //    result = result.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x, null));
            //}
            total = 0;

            return new List<ProductInLanguage>();
        }
        public List<Product> FindAll(FilterProduct filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<Product> items = _context.Product.AsQueryable();
                    items = items.Include(x => x.ProductInLanguage);

                    //items = items.Where(x => x.LanguageCode.Trim().Equals(filter.languageCode.Trim()));
                    if (!String.IsNullOrEmpty(filter.keyword))
                    {
                        filter.keyword = filter.keyword.ToLower();
                        items = items.Where(x => x.Name.ToLower().Contains(filter.keyword) || x.ProductInLanguage.Any(d => d.Title.Contains(filter.keyword)) || x.Id.ToString() == filter.keyword || x.Code.ToLower() == filter.keyword.ToLower());
                    }
                    if (!String.IsNullOrEmpty(filter.voucher))
                        items = items.Where(x => x.Voucher == filter.voucher || x.Voucher.StartsWith(filter.voucher + ",") || x.Voucher.EndsWith("," + filter.voucher) || x.Voucher.Contains("," + filter.voucher + ","));
                    if (filter.isInstallment)
                        items = items.Where(x => x.IsInstallment == filter.isInstallment);

                    if (filter.trangThai != Entity.Enums.StatusProduct.All)
                        items = items.Where(x => x.Status == (byte)filter.trangThai);
                    else
                    {
                        if (filter.trangThai != Entity.Enums.StatusProduct.Deleted)
                        {
                            items = items.Where(x => x.Status != (byte)Entity.Enums.StatusProduct.Deleted && x.Status != (byte)Entity.Enums.StatusProduct.Combo);
                        }
                    }

                    if (filter.idZones.Count > 0)
                    {
                        items = items.Include(x => x.ProductInZone);
                        items = items.Where(x => x.ProductInZone.Any(d => filter.idZones.Contains(d.ZoneId)));
                    }
                    else if (filter.idZone > 0)
                    {
                        items = items.Include(x => x.ProductInZone);
                        items = items.Where(x => x.ProductInZone.Any(d => d.ZoneId == filter.idZone));
                    }

                    if (filter.idPromotion > 0)
                    {
                        items = items.Include(x => x.ProductInPromotion);
                        items = items.Where(x => x.ProductInPromotion.Any(d => d.PromotionId == filter.idPromotion));
                    }
                    filter.sortBy = String.IsNullOrEmpty(filter.sortBy) ? "Id" : filter.sortBy;

                    if (filter.sortDir == "asc")
                        items = items.OrderBy(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));
                    else
                        items = items.OrderByDescending(x => x.GetType().GetProperty(filter.sortBy).GetValue(x, null));

                    total = items.Count();
                    filter.pageIndex = filter.pageIndex < 1 ? 1 : filter.pageIndex;

                    return items.Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).Select(x => new
                    {
                        x.Id,
                        x.Code,
                        x.Name,
                        x.Price,
                        LangCount = x.ProductInLanguage.Count,
                        x.Avatar,
                        x.ProductInZone,
                        x.Status,
                        x.ViewCount,
                        x.SortOrder,
                        ListUrl = x.ProductInLanguage != null ? x.ProductInLanguage.Select(d => new KeyValuePair<string, string>(d.LanguageCode.Trim(), d.Url)) : new List<KeyValuePair<string, string>>()
                    }).ToList().Select(d => new Product
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Name = d.Name,
                        LangCount = d.LangCount,
                        Avatar = d.Avatar,
                        ProductInZone = d.ProductInZone,
                        Price = d.Price,
                        Status = d.Status,
                        ViewCount = d.ViewCount ?? 0,
                        ListUrl = d.ListUrl,
                        SortOrder = d.SortOrder
                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<Product>();
        }

        public List<ProductInZone> GetByZone(FilterProduct filter, out int total)
        {
            total = 0;
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<Product> items = _context.Product.Where(x => x.Status != (byte)Entity.Enums.StatusProduct.Deleted).AsQueryable();
                    IQueryable<ProductInZone> items2 = _context.ProductInZone.Where(x => filter.idZone == x.ZoneId);
                    //items = items.Where(x => x.LanguageCode.Trim().Equals(filter.languageCode.Trim()));
                    if (!String.IsNullOrEmpty(filter.keyword))
                    {
                        items = items.Where(x => x.ProductInLanguage.Any(d => d.Title.Contains(filter.keyword)) || x.Id.ToString() == filter.keyword || x.Code.ToLower() == filter.keyword.ToLower());
                    }

                    total = items.Join(items2, x => x.Id, y => y.ProductId, (query1, query2) => new { product = query1, zone = query2 }).Count();
                    filter.pageIndex = filter.pageIndex < 1 ? 1 : filter.pageIndex;
                    return items.Join(items2, x => x.Id, y => y.ProductId, (query1, query2) => new { product = query1, zone = query2 }).OrderByDescending(x => x.product.CreatedDate).Skip((filter.pageIndex - 1) * filter.pageSize).Take(filter.pageSize).Select(x => new
                    {
                        x.zone.ProductId,
                        x.zone.IsHot,
                        x.zone.BigThumb,
                        x.zone.IsPrimary,
                        x.zone.ZoneId,
                        x.product
                    }).ToList().Select(d => new ProductInZone
                    {
                        ProductId = d.ProductId,
                        BigThumb = d.BigThumb,
                        IsHot = d.IsHot,
                        IsPrimary = d.IsPrimary,
                        ZoneId = d.ZoneId,
                        Product = d.product
                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<ProductInZone>();
        }

        public Dictionary<int, KeyValuePair<string, string>> GetInfo(Expression<Func<Product, bool>> predicate)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.Product.Where(predicate).Select(x => new { x.Id, x.Name, x.Avatar }).ToList().ToDictionary(x => x.Id, x => new KeyValuePair<string, string>(x.Name, x.Avatar));
            }
        }
        public Dictionary<int, string> GetURLById(List<int> lstId)
        {
            using (IDbContext _context = new IDbContext())
            {
                return _context.ProductInLanguage.Where(x => lstId.Contains(x.ProductId) && x.LanguageCode.Trim() == Utils.Utility.DefaultLang).Select(x => new { x.ProductId, x.Url }).ToList().ToDictionary(d => d.ProductId, d => d.Url);
            }
        }
        public bool ExistUrl(string url, int id)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    return _context.ProductInLanguage.Any(x => x.Url == url && x.ProductId != id);
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool Add(ProductInLanguage obj, List<int> lstTag)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    _context.BulkInsertOrUpdate<TagInProductLanguage>(lstTag.Select(x => new TagInProductLanguage { TagId = x, ProductInLanguageId = obj.Id, CreatedDate = DateTime.Now, TagMode = 0 }).ToList());
                    _context.BulkInsertOrUpdate<ProductInLanguage>(new List<ProductInLanguage> { obj });
                    //_context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool Update(ProductInLanguage obj, List<int> lstTag)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {

                    _context.TagInProductLanguage.RemoveRange(_context.TagInProductLanguage.Where(x => x.ProductInLanguageId == obj.Id));
                    _context.TagInProductLanguage.AddRange(lstTag.Select(x => new TagInProductLanguage { TagId = x, ProductInLanguageId = obj.Id, CreatedDate = DateTime.Now, TagMode = 0 }));

                    var entry = _context.Entry(obj);
                    entry.Property(x => x.Catalog).IsModified = true;
                    entry.Property(x => x.Content).IsModified = true;
                    entry.Property(x => x.Description).IsModified = true;
                    entry.Property(x => x.MetaDescription).IsModified = true;
                    entry.Property(x => x.MetaKeyword).IsModified = true;
                    entry.Property(x => x.MetaTitle).IsModified = true;
                    entry.Property(x => x.MetaNoIndex).IsModified = true;
                    entry.Property(x => x.MetaCanonical).IsModified = true;
                    entry.Property(x => x.SocialDescription).IsModified = true;
                    entry.Property(x => x.SocialImage).IsModified = true;
                    entry.Property(x => x.Title).IsModified = true;
                    entry.Property(x => x.SocialTitle).IsModified = true;
                    entry.Property(x => x.Url).IsModified = true;
                    entry.Property(x => x.MetaWebPage).IsModified = true;
                    entry.Property(x => x.UrlOld).IsModified = true;
                    //_context.ProductInLanguage.Update(obj);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public List<ProductInLanguage> FindByListId(List<int> lstId, string languageCode = "vi-VN")
        {

            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    IQueryable<ProductInLanguage> items = _context.ProductInLanguage.AsQueryable();
                    items = items.Include(x => x.Product);

                    return items.Where(x => x.LanguageCode.Trim() == languageCode && lstId.Contains(x.ProductId)).Select(x => new { x.ProductId, x.Product.Price, x.Title, x.Product.Avatar }).ToList().Select(d => new ProductInLanguage { ProductId = d.ProductId, Title = d.Title, Product = new Product { Avatar = d.Avatar, Price = d.Price } }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return new List<ProductInLanguage>();
        }

        public List<ProductInLanguage> GetByProductId(int productId)
        {
            List<ProductInLanguage> productInLanguages = new List<ProductInLanguage>();
            try
            {
                productInLanguages = _context.ProductInLanguage.Where(x => x.ProductId == productId).ToList();


            }
            catch (Exception ex)
            {

            }
            return productInLanguages;
        }


    }
}
