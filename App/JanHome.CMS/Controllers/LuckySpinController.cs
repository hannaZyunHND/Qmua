using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuckySpinController : ControllerBase
    {
        LuckySpinBCL luckySpinBCL;
        public LuckySpinController()
        {
            luckySpinBCL = new LuckySpinBCL();
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody] LuckySpin obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = luckySpinBCL.Add(obj);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    return responseData;
                }

            }
            catch (Exception ex)
            {

            }
            responseData.Success = false;
            responseData.Message = "Thất bại";
            return responseData;
        }
        [HttpPut("Update")]
        public ResponseData Update([FromBody] LuckySpin obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = luckySpinBCL.Update(obj);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    return responseData;
                }

            }
            catch (Exception ex)
            {

            }
            responseData.Success = false;
            responseData.Message = "Thất bại";
            return responseData;
        }
        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = luckySpinBCL.FindAll().ToList();
                responseData.Success = true;
              //  responseData.Message = Utility.SuccessMessage;
                responseData.ListData = data.ToList<object>();
            }
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.Message = ex.Message;
            }
            return responseData;
        }
        [HttpGet("Get")]
        public ResponseData Get(Guid id)
        {
            var responseData = new ResponseData();
            try
            {
               
                var data = luckySpinBCL.Get(id);
                responseData.Data = data;
                
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpDelete("Delete")]
        public ResponseData Delete(Guid id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = luckySpinBCL.Delete(id);
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
