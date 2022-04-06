using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class Promotion
    {
        public int LangCount;
        public Promotion()
        {
            ProductInPromotion = new List<ProductInPromotion>();
            PromotionInLanguage = new List<PromotionInLanguage>();
        }
        public virtual List<ProductInPromotion> ProductInPromotion { get; set; }
        public virtual List<PromotionInLanguage> PromotionInLanguage { get; set; }
    }
}
