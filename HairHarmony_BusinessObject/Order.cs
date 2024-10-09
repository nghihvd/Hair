using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_BusinessObject
{
    public partial class Order
    {
        public int ServiceId { get; set; }
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
        public virtual Service Service { get; set; } = null!;
    }
}
