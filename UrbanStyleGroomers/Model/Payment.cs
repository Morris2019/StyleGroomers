using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int? CurrencyId { get; set; }
        public int BookingId { get; set; }
        public double Amount { get; set; }
        public string Gateway { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public DateTime? PaidOn { get; set; }
        public string CustomerId { get; set; }
        public string EventId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
