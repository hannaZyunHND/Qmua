using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductSpecificationTemplate
    {
        public ProductSpecificationTemplate()
        {
            ProductSpecificationTemplateInLanguage = new HashSet<ProductSpecificationTemplateInLanguage>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public int? ZoneId { get; set; }
        public int? SortOrder { get; set; }

        public ICollection<ProductSpecificationTemplateInLanguage> ProductSpecificationTemplateInLanguage { get; set; }
    }
}
