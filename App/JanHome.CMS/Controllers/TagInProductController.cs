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
    public class TagInProductController : ControllerBase
    {
        TagInProductBCL tagInProductBCL;
        public TagInProductController()
        {
            tagInProductBCL = new TagInProductBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = tagInProductBCL.FindAll().ToList();
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
                var ads = tagInProductBCL.FindById(id);
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
        public ResponseData Add([FromBody]List<TagInProductLanguage> ads)
        {
            ResponseData responseData = new ResponseData();
            int count = 0;
            try
            {
                for (int i = 0; i < ads.Count(); i++)
                {
                    bool result = tagInProductBCL.Add(ads[i]);
                    if (result)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count == ads.Count())
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = "Thất bại";
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
        [HttpPut("Update")]
        public ResponseData Update([FromBody]TagInProductLanguage ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = tagInProductBCL.Update(ads);
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
                bool ads = tagInProductBCL.Remove(id);
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
