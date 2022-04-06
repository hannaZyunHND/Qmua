using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Locations.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PhamGiaLandingPage.Web.Controllers
{
    public class CookieController : BaseController
    {
        private readonly ILocationsRepository _locationsRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public CookieController(ILocationsRepository locationsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _locationsRepository = locationsRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult ChangeCurrentLocation(int location_id, string location_name)
        {

            CookieOptions cookie = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                Secure = false,
                IsEssential = true
            };
            var cookie_current_location = _httpContextAccessor.HttpContext.Request.Cookies[CookieLocationId];
            if (cookie_current_location != null)
            {
                //Xoa cookie hien tai co san
                Response.Cookies.Delete(CookieLocationId);
                Response.Cookies.Delete(CookieLocationName);
                //Create cookie moi
                Response.Cookies.Append(CookieLocationId, location_id.ToString(), cookie);
                Response.Cookies.Append(CookieLocationName, location_name, cookie);
            }
            return Ok("Set Location successful!");
        }

        public IActionResult SetCookieDefault()
        {
            CookieOptions cookie = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                Secure = false,
                IsEssential = true
            };
            if (Request.Cookies[CookieLocationId] == null)
            {
                var location_default = _locationsRepository.GetLocationFirst(CurrentLanguageCode);
                if (location_default != null)
                {
                    Response.Cookies.Append(CookieLocationId, location_default.Id.ToString(), cookie);
                    Response.Cookies.Append(CookieLocationName, location_default.Name, cookie);
                    ViewBag.LocationId = location_default.Id;
                    ViewBag.LocationName = location_default.Name;
                }
            }
            return Ok("Set Cookie successful!");
        }
    }
}