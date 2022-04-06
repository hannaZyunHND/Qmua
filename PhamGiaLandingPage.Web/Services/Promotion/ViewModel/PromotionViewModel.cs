using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Promotion.ViewModel
{
    public class PromotionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public int IsDiscountPrice { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
    }
}
