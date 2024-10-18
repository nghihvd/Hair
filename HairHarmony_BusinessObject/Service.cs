using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class Service
    {
        public Service()
        {
            StylistServices = new HashSet<StylistService>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<StylistService> StylistServices { get; set; }
    }
}
