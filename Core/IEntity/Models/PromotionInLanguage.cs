using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class PromotionInLanguage
    {
        public int PromotionId { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
