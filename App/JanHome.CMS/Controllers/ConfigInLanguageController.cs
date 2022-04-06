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
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigInLanguageController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        ConfigInLanguageBCL configInLanguageBCL;
        public ConfigInLanguageController(IHostingEnvironment env)
        {
            configInLanguageBCL = new ConfigInLanguageBCL();
            _env = env;
        }
        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = configInLanguageBCL.FindAll().ToList();
                responseData.Success = true;
                responseData.Message = Utility.SuccessMessage;
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
                var data = configInLanguageBCL.Get(keyword, pageIndex, pageSize, sortBy, sortDir, out total);
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
                var ads = configInLanguageBCL.FindById(id);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]ConfigInLanguage promotionInLanguage)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = configInLanguageBCL.Add(promotionInLanguage);
                if (result)
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
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.Message = ex.InnerException.Message;
            }

            return responseData;
        }
        [HttpPut("Update")]
        public ResponseData Update([FromBody]ConfigInLanguage promotionInLanguage)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = configInLanguageBCL.Update(promotionInLanguage);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                responseData.Success = false;
                responseData.Message = ex.InnerException.Message;
            }

            return responseData;
        }
        [HttpPut("Updates")]
        public ResponseData Updates([FromBody]List<ConfigInLanguage> promotionInLanguage)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = configInLanguageBCL.Update(promotionInLanguage);
                if (result)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
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
