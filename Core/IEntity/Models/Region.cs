using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Region
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? SortOrder { get; set; }
        public int? ParentId { get; set; }
        public byte Status { get; set; }
        public string Avatar { get; set; }
        public string Background { get; set; }
        public byte? Type { get; set; }
        public string Banner { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Icon { get; set; }
        public bool? IsShowMenu { get; set; }
    }
}
