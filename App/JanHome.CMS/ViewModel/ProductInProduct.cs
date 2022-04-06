using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.ViewModel
{
    public class ProductInProduct
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }

    }
    public class ProductInCombo
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<ComboObject> Combos { get; set; }
    }

    public class ComboObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string type { get; set; }
        public string code { get; set; }
        public bool active { get; set; }
        public string avatar { get; set; }
        public string configPrice { get; set; }
        public string configNote { get; set; }
    }

    /*
     active: (...)
avatar: (...)
code: (...)
configNote: (...)
configPrice: (...)
id: (...)
name: (...)
price: (...)
type: (...)*/
}
