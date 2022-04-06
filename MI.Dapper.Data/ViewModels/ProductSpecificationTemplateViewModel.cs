namespace MI.Dapper.Data.ViewModels
{
    public class ProductSpecificationTemplateViewModel
    {
        public int Id { get; set; }
        public string Unit { get; set; }
        public string Url { get; set; }
        public string ZoneIds { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public bool IsForAllProduct { get; set; }
        public bool IsFilter { get; set; }
    }
}