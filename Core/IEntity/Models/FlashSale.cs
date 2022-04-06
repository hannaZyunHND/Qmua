using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class FlashSale
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
