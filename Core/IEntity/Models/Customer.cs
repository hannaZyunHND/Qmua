using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public string MetaData { get; set; }
        public string Ip { get; set; }
        public string Pcname { get; set; }
        public string Os { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public byte Type { get; set; }
        public byte Source { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
