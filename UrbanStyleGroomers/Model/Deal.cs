using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Deal
    {
        public Deal()
        {
            Bookings = new HashSet<Booking>();
            DealItems = new HashSet<DealItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int LocationId { get; set; }
        public string DealType { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public int? UsesLimit { get; set; }
        public int? UsedTime { get; set; }
        public double? OriginalAmount { get; set; }
        public double? DealAmount { get; set; }
        public string Days { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string DiscountType { get; set; }
        public int? Percentage { get; set; }
        public string DealAppliedOn { get; set; }
        public int? MaxOrderPerCustomer { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<DealItem> DealItems { get; set; }
    }
}
