using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationsController : ControllerBase
    {
        SpecificationsBCL specificationsBCL;
        ProductSpecificationsBCL productSpecificationsController;
        public ProductSpecificationsController()
        {
            productSpecificationsController = new ProductSpecificationsBCL();
            specificationsBCL = new SpecificationsBCL();
        }

        [HttpGet("GetByProductId")]
        public IActionResult GetByProductId(int productId)
        {
            dynamic responseData = new System.Dynamic.ExpandoObject();
            try
            {
                var productSpecification = productSpecificationsController.FindAll(x => x.ProductId == productId);
                var Specification = specificationsBCL.GetAll(productSpecification.Select(x => x.SpectificationId ?? 0).ToList());

                var DicName = specificationsBCL.FindAll().ToDictionary(x => x.Id, x => x);

                responseData.ListObj1 = Specification;
                responseData.ListObj2 = productSpecification.Select(x => new
                {
                    x.Id,
                    x.ProductId,
                    x.Value,
                    x.IsShowLayout,
                    x.SpectificationId,
                    Name = DicName.ContainsKey(x.SpectificationId ?? 0) ? DicName[x.SpectificationId ?? 0].Name : "",
                    x.LanguageCode,
                    Hidden = x.LanguageCode.Trim() != Utils.Utility.DefaultLang
                }).ToList<object>();

            }
            catch (Exception ex)
            {

            }
            return Ok(responseData);
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]ViewModel.ProductInSpecifications obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                if (obj.Specifications != null && obj.Specifications.Count > 0)
                {
                    foreach (var item in obj.Specifications)
                    {
                        if (String.IsNullOrEmpty(item.LanguageCode))
                        {
                            item.LanguageCode = Utils.Utility.DefaultLang;
                        }

                    }
                }
                bool result = productSpecificationsController.Add(obj.Id, obj.Specifications);
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
