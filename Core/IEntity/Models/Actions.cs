using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Actions
    {
        public Actions()
        {
            ActionInFunctions = new HashSet<ActionInFunctions>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<ActionInFunctions> ActionInFunctions { get; set; }
    }
}
