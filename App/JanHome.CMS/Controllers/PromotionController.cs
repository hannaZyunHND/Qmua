using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using Utils;


namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        PromotionInLanguageBCL promotionInLanguageBCL;
        PromotionBCL promotionBCL;
        private readonly IPromotionRepository _promotionRepository;

        public PromotionController(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
            promotionInLanguageBCL = new PromotionInLanguageBCL();
            promotionBCL = new PromotionBCL();
        }

        [HttpGet("GetPromotionByProductId")]
        public async Task<IActionResult> GetPromotionByProductId(int productId)
        {
            var result = await _promotionRepository.GetListPromotionByProductId(productId);
            return Ok(result);
        }
        [HttpGet("GetAllNamePromotion")]
        public IActionResult GetAllNamePromotion()
        {
            var result = promotionBCL.GetName();
            return Ok(result);
        }

        [HttpPost("CreatePromotionWithProduct")]
        public async Task<IActionResult> CreatePromotionWithProduct(
            [FromBody] List<CreatePromotionWithProduct> promotionWithProducts)
        {
            ResponseData responseData = new ResponseData();
            promotionWithProducts = promotionWithProducts.DistinctBy(x => new { x.Id, x.ProductId }).ToList();
            var result = await _promotionRepository.AddPromotionInProduct(promotionWithProducts);
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
            return Ok(responseData);
        }


        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = promotionBCL.FindAll().ToList();
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
        public ResponsePaging Get(Utils.FilterPromotion filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = promotionBCL.Get(filter, out total);

                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.IsDiscountPrice,
                    x.Type,
                    Value = Utils.Utility.ConvertCurentce(x.Value.ToString()),
                    TypeName = MI.Entity.Enums.Promotion.PromotionTypeGetByKey(x.Type),
                    x.Name,
                    x.LangCount,
                    x.Status,
                    StartDate = x.StartDate != null ? x.StartDate.Value.ToString("dd/MM/yyyy") : "",
                    EndDate = x.EndDate != null ? x.EndDate.Value.ToString("dd/MM/yyyy") : "",
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("GetAllPaging")]
        public ResponsePaging GetAllPaging(Utils.FilterQuery filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = promotionBCL.GetAll(filter, out total);

                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.Name,
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
                var ads = promotionBCL.FindById(x => x.Id == id, x => x.PromotionInLanguage);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                    responseData.ListData = ads.PromotionInLanguage.ToList<object>();
                    ads.PromotionInLanguage = new List<PromotionInLanguage>();
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }

        [HttpPost("Add")]
        public ResponseData Add([FromBody] MI.Entity.Models.Promotion ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                ads.Status = (byte)MI.Entity.Enums.StatusPromotion.Public;
                int result1 = promotionBCL.CreateProduct(ads);
                if (result1 > 0)
                {

                    responseData.Data = new { promotionId = result1 };
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
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
        public ResponseData Update([FromBody] MI.Entity.Models.Promotion ads)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = promotionBCL.Update(ads);
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

        [HttpPut("UpdateStatus")]
        public IActionResult UpdateStatus(int id, byte status)
        {
            var result = new KeyValuePair<bool, string>(false, "Thất bại");
            result = promotionBCL.UpdateStatus(new MI.Entity.Models.Promotion { Id = id, Status = status });
            return Ok(result);
        }
    }
}
