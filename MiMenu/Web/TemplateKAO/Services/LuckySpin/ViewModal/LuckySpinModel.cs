using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateKAO.Services.LuckySpin.ViewModal
{
    public partial class LuckySpinModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Enable { get; set; }
        public double Value { get; set; }
        public int Ratio { get; set; }
    }
}
