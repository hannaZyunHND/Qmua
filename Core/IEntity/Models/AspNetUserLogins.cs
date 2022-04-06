using System;
using System.Collections.Generic;

namespace MI.Entity.Models
{
    public partial class AspNetUserLogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public Guid UserId { get; set; }

        public AspNetUsers User { get; set; }
    }
}
