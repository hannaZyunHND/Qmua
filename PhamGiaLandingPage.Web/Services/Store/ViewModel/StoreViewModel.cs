using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.Store.ViewModel
{
    public class StoreViewModel
    {
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Distance { get; set; }
        public string LanguageCode { get; set; }
        public int SortOrder { get; set; }
    }
    public class StoreResponse
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string LanguageCode { get; set; }
        public string Email { get; set; }
        public string Hotline { get; set; }
        public string Distance { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int LocationId { get; set; }
    }
}
