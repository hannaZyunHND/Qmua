using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public byte? Status { get; set; }
        public int? ParentId { get; set; }
    }
}
