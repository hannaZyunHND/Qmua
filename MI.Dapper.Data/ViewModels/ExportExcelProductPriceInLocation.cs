using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Dapper.Data.ViewModels
{
    public class ExportExcelProductPriceInLocation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public decimal df_SalePrice { get; set; }
        public decimal df_DiscountPercent { get; set; }
        public decimal df_Price { get; set; }
        public decimal SalePrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal Price { get; set; }
    }

    public class ExportExcelMaintainSpectificationInProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecId { get; set; }
        public string SpecValue { get; set; }
    }
}
