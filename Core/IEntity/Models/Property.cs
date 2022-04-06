using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Property
    {
        public Property()
        {
            PropertyLanguage = new HashSet<PropertyLanguage>();
        }

        public int Id { get; set; }
        public byte GroupId { get; set; }
        public string Thumb { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }

        public ICollection<PropertyLanguage> PropertyLanguage { get; set; }
    }
}
