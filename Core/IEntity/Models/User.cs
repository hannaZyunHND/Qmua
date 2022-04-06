using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsFullPermission { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? LastLogined { get; set; }
        public DateTime? LastChangePass { get; set; }
        public string OtpSecretKey { get; set; }
        public string ActiveCode { get; set; }
        public byte? LoginType { get; set; }
        public string SocialId { get; set; }
        public long? IsSystem { get; set; }
        public string Address { get; set; }
    }
}
