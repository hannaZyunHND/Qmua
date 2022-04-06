using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using PhamGiaLandingPage.Web.Services.Locations.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Controllers
{
    public class SessionController : BaseController
    {
        private readonly ILocationsRepository _locationsRepository;

        private readonly ISession session;
        const string SessionLocationId = "_LocationId";
        const string SessionLocationName = "_LocationName";

        public SessionController(ILocationsRepository locationsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _locationsRepository = locationsRepository;
            this.session = httpContextAccessor.HttpContext.Session;
        }
        [HttpPost]
        public IActionResult ChangeCurrentLocation(int location_id, string location_name, string currentUrl)
        {
            this.session.SetInt32(SessionLocationId, location_id);
            this.session.SetString(SessionLocationName, location_name);
            return Ok();
        }
    }
}