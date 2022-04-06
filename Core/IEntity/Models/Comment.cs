using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? Status { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByCustomerId { get; set; }
        public string CreatedByAdminId { get; set; }
        public string Content { get; set; }
        public int? ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string LanguageCode { get; set; }
        public string Name { get; set; }
        public string PhoneOrMail { get; set; }
        public string Avatar { get; set; }
        public string Type { get; set; }
        public int? Rate { get; set; }
    }
}
