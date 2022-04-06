using MI.Entity.Common;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using JanHome.CMS.Web.Utils;
using Utils;
using System.Collections.Generic;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        LocationBCL locationBCL;
        DepartmentInLanguageBCL departmentInLanguageBCL;
        DepartmentBCL departmentBCL;
        public DepartmentController()
        {
            departmentInLanguageBCL = new DepartmentInLanguageBCL();
            departmentBCL = new DepartmentBCL();
            locationBCL = new LocationBCL();
        }

        [HttpGet("GetAll")]
        public ResponseData GetAll()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var data = departmentBCL.FindAll().ToList();
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
        public ResponsePaging Get(FilterDepartment filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = departmentBCL.Get(filter, out total);
                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.Longitude,
                    x.Latitude,
                    x.SortOrder,
                    x.Name,
                    x.LangCount
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
                var ads = departmentBCL.FindById(x => x.Id == id, x => x.DepartmentInLanguage);
                if (ads != null)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                    responseData.ListData = ads.DepartmentInLanguage.OrderBy(n => n.LanguageCode).ToList<object>();
                    ads.DepartmentInLanguage = new List<DepartmentInLanguage>();
                    responseData.Data = ads;
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpPost("Add")]
        public ResponseData Add([FromBody]Department department)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                int result1 = departmentBCL.Create(department);
                if (result1 > 0)
                {
                    responseData.Data = new { departmentId = department.Id };
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
        public ResponseData Update([FromBody]Department department)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result = departmentBCL.Update(department);
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

        [HttpDelete("Delete")]
        public ResponseData Delete(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool result1 = departmentInLanguageBCL.Delete(id);
                if (result1)
                {
                    bool ads = departmentBCL.Remove(id);
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
