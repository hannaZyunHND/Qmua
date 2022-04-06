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

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInLanguageController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        ProductInLanguageBCL productInLanguageBCL;
        TagBCL tagBCL;
        public ProductInLanguageController(IHostingEnvironment env)
        {
            productInLanguageBCL = new ProductInLanguageBCL();
            tagBCL = new TagBCL();
            _env = env;
        }
        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = productInLanguageBCL.FindAll().ToList();
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
        public ResponsePaging Get(string keyword, int pageIndex, int pageSize, string sortBy, string sortDir)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = productInLanguageBCL.Get(keyword, pageIndex, pageSize, sortBy, sortDir, out total);
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
                var ads = productInLanguageBCL.FindById(id);
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
        public ResponseData Add([FromBody]ProductInLanguage productInLanguage)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                List<int> lstIdTag = new List<int>();

                var response = Utils.Utility.AutoCheckSlug(productInLanguage.Title, productInLanguage.Url);
                if (response.Key)
                {
                    productInLanguage.Url = response.Value;
                    var ExistUrl = productInLanguageBCL.ExistUrl(productInLanguage.Url, productInLanguage.ProductId);
                    if (!ExistUrl)
                    {

                        if (productInLanguage.ListTagName.Count > 0)
                        {
                            lstIdTag = tagBCL.MergeTag(productInLanguage.ListTagName);
                            lstIdTag = lstIdTag.Distinct().ToList();
                        }
                        if (!productInLanguage.Id.ToString().Equals("00000000-0000-0000-0000-000000000000") && !String.IsNullOrEmpty(productInLanguage.Id))
                        {
                            bool result = productInLanguageBCL.Update(productInLanguage, lstIdTag);
                            if (result)
                            {
                                responseData.Success = true;
                                responseData.Message = "Thành công";
                            }
                        }
                        else
                        {
                            productInLanguage.Id = Guid.NewGuid().ToString();
                            bool result = productInLanguageBCL.Add(productInLanguage, lstIdTag);
                            if (result)
                            {
                                responseData.Success = true;

                                responseData.Data = productInLanguage;
                                responseData.Message = "Thành công";
                            }
                        }
                    }
                    else
                    {
                        responseData.Message = "Đường dẫn đã tồn tại";
                    }
                }
                else
                {
                    responseData.Message = response.Value;
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
        public ResponseData Update([FromBody]ProductInLanguage productInLanguage)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var ExistUrl = productInLanguageBCL.ExistUrl(productInLanguage.Url, productInLanguage.ProductId);
                if (!ExistUrl)
                {
                    bool result = productInLanguageBCL.Update(productInLanguage);
                    if (result)
                    {
                        responseData.Success = true;
                        responseData.Message = "Thành công";
                    }
                    else
                    {
                        responseData.Message = "Thất bại";
                    }
                }
                else
                {
                    responseData.Message = "Đường dẫn đã tồn tại";
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
