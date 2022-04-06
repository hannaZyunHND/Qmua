using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string MetaData { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public int? OrderSourceType { get; set; }
        public int? OrderSourceId { get; set; }
        public bool? Vat { get; set; }
        public string MetaOrder { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
