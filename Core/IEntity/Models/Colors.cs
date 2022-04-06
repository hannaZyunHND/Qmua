using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Colors
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Show { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LanguageCode { get; set; }
    }
}
