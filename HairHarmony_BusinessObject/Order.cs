﻿using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class Order
    {
        public Order()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int ServiceId { get; set; }
        public int AppointmentId { get; set; }
        public string StylistId { get; set; } = null!;
        public decimal? Price { get; set; }
        public bool? status { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;
        public virtual StylistService S { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
