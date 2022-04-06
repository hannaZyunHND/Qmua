using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Dapper.Data.ViewModels;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly IManufacturerRepository _manufacturerRepository;
        ManufacturerBCL manufacturerBCL;

        public ManufacturerController(IHostingEnvironment env, IManufacturerRepository manufacturerRepository)
        {
            manufacturerBCL = new ManufacturerBCL();
            _env = env;
            _manufacturerRepository = manufacturerRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ResponseData> GetAll()
        {
            var responseData = new ResponseData();
            try
            {
               var data = manufacturerBCL.FindAll();
                //var data = await _manufacturerRepository.GetAllManufacturer();
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
                int total = 0;
                // var data = manufacturerBCL.Get(fillter, out total);
                var data = await _manufacturerRepository.GetAllPagingManufacturer(filter);
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
                var ads = await _manufacturerRepository.GetManufacturerById(id);
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
                var ads = await _manufacturerRepository.UnPublish(id);
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
        public async Task<ResponseData> Add(ManufacturerViewModel manufacturer)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                // bool ads = manufacturerBCL.Add(manufacturer);
                var ads = await _manufacturerRepository.AddManufacturer(manufacturer);
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
        public async Task<ResponseData> Update(ManufacturerViewModel manufacturer)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                // bool ads = manufacturerBCL.Update(manufacturer);
                var ads = await _manufacturerRepository.UpdateManufacturer(manufacturer);
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

        [HttpDelete("Delete")]
        public ResponseData Delete(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = manufacturerBCL.Remove(id);
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
    }
}
