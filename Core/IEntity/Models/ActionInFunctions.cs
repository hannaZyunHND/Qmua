using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class ActionInFunctions
    {
        public string FunctionId { get; set; }
        public string ActionId { get; set; }

        public Actions Action { get; set; }
        public Functions Function { get; set; }
    }
}
