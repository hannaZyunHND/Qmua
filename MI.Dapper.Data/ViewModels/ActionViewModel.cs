using System.Collections.Generic;

namespace MI.Dapper.Data.ViewModels
{
    public class ActionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Functions { get; set; } = new List<string>();
    }
}