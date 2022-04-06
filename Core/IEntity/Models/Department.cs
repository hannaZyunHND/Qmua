using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Department
    {
       

        public int Id { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? SortOrder { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LocationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Hotline { get; set; }

        public ICollection<DepartmentInLanguage> DepartmentInLanguage { get; set; }
    }
}
