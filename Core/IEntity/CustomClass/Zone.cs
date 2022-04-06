using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class Zone
    {
        public ICollection<ArticlesInZone> ArticlesInZone { get; set; }
        public ICollection<ProductInRegion> ProductInRegion { get; set; }
        public ICollection<ProductInZone> ProductInZone { get; set; }
        public ICollection<ZoneInLanguage> ZoneInLanguage { get; set; }

        public List<int> ManufacturerIds;
        public Zone()
        {
            this.Status = 1;
            this.Type = 0;
            this.Fillter = string.Empty;
            ArticlesInZone = new List<ArticlesInZone>();
            ProductInZone = new List<ProductInZone>();
            ZoneInLanguage = new List<ZoneInLanguage>();
            ProductInRegion = new List<ProductInRegion>();
            this.Name = string.Empty;
            this.IsShowSearch = false;
            this.ManufacturerId = string.Empty;
            this.ManufacturerIds = new List<int>();
        }

    }
    public partial class ZoneInLanguage
    {
        public ZoneInLanguage()
        {
            this.LanguageCode = "vi-VN     ";
            this.LanguageCodeNavigation = new Language();
            this.MetaDescription = string.Empty;
            this.MetaKeyword = string.Empty;
            this.MetaTitle = string.Empty;
            this.MetaCanonical = string.Empty;
            this.MetaNoIndex = string.Empty;
            this.Name = string.Empty;
            this.Url = string.Empty;
            this.Zone = new Zone();
            this.ZoneId = 0;

        }

    }
}
