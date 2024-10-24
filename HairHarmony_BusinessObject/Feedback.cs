using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int? AppointmentId { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
        public int? ServiceId { get; set; }
        public string? StylistId { get; set; }

        public virtual Order? Order { get; set; }
    }
}
