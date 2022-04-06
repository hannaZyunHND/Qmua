using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }
        public Guid Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
