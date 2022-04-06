using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        LocationInLanguageBCL locationInLanguageBCL;
        LocationBCL locationBCL;
        public LocationController()
        {
            locationInLanguageBCL = new LocationInLanguageBCL();
            locationBCL = new LocationBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = locationBCL.FindAll().ToList();
                responseData.Success = true;
                responseData.Message = Utility.SuccessMessage;
                responseData.ListData = data.ToList<object>();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Get")]
        public ResponsePaging Get(Utils.FilterQuery filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = locationBCL.Get(filter, out total);

                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.Code,
                    x.Name,
                    x.ParentId,
                    x.LangCount
                }).ToList<object>();
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
                var ads = locationBCL.FindById(x => x.Id == id, x => x.LocationInLanguage);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                    responseData.ListData = ads.LocationInLanguage.OrderBy(n => n.LanguageCode).ToList<object>();
                    ads.LocationInLanguage = new List<LocationInLanguage>();
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]Location ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                int result1 = locationBCL.Create(ads);
                if (result1 > 0)
                {
                    responseData.Data = new { locationId = ads.Id };
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                }
                else
                {
                    switch (result1)
                    {
                        case -1:
                            responseData.Success = false;
                            responseData.Message = "ID đã tồn tại trên hệ thống.";
                            break;
                        default:
                            responseData.Success = false;
                            responseData.Message = Utility.ErrorMessage;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
        [HttpPut("Update")]
        public ResponseData Update([FromBody]Location ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = locationBCL.Update(ads);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
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
                bool result1 = locationInLanguageBCL.Delete(id);
                if (result1)
                {
                    bool ads = locationBCL.Remove(id);
                    if (ads)
                    {
                        responseData.Success = true;
                        responseData.Message = Utility.SuccessMessage;
                    }
                    else
                    {
                        responseData.Success = false;
                        responseData.Message = Utility.ErrorMessage;
                    }
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = Utility.ErrorMessage;
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
    }
}
