using System;
using System.Collections.Generic;
using MI.Entity.Models;

namespace MI.Entity.ViewModel
{
    public class ArticleViewModel:Article
    {

        public List<Zone> Zones { get; set; }
    }
}