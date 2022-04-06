using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public byte? Type { get; set; }
        public string Source { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte? Status { get; set; }
        public string UrlRef { get; set; }
    }
}
