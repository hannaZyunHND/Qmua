using System.Collections.Generic;
using MI.Dapper.Data.Models;

namespace MI.Dapper.Data.ViewModels
{
    public class ProductSpecificationTemplateByIdViewModel
    {
        public ProductSpecificationTemplate ProductSpecificationTemplate { get; set; }
        public List<ProductSpecificationTemplateInLanguage> ProductSpecificationTemplateInLanguage { get; set; }
        public List<int> ListZoneIds { get; set; }
        public ProductSpecificationTemplates ProductSpecificationTemplates { get; set; }
    }

    public class ProductSpecificationTemplates
    {
        public bool IsForAllProduct { get; set; }
        public bool IsFilter { get; set; }
    }
}