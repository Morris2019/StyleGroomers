using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int? DealId { get; set; }
        public double? DealQuantity { get; set; }
        public long? CouponId { get; set; }
        public int? UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public string PaymentGateway { get; set; }
        public double OriginalAmount { get; set; }
        public double Discount { get; set; }
        public double? CouponDiscount { get; set; }
        public double DiscountPercent { get; set; }
        public string TaxName { get; set; }
        public double? TaxPercent { get; set; }
        public double? TaxAmount { get; set; }
        public double AmountToPay { get; set; }
        public string PaymentStatus { get; set; }
        public string Source { get; set; }
        public string AdditionalNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Deal Deal { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BookingItem> BookingItems { get; set; }
        public virtual ICollection<BookingUser> BookingUsers { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
