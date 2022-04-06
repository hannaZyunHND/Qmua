using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamGiaLandingPage.Web.Services.LuckySpin.ViewModal
{
    public partial class CustomLuckyModel
    {
        public string Id { get; set; }
        public int LuckySpinValue { get; set; }
        public string PhoneNumber { get; set; }
        public string LuckySpinName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
