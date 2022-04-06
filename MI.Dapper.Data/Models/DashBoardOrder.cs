using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Dapper.Data.Models
{
    public class DashBoardOrder
    {
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string MetaData { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public int OrderSourceType { get; set; }
        public int OrderSourceId { get; set; }
        public int VAT { get; set; }
        public string CusName { get; set; }
        public string CusAddress { get; set; }
        public string CusPhoneNumber { get; set; }
    }
}
