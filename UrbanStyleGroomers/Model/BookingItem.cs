using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public partial class BookingItem
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int BusinessServiceId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual BusinessService BusinessService { get; set; }
    }
}
