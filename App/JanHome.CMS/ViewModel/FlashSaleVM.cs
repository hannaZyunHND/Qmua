using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.ViewModel
{
    public class FlashSaleVM
    {
        public FlashSale Flash { get; set; }
        public List<ProductInFlashSale> ListProduct { get; set; }
    }
}
