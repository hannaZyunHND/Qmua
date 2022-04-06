using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class CustomLucky
    {
        public int Id { get; set; }
        public int? LuckySpinValue { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LuckySpinName { get; set; }
    }
}
