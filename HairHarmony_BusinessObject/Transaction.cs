using System;
using System.Collections.Generic;

namespace HairHarmony_BusinessObject
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string? StylistId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }

        public virtual Account? Stylist { get; set; }
    }
}
