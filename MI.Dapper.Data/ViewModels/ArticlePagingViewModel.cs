using System;

namespace MI.Dapper.Data.ViewModels
{
    public class ArticlePagingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public int CountLanguage { get; set; }
        public string Author { get; set; }
    }
}