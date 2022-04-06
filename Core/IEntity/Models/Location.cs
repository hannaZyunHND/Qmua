using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Location
    {
        public Location()
        {
            LocationInLanguage = new HashSet<LocationInLanguage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public int? ParentId { get; set; }
        public string LanguageCode { get; set; }

        public ICollection<LocationInLanguage> LocationInLanguage { get; set; }
    }
}
