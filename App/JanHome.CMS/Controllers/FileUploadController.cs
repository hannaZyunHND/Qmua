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
    public class FileUploadController : ControllerBase
    {
        FileUploadBCL _fileUploadBCL;
        public FileUploadController()
        {
            _fileUploadBCL = new FileUploadBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = _fileUploadBCL.FindAll().ToList();
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();

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
                var ads = _fileUploadBCL.FindById(id);
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
        public ResponseData Add([FromBody]FileUpload ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = _fileUploadBCL.Update(ads);
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
        public ResponseData Update([FromBody]FileUpload ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = _fileUploadBCL.Update(ads);
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
                bool ads = _fileUploadBCL.Remove(id);
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
