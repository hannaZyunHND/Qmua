using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.ViewModel
{
    public class ProductInSpecifications
    {
        public int Id { get; set; }
       
        public List<MaintainSpectificatinInProduct> Specifications { get; set; }
        public ProductInSpecifications()
        {
            this.Id = 0;
           
            this.Specifications = new List<MaintainSpectificatinInProduct>();
        }
    }
}
