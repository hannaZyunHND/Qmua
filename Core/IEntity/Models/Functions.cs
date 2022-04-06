using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Functions
    {
        public Functions()
        {
            ActionInFunctions = new HashSet<ActionInFunctions>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ParentId { get; set; }
        public int? SortOrder { get; set; }
        public string CssClass { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ActionInFunctions> ActionInFunctions { get; set; }
    }
}
