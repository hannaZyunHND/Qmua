using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.ViewModel
{
    public class OrderVM
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Src { get; set; }
        public string CreateDate { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
        public MI.Entity.Models.Customer Customer { get; set; }
        public List<OrderDetailVM> OdderDetail { get; set; }
    }


    public class OrderDetailVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Voucher { get; set; }
        public string VoucherPrice { get; set; }
        public string VoucherMeta { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string strPromotion { get; set; }
        public string strVoucher { get; set; }
        public string OderPrice { get; set; }
        public string Url { get; set; }
    }
}
