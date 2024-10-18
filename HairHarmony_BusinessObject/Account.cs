using System;
using System.Collections.Generic;

namespace WpfApp1
{
    public partial class Account
    {
        public Account()
        {
            Appointments = new HashSet<Appointment>();
            Shifts = new HashSet<Shift>();
            StylistServices = new HashSet<StylistService>();
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

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<StylistService> StylistServices { get; set; }
    }
}
