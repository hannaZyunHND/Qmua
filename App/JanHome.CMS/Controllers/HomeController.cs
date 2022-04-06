
using JanHome.CMS.ExcelHelper;
using MI.Dal.IDbContext;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace JanHome.CMS.Controllers
{
    public class HomeController : Controller
    {
        //IDbContext _db = new IDbContext();


        public IActionResult Index()
        {

            return View();

        }
        

        public IActionResult Error()
        {
            return View();
        }
    }
}
