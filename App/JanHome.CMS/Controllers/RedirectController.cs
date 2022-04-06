using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        RedirectBCL colorsBCL;
        public RedirectController()
        {
            colorsBCL = new RedirectBCL();
        }
        [HttpPost("Get")]
        public IActionResult Get([FromBody] Utils.FilterQuery filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            var total = 0;
            var data = colorsBCL.Find(filter, out total);
            responseData.ListData = data.ToList<object>();
            responseData.Total = total;
            return Ok(responseData);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Redirect obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            if (!String.IsNullOrEmpty(obj.UrlNew) && !String.IsNullOrEmpty(obj.UrlNew))
            {
                var data = colorsBCL.Merge(obj);
                if (data == true)
                {
                    response.Success = true;
                    response.Message = "Thành công";
                }
            }
            else
            {
                response.Message = "Mời bạn kiểm tra lại url";
            }
            return Ok(response);
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] Redirect obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = colorsBCL.Remove(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
    }
}
