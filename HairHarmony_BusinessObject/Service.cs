using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class Service
    {
        public Service()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
