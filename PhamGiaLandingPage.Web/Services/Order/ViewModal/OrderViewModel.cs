using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Order.ViewModal
{
    public class OrderViewModel
    {
        public string OrderCode { get; set; }
        public CustomerInOrderViewModel Customer { get; set; }
        public List<ProductInOrderViewModel> Products { get; set; }
        public List<string> Extras { get; set; }
    }

    public class ExtraInOrderViewModel
    {
        public string Extra { get; set; }
    }
    public class CustomerInOrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Gender { get; set; }
    }
    public class ProductInOrderViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal LogPrice { get; set; }
        public double Quantity { get; set; }
        public int OrderSourceType { get; set; }
        public int OrderSourceId { get; set; }
        public string Voucher { get; set; }
        public float VoucherPrice { get; set; }
        public int VoucherType { get; set; }

        public List<PromotionsInProductViewModel> Promotions { get; set; }

    }

    public class PromotionsInProductViewModel
    {
        public int PromotionId { get; set; }
        public string LogType { get; set; }
        public string LogName { get; set; }
        public decimal LogValue { get; set; }
    }




}
