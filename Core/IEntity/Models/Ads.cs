using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Ads
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public byte? Type { get; set; }
        public byte? Position { get; set; }
        public string Url { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsEnable { get; set; }
        public string Thumb { get; set; }
    }
}
