using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class DepartmentInLanguage
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string LanguageCode { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
