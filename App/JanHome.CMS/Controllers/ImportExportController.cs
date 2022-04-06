using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.ExcelHelper;
using MI.Dal.IDbContext;
using MI.Dapper.Data.Repositories.Interfaces;
using MI.Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExportController : ControllerBase
    {
        private readonly IExportUtility _export;
        IProductRepository _productRepository;
        IHostingEnvironment _hostingEnvironment;
        public ImportExportController(IProductRepository productRepository, IHostingEnvironment hostingEnvironment, IExportUtility export)
        {

            _productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;
            _export = export;

        }
        [Route("ExportProductPriceLocation")]
        public IActionResult Test([FromBody] FilterProduct filter)
        {

            var x = _export.ExportProductPriceLocationExcel(filter);
            return Ok(x);
        }
        [Route("ExportSpectification")]
        public IActionResult TestMaintain([FromBody] FilterProduct filter)
        {

            var x = _export.ExportMaintainSpectificationInProductExcel(filter);
            return Ok(x);
        }
        //[HttpPost]
        //public string ExportViewCountArticle([FromBody] FilterArticleWithMonth filter)
        //{
        //    var result = "";
        //    try
        //    {
        //        filter.pageIndex = 1;
        //        filter.pageSize = 1000000;
        //        int total = 0;
        //        var data = _articleBcl.GetPage(filter, out total);
        //        var ls_id = new List<int>();
        //        Parallel.ForEach(data, it =>
        //        {
        //            ls_id.Add(it.Id);
        //        });
        //        var str_lsId = string.Join(',', ls_id);



        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return result;
        //}
        //[Route("TestImport")]
        //public IActionResult TestImport()
        //{
        //    var x = _export.ImportProductPriceLocationExcel();
        //    return Ok(x);
        //}
    }
}
