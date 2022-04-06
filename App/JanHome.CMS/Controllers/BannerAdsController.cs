using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerAdsController : ControllerBase
    {
        BannerAdsBCL bannerAdsBCL;
        IConnectionMultiplexer _multiplexer;
        IConfiguration _configuration;
        public BannerAdsController(IConnectionMultiplexer multiplexer, IConfiguration configuration)
        {
            bannerAdsBCL = new BannerAdsBCL();
            _multiplexer = multiplexer;
            _configuration = configuration;

        }
        [HttpGet("Get")]
        public IActionResult Get(string tuKhoa)
        {
            try
            {
                var data = bannerAdsBCL.Find(tuKhoa);
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new KeyValuePair<string, string>());
        }

        [HttpGet("GetAllCode")]
        public IActionResult GetAllCode()
        {
            try
            {
                var data = bannerAdsBCL.GetAllCode();
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new List<string>());
        }
        [HttpGet("GetByCode")]
        public IActionResult GetByCode(string code)
        {
            try
            {
                var data = bannerAdsBCL.FindAll(x => x.Code == code);
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new List<string>());
        }
        [HttpPost("GetAll")]
        public IActionResult GetAll(FilterQuery filter)
        {
            dynamic responseData = new System.Dynamic.ExpandoObject();
            try
            {
                var data = bannerAdsBCL.FindAll();
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }

        [HttpGet("GetGuarantee")]
        public IActionResult GetGuarantee()
        {
            try
            {
                var data = bannerAdsBCL.GetGuarantee();
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new KeyValuePair<string, string>());
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] BannerAds obj)
        {
            KeyValuePair<bool, string> responseData = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                if (!String.IsNullOrEmpty(obj.LanguageCode) && !String.IsNullOrEmpty(obj.Code))
                {
                    obj.Code = Utils.Utility.GenerateCode(obj.Code);
                    obj.Id = $"{obj.LanguageCode}_{obj.Code}";
                    responseData = bannerAdsBCL.AddOrUpdate(obj);
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "banner");
                }
                else
                {
                    responseData = new KeyValuePair<bool, string>(false, "Không được để trống mã code hoặc ngôn ngữ");
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(responseData);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(string Id)
        {
            dynamic responseData = new System.Dynamic.ExpandoObject();
            try
            {
                //Chuẩn hóa lại json nếu không tồn tại
                var data = bannerAdsBCL.FindById(x => x.Id == Id);
                if (data != null)
                {
                    data.MetaData = data.CheckJson();
                }
                else
                {
                    return Ok(new BannerAds());
                }
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new BannerAds());
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(string key)
        {

            try
            {
                var data = bannerAdsBCL.Remove(key);
                return Ok(data);
            }
            catch (Exception ex)
            {

            }

            return Ok(new KeyValuePair<bool, string>(false, "Thất bại"));
        }

    }
}
