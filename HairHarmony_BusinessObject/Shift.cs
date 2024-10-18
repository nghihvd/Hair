using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class Shift
    {
        public int ShiftId { get; set; }
        public string StylistId { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime Date { get; set; }

        public virtual Account Stylist { get; set; } = null!;
    }
}
