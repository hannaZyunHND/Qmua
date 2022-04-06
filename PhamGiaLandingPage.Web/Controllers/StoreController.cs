using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhamGiaLandingPage.Web.Services.Locations.Repository;
using PhamGiaLandingPage.Web.Services.Locations.ViewModal;
using PhamGiaLandingPage.Web.Services.Store.Repository;
using PhamGiaLandingPage.Web.Services.Store.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StoreList()
        {
            List<StoreResponse> model = new List<StoreResponse>();
            try
            {
                var location =
                model = _storeRepository.GetAllDepartment(CurrentLanguageCode);
            }
            catch (Exception ex)
            {

            }
            ViewBag.ListDepartment = model;
            return View(model);
        }

        public IActionResult ListDepartmentByLocaion(int locationID)
        {
            List<StoreResponse> lstData = new List<StoreResponse>();
            try
            {
                lstData = _storeRepository.GetDepartmentByLocationID(CurrentLanguageCode, locationID);
            }
            catch (Exception ex)
            {

            }
            ViewBag.ListDepartment = lstData;
            return PartialView(lstData);
        }
        //[HttpPost]
        public IActionResult ListDeptByLocId(int id, int needView = 1)
        {
            List<StoreResponse> model = new List<StoreResponse>();
            try
            {
                model = _storeRepository.GetDepartmentByLocationID(CurrentLanguageCode, id);
                //return Ok(JsonConvert.SerializeObject(lstData));
                ViewBag.ListDepartment = model;
                if (needView == 1)
                    return View(model);
                else
                    return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //[HttpPost]


        [HttpPost]
        public IActionResult NearLocation(StoreViewModel storeViewModel)
        {
            List<StoreResponse> storeViewModels = new List<StoreResponse>();
            int total = 0;
            try
            {
                double x = Convert.ToDouble(storeViewModel.Longitude);
                storeViewModels = _storeRepository.GetNearLocation(storeViewModel.Longitude, storeViewModel.Latitude, storeViewModel.Distance, storeViewModel.LanguageCode, storeViewModel.SortOrder, out total);
            }
            catch (Exception ex)
            {

            }
            return Ok(storeViewModels);
        }
        [HttpPost]
        public IActionResult NearLocationHTML()
        {
            return ViewComponent("StoreListHTML");
        }
    }
}