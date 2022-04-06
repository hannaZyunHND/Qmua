using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class MaintainSpectificationInLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string LanguageCode { get; set; }
        public int? SpectificationId { get; set; }
    }
}
