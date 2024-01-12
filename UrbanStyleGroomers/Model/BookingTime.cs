using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class BookingTime
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string MultipleBooking { get; set; }
        public int MaxBooking { get; set; }
        public string Status { get; set; }
        public int SlotDuration { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
