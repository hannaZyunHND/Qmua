using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateKAO.Controllers
{
    public class P404Controller : BaseController
    {
        public IActionResult P404()
        {
            return View();
        }
    }
}