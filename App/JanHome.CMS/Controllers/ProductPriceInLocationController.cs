using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceInLocationController : ControllerBase
    {
        ProductPriceInLocationBCL productPriceInLocationBCL;
        LocationInLanguageBCL locationInLanguageBCL;
        public ProductPriceInLocationController()
        {
            productPriceInLocationBCL = new ProductPriceInLocationBCL();
            locationInLanguageBCL = new LocationInLanguageBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = productPriceInLocationBCL.FindAll().ToList();
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();

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
                var locations = locationInLanguageBCL.FindAll(x => x.LanguageCode.Trim() == "vi-VN").ToDictionary(x => x.LocationId, x => x);
                var locationPrice = productPriceInLocationBCL.FindAll(x => x.ProductId == idProduct);

                responseData.ListObj1 = locations.Values.Where(x =>
                !locationPrice.Select(d => d.LocationId).Contains(x.LocationId)).Select(x => new
                {
                    Id = 0,
                    LocationId = x.LocationId,
                    Price = 0,
                    SalePrice = 0,
                    Ten = x.Name,
                    ProductId = idProduct,
                    DiscountPercent = 0,
                }).ToList<object>();
                responseData.ListObj2 = locationPrice.Select(x => new
                {
                    x.Id,
                    x.LocationId,
                    x.Price,
                    x.ProductId,
                    x.SalePrice,
                    Ten = locations.ContainsKey(x.LocationId) ? locations[x.LocationId].Name : "",
                    x.DiscountPercent
                }).ToList<object>();

                //int total = 0;
                //var data = productBCL.Get(pageIndex, pageSize, sortBy, sortDir, out total);
                //responseData.ListData = data.ToList<object>();
                //responseData.Total = total;
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetById")]
        public ResponseData GetById(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = productPriceInLocationBCL.FindById(id);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]ViewModel.ProductPriceInLocations obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = productPriceInLocationBCL.Add(obj.Id, obj.PriceInLocation);
                if (result)
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
        [HttpPut("Update")]
        public ResponseData Update([FromBody]ProductPriceInLocation ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = productPriceInLocationBCL.Update(ads);
                if (result)
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

        [HttpDelete("Delete")]
        public ResponseData Delete(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = productPriceInLocationBCL.Remove(id);
                if (ads)
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
