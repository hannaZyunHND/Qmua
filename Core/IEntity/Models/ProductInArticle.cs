using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ProductInArticle
    {
        public int ArticleId { get; set; }
        public int ProductId { get; set; }
        public int? SortOrder { get; set; }

        public Article Article { get; set; }
        public Product Product { get; set; }
    }
}
