using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Config
    {
        public Config()
        {
            ConfigInLanguage = new HashSet<ConfigInLanguage>();
        }

        public string Page { get; set; }
        public string ConfigName { get; set; }
        public string ConfigGroupKey { get; set; }
        public string ConfigLabel { get; set; }
        public string ConfigValue { get; set; }
        public int? ConfigValueType { get; set; }

        public ICollection<ConfigInLanguage> ConfigInLanguage { get; set; }
    }
}
