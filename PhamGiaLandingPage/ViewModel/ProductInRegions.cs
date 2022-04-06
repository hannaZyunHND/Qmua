using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.ViewModel
{
    public class ProductInRegions
    {

        public int zoneId { get; set; }
        public List<ProductInRegion> Regions { get; set; }
    }
}
