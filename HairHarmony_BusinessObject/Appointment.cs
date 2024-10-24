using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class Appointment
    {
        public Appointment()
        {
            Orders = new HashSet<Order>();
        }

        public int AppointmentId { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Status { get; set; }

        public virtual Account? Customer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
