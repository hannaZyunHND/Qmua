using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.CustomClass;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankInstallmentController : ControllerBase
    {
        BankInstallmentBCL bankInstallmentBCL;
        public BankInstallmentController()
        {
            bankInstallmentBCL = new BankInstallmentBCL();
        }
        [HttpGet("Get")]
        public IActionResult Get(string tuKhoa)
        {
            try
            {
                var data = bankInstallmentBCL.Find(tuKhoa);
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new KeyValuePair<string, string>());
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var data = bankInstallmentBCL.FindAll();
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new List<string>());
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                BankInstallmentDetail bankInstallmentDetail = new BankInstallmentDetail();
                var data = bankInstallmentBCL.FindById(x => x.Id == id);
                if(data.InfoCard != null)
                {
                    data.InfoCard = data.CheckJson();
                }
                else
                {
                    data.InfoCard = JsonConvert.SerializeObject(bankInstallmentDetail);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {

            }
            return Ok(new List<string>());
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] BankInstallment obj)
        {
            KeyValuePair<bool, string> responseData = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                responseData = bankInstallmentBCL.AddOrUpdate(obj);
            }
            catch (Exception ex)
            {

            }
            return Ok(responseData);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = bankInstallmentBCL.Remove(id);
                return Ok(data);
            }
            catch (Exception ex)
            {

            }

            return Ok(new KeyValuePair<bool, string>(false, "Thất bại"));
        }

        [HttpGet("GetCardType")]
        public IActionResult GetCardType()
        {
            try
            {
                var TypeCustomer = MI.Entity.EnumHelper.ToList(typeof(MI.Entity.Enums.BankInstallment.CardType), true);

                return Ok(TypeCustomer);
            }
            catch (Exception e)
            {
            }

            return Ok();
        }
    }
}
