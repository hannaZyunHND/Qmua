using MI.Bo.Bussiness;
using MI.Dal.IDbContext;
using MI.Dapper.Data.Models;
using MI.Dapper.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
//using OfficeOpenXml;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace JanHome.CMS.ExcelHelper
{
    public interface IExportUtility
    {
        string ExportProductPriceLocationExcel(FilterProduct filter);
        string ExportMaintainSpectificationInProductExcel(FilterProduct filter);
        string ImportProductPriceLocationExcel(string path);
        string ImportMMaintainSpectificatinInProduct(string path);
        string ExportViewCountArticle(FilterArticleWithMonth filter);
        string ExportViewCountProduct(FilterProductWithMonth filter);
    }
    public class ExportUtility : IExportUtility
    {
        LocationBCL locationsBCL;
        ProductBCL productsBCL;
        ProductPriceInLocationBCL productPriceInLocationBCL;
        ProductInLanguageBCL productInLanguageBCL;
        SpecificationsBCL specificationsBCL;
        ArticleBCL articleBCL;
        IProductRepository _productRepository;
        IArticlesRepository _articlesRepository;
        IHostingEnvironment _hostingEnvironment;
        IConfiguration _configuration;
        public ExportUtility(IProductRepository productRepository, IHostingEnvironment hostingEnvironment, IArticlesRepository articlesRepository, IConfiguration configuration)
        {
            locationsBCL = new LocationBCL();
            productsBCL = new ProductBCL();
            productPriceInLocationBCL = new ProductPriceInLocationBCL();
            productInLanguageBCL = new ProductInLanguageBCL();
            specificationsBCL = new SpecificationsBCL();
            articleBCL = new ArticleBCL();
            _productRepository = productRepository;
            _articlesRepository = articlesRepository;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;

        }
        public string ExportProductPriceLocationExcel(FilterProduct filter)
        {

            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ExportProductPriceInLocation.xlsx";
            //string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            }

            var locations = locationsBCL.GetAll();
            Dictionary<int, string> displayLocation = new Dictionary<int, string>();
            foreach (var item in locations.OrderBy(r => r.Id))
            {
                displayLocation.Add(item.Id, item.Name);
            }
            int total = 0;

            var list = productInLanguageBCL.FindAll(filter, out total);
            using (var excel = new ExcelPackage(file))
            {
                if (list != null)
                {

                    var ls_id = list.Select(p => new { Id = p.Id });
                    var string_int = string.Join(",", ls_id.Select(x => x.Id));
                    //Dung file Excel
                    try
                    {
                        var workSheet = excel.Workbook.Worksheets.Add("Sheet 1");
                        var start = 4;
                        foreach (var item in displayLocation)
                        {
                            var index_excel = 2;
                            var r = _productRepository.ExportExcel(string_int, item.Key);
                            if (start == 4)
                            {
                                workSheet.Cells[1, 1].Value = "Id";
                                workSheet.Cells[1, 2].Value = "Name";
                                workSheet.Cells[1, 3].Value = "Mặc định [0]";
                                //workSheet.Column(1).Style.WrapText = true;
                                //workSheet.Column(2).Style.WrapText = true;
                            }
                            workSheet.Cells[1, start].Value = "[" + item.Key.ToString() + "] " + item.Value;
                            foreach (var p in r)
                            {
                                workSheet.Cells[index_excel, 1].Value = p.Id;
                                workSheet.Cells[index_excel, 2].Value = p.Name;
                                workSheet.Cells[index_excel, 3].Value = string.Format("{0},{1},{2}", p.df_Price, p.df_SalePrice, p.df_DiscountPercent);
                                workSheet.Cells[index_excel, start].Value = string.Format("{0},{1},{2}", p.Price, p.SalePrice, p.DiscountPercent);
                                //workSheet.Column(start).Style.WrapText = true;
                                index_excel++;
                            }
                            start++;
                        }
                        workSheet.Cells.AutoFitColumns();
                        excel.Save();

                        return fileName;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
            return null;
        }

        public string ExportMaintainSpectificationInProductExcel(FilterProduct filter) {
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ExportMaintainSpectificationInProduct.xlsx";
            //string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            }

            var locations = specificationsBCL.GetMaintainSpectifications();
            Dictionary<int, string> displayLocation = new Dictionary<int, string>();
            foreach (var item in locations.OrderBy(r => r.Id))
            {
                displayLocation.Add(item.Id, item.Name);
            }
            //var t1 = 0;
            //var list = productsBCL.Get(1, 10, "Id", "asc", out t1);
            int total = 0;

            var list = productInLanguageBCL.FindAll(filter, out total);

            using (var excel = new ExcelPackage(file))
            {
                if (list != null)
                {

                    var ls_id = list.Select(p => new { Id = p.Id });
                    var string_int = string.Join(",", ls_id.Select(x => x.Id));
                    //Dung file Excel
                    try
                    {
                        var workSheet = excel.Workbook.Worksheets.Add("Sheet 1");
                        var start = 3;
                        foreach (var item in displayLocation)
                        {
                            var index_excel = 2;
                            var r = _productRepository.ExportExcelMaintainSpectification(string_int, item.Key,"vi-VN");
                            if (start == 3)
                            {
                                workSheet.Cells[1, 1].Value = "Id";
                                workSheet.Cells[1, 2].Value = "Name";
                                //workSheet.Column(1).Style.WrapText = true;
                                //workSheet.Column(2).Style.WrapText = true;


                            }
                            workSheet.Cells[1, start].Value = "[" + item.Key.ToString() + "] " + item.Value;
                            foreach (var p in r)
                            {
                                workSheet.Cells[index_excel, 1].Value = p.Id;
                                workSheet.Cells[index_excel, 2].Value = p.Name;
                                workSheet.Cells[index_excel, start].Value = string.Format("{0}", p.SpecValue);
                                //workSheet.Column(start).Style.WrapText = true;
                                index_excel++;
                            }
                            start++;
                        }
                        workSheet.Cells.AutoFitColumns();
                        excel.Save();

                        return fileName;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
            return null;
        }
        public string ImportProductPriceLocationExcel(string path)
        {
            //string sWebRootFolder = _hostingEnvironment.WebRootPath;
            //string sFileName = @"ExportProductPriceInLocation.xlsx";
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;
                    DataTable mergeTbl = new DataTable();
                    mergeTbl.Columns.Add("ProductId", typeof(int));
                    mergeTbl.Columns.Add("LocationId", typeof(int));
                    mergeTbl.Columns.Add("Price", typeof(decimal));
                    mergeTbl.Columns.Add("SalePrice", typeof(decimal));
                    mergeTbl.Columns.Add("DiscountPercent", typeof(decimal));
                    for (int col = 3; col <= colCount; col++)
                    {
                        var tt_location = worksheet.Cells[1, col].Value;
                        string pattern = @"\[(\d+)\]";
                        string input = tt_location.ToString();
                        RegexOptions options = RegexOptions.Multiline;
                        var loc_id = 0;
                        var m = Regex.Matches(input, pattern, options).FirstOrDefault();
                        if (m != null)
                        {
                            loc_id = int.Parse(m.Groups[1].Value.ToString());
                        }
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var product_id = worksheet.Cells[row, 1].Value.ToString();
                            var dl_price = worksheet.Cells[row, col].Value.ToString();
                            if (dl_price != "0,0,0" && dl_price.Contains(",") && !string.IsNullOrEmpty(dl_price))
                            {
                                var res = dl_price.Split(",");
                                decimal res_price = 0;
                                decimal res_salePrice = 0;
                                decimal res_discountPercent = 0;
                                if (res.Length >= 1)
                                {
                                    res_price = decimal.Parse(res[0]);
                                }
                                if (res.Length >= 2)
                                {
                                    res_salePrice = decimal.Parse(res[1]);
                                }
                                if (res.Length >= 3)
                                {
                                    res_discountPercent = decimal.Parse(res[2]);
                                }
                                DataRow dr = mergeTbl.NewRow();
                                dr[0] = product_id;
                                dr[1] = loc_id;
                                dr[2] = res_price;
                                dr[3] = res_salePrice;
                                dr[4] = res_discountPercent;
                                mergeTbl.Rows.Add(dr);
                            }
                        }
                    }
                    Console.WriteLine(mergeTbl);
                    var kq = _productRepository.ImportExcelProductPriceInLocation(mergeTbl);

                    return kq;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return "error";
            }
        }

        public string ImportMMaintainSpectificatinInProduct(string path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            try
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;
                    DataTable mergeTbl = new DataTable();
                    mergeTbl.Columns.Add("ProductId", typeof(int));
                    mergeTbl.Columns.Add("SpecId", typeof(int));
                    mergeTbl.Columns.Add("SpecValue", typeof(string));
                    //mergeTbl.Columns.Add("SalePrice", typeof(decimal));
                    //mergeTbl.Columns.Add("DiscountPercent", typeof(decimal));
                    for (int col = 3; col <= colCount; col++)
                    {
                        var tt_location = worksheet.Cells[1, col].Value;
                        string pattern = @"\[(\d+)\]";
                        string input = tt_location.ToString();
                        RegexOptions options = RegexOptions.Multiline;
                        var loc_id = 0;
                        var m = Regex.Matches(input, pattern, options).FirstOrDefault();
                        if (m != null)
                        {
                            loc_id = int.Parse(m.Groups[1].Value.ToString());
                        }
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var product_id = worksheet.Cells[row, 1].Value.ToString();
                            var dl_price = worksheet.Cells[row, col].Value.ToString();
                            if (!string.IsNullOrEmpty(dl_price))
                            {
                                DataRow dr = mergeTbl.NewRow();
                                dr[0] = product_id;
                                dr[1] = loc_id;
                                dr[2] = dl_price;
                                mergeTbl.Rows.Add(dr);
                            }
                        }
                    }
                    Console.WriteLine(mergeTbl);
                    var kq = _productRepository.ImportExcelMaintainSpectificatinInProduct(mergeTbl);
                    return kq;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return "error";
            }
        }

        public string ExportViewCountArticle(FilterArticleWithMonth filter)
        {
            filter.pageIndex = 1;
            filter.pageSize = 1000000;
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"ExportArticleViewCount.xlsx";
            //string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);
            System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            if (file.Exists)
            {
                file.Delete();
                file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
            }


            //var locations = locationsBCL.GetAll();
            //Dictionary<int, string> displayLocation = new Dictionary<int, string>();
            //foreach (var item in locations.OrderBy(r => r.Id))
            //{
            //    displayLocation.Add(item.Id, item.Name);
            //}
            int total = 0;

            var list = articleBCL.GetPage(filter, out total);
            using (var excel = new ExcelPackage(file))
            {
                if (list != null)
                {
                    var base_domain = _configuration["AppSettings:BaseDomain"];
                    var ls_id = list.Select(p => new { Id = p.Id });
                    var string_int = string.Join(",", ls_id.Select(x => x.Id));
                    //Dung file Excel
                    try
                    {
                        var r = _articlesRepository.ExportExcelArticleViewCount(string_int, filter.year);
                        var workSheet = excel.Workbook.Worksheets.Add("Sheet 1");
                        var start = 2;
                        workSheet.Cells[1, 1].Value = "Id";
                        workSheet.Cells[1, 2].Value = "Tên";
                        
                        workSheet.Cells[1, 3].Value = "Tháng 1";
                        workSheet.Cells[1, 4].Value = "Tháng 2";
                        workSheet.Cells[1, 5].Value = "Tháng 3";
                        workSheet.Cells[1, 6].Value = "Tháng 4";
                        workSheet.Cells[1, 7].Value = "Tháng 5";
                        workSheet.Cells[1, 8].Value = "Tháng 6";
                        workSheet.Cells[1, 9].Value = "Tháng 7";
                        workSheet.Cells[1, 10].Value = "Tháng 8";
                        workSheet.Cells[1, 11].Value = "Tháng 9";
                        workSheet.Cells[1, 12].Value = "Tháng 10";
                        workSheet.Cells[1, 13].Value = "Tháng 11";
                        workSheet.Cells[1, 14].Value = "Tháng 12";
                        workSheet.Cells[1, 15].Value = "Url";
                        foreach (var item in r)
                        {
                            workSheet.Cells[start, 1].Value = item.Id;
                            workSheet.Cells[start, 2].Value = item.Name;
                            workSheet.Cells[start, 3].Value = item.Thang_1;
                            workSheet.Cells[start, 4].Value = item.Thang_2;
                            workSheet.Cells[start, 5].Value = item.Thang_3;
                            workSheet.Cells[start, 6].Value = item.Thang_4;
                            workSheet.Cells[start, 7].Value = item.Thang_5;
                            workSheet.Cells[start, 8].Value = item.Thang_6;
                            workSheet.Cells[start, 9].Value = item.Thang_7;
                            workSheet.Cells[start, 10].Value = item.Thang_8;
                            workSheet.Cells[start, 11].Value = item.Thang_9;
                            workSheet.Cells[start, 12].Value = item.Thang_10;
                            workSheet.Cells[start, 13].Value = item.Thang_11;
                            workSheet.Cells[start, 14].Value = item.Thang_12;
                            workSheet.Cells[start, 15].Formula = "HYPERLINK(\"" + string.Format("{0}/{1}.html",base_domain,item.Url) + "\",\"" + "Link bài" + "\")";


                            start++;
                        }
                        workSheet.Cells.AutoFitColumns();
                        excel.Save();

                        return fileName;
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                        return null;
                    }
                }
            }
            return null;
        }

        public string ExportViewCountProduct(FilterProductWithMonth filter)
        {
            throw new NotImplementedException();
        }
    }
}
