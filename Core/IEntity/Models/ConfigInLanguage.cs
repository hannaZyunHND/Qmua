using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ConfigInLanguage
    {
        public string Content { get; set; }
        public string LanguageCode { get; set; }
        public string ConfigName { get; set; }

        public Config ConfigNameNavigation { get; set; }
    }
}
