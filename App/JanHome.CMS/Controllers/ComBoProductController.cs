using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Dal.IDbContext;
using MI.Entity.Common;
using MI.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComBoProductController : ControllerBase
    {
        ProductInProductBCL productInProductBCL;
        ProductInLanguageBCL productInLanguageBCL;
        IDbContext _context = new IDbContext();
        public ComBoProductController()
        {
            productInLanguageBCL = new ProductInLanguageBCL();
            productInProductBCL = new ProductInProductBCL();

        }
        [HttpPost("SearchAll")]
        public ResponsePaging Search([FromBody]FilterProduct filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;

                var a = _context.ArticlesInZone.Where(r => r.ZoneId == filter.idZone).Include(r => r.Article);
                var total_a = a.Count();

                var q = a.Select(x => new
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Avatar = x.Article.Avatar,
                    price = 0,
                    Code = "BV - " + x.Article.Id,
                    type = 2
                });

                var data = productInLanguageBCL.FindAll(filter, out total);
                if (total <= 0)
                    total = total_a;
                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Avatar,
                    price = x.Price,
                    x.Code,
                    type = 1
                }).ToList<object>();
                responseData.ListData.AddRange(q.Skip((filter.pageIndex - 1)* filter.pageSize).Take(filter.pageSize));
                responseData.Total = total;
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        public ResponsePaging SearchProductAndArticle([FromBody] FilterProduct filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = productInLanguageBCL.FindAll(filter, out total);

                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Avatar,
                    price = x.Price,
                    x.Code
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }



        [HttpGet("GetByProductId")]
        public ResponseData GetByProductId(int productId)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ComboProduct = productInProductBCL.FindAll(x => x.ProductId == productId).Where(r => r.Type.Equals(TypeComboProduct.combo));
                var lstId = ComboProduct.Select(x => (int)x.ProductItemId).ToList();
                var data = productInLanguageBCL.FindByListId(lstId);

                var query = from c in ComboProduct
                            join d in data on c.ProductItemId equals d.ProductId
                            select new
                            {
                                id = c.ProductId,
                                name = d.Title,
                                avatar = d.Product.Avatar,
                                price = d.Product.Price,
                                configPrice = c.ConfigPrice,
                                configNote = c.ConfigNote
                            };


                responseData.ListData = query.ToList<object>();



            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetSameByProductId")]
        public ResponseData GetSameByProductId(int productId)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ComboProduct = productInProductBCL.FindAll(x => x.ProductId == productId).Where(r => r.Type.Equals(TypeComboProduct.same));
                var lstId = ComboProduct.Select(x => (int)x.ProductItemId).ToList();
                var data = productInLanguageBCL.FindByListId(lstId);

                var query = from c in ComboProduct
                            join d in data on c.ProductItemId equals d.ProductId
                            select new
                            {
                                id = c.ProductId,
                                name = d.Title,
                                avatar = d.Product.Avatar,
                                price = d.Product.Price,
                                configPrice = c.ConfigPrice,
                                configNote = c.ConfigNote
                            };


                responseData.ListData = query.ToList<object>();



            }
            catch (Exception ex)
            {

            }
            return responseData;
        }



        [HttpPost("Add")]
        public ResponseData Add(ViewModel.ProductInCombo obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var Combo = new List<MI.Entity.Models.ProductInProduct>();
                int i = 0;
                foreach (var item in obj.Combos)
                {
                    Combo.Add(new MI.Entity.Models.ProductInProduct
                    {
                        ProductId = obj.Id,
                        ProductItemId = item.id,
                        SortOrder = i,
                        Type = obj.Type,
                        ConfigPrice = decimal.Parse(string.IsNullOrEmpty(item.configPrice) ? "0" : item.configPrice),
                        ConfigNote = item.configNote
                    });
                    i++;
                }
                bool isAdd = false;
                if (obj.Type.Equals(TypeComboProduct.same))
                {
                    isAdd = productInProductBCL.Merge(obj.Id, obj.Type, Combo);
                }
                if (obj.Type.Equals(TypeComboProduct.combo))
                {
                    isAdd = productInProductBCL.MergeCombo(obj.Id, obj.Type, Combo);
                }
                
                if (isAdd)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";

                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        
    }
}
