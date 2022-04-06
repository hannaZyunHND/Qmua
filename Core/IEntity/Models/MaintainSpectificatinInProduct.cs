using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class MaintainSpectificatinInProduct
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Value { get; set; }
        public bool? IsShowLayout { get; set; }
        public string LanguageCode { get; set; }
        public int? SpectificationId { get; set; }
    }
}
