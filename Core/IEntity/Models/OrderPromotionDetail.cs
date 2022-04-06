using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class OrderPromotionDetail
    {
        public int Id { get; set; }
        public int? OrderDetailId { get; set; }
        public string LogName { get; set; }
        public decimal? LogValue { get; set; }
        public string LogType { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
