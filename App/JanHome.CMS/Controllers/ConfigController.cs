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
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace JanHome.CMS.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : Controller
    {
        ConfigInLanguageBCL configInLanguageBCL;
        ConfigBCL configBCL;
        IConnectionMultiplexer _multiplexer;
        IConfiguration _configuration;
        public ConfigController(IConnectionMultiplexer multiplexer, IConfiguration configuration)
        {
            configInLanguageBCL = new ConfigInLanguageBCL();
            configBCL = new ConfigBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll(string languageCode)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = configBCL.FindAll(x => x.ConfigInLanguage).ToList();
                responseData.Success = true;

                responseData.Message = Utility.SuccessMessage;
                var lang = data.SelectMany(x => x.ConfigInLanguage.Where(d => d.LanguageCode.Trim() == languageCode)).ToDictionary(x => x.ConfigName, x => x);

                responseData.ListData = data.Select(x => new
                {
                    x.Page,
                    x.ConfigName,
                    x.ConfigGroupKey,
                    x.ConfigLabel,
                    x.ConfigValueType,
                    Content = lang.ContainsKey(x.ConfigName) ? lang[x.ConfigName].Content : "",
                    LanguageCode = lang.ContainsKey(x.ConfigName) ? lang[x.ConfigName].LanguageCode : languageCode,
                }).ToList<object>();
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
                var data = configBCL.Get(pageIndex, pageSize, sortBy, sortDir, out total);
                responseData.ListData = data.ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetById")]
        public ResponseData GetById(string id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = configBCL.FindById(x => x.ConfigGroupKey == id, x => x.ConfigInLanguage);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                    responseData.ListData = ads.ConfigInLanguage.ToList<object>();
                    ads.ConfigInLanguage = new List<ConfigInLanguage>();
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
                int result1 = configBCL.CreateProduct(ads);
                if (result1 > 0)
                {
                    responseData.Data = new { configGroupKey = ads.ConfigGroupKey };
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
        public ResponseData Update([FromBody]Config ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = configBCL.Update(ads);
                if (result)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "config");
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
        public ResponseData Delete(string id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result1 = configInLanguageBCL.Delete(id);
                if (result1)
                {
                    bool ads = configBCL.Delete(id);
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
