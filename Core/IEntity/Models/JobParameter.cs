using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class JobParameter
    {
        public long JobId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Job Job { get; set; }
    }
}
