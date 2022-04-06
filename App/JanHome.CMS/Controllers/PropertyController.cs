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
    public class PropertyController : ControllerBase
    {
        PropertyLanguageBCL propertyLanguageBCL;
        PropertyBCL propertyBCL;
        public PropertyController()
        {
            propertyLanguageBCL = new PropertyLanguageBCL();
            propertyBCL = new PropertyBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = propertyBCL.FindAll().ToList();
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
                var data = propertyBCL.Get(filter, out total);
                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    GroupNane = MI.Entity.EnumHelper.GetDescription((MI.Entity.Enums.GroupProp)x.GroupId),
                    x.GroupId,
                    x.Thumb,
                    x.Name,
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
                var ads = propertyBCL.FindById(x => x.Id == id, x => x.PropertyLanguage);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                    responseData.ListData = ads.PropertyLanguage.ToList<object>();
                    ads.PropertyLanguage = new List<PropertyLanguage>();
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]Property ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                int result1 = propertyBCL.CreateProduct(ads);
                if (result1 > 0)
                {
                    responseData.Data = new { propertyId = ads.Id };
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
        public ResponseData Update([FromBody]Property ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ads.Position = ads.Position.ToString();
                bool result = propertyBCL.Update(ads);
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
                bool result1 = propertyLanguageBCL.Delete(id);
                if (result1)
                {
                    bool ads = propertyBCL.Remove(id);
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
