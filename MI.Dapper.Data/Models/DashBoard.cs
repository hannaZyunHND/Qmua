using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Dapper.Data.Models
{
    public class DashBoard
    {
        public int TotalOrder { get; set; }
        public int TotalNewOrder { get; set; }
        public int TotalProcessingOrder { get; set; }
        public int TotalApprovedOrder { get; set; }
        public int TotalSuccessOrder { get; set; }
        public int TotalCancleOrder { get; set; }
    }
    public class DashBoard2
    {
        public int New { get; set; }
        public int Processing{ get; set; }
        public int Approved{ get; set; }
        public int Success { get; set; }
        public int Cancle { get; set; }
        public string CreatedDate { get; set; }
    }
}
