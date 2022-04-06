using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class LuckySpin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Enable { get; set; }
        public double? Value { get; set; }
        public int Ratio { get; set; }
    }
}
