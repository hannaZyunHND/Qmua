using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        RatingBCL ratingBCL;
        public RatingController()
        {
            ratingBCL = new RatingBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = ratingBCL.FindAll().ToList();
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
        public ResponsePaging Get(int pageIndex, int pageSize, string sortBy, string sortDir)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                //var data = ratingBCL.Get(pageIndex, pageSize, sortBy, sortDir, out total);
                //responseData.ListData = data.ToList<object>();
                //responseData.Total = total;
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
                var ads = ratingBCL.FindById(id);
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
        public ResponseData Add([FromBody]Config ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                //bool result = ratingBCL.Add(ads);
                //if (result)
                //{
                //    responseData.Success = true;
                //    responseData.Message = "Thành công";
                //}
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
        [HttpPut("Update")]
        public ResponseData Update([FromBody]Config ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                //bool result = ratingBCL.Update(ads);
                //if (result)
                //{
                //    responseData.Success = true;
                //    responseData.Message = "Thành công";
                //}
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
                bool ads = ratingBCL.Remove(id);
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
