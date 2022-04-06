using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int? ObjectId { get; set; }
        public int? Type { get; set; }
        public decimal? Rate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
