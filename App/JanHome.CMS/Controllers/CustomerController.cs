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
    public class CustomerController : ControllerBase
    {
        public CustomerBCL customerBCL;
        public CustomerController()
        {
            customerBCL = new CustomerBCL();
        }
        [HttpPost("Get")]
        public ResponsePaging Get([FromBody]FilterCustomer fillter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = customerBCL.Find(fillter, out total);
                responseData.ListData = data.Select(x => new
                {
                    x.CreatedDate,
                    x.Email,
                    x.Gender,
                    x.Id,
                    x.Ip,
                    x.MetaData,
                    x.Name,
                    x.Note,
                    x.Orders,
                    x.Os,
                    x.Pcname,
                    x.PhoneNumber,
                    x.Source,
                    x.Type,
                    x.Address,
                    SourceName = MI.Entity.EnumHelper.GetDescription((MI.Entity.Enums.SourceCustomer)x.Source)
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Customer obj)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            response.Success = false;
            response.Message = "Thất bại";
            var data = customerBCL.Merge(obj);
            if (data == true)
            {
                response.Success = true;
                response.Message = "Thành công";
            }
            return Ok(response);
        }

    }
}
