using MI.Bo.Bussiness;
using MI.Entity.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMenu.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInPromotionController : ControllerBase
    {
        ProductInPromotionBCL productInPromotionBCL;
        PromotionInLanguageBCL promotionInLanguageBCL;
        PromotionBCL promotions;
        public ProductInPromotionController()
        {
            productInPromotionBCL = new ProductInPromotionBCL();
            promotionInLanguageBCL = new PromotionInLanguageBCL();
            promotions = new PromotionBCL();
        }


        [HttpGet("GetByProductId")]
        public ResponseTwoList GetByProductId(int productId)
        {
            ResponseTwoList responseData = new ResponseTwoList();
            try
            {
                var promotion = promotions.FindAll(x => x.Status != 3).ToDictionary(x => x.Id, x => x);
                var promotioninProduct = productInPromotionBCL.FindAll(x => x.ProductId == productId);

                responseData.ListObj1 = promotion.Values.Where(x =>
                !promotioninProduct.Select(d => d.PromotionId).Contains(x.Id)).Select(x => new { Id = 0, PromotionId = x.Id, ProductId = productId, Name = x.Name }).ToList<object>();
                responseData.ListObj2 = promotioninProduct.Select(x => new { x.Id, PromotionId = x.PromotionId, ProductId = x.ProductId, Name = promotion.ContainsKey(x.PromotionId ?? 0) ? promotion[x.PromotionId.Value].Name : "" }).ToList<object>();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpPost("Add")]
        public ResponseData Add([FromBody] ViewModel.ProductInPromotions obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = productInPromotionBCL.Add(obj.Id, obj.Promotions);
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

    }
}
