using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_BusinessObject
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string? CustomerId { get; set; }
        public int? AppointmentId { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }

        public virtual Appointment? Appointment { get; set; }
        public virtual Account? Customer { get; set; }
    }
}
