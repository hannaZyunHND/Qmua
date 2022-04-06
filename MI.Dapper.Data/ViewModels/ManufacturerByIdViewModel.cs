using System.Collections.Generic;
using MI.Dapper.Data.Models;

namespace MI.Dapper.Data.ViewModels
{
    public class ManufacturerByIdViewModel
    {
        public Manufacturers Manufacturers { get; set; }
        public List<ManufacturersInLanguage> ManufacturersInLanguages { get; set; }
    }
}