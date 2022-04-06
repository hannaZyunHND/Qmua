using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInZoneController : ControllerBase
    {
        ProductInLanguageBCL productInLanguageBCL;
        ProductInZoneBCL productInZoneBCL;
        public ProductInZoneController()
        {
            productInLanguageBCL = new ProductInLanguageBCL();
            productInZoneBCL = new ProductInZoneBCL();
        }

        [HttpGet("Search")]
        public ResponsePaging Search([FromQuery]FilterProduct filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = productInLanguageBCL.GetByZone(filter, out total);
                responseData.ListData = data.Select(x => new
                {
                    x.ProductId,
                    x.BigThumb,
                    x.IsHot,
                    x.ZoneId,
                    x.IsPrimary,
                    Avatar = x.Product.Avatar,
                    Code = x.Product.Code,
                    Name = x.Product.Name,
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpPut("Update")]
        public ResponseData Update([FromBody]ProductInZone obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var isAdd = productInZoneBCL.Update(obj);
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
