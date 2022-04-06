using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MI.Dapper.Data.Repositories.Interfaces;
using Utils;
using System.Text;
using MI.ES;

using Nest;
using Newtonsoft.Json;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductInLanguageBCL productInLanguageBCL;
        LocationBCL locationBCL;
        ZoneInLanguageBCL zoneInLanguageBCL;
        ZoneBCL zoneBCL;
        ProductPriceInLocationBCL priceInLocationBCL;
        ProductBCL productBCL;
        TagInProductBCL tagInProductBCL;
        ProductInZoneBCL productInZoneBCL;
        PropertyBCL propertyBCL;
        ArticleBCL articleBCL;
        IConnectionMultiplexer _multiplexer;
        IConfiguration _configuration;

        public ProductController(IProductRepository productRepository, IConnectionMultiplexer multiplexer, IConfiguration configuration)
        {
            productBCL = new ProductBCL();
            productInLanguageBCL = new ProductInLanguageBCL();
            zoneInLanguageBCL = new ZoneInLanguageBCL();
            tagInProductBCL = new TagInProductBCL();
            priceInLocationBCL = new ProductPriceInLocationBCL();
            locationBCL = new LocationBCL();
            productInZoneBCL = new ProductInZoneBCL();
            propertyBCL = new PropertyBCL();
            zoneBCL = new ZoneBCL();
            articleBCL = new ArticleBCL();
            _multiplexer = multiplexer;
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = productBCL.FindAll();
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }
        [HttpGet("GetPropertyProduct")]
        public IActionResult GetPropertyProduct()
        {
            var lstStatus = propertyBCL.FindAll();
            return Ok(lstStatus.Select(x => new { id = x.Id, label = x.Name }).ToList<object>());

        }

        [HttpGet("Supports")]
        public ResponseSuports Supports()
        {
            ResponseSuports responseData = new ResponseSuports();
            try
            {
                var lstStatus = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.StatusProduct), true);
                responseData.ListStatus = lstStatus.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpGet("GetPriceByLocation")]
        public ResponseTwoList GetPriceByLocation(int idProduct)
        {
            ResponseTwoList responseData = new ResponseTwoList();
            try
            {
                var locations = locationBCL.FindAll().ToDictionary(x => x.Id, x => x);
                var locationPrice = priceInLocationBCL.FindAll(x => x.ProductId == idProduct);

                responseData.ListObj1 = locations.Values.Where(x =>
                        !locationPrice.Select(d => d.LocationId).Contains(x.Id)).Select(x => new
                        { Id = 0, LocationId = x.Id, Price = 0, SalePrice = 0, Ten = x.Name, ProductId = idProduct })
                    .ToList<object>();
                responseData.ListObj2 = locationPrice.Select(x => new
                {
                    x.Id,
                    x.LocationId,
                    x.Price,
                    x.ProductId,
                    x.SalePrice,
                    Ten = locations.ContainsKey(x.LocationId) ? locations[x.LocationId].Name : ""
                }).ToList<object>();
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }


        [HttpPost("Search")]
        public ResponsePaging Search([FromBody] FilterProduct filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;

                var data = productInLanguageBCL.FindAll(filter, out total);
                var lstId = data.Select(x => x.ProductInZone).SelectMany(d => d).ToList().Select(x => x.ZoneId);
                var dicZone = zoneBCL.GetById(lstId.Distinct().ToList(), filter.languageCode)
                    .ToDictionary(x => x.Id, z => z.Name);

                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    Name = x.Name,
                    Lang = x.LangCount,
                    x.Avatar,
                    x.Status,
                    price = Utils.Utility.ConvertCurentce(x.Price.ToString()),
                    category = Utils.Utility.BuildZone(x.ProductInZone.Select(d => d.ZoneId).ToList(), dicZone),
                    x.Code,
                    x.ViewCount,
                    x.SortOrder,
                    x.ArticleId,
                    BaseUrl = x.ListUrl.Select(d => new KeyValuePair<string, string>(d.Key, $"{Utils.Utility.BaseDomain(Utils.BaseBA.UrlProductJanhome("", d.Value))}")).ToList()
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }


        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            ViewModel.Products responseData = new ViewModel.Products();
            try
            {
                var objPro = productBCL.FindById(x => x.Id == id, x => x.ProductInZone);

                if (objPro != null)
                {
                    responseData.ListCat = objPro.ProductInZone.OrderByDescending(x => x.IsPrimary).Select(x => x.ZoneId).ToList();
                    responseData.ListProp = Utils.Utility.SplitStringToListInt(objPro.PropertyId);
                    responseData.ListColor = Utils.Utility.SplitStringToList(objPro.Color);
                    responseData.ListArticle = MI.Cache.RamCache.DicArticle.ToList();
                    objPro.ProductInZone = new List<ProductInZone>();
                    if (String.IsNullOrEmpty(objPro.MetaFile))
                    {
                        objPro.MetaFile = JsonConvert.SerializeObject(new MI.Entity.CustomClass.FileProduct());
                    }
                    responseData.Data = objPro;
                }
            }
            catch (Exception ex)
            {
            }

            return Ok(responseData);
        }

        [HttpGet("GetLanguageById")]
        public ResponseData GetLanguageById(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ProductInLanguage = productInLanguageBCL.GetByProductId(id);
                if (ProductInLanguage != null && ProductInLanguage.Count > 0)
                {
                    var TagName = tagInProductBCL.FindByProductId(ProductInLanguage.Select(x => x.Id).ToList());

                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    responseData.ListData = ProductInLanguage.Select(x =>
                            new ProductInLanguage(x, TagName.ContainsKey(x.Id) ? TagName[x.Id] : new List<string>()))
                        .ToList<object>();
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("Add")]
        public ResponseData Add([FromBody] Product product)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                //insert product
                product.CreatedBy = product.ModifyBy;
                product.Status = (int)MI.Entity.Enums.StatusProduct.NotPublic;
                product.CreatedDate = DateTime.Now;
                if (String.IsNullOrWhiteSpace(product.Code))
                {
                    product.Code = "SP" + Guid.NewGuid().ToString().Substring(0, 7);
                }
                var respone = Utils.Utility.AutoCheckSlug(product.Name, product.Url);
                if (respone.Key)
                {
                    product.Url = respone.Value;
                    product.Status = 2;
                    product.Avatar = Utils.Utility.RemoveDomainImage(product.Avatar);


                    int result1 = productBCL.Add(product, product.ProductInZone.ToList());
                    if (result1 > 0)
                    {
                        responseData.Success = true;
                        responseData.Message = "Thành công";
                        responseData.Id = result1;
                    }
                    else
                    {
                        if (result1 == -1)
                        {
                            responseData.Success = false;
                            responseData.Message = "Mã sản phẩm đã tồn tại";
                        }
                        else
                        {
                            responseData.Success = false;
                            responseData.Message = "Thất bại";
                        }

                    }
                }
                else
                {
                    responseData.Message = respone.Value;
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPut("Update")]
        public ResponseData Update([FromBody] Product product)
        {
            ResponseData responseData = new ResponseData();
            try
            {

                var respone = Utils.Utility.AutoCheckSlug(product.Name, product.Url);
                if (respone.Key)
                {
                    product.Url = respone.Value;
                    product.ModifyDate = DateTime.Now;
                    int result = productBCL.Update(product, product.ProductInZone.ToList());
                    if (result > 0)
                    {
                        responseData.Success = true;
                        responseData.Message = "Thành công";
                        MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "productInRegion");
                    }
                    else if (result == -1)
                    {
                        responseData.Success = false;
                        responseData.Message = "Mã sản phẩm đã tồn tại";
                    }
                }
                else
                {
                    responseData.Message = respone.Value;
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPut("Delete")]
        public ResponseData Delete(int id, int status)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = productBCL.UpdateTrangThai(new Product { Id = id, Status = status });
                if (ads)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Thất bại";
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }
        [HttpPut("UpdateSort")]
        public ResponseData UpdateSort(int id, int sortNew)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = productBCL.UpdateSort(new Product { Id = id, SortOrder = sortNew });
                if (ads)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Thất bại";
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }
    }
}
