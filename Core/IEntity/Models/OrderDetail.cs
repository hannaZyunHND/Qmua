using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderPromotionDetail = new HashSet<OrderPromotionDetail>();
        }

        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? LogPrice { get; set; }
        public int? OrderSourceType { get; set; }
        public int? OrderSourceId { get; set; }
        public string Voucher { get; set; }
        public byte? VoucherType { get; set; }
        public double? VoucherPrice { get; set; }
        public string VoucherMeta { get; set; }
        public bool? Vat { get; set; }
        public double? VatPrice { get; set; }

        public Orders Order { get; set; }
        public ICollection<OrderPromotionDetail> OrderPromotionDetail { get; set; }
    }
}
