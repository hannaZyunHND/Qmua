using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashSaleController : ControllerBase
    {
        public FlashSaleBCL flashSaleBCL;

        public FlashSaleController()
        {
            flashSaleBCL = new FlashSaleBCL();
        }
        [HttpPost("Get")]
        public IActionResult Get(Utils.FilterQuery filter)
        {
            dynamic response = new System.Dynamic.ExpandoObject();
            int total = 0;
            var data = flashSaleBCL.Get(filter, out total);

            response.listData = data.Select(x => new
            {
                x.Id,
                x.Name,
                StartTime = x.StartTime.ToString("dd/MM/yyyy HH:mm"),
                EndTime = x.EndTime.ToString("dd/MM/yyyy HH:mm"),
                x.Status
            }).ToList();
            response.total = total;
            return Ok(response);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            dynamic response = new System.Dynamic.ExpandoObject();

            var data = flashSaleBCL.FindById(Id);
            response.data = data;

            response.listData = flashSaleBCL.GetProduct(data.Id);
            return Ok(response);
        }

        [HttpPost("Add")]
        public IActionResult Add(ViewModel.FlashSaleVM flashSale)
        {
            KeyValuePair<bool, string> respone = new KeyValuePair<bool, string>(false, "Thất bại");

            respone = flashSaleBCL.Merge(flashSale.Flash, flashSale.ListProduct);
            return Ok(respone);
        }
        [HttpPut("UpdateTrangThai")]
        public IActionResult UpdateTrangThai(int id, int status)
        {
            KeyValuePair<bool, string> respone = new KeyValuePair<bool, string>(false, "Thất bại");
            respone = flashSaleBCL.UpdateStatus(new FlashSale { Id = id, Status = status });
            return Ok(respone);
        }
    }
}
