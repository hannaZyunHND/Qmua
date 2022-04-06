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
    public class ContactController : ControllerBase
    {

        ContactBCL contactBCL;
        public ContactController()
        {
            contactBCL = new ContactBCL();
        }

        [HttpPost("Get")]
        public IActionResult Get([FromBody] Utils.FilterContact filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            var total = 0;
            var data = contactBCL.Find(filter, out total);

            responseData.ListData = data.Select(x => new
            {
                x.Address,
                x.Content,
                x.CreatedDate,
                x.Email,
                x.Gender,
                x.Id,
                x.ModifiedBy,
                x.ModifiedDate,
                x.Name,
                x.Note,
                x.Phone,
                x.Source,
                x.Status,
                x.Title,
                x.Type,
                UrlRef = Utils.BaseBA.UrlBase(x.UrlRef)
            }).ToList<object>();
            responseData.Total = total;
            return Ok(responseData);
        }

        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus([FromBody] Contact obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = contactBCL.UpdateStatus(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote([FromBody] Contact obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            obj.ModifiedDate = DateTime.Now;
            var data = contactBCL.UpdateNote(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Contact obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = contactBCL.Add(obj);
            if (data == true)
            {

                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] Contact obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = contactBCL.Remove(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }
    }
}
