using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairHarmony_BusinessObject
{
    public partial class Service
    {
        public Service()
        {
            StylistServices = new HashSet<StylistService>();
        }

        public int ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }

        [NotMapped]
        private bool? _isSelected;

        [NotMapped]
        public bool? IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                   _isSelected = value;                
                }
            }
        }
        public virtual ICollection<StylistService> StylistServices { get; set; }
    }
}
