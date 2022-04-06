using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JanHome.CMS.ViewModel;
using MI.Bo.Bussiness;
using MI.Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Utils;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponChildController : ControllerBase
    {
        public CouponChildBCL couponChildBCL;
        public ProductInZoneBCL productInZoneBCL;
        public ProductBCL productBCL;
        private readonly IHostingEnvironment _env;
        public CouponChildController(IHostingEnvironment env)
        {
            couponChildBCL = new CouponChildBCL();
            productInZoneBCL = new ProductInZoneBCL();
            productBCL = new ProductBCL();
            _env = env;

        }
        [HttpPost("GetAllPaging")]
        public IActionResult Get([FromBody] FilterCouponChild filter)
        {
            dynamic result = new System.Dynamic.ExpandoObject();
            try
            {
                int total = 0;
                var data = couponChildBCL.FindAll(filter, out total);
                var data1 = couponChildBCL.GetCouponChildOrder(data.Select(x => x.Ma).ToList());
                result.data = data.Select(x => new
                {
                    x.CreateBy,
                    x.CreateDate,
                    x.ExprireDate,
                    x.Ma,
                    SoLanSuDung = data1.Where(o => o.Voucher == x.Ma).Count(),
                    DoanhSo = Utils.Utility.ConvertCurentce(data1.Where(o => o.Voucher == x.Ma).Sum(o => o.LogPrice).ToString()),
                    x.Name,
                    x.NumberUseCode,
                    x.Parrent,
                    x.Status,
                    Nhom = MI.Cache.RamCache.DicCoupon.ContainsKey(x.Parrent ?? 0) ? MI.Cache.RamCache.DicCoupon[x.Parrent ?? 0].Code : "",
                });
                result.totals = total;
                return Ok(result);
            }
            catch (Exception ex)
            {
            }
            return Ok();
        }
        [HttpGet("GetById")]
        public IActionResult GetById(string ma)
        {
            try
            {
                var data = couponChildBCL.FindById(x => x.Ma == ma);
                return Ok(data);
            }
            catch (Exception ex)
            {
            }
            return Ok(new MI.Entity.Models.CouponChild());
        }
        [HttpGet("GetByParrentId")]
        public IActionResult GetByParrentId(int parrentId)
        {
            try
            {
                var data = couponChildBCL.GetByParrentId(parrentId);
                return Ok(data.Select(x => new { id = x.Key, label = x.Key + "-" + x.Value }).ToList());
            }
            catch (Exception ex)
            {
            }
            return Ok(new MI.Entity.Models.CouponChild());
        }
        [HttpPost("ChangeStatus")]
        public IActionResult ChangeStatus([FromBody] CouponChild coupon)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                if (coupon.Status == null)
                    coupon.Status = 1;

                if (!String.IsNullOrEmpty(coupon.Ma))
                {
                    if (coupon.Status != 3)
                    {
                        Result = couponChildBCL.UpdateStatus(coupon);
                    }
                    else
                    {
                        bool isSuccess = couponChildBCL.Remove(coupon);
                        if (isSuccess)
                        {
                            Result = new KeyValuePair<bool, string>(true, "Thành công");
                        }
                        else
                        {
                            Result = new KeyValuePair<bool, string>(false, "Thất bại");
                        }
                    }

                }
                else
                    Result = new KeyValuePair<bool, string>(false, "Không được để trống mã");



                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }

        [HttpPost("AddByListProduct")]
        public IActionResult AddByListProduct([FromBody] VoucherAddMulti obj)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                var dicProduct = productBCL.GetVoucherById(obj.ListKey);
                foreach (var item in dicProduct.ToList())
                {
                    dicProduct[item.Key] = String.Join(",", obj.ListVoucher.Union(Utils.Utility.SplitStringToList(dicProduct[item.Key])));
                }

                Result = productBCL.UpdateVoucher(dicProduct);

                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }


        [HttpPost("AddByListZone")]
        public IActionResult AddByListZone([FromBody] VoucherAddMulti obj)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                var lstKey = productInZoneBCL.FindAll(x => obj.ListKey.Contains(x.ZoneId));
                var dicProduct = productBCL.GetVoucherById(lstKey.Select(x => x.ProductId).Distinct().ToList());
                foreach (var item in dicProduct.ToList())
                {
                    dicProduct[item.Key] = String.Join(",", obj.ListVoucher.Union(Utils.Utility.SplitStringToList(dicProduct[item.Key])));
                }
                Result = productBCL.UpdateVoucher(dicProduct);
                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }
        [HttpPost("RemoveByListZone")]
        public IActionResult RemoveByListZone([FromBody] VoucherAddMulti obj)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                var lstKey = productInZoneBCL.FindAll(x => obj.ListKey.Contains(x.ZoneId));
                var dicProduct = productBCL.GetVoucherById(lstKey.Select(x => x.ProductId).Distinct().ToList());
                foreach (var item in dicProduct.ToList())
                {
                    dicProduct[item.Key] = String.Join(",", Utils.Utility.SplitStringToList(dicProduct[item.Key]).Where(x => !obj.ListVoucher.Contains(x)));
                }
                Result = productBCL.UpdateVoucher(dicProduct);
                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }
        [HttpPost("RemoveByListProduct")]
        public IActionResult RemoveByListProduct([FromBody] VoucherAddMulti obj)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                var lstKey = productInZoneBCL.FindAll(x => obj.ListKey.Contains(x.ZoneId));
                var dicProduct = productBCL.GetVoucherById(lstKey.Select(x => x.ProductId).Distinct().ToList());
                foreach (var item in dicProduct.ToList())
                {
                    dicProduct[item.Key] = String.Join(",", Utils.Utility.SplitStringToList(dicProduct[item.Key]).Where(x => !obj.ListVoucher.Contains(x)));
                }
                Result = productBCL.UpdateVoucher(dicProduct);
                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }
        [HttpPost("Merge")]
        public IActionResult Merge([FromBody] CouponChild coupon)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                if (coupon.Status == null)
                    coupon.Status = 1;
                if (coupon.Parrent > 0)
                {
                    if (!String.IsNullOrEmpty(coupon.Ma))
                    {
                        var data = couponChildBCL.Merge(coupon);
                        if (data)
                            Result = new KeyValuePair<bool, string>(data, "Thành công");
                        else
                            Result = new KeyValuePair<bool, string>(data, "Thất bại");
                    }
                    else
                        Result = new KeyValuePair<bool, string>(false, "Không được để trống mã");

                }
                else
                    Result = new KeyValuePair<bool, string>(false, "Không được để trống nhóm");
                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }
        [HttpPost("MergeList")]
        public IActionResult MergeList([FromBody] List<CouponChild> CouponChild)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                var data = couponChildBCL.Merge(CouponChild);
                if (data)
                    Result = new KeyValuePair<bool, string>(data, "Thành công");
                else
                    Result = new KeyValuePair<bool, string>(data, "Thất bại");
                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] CouponChild coupon)
        {
            KeyValuePair<bool, string> Result = new KeyValuePair<bool, string>(false, "Thất bại");
            try
            {
                var data = couponChildBCL.Remove(coupon);
                if (data)
                    Result = new KeyValuePair<bool, string>(data, "Thành công");
                else
                    Result = new KeyValuePair<bool, string>(data, "Thất bại");
                return Ok(Result);
            }
            catch (Exception ex)
            {
            }
            return Ok(Result);
        }

        // [HttpPost("ReadExcel")]

        [Route("ReadExcel")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult ReadExcel(List<IFormFile> files1)
        {

            dynamic result = new System.Dynamic.ExpandoObject();

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                try
                {
                    string FilePath = string.Empty;
                    var localFile = Path.Combine(_env.WebRootPath, "assets");
                    var uniqueFileName = string.Format("{0}_{1}_{2}", DateTime.Now.ToString("dd-MM-yyyy"), DateTime.Now.Ticks, file.FileName);
                    FilePath = Path.Combine(localFile, uniqueFileName);
                    using (var filestrem = new FileStream(FilePath, FileMode.Create))
                    {
                        file.CopyTo(filestrem);
                    }
                    System.IO.FileInfo files = new System.IO.FileInfo(FilePath);
                    using (ExcelPackage package = new ExcelPackage(files))
                    {
                        ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                        var totalRow = sheet.Dimension.Rows;
                        List<CouponChild> coups = new List<CouponChild>();
                        for (int i = 2; i < totalRow + 1; i++)
                        {
                            CouponChild coup = new CouponChild();
                            coup.Ma = sheet.Cells[i, 1].Value != null ? sheet.Cells[i, 1].Value.ToString() : "";
                            coup.Name = sheet.Cells[i, 2].Value != null ? sheet.Cells[i, 2].Value.ToString() : "";
                            coup.Email = sheet.Cells[i, 3].Value != null ? sheet.Cells[i, 3].Value.ToString() : "";
                            if (sheet.Cells[i, 4].Value != null)
                            {
                                coup.CreateDate = DateTime.Parse(sheet.Cells[i, 4].Value.ToString());
                            }
                            if (sheet.Cells[i, 5].Value != null)
                            {
                                coup.ExprireDate = DateTime.Parse(sheet.Cells[i, 5].Value.ToString());
                            }
                            coup.CreateBy = string.Empty;
                            coup.Status = 1;
                            coup.NumberUseCode = 0;
                            coup.Parrent = 0;
                            coups.Add(coup);

                        }
                        result.data = coups;
                        return Ok(result);
                    }
                }
                catch
                {
                    return Ok(null);
                }
            }
            return Ok(result);
        }

    }
}
