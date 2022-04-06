using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ArticlesInZone
    {
        public int ArticleId { get; set; }
        public int ZoneId { get; set; }
        public int? IsPrimary { get; set; }

        public Article Article { get; set; }
        public Zone Zone { get; set; }
    }
}
