using System.Collections.Generic;

namespace MI.Dapper.Data.ViewModels
{
    public class FunctionPermissionViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public string ParentId { get; set; }
        public List<FunctionPermissionViewModel> Children { get; set; }
    }
}