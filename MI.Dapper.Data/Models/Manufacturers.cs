namespace MI.Dapper.Data.Models
{
    public class Manufacturers
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string LangCount { get; set; }
    }

    public class ManufacturersInLanguage
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string LanguageCode { get; set; }
        public int ManufacturersId { get; set; }
    }
}