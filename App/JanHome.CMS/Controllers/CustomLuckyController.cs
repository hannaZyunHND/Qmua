using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MI.Bo.Bussiness;
using MI.Entity.Common;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomLuckyController : ControllerBase
    {

        [HttpGet("GetAll")]
        public ResponseData GetAll(string phone, int pageIndex,int pageSize)
        {
            pageIndex = pageIndex > 1 ? pageIndex : 1;
            pageSize = pageSize > 1 ? pageSize : 10;
            ResponseData responseData = new ResponseData();
            try
            {
                int  total = 0;
                var data = CustomerLuckyBCL.Find(phone, pageIndex, pageSize, out total);
                responseData.Success = true;
                responseData.TotalRow = total;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }
    }
}
