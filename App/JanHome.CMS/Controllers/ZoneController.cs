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
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        ZoneBCL zoneBCL;
        ZoneInLanguageBCL zoneLangBCL;
        IConnectionMultiplexer _multiplexer;
        IConfiguration _configuration;
        public ZoneController(IConnectionMultiplexer multiplexer, IConfiguration configuration)
        {
            zoneBCL = new ZoneBCL();
            zoneLangBCL = new ZoneInLanguageBCL();
            _multiplexer = multiplexer;
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll(string tuKhoa, string languageCode, MI.Entity.Enums.StatusZone trangThai = MI.Entity.Enums.StatusZone.All, MI.Entity.Enums.TypeZone loai = MI.Entity.Enums.TypeZone.All)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = zoneLangBCL.FindAll(tuKhoa, trangThai, loai, languageCode);
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Lang = x.ZoneInLanguage.Count,
                    SortOrder = x.SortOrder ?? 1,
                    ParentId = x.ParentId,
                    x.IsShowHome,
                    x.IsShowSearch,
                    x.IsShowMenu,
                }).ToList<object>();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }




        [HttpGet("GetZones")]
        public ResponseData GetZones(MI.Entity.Enums.TypeZone loai = MI.Entity.Enums.TypeZone.All)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = new List<Zone>();
                if (loai == MI.Entity.Enums.TypeZone.All)
                {
                    data = MI.Cache.RamCache.DicZone.Values.Where(x => x.Status != (byte)MI.Entity.Enums.StatusZone.Delete).ToList();
                }
                else
                {
                    data = MI.Cache.RamCache.DicZone.Values.Where(x => x.Type == (byte)loai && x.Status != (byte)MI.Entity.Enums.StatusZone.Delete).ToList();

                }
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.Select(x => new { id = x.Id, label = x.Name, parentId = x.ParentId, type = x.Type }).ToList<object>();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }



        [HttpGet("Supports")]
        public ResponseSuports Supports()
        {
            ResponseSuports responseData = new ResponseSuports();
            try
            {
                var lstTypes = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.TypeZone), true);
                var lstStatus = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.StatusZone), true);
                responseData.ListTypes = lstTypes.Cast<object>().ToList();
                responseData.ListStatus = lstStatus.Cast<object>().ToList();
                //responseData.Success = true;
                //responseData.Message = "Thành công";
                //responseData.ListData =
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("Get")]
        public ResponsePaging Get([FromQuery]FilterZone filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = zoneBCL.Get(filter, out total);
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
                var ads = zoneBCL.FindById(x => x.Id == id, x => x.ZoneInLanguage);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                    responseData.ListData = ads.ZoneInLanguage.ToList<object>();
                    ads.ZoneInLanguage = new List<ZoneInLanguage>();
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]Zone ads)
        {
            ResponseData responseData = new ResponseData();
            MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
            try
            {
                ads.CreatedDate = DateTime.Now;

                if (String.IsNullOrWhiteSpace(ads.Url))
                {
                    ads.Url = Utils.Utility.GenerateSlug(ads.Name);
                }
                else
                {
                    ads.Url = Utils.Utility.GenerateSlug(ads.Url);
                }

                var result = zoneBCL.AddReturnObj(ads);
                if (result != null && result.Id > 0)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "zone");
                    MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
                    responseData.Id = result.Id;
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
        public ResponseData Update([FromBody]Zone ads)
        {
            ResponseData responseData = new ResponseData();
            MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
            try
            {
                ads.ModifiedDate = DateTime.Now;

                if (String.IsNullOrWhiteSpace(ads.Url))
                {
                    ads.Url = Utils.Utility.GenerateSlug(ads.Name);
                }
                else
                {
                    ads.Url = Utils.Utility.GenerateSlug(ads.Url);
                }
                bool result = zoneBCL.Update(ads);
                if (result)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "zone");
                    MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
        [HttpPut("UpdateSort")]
        public ResponseData UpdateSort([FromBody]Zone ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {

                bool result = zoneBCL.UpdateSort(ads);
                if (result)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "zone");
                    MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
        [HttpPut("UpdateShowLayout")]
        public ResponseData UpdateShowLayout([FromBody]Zone ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ads.IsShowHome = ads.IsShowHome == true ? false : true;
                bool result = zoneBCL.UpdateShowLayout(ads);
                if (result)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "zone");
                    MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {

            }

            return responseData;
        }
        [HttpPut("UpdateShowSearch")]
        public ResponseData UpdateShowSearch([FromBody]Zone ads)
        {
            ResponseData responseData = new ResponseData();

            try
            {
                if (ads.IsShowSearch == true)
                {
                    ads.IsShowSearch = false;
                }
                else
                {
                    ads.IsShowSearch = true;
                }
                bool result = zoneBCL.UpdateShowSearch(ads);
                if (result)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "zone");
                    MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }


        [HttpPost("MergeLang")]
        public ResponseData Merge([FromBody]ZoneInLanguage ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                if (String.IsNullOrWhiteSpace(ads.Url))
                {
                    ads.Url = Utils.Utility.GenerateSlug(ads.Name);
                }
                else
                {
                    ads.Url = Utils.Utility.GenerateSlug(ads.Url);
                }
                var exist = zoneLangBCL.ExistUrl(ads.Url, ads.ZoneId);
                if (!exist)
                {
                    bool result = zoneLangBCL.Merge(ads);
                    if (result)
                    {
                        responseData.Success = true;
                        responseData.Message = "Thành công";
                    }
                }
                else
                {
                    responseData.Message = "Đường dẫn đã tồn tại";
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
            MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;

            try
            {
                bool ads = zoneBCL.UpdateTrangThai(new Zone { Id = id, Status = (byte)MI.Entity.Enums.StatusZone.Delete });
                if (ads)
                {
                    //Clear cache
                    MI.Cache.RedisUtils.DeleteAllCacheAsyn(_multiplexer, _configuration, "zone");
                    MI.Cache.RamCache.LastestLoadZone = DateTime.MinValue;
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
