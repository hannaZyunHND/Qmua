using System;
using System.Collections.Generic;
using MI.Dapper.Data.Models;
using MI.Entity.Models;

namespace MI.Dapper.Data.ViewModels
{
    public class ArticlesViewModel
    {
        public Articles Articles { get; set; }
        public List<TagsViewModel> Tags { get; set; }
        public List<int> ListZoneIds { get; set; }
        public List<ProductAtArticle> ProductAtArticles { get; set; }
        public List<ArticleInLanguageViewModel> ArticleInLanguageViewModels { get; set; }
    }
}