using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Dapper.Data.ViewModels
{
    public class PromotionGetByProductIdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChoose { get; set; }
        public bool IsDelete { get; set; }
        public int ProductId { get; set; }
    }
}
