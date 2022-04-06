using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductSpecificationTemplateInLanguage
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        public int ProductSpecificationTemplateId { get; set; }
        public string Name { get; set; }

        public ProductSpecificationTemplate ProductSpecificationTemplate { get; set; }
    }
}
