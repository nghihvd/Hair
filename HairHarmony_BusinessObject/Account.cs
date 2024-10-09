using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_BusinessObject
{
    public partial class Account
    {
        public Account()
        {
            AppointmentCustomers = new HashSet<Appointment>();
            AppointmentStylists = new HashSet<Appointment>();
            Feedbacks = new HashSet<Feedback>();
        }

        public string AccountId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? LoyaltyPoints { get; set; }
        public int? RoleId { get; set; }
        public string? Password { get; set; }
        public string? Level { get; set; }
        public decimal? Salary { get; set; }
        public decimal? CommissionRate { get; set; }

        public virtual ICollection<Appointment> AppointmentCustomers { get; set; }
        public virtual ICollection<Appointment> AppointmentStylists { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
