using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Redirect
    {
        public string UrlOld { get; set; }
        public string UrlNew { get; set; }
        public int? UrlType { get; set; }
        public bool? Status { get; set; }
    }
}
