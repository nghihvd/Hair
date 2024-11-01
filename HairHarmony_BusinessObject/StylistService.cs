using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class StylistService
    {
        public StylistService()
        {
            Orders = new HashSet<Order>();
        }

        public string StylistId { get; set; } = null!;
        public int ServiceId { get; set; }
        public double? CommissionRate { get; set; }
        public bool Status { get; set; }

        public virtual Service Service { get; set; } = null!;
        public virtual Account Stylist { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
