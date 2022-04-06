using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using TemplateKAO.Models;
using TemplateKAO.Services.Locations.Repository;
using TemplateKAO.Services.Zone.Repository;
using TemplateKAO.Utility;
using Utils;

namespace TemplateKAO.Controllers
{
    public class HomeController : BaseController
    {
        private IHostingEnvironment _env;
        private readonly IZoneRepository _zoneRepository;
        private readonly ILocationsRepository _locationsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICookieUtility _cookieUtility;
        private readonly IActionContextAccessor _accessor;
        public HomeController(IHostingEnvironment envrnmt, IZoneRepository zoneRepository, ILocationsRepository locationsRepository, IHttpContextAccessor httpContextAccessor, ICookieUtility cookieUtility, IActionContextAccessor accessor)
        {
            _zoneRepository = zoneRepository;
            _locationsRepository = locationsRepository;
            _cookieUtility = cookieUtility;
            _httpContextAccessor = httpContextAccessor;
            _env = envrnmt;
            _accessor = accessor;

        }
        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            CookieOptions cookie = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true, //<- there
                Expires = DateTime.Now.AddMonths(1),
            };
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                cookie
            );

            return RedirectToAction("IndexPublic");
        }
        public ActionResult RedirectToDefaultCulture()
        {
            var culture = CurrentLanguage;
            if (string.IsNullOrEmpty(culture))
                culture = "vi";

            return RedirectToAction("IndexPublic");
        }
        public IActionResult Index()
        {


            return View();
            //return Ok("Vao trang chu thanh cong");
            //return BadRequest("Vao ma loi");
        }

        public IActionResult DichVu(string alias)
        {
            return View();
        }
        public IActionResult Introduce()
        {
            return View();
        }

        public IActionResult IndexPublic()
        {
            //Console.WriteLine(customer_ip);
            //GetIpValue(out customer_ip);
            //Lay location from IP

            //Kiem tra co ton tai cookie location_id khong
            CookieOptions cookie = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                Secure = false,
                IsEssential = true
            };
            if (Request.Cookies[CookieLocationId] == null)
            {
                //Lay IP
                var customer_ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
                var demo_ip = "27.76.210.115";
                //Lay thong tin location
                using (var _http = new HttpClient())
                {
                    var url_request = "https://freegeoip.app/json/" + customer_ip;
                    var res = _http.GetAsync(url_request).Result;
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var x = res.Content.ReadAsStringAsync().Result;
                        var ip_info = Newtonsoft.Json.JsonConvert.DeserializeObject<FreeGeoIPInfo>(x);
                        if (!string.IsNullOrEmpty(ip_info.region_name) || ip_info.region_code != "null")
                        {
                            var n = ip_info.region_name.Replace("Tinh", "").TrimStart();

                            //Kiem tra region 
                            var location_affected = _locationsRepository.GetLocations(CurrentLanguageCode).Where(r => UIHelper.ChuyenCoDauThanhKhongCoDau(r.Name).Contains(n)).FirstOrDefault();
                            if (location_affected != null)
                            {
                                cookie.Expires = DateTime.Now.AddDays(7);
                                Response.Cookies.Append(CookieLocationId, location_affected.Id.ToString(), cookie);
                                Response.Cookies.Append(CookieLocationName, location_affected.Name, cookie);
                                ViewBag.LocationId = location_affected.Id;
                                ViewBag.LocationName = location_affected.Name;
                            }
                        }

                    }
                    else
                    {
                        var location_default = _locationsRepository.GetLocationFirst(CurrentLanguageCode);
                        if (location_default != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Append(CookieLocationId, location_default.Id.ToString(), cookie);
                            Response.Cookies.Append(CookieLocationName, location_default.Name, cookie);
                            ViewBag.LocationId = location_default.Id;
                            ViewBag.LocationName = location_default.Name;
                        }
                    }

                }
            }
            return View();
        }
        public IActionResult TestHomNay()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SwitchRegion(int region_id)
        {
            return ViewComponent("SwitchRegion", new { region_id = region_id });
        }
        [HttpPost]
        public IActionResult ViewMoreRegion(int zone_parent_id, int locationId, int skip, int size)
        {
            return ViewComponent("ViewMoreRegion", new { zone_parent_id = zone_parent_id, locationId = locationId, skip = skip, size = size });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("~/Views/P404/P404.cshtml");
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
