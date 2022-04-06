using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.ViewModel;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using StackExchange.Redis;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInRegionController : ControllerBase
    {
        ProductInRegionBCL productInRegionBCL;
        ZoneInLanguageBCL zoneInLanguageBCL;
        ProductInLanguageBCL productInLanguageBCL;
        IConnectionMultiplexer _multiplexer;
        IConfiguration _configuration;
        public ProductInRegionController(IConfiguration configuration, IConnectionMultiplexer multiplexer)
        {
            productInRegionBCL = new ProductInRegionBCL();
            zoneInLanguageBCL = new ZoneInLanguageBCL();
            productInLanguageBCL = new ProductInLanguageBCL();
            _multiplexer = multiplexer;
            _configuration = configuration;
        }
        [HttpGet("GetAllRegion")]
        public IActionResult GetAllRegion()
        {
            return Ok(MI.Entity.Enums.RegionPage.RegionPages.ToList());
        }
        [HttpGet("GetProductByRegion")]
        public IActionResult GetProductByRegion(int zoneId = 0)
        {
            var lstProductRegion = new List<ProductInRegion>();
            if (zoneId > 0)
            {
                lstProductRegion = productInRegionBCL.FindAll(d => d.ZoneId == zoneId);
            }
            var zone = zoneInLanguageBCL.GetByName(x => lstProductRegion.Any(d => d.ZoneId == x.Id));
            var product = productInLanguageBCL.GetInfo(x => lstProductRegion.Any(d => d.ProductId == x.Id));

            return Ok(lstProductRegion.Select(x => new
            {
                x.ProductId,
                x.IsHot,
                ProductName = product.ContainsKey(x.ProductId) ? product[x.ProductId].Key : "Sản phẩm bị xóa",
                Avatar = product.ContainsKey(x.ProductId) ? $"{product[x.ProductId].Value}" : "/assets/img/unnamed.jpg",
                x.ZoneId,
                ZoneName = zone.ContainsKey(x.ZoneId) ? zone[x.ZoneId] : "",
                x.BigThumb,
            }).ToList());
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]ProductInRegions obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstObj = new List<ProductInRegion>();
                int index = 1;
                foreach (var item in obj.Regions)
                {
                    //item.SortOrder = index;
                    if (!lstObj.Any(x => x.ProductId == item.ProductId))
                    {
                        item.ZoneId = item.ZoneId;
                        item.SortOrder = index;
                        lstObj.Add(item);
                        index++;
                    }
                }
                var data = productInRegionBCL.Add(obj.zoneId, lstObj);
                if (data)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "productInRegion");
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
