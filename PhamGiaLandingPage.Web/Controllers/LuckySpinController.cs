using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.LuckySpin.Repository;
using PhamGiaLandingPage.Web.Services.LuckySpin.ViewModal;
using System;
using System.Collections.Generic;
using LuckySpinModel = PhamGiaLandingPage.Web.Services.LuckySpin.ViewModal.LuckySpinModel;

namespace PhamGiaLandingPage.Web.Controllers
{
    public class LuckySpinController : BaseController
    {
        private readonly ILuckySpinRepository _luckySpinRepository;
        public LuckySpinController(ILuckySpinRepository luckySpinRepository)
        {
            _luckySpinRepository = luckySpinRepository;

        }
        [HttpGet]
        public IActionResult GetCustomLucky()
        {
            var models = new List<CustomLuckyModel>();
            models = _luckySpinRepository.CustomLucky_GetTop(15);
            var temp = new List<CustomLuckyModel>();
            foreach (var element in models)
            {
                element.PhoneNumber = element.PhoneNumber.Substring(0, element.PhoneNumber.Length - 2) + "**";
                temp.Add(element);
            }
            return Ok(models);

        }
        [HttpGet]
        public IActionResult GetLuckySpin()
        {
            var models = new List<LuckySpinModel>();
            models = _luckySpinRepository.LuckySpin_GetAll();
            return Ok(models);

        }
        [HttpGet]
        public IActionResult CustomLuckyCheckExist(string phoneNumber)
        {
            if (phoneNumber.Trim().Length < 10)
            {
                return Ok(false);
            }
            var flag = _luckySpinRepository.CustomLucky_CheckExist(phoneNumber);
            return Ok(flag);
        }
        [HttpPost]
        public IActionResult CustomLuckyAdd(string phoneNumber, string value)
        {
            if (phoneNumber.Length < 10)
            {
                return Ok(false);
            }
            var flag = _luckySpinRepository.CustomLucky_CheckExist(phoneNumber.Trim());
            if (!flag)
            {
                return Ok(_luckySpinRepository.CustomLucky_Add(phoneNumber, value));
            }

            return Ok(false);
        }
        public IActionResult LuckySpin()
        {
            return View();
        }
    }
}