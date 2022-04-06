using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class Department
    {
        public int LangCount;
        public Department()
        {
            DepartmentInLanguage = new HashSet<DepartmentInLanguage>();
            this.LangCount = 0;
            this.Name = string.Empty;
            this.Url = string.Empty;
            this.Latitude = 0;
            this.LocationId = 0;
            this.Longitude = 0;
        }

    }
}
