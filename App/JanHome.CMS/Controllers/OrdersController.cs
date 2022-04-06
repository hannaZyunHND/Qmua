using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MI.Entity.Enums;
using Utils;
using JanHome.CMS.ViewModel;
using System.Text;
using System.Globalization;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        OrdersBCL ordersBCL;
        OrderDetailBCL orderDetailBCL;
        ProductBCL productBCL;
        PromotionBCL promotionBCL;
        public OrdersController()
        {
            ordersBCL = new OrdersBCL();
            orderDetailBCL = new OrderDetailBCL();
            productBCL = new ProductBCL();
            promotionBCL = new PromotionBCL();
        }
        [HttpPost("Get")]
        public ResponsePaging Get(Utils.FilterOrders filter)
        {
            ResponsePaging responseData = new ResponsePaging();
            try
            {
                int total = 0;
                var data = ordersBCL.Get(filter, out total);
                var lstIdP = data.SelectMany(x => x.OrderDetail.Select(d => d.ProductId)).Distinct();
                var dicProduct = productBCL.FindAll(a => lstIdP.Contains(a.Id)).ToDictionary(x => x.Id, x => x);
                responseData.ListData = data.Select(x => new
                {
                    x.Id,
                    x.OrderCode,
                    CreatedDate = x.CreatedDate == null ? "" : x.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm"),
                    x.CreatedBy,
                    x.Status,
                    Type = MI.Entity.EnumHelper.GetDescription((MI.Entity.Enums.OrderType)x.OrderSourceType),
                    CustumerName = x.Customer.Name,
                    CustumerGender = x.Customer.Gender,
                    CustumerPhone = x.Customer.PhoneNumber,
                    CustumerNote = x.Customer.Note,
                    CustumerEmail = x.Customer.Email,
                    OrderDetail = x.OrderDetail.Select(d => new
                    {
                        d.Id,
                        LogPrice = Utils.Utility.ConvertCurentce(d.LogPrice == null ? "0" : d.LogPrice.Value.ToString()),
                        Code = d.ProductId != null ? dicProduct.ContainsKey(d.ProductId ?? 0) ? dicProduct[d.ProductId.Value].Code : "" : "",
                        Name = d.ProductId != null ? dicProduct.ContainsKey(d.ProductId ?? 0) ? dicProduct[d.ProductId.Value].Name : "" : "",
                        d.ProductId,
                        d.Quantity,
                        d.Voucher,
                        VoucherPrice = d.VoucherPrice ?? 0,
                        d.VoucherMeta
                    }).ToList(),
                    VoucherDetail = x.MetaData.Select(d => new VoucherTomTat() {
                        VoucherCode = Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"","\"").Replace("\"{","{").Replace("}\"","}")).MaVoucher,
                        VoucherGiaTri = Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"","\"").Replace("\"{","{").Replace("}\"","}")).GiaTriVoucher,
                        TongTien = Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"","\"").Replace("\"{","{").Replace("}\"","}")).TongTien,
                        SoBan = Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}")).SoBan,
                        ThoiGianGiao = !string.IsNullOrEmpty(Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}")).ThoiGianGiao) ? DateTime.ParseExact(Newtonsoft.Json.JsonConvert.DeserializeObject<MetaDataOrderController>(x.MetaData.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}")).ThoiGianGiao, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH:mm") : ""
                    }).ToList().Take(1).SingleOrDefault(),
                }).ToList<object>();
                responseData.Total = total;
            }
            catch (Exception ex)
            {
                throw(ex);
            }
            return responseData;
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            OrderVM result = new OrderVM();
            try
            {
                var ads = ordersBCL.FindById(x => x.Id == id, x => x.OrderDetail, x => x.Customer);
                if (ads != null)
                {
                    var orderDetail = orderDetailBCL.FindAll(x => x.OrderId == ads.Id, x => x.OrderPromotionDetail);
                    var product = productBCL.FindAll(x => orderDetail.Select(d => d.ProductId).Contains(x.Id)).ToDictionary(x => x.Id, x => x);
                    var number = orderDetail.Select(x => x.OrderPromotionDetail.Where(a => a.LogType == "discount").Sum(d => d.LogValue ?? 0));
                    var npromotionDetail = orderDetail.SelectMany(x => x.OrderPromotionDetail.Where(a => a.LogType == "discount")).ToList().GroupBy(x => x.OrderDetailId.Value).ToDictionary(x => x.Key, x => x.Sum(a => a.LogValue));


                    result.Code = ads.OrderCode;
                    result.CreateDate = ads.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
                    result.Price = Utils.Utility.ConvertCurentce(((int)ads.OrderDetail.Sum(x => x.LogPrice * x.Quantity) - (int)number.Sum(x => x) - (int)orderDetail.Sum(x => x.VoucherPrice ?? 0)).ToString());


                    result.OdderDetail = orderDetail.Select(x => new ViewModel.OrderDetailVM
                    {
                        
                        Code = product.ContainsKey(x.ProductId ?? 0) ? product[x.ProductId.Value].Code : "Default",
                        Name = product.ContainsKey(x.ProductId ?? 0) ? product[x.ProductId.Value].Name : "Default",
                        Number = x.Quantity == null ? 0 : (int)x.Quantity.Value,
                        Price = Utility.ConvertCurentce(product.ContainsKey(x.ProductId ?? 0) ? (product[x.ProductId.Value].DiscountPrice > 0? product[x.ProductId.Value].DiscountPrice.ToString(): product[x.ProductId.Value].Price.ToString()) : "0".ToString()) + "đ",
                        TotalPrice = Utility.ConvertCurentce(product.ContainsKey(x.ProductId ?? 0) ? (product[x.ProductId.Value].DiscountPrice > 0 ? (product[x.ProductId.Value].DiscountPrice * (int)x.Quantity - (int)(npromotionDetail.ContainsKey(x.Id) ? npromotionDetail[x.Id] * x.Quantity : 0)).ToString() : (product[x.ProductId.Value].Price * (int)x.Quantity - (int)(npromotionDetail.ContainsKey(x.Id) ? npromotionDetail[x.Id] * x.Quantity : 0)).ToString()) : "0".ToString()) + "đ",
                        VoucherPrice = x.VoucherType == 1 ? x.VoucherPrice + "%": Utility.ConvertCurentce((x.VoucherPrice ?? 0).ToString())+ "đ",
                        Voucher = x.Voucher,
                        VoucherMeta = x.VoucherMeta,
                        strPromotion = BuildPromotion(x.OrderPromotionDetail.ToList()),
                        strVoucher = x.VoucherMeta,
                        OderPrice = BuildOrderPrice(orderDetail, product, npromotionDetail),
                        Url = product.ContainsKey(x.ProductId ?? 0) ? (!product[x.ProductId.Value].Url.StartsWith("http") ? Utils.Settings.AppSettings.BaseDomain + "/" + product[x.ProductId.Value].Url + ".html" : product[x.ProductId.Value].Url) : "Default",
                    }).ToList();
                    ads.Customer.Orders = null;
                    result.Customer = ads.Customer;
                    result.Address = ads.Address;
                    result.Src = MI.Entity.EnumHelper.GetDescription((MI.Entity.Enums.OrderType)ads.OrderSourceType);
                    result.Status = StatusOrders.GetStatusName(ads.Status);
                }

            }
            catch (Exception ex)
            {

            }
            return Ok(result);
        }
        private string BuildOrderPrice(List<OrderDetail> ListorderDetails, Dictionary<int, Product> product, Dictionary<int, decimal?> npromotionDetail)
        {
            var rsult = string.Empty;
            double? total = 0;
            try
            {
                foreach (var item in ListorderDetails)
                {
                    total +=  (product[item.ProductId.Value].DiscountPrice > 0 ? product[item.ProductId.Value].DiscountPrice * (int)item.Quantity : product[item.ProductId.Value].Price * (int)item.Quantity) - (int)(npromotionDetail.ContainsKey(item.Id) ? npromotionDetail[item.Id] * item.Quantity : 0);
                }
                if(ListorderDetails[0].VoucherType == 1)
                {
                    rsult = Utility.ConvertCurentce((total - (total / 100 * ListorderDetails[0].VoucherPrice)).ToString()) + "đ";

                }
                else
                {
                    rsult = Utility.ConvertCurentce((total - ListorderDetails[0].VoucherType).ToString()) + "đ";
                }
                return rsult;
            }

            catch
            {
            }
            return rsult;
        }
        private string BuildTotalPrice(OrderDetail orderDetail, List<OrderPromotionDetail> lstPromotion, Dictionary<int, decimal?> npromotionDetail)
        {
            string result = "";
            try
            {
                foreach (var item in lstPromotion)
                {
                    if (item.LogType.Trim() == "discount-percent")
                    {
                        int amountDiscount = (((((int)orderDetail.LogPrice * (int)item.LogValue) / 100) * (int)orderDetail.Quantity) - (int)(npromotionDetail.ContainsKey(orderDetail.Id) ? npromotionDetail[orderDetail.Id] : 0));
                        int totalAmount = (int)orderDetail.LogPrice * (int)orderDetail.Quantity;
                        result = Utility.ConvertCurentce((totalAmount - amountDiscount).ToString()) + "đ";
                    }
                    else
                    {
                        result = Utility.ConvertCurentce((((int)orderDetail.LogPrice * (int)orderDetail.Quantity) - (int)(npromotionDetail.ContainsKey(orderDetail.Id) ? npromotionDetail[orderDetail.Id] : 0)).ToString()) + "đ";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        private string BuildPromotion(List<OrderPromotionDetail> lstPromotion)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lstPromotion)
            {
                sb.Append("<p>" + item.LogName + $": {Utility.ConvertCurentce(item.LogValue.ToString())} </p>");
            }
            return sb.ToString();
        }
        private string BuildVoucher(string voucher, double price, string meta)
        {
            StringBuilder sb = new StringBuilder();
            if (!String.IsNullOrEmpty(voucher))
            {
                sb.Append($"Sử dụng: {voucher}: {Utils.Utility.ConvertCurentce(price.ToString())} đ<br/>");
                sb.Append($"Chi tiết: {meta}");
            }
            return sb.ToString();
        }

        [HttpPut("UpdateOrderStatus")]
        public ResponseData UpdateOrderStatus([FromBody] Orders obj)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = ordersBCL.UpdateTrangThai(obj);
                if (ads)
                {
                    responseData.Success = true;
                    responseData.Message = Utility.SuccessMessage;
                }
                else
                {
                    responseData.Success = false;
                    responseData.Message = Utility.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                responseData.Message = Utility.SuccessMessage;
            }
            return responseData;
        }
    }
    public class MetaDataOrderController{

        /*"{\"IsNgoiTaiQuan\":\"1\",\"SoBan\":\"ádasd\",\"GhiChu\":\"áda\",\"HHThanhToan\":\"\",\"ThoiGianGiao\":\"2021-04-08\",\"MaVoucher\":\"\",\"GiaTriVoucher\":0}"*/
        public string IsNgoiTaiQuan { get; set; }
        public string SoBan { get; set; }
        public string GhiChu { get; set; }
        public string HHThanhToan { get; set; }
        public string ThoiGianGiao { get; set; }
        public string MaVoucher { get; set; }
        public int GiaTriVoucher { get; set; }
        public int TongTien { get; set; }

    }
    public class VoucherTomTat {
        public string VoucherCode { get; set; }
        public int VoucherGiaTri { get; set; }
        public int TongTien { get; set; }
        public string SoBan { get; set; }
        public string ThoiGianGiao { get; set; }
    }
}
