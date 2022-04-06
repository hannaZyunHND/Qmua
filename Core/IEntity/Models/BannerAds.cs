using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class BannerAds
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string MetaData { get; set; }
        public byte? Type { get; set; }
        public string LanguageCode { get; set; }
    }
}
