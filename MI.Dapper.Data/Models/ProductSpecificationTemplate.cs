namespace MI.Dapper.Data.Models
{
    public class ProductSpecificationTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }

    public class ProductSpecificationTemplateInLanguage
    {
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int SpectificationId { get; set; }
        public string Url { get; set; }
    }
}