using EFCore.BulkExtensions;
using MI.Dal.IDbContext;
using MI.Entity.Enums;
using MI.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace MI.Bo.Bussiness
{
    public class ProductInProductBCL : Base<ProductInProduct>
    {
        public ProductInProductBCL()
        {

        }

        public bool Merge(int idProduct, string type, List<ProductInProduct> lstObj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var affected = _context.Product.Find(idProduct);
                    _context.ProductInProduct.RemoveRange(_context.ProductInProduct.Where(x => x.ProductId == idProduct && x.Type == type));
                    _context.ProductInProduct.AddRange(lstObj);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);

                return false;
            }



        }
        public bool MergeCombo(int idProduct, string type, List<ProductInProduct> lstObj)
        {
            try
            {
                using (IDbContext _context = new IDbContext())
                {
                    var isDeleted = false;
                    if (lstObj.Count == 0)
                    {
                        isDeleted = true;
                    }
                    var affected = _context.Product.Find(idProduct);
                    var cbbb = 0;
                    var af = _context.Product.AsNoTracking().Where(r => r.ProductComboParentId == idProduct).FirstOrDefault();
                    if (af != null)
                        cbbb = af.Id;
                    Console.WriteLine(idProduct);

                    var z = 0;
                    var zone_affected = _context.Zone.Where(r => r.Type == (int)TypeZone.Combo).FirstOrDefault();
                    if (zone_affected != null)
                    {
                        z = zone_affected.Id;
                    }
                    var name = "Combo của sản phẩm " + affected.Name;

                    var checker = lstObj.Where(r => r.ProductItemId == idProduct).FirstOrDefault();
                    if (checker == null)
                    {
                        decimal p = 0;
                        if (affected.DiscountPrice > 0 && affected.DiscountPrice < affected.Price)
                            p = (decimal)affected.DiscountPrice;
                        else
                            p = (decimal)affected.Price;
                        var x = new ProductInProduct { ProductId = idProduct, ProductItemId = idProduct, SortOrder = 0, Type = TypeComboProduct.combo, ConfigPrice = p, ConfigNote = "" };
                        lstObj.Add(x);
                    }
                    double price = 0;
                    foreach (var item in lstObj)
                    {
                        price += (double)item.ConfigPrice;
                    }
                    _context.ProductInProduct.RemoveRange(_context.ProductInProduct.Where(x => x.ProductId == idProduct && x.Type == type));
                    if (isDeleted == false)
                        _context.ProductInProduct.AddRange(lstObj);
                    _context.SaveChanges();

                    var _newproduct = new Product { Name = name, Status = (int)StatusProduct.Combo, DiscountPercent = 0, Price = price, DiscountPrice = price, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now, ExprirePromotion = DateTime.Now, ProductComboParentId = idProduct, Avatar = affected.Avatar };

                    if (isDeleted == true)
                    {
                        var d = _context.ProductInZone.Where(r => r.ProductId == cbbb && r.ZoneId == z);

                        _context.ProductInZone.RemoveRange(d);
                        var l = _context.ProductInLanguage.Where(r => r.ProductId == cbbb);
                        _context.ProductInLanguage.RemoveRange(l);
                        _context.SaveChanges();
                        var c = _context.Product.Where(r => r.ProductComboParentId == idProduct);
                        _context.Product.RemoveRange(c);
                        _context.SaveChanges();
                    }
                    if(isDeleted == false)
                    {
                        if (af == null)
                        {
                            var generate_url = UIHelper.RandomString(32);
                            _newproduct.Url = generate_url;
                            _context.Product.Add(_newproduct);
                            _context.SaveChanges();
                            var outid = _newproduct.Id;
                            var _newlanguage = new ProductInLanguage { ProductId = outid, LanguageCode = "vi-VN", Title = name };
                            _context.ProductInLanguage.Add(_newlanguage);
                            _context.ProductInZone.Add(new ProductInZone { ProductId = outid, ZoneId = z, IsPrimary = true });

                        }
                        if (af != null)
                        {
                            var afid = af.Id;
                            _newproduct.Id = afid;
                            _context.Product.Update(_newproduct);
                            var _lang = _context.ProductInLanguage.Where(r => r.ProductId == af.Id && r.LanguageCode == "vi-VN").FirstOrDefault();
                            if (_lang != null)
                            {
                                _lang.Title = name;
                                _context.ProductInLanguage.Update(_lang);
                            }
                        }

                        _context.SaveChanges();
                    }

                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Logger.WriteLog(Logger.LogType.Error, ex.Message);
                return false;
            }
        }
    }
}
