using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Common;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponInProductBCL couponInProductBCL;
        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            couponInProductBCL = new CouponInProductBCL();
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var responseData = new ResponseData();
            try
            {
                var ads = MI.Cache.RamCache.DicCoupon;
                return Ok(ads.Values.ToList());
            }
            catch (Exception ex)
            {
            }
            return Ok(new List<MI.Entity.Models.Coupon>());
        }

        [HttpGet("UnPublish")]
        public async Task<ResponseData> Unpublish(int id, int type)
        {
            var responseData = new ResponseData();
            try
            {
                var ads = await _couponRepository.Unpublish(id, type);
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

        [HttpPost("Add")]
        public async Task<ResponseData> Add(CouponViewModel coupon)
        {
            var responseData = new ResponseData();
            responseData.Id = 0;
            try
            {
                MI.Cache.RamCache.LastestLoadCoupon = DateTime.MinValue;
                if (coupon.DiscountOption == 1 && int.Parse(coupon.ValueDiscount) > 30)
                {
                    responseData.Success = false;
                    responseData.Message = "Giảm giá tối đa 30%";
                }
                else
                {
                    var ads = await _couponRepository.Create(coupon);
                    if (ads.Key > 0)
                    {
                        responseData.Success = true;
                        responseData.Message = "Thành công";
                        responseData.Id = ads.Key;
                    }
                    else
                    {
                        responseData.Message = ads.Value;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpPut("Update")]
        public async Task<ResponseData> Update(CouponViewModel coupon)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                if (coupon.DiscountOption == 1 && int.Parse(coupon.ValueDiscount) > 30)
                {
                    responseData.Success = false;
                    responseData.Message = "Giảm giá tối đa 30%";
                }
                else
                {
                    MI.Cache.RamCache.LastestLoadCoupon = DateTime.MinValue;
                    var ads = await _couponRepository.Update(coupon);
                    if (ads.Key)
                    {
                        responseData.Success = true;
                        responseData.Message = "Thành công";
                    }
                    else
                    {
                        responseData.Message = ads.Value;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpGet("GetAllPaging")]
        public async Task<IActionResult> Get([FromQuery] FilterQuery filter)
        {
            try
            {
                var data = await _couponRepository.GetAllPaging(filter);
                return Ok(data);
            }
            catch (Exception ex)
            {
            }
            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = await _couponRepository.GetById(id);
                return Ok(ads);
            }
            catch (Exception ex)
            {
            }
            return Ok();
        }


        [HttpGet("GetProductInCounpon")]
        public IActionResult GetProductInCounpon(int id)
        {
            dynamic result = new System.Dynamic.ExpandoObject();
            try
            {
                var products = MI.Cache.RamCache.DicProduct;
                var data = couponInProductBCL.FindAll(x => x.CouponId == id);
                result.ListData = data.Select(x => new { x.CouponChildMa, x.CouponId, x.ProductId, x.Type, x.Value, Name = products.ContainsKey(x.ProductId) ? products[x.ProductId] : "" }).ToList();
                result.ListProduct = products.Select(x => new { id = x.Key, label = x.Value }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
            }
            return Ok(result);
        }

        [HttpPost("AddProductInCounpon")]
        public IActionResult AddProductInCounpon(ViewModel.CouponInProductVM obj)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>();
            try
            {
                if (obj.id > 0)
                {
                    if (obj.lstObj.Any(x => x.Value > 30 && x.Type == 1))
                    {
                        result = new KeyValuePair<bool, string>(false, "Mã giảm giá tối đa 30%");
                    }
                    else
                    {
                        var res = couponInProductBCL.Merge(obj.lstObj, obj.id);
                        if (res)
                            result = new KeyValuePair<bool, string>(true, "Thành công");
                        else
                            result = new KeyValuePair<bool, string>(false, "Thất bại");
                    }
                }
                else
                {
                    result = new KeyValuePair<bool, string>(false, "Bạn cần tạo mã giảm giá trước khi thêm sản phẩm vào");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
            }
            return Ok(result);
        }
    }
}
