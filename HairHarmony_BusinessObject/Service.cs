using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_BusinessObject
{
    public partial class Service
    {
        public Service()
        {
            Orders = new HashSet<Order>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }
        public bool? status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
