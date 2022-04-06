using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Tag
    {
        public int Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool Invisibled { get; set; }
        public bool IsHotTag { get; set; }
        public int Type { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public string LanguageCode { get; set; }
    }
}
