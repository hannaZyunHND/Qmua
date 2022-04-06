using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Language
    {
        public Language()
        {
            ArticleInLanguage = new HashSet<ArticleInLanguage>();
            ZoneInLanguage = new HashSet<ZoneInLanguage>();
        }

        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public bool? SetDefault { get; set; }

        public ICollection<ArticleInLanguage> ArticleInLanguage { get; set; }
        public ICollection<ZoneInLanguage> ZoneInLanguage { get; set; }
    }
}
