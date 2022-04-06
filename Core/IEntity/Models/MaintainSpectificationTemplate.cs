using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class MaintainSpectificationTemplate
    {
        public int Id { get; set; }
        public int? ZoneId { get; set; }
        public int? SortOrder { get; set; }
        public int? SpectificationId { get; set; }
        public bool? IsForAllProduct { get; set; }
        public bool? IsFilter { get; set; }
    }
}
