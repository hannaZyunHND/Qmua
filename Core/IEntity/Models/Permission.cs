using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Permission
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}
