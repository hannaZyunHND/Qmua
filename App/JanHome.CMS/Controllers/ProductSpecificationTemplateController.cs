using System;
using System.Linq;
using System.Threading.Tasks;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Common;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecificationTemplateController : ControllerBase
    {
        private readonly IProductSpecificationTemplateRepository _productSpecificationTemplateRepository;

        public ProductSpecificationTemplateController(
            IProductSpecificationTemplateRepository productSpecificationTemplateRepository)
        {
            _productSpecificationTemplateRepository = productSpecificationTemplateRepository;
        }


        [HttpGet("GetAll")]
        public async Task<ResponseData> GetAll()
        {
            var responseData = new ResponseData();
            try
            {
                var data = await _productSpecificationTemplateRepository.GetAll();
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.ListData = data.ToList<object>();
            }
            catch (Exception ex)
            {
            }

            return responseData;
        }


        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] FilterQuery filter)
        {
            try
            {
                var data = await _productSpecificationTemplateRepository.GetAllPaging(filter);
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
                // var ads = manufacturerBCL.FindById(id);
                var ads = await _productSpecificationTemplateRepository.GetById(id);
                return Ok(ads);
            }
            catch (Exception ex)
            {
            }

            return Ok();
        }

        [HttpPost("UnPublish")]
        public async Task<ResponseData> UnPublish(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = await _productSpecificationTemplateRepository.UnPublish(id);
                if (ads > 0)
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
        public async Task<ResponseData> Add(ProductSpecificationTemplateViewModel productSpecificationTemplateViewModel)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = await _productSpecificationTemplateRepository.AddProductSpecificationTemplate(
                    productSpecificationTemplateViewModel);
                if (ads > 0)
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

        [HttpPut("Update")]
        public async Task<ResponseData> Update(
            ProductSpecificationTemplateViewModel productSpecificationTemplateViewModel)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ads = await _productSpecificationTemplateRepository.UpdateProductSpecificationTemplate(
                    productSpecificationTemplateViewModel);
                if (ads > 0)
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
