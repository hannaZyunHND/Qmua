using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class TagInProductLanguage
    {
        public int TagId { get; set; }
        public int TagMode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ProductInLanguageId { get; set; }
    }
}
