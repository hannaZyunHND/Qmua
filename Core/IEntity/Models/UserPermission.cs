using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class UserPermission
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
        public int ZoneId { get; set; }
    }
}
