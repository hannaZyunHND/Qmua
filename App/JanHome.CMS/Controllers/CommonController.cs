using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Dapper.Data.Constant;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity;
using MI.Entity.Common;
using MI.Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using Promotion = MI.Entity.Enums.Promotion;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public CommonController(ILanguageRepository languageRepository, IManufacturerRepository manufacturerRepository)
        {
            _languageRepository = languageRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        [HttpGet("GetAllLanguageOptions")]
        public async Task<List<Languages>> GetAllLanguageOptions()
        {
            try
            {
                var result = await _languageRepository.GetAllLanguage();
                return result;
            }
            catch (Exception e)
            {
            }

            return new List<Languages>();
        }


        [HttpGet("GetAllManufacturerOptions")]
        public async Task<List<Manufacturers>> GetAllManufacturerOptions()
        {
            try
            {
                var result = await _manufacturerRepository.GetAllManufacturer();
                return result;
            }
            catch (Exception e)
            {
            }

            return new List<Manufacturers>();
        }
        [HttpGet("GetAllTypeMaterial")]
        public ResponseData GetAllTypeMaterial()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.MaterialType), true);
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetAllStatusOptions")]
        public List<StatusViewModel> GetAllStatusOptions()
        {
            try
            {
                var result = ListConstant.ListStatusViewModel;
                return result;
            }
            catch (Exception e)
            {
            }

            return new List<StatusViewModel>();
        }
        [HttpGet("GetAllStatusComment")]
        public ResponseData GetAllStatusComment()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.StatusComment), true);
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetAllTypeOrder")]
        public ResponseData GetAllTypeOrder()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstOrderType = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.OrderType), true);
                responseData.ListData = lstOrderType.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("GetAllTypeComment")]
        public ResponseData GetAllTypeComment()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.CommentType), true);
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("GetAllStatusContact")]
        public ResponseData GetAllStatusContact()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lststatus = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.StatusContact), true);
                responseData.ListData = lststatus.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("GetAllTypeContact")]
        public ResponseData GetAllTypeContact()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstType = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.TypeContact), true);
                responseData.ListData = lstType.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("GetAllTypeOptions")]
        public List<TypeViewModel> GetAllTypeOptions()
        {
            try
            {
                var lstType = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.TypeArticle), true);
                var result = lstType.Cast<dynamic>().Select(x => new TypeViewModel { Id = (int)x.Key, Label = x.Value }).ToList();
                return result;
            }
            catch (Exception e)
            {
            }

            return new List<TypeViewModel>();
        }



        [HttpGet("ProductUnitGet")]
        public ResponseData ProductUnitGet()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.ProductUnit), true);
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("PromotionTypeGet")]
        public ResponseData PromotionTypeGet()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = Promotion.Type;
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("OrderStatusGet")]
        public ResponseData OrderStatusGet()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstData = StatusOrders.ListStatusOrders;
                responseData.ListData = lstData.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }

        [HttpGet("PropertyGetGroupId")]
        public ResponseData PropertyGetGroupId()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.GroupProp), true);
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("PropertyGetPosition")]
        public ResponseData PropertyGetPosition()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstPositionProp = MI.Entity.EnumHelper.ToList2(typeof(MI.Entity.Enums.PositionProp), true);
                responseData.ListData = lstPositionProp.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }


        [HttpGet("ConfigTypeValueGet")]
        public ResponseData ConfigTypeValueGet()
        {
            ResponseData responseData = new ResponseData();
            try
            {
                var lstProductUnit = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.ConfigType), true);
                responseData.ListData = lstProductUnit.Cast<object>().ToList();
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }


        [HttpGet("GetAllTypeCustomer")]
        public IActionResult GetAllTypeCustomer()
        {
            try
            {
                var TypeCustomer = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.TypeCustomer), true);

                return Ok(TypeCustomer);
            }
            catch (Exception e)
            {
            }

            return Ok();
        }
        [HttpGet("GetAllSourceCustomer")]
        public IActionResult GetAllSourceCustomer()
        {
            try
            {
                var SourceCustomer = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.SourceCustomer), true);

                return Ok(SourceCustomer);
            }
            catch (Exception e)
            {
            }

            return Ok();
        }
    }
}
