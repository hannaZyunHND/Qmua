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
    public class ColorsController : ControllerBase
    {
        ColorsBCL colorsBCL;
        public ColorsController()
        {
            colorsBCL = new ColorsBCL();
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
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var data = colorsBCL.FindAll(x=>x.LanguageCode.Trim() == Utils.Utility.DefaultLang);
            return Ok(data.Select(x=>new { id = x.Code, label = x.Name }).ToList());
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Colors obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            obj.CreateDate = DateTime.Now;
            obj.Show = true;
            var data = colorsBCL.Merge(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] Colors obj)
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
