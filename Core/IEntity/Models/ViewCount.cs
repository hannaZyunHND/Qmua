using System;
using System.Collections.Generic;
using System.Text;

namespace MI.Entity.Models
{
    public partial class ViewCount
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public int ObjectType { get; set; }
        public DateTime ViewDate { get; set; }
        public string Device { get; set; }
    }
}
