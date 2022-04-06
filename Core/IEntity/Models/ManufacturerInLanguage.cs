using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ManufacturerInLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string LanguageCode { get; set; }
        public int ManufacturerId { get; set; }
    }
}
