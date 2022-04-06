using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.ViewModel
{
    public class ProductPriceInLocations
    {
        public int Id { get; set; }
        public List<ProductPriceInLocation> PriceInLocation { get; set; }

    }
}
