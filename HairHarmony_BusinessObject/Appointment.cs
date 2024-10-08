using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class Appointment
    {
        public Appointment()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int AppointmentId { get; set; }
        public string? CustomerId { get; set; }
        public string? StylistId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? Status { get; set; }

        public virtual Account? Customer { get; set; }
        public virtual Service? Service { get; set; }
        public virtual Account? Stylist { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
