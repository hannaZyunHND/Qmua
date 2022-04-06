using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class PropertyLanguage
    {
        public int PropertyId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public Property Property { get; set; }
    }
}
