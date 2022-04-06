
using MI.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanHome.CMS.ViewModel
{
    public class Products
    {
        public Product Data { get; set; }
        public List<int> ListCat { get; set; }
        public List<int> ListProp { get; set; }
        public List<string> ListColor { get; set; }
        public List<KeyValuePair<int, string>> ListArticle { get; set; }
    }
}
