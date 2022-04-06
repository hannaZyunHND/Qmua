using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        AdsBCL adsBCL;
        public AdsController(IHostingEnvironment env)
        {
            adsBCL = new AdsBCL();
            _env = env;
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = adsBCL.FindAll().ToList();
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
        public ResponsePaging Get([FromQuery]FilterQuery fillter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = adsBCL.Get(fillter, out total);
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
                var ads = adsBCL.FindById(id);
                
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
        public ResponseData Add()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = new Ads();

                var data = Request.Form.Keys;
                if (data.Count > 0)
                {
                    if (data.Contains("ads"))
                    {
                        ads = JsonConvert.DeserializeObject<Ads>(Request.Form["ads"]);
                    }
                    if (ads.Id > 0)
                    {
                        if (Request.Form.Files.Count > 0)
                        {
                            ads.Thumb = Utils.Utility.GetLinkImage(Request.Form.Files["file"], _env, "ads");
                        }
                        bool result = adsBCL.Add(ads);
                        if (result)
                        {
                            responseData.Success = true;
                            responseData.Message = "Thành công";
                        }
                    }
                    else
                    {
                        responseData.Message = "Thông tin không phù hợp";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }

        [HttpPut("Update")]
        public ResponseData Update()
        {
            ResponseData responseData = new ResponseData();

            var ads = new Ads();

            var data = Request.Form.Keys;
            if (data.Count > 0)
            {
                try
                {
                    if (data.Contains("ads"))
                    {
                        var data1 = Request.Form["ads"];
                        ads = JsonConvert.DeserializeObject<Ads>(Request.Form["ads"]);
                    }
                    if (ads.Id > 0)
                    {
                        if (Request.Form.Files.Count > 0)
                        {
                            ads.Thumb = Utils.Utility.GetLinkImage(Request.Form.Files[0], _env, "ads");
                        }
                        bool result = adsBCL.Update(ads);
                        if (result)
                        {
                            responseData.Success = true;
                            responseData.Message = "Thành công";
                        }
                    }
                    else
                    {
                        responseData.Message = "Thông tin không phù hợp";
                    }
                }
                catch (Exception ex)
                {

                }
            }


            return responseData;
        }

        [HttpDelete("Delete")]
        public ResponseData Delete(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = adsBCL.Remove(id);
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
