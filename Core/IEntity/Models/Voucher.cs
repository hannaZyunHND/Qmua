using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Voucher
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
        public byte? Type { get; set; }
        public byte? Status { get; set; }
        public string LanguageCode { get; set; }
    }
}
