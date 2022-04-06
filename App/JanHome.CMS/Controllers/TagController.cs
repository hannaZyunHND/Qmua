using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        TagBCL tagBCL;
        public TagController()
        {
            tagBCL = new TagBCL();
        }
        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = tagBCL.GetAllNameTags();
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();

            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("Get")]
        public ResponsePaging Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = tagBCL.Get(keyword, pageIndex, pageSize, sortBy, sortDir, out total);
                responseData.ListData = data.ToList<object>();
                responseData.Total = total;
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
                var ads = tagBCL.FindById(id);
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
        public ResponseData Add([FromBody]Tag tag)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = tagBCL.Add(tag);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.Message = ex.InnerException.Message;
            }

            return responseData;
        }
        [HttpPut("Update")]
        public ResponseData Update([FromBody]Tag tag)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = tagBCL.Update(tag);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.Message = ex.InnerException.Message;
            }

            return responseData;
        }
    }
}
