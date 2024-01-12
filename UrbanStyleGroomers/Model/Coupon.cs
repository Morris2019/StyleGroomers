using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Coupon
    {
        public Coupon()
        {
            Bookings = new HashSet<Booking>();
            CouponUsers = new HashSet<CouponUser>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? UsesLimit { get; set; }
        public int? UsedTime { get; set; }
        public double? Amount { get; set; }
        public int? Percent { get; set; }
        public int MinimumPurchaseAmount { get; set; }
        public string Days { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<CouponUser> CouponUsers { get; set; }
    }
}
