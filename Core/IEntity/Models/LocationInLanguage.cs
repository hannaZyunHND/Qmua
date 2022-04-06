using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class LocationInLanguage
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
