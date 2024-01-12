using System;
using System.Collections.Generic;

namespace UrbanStyleGroomers.Model
{
    public class CompletedBookings
    {
        public int id { get; set; }
        public object dealId { get; set; }
        public object dealQuantity { get; set; }
        public object couponId { get; set; }
        public int userId { get; set; }
        public DateTime dateTime { get; set; }
        public string status { get; set; }
        public string paymentGateway { get; set; }
        public double originalAmount { get; set; }
        public double discount { get; set; }
        public object couponDiscount { get; set; }
        public double discountPercent { get; set; }
        public object taxName { get; set; }
        public object taxPercent { get; set; }
        public object taxAmount { get; set; }
        public double amountToPay { get; set; }
        public string paymentStatus { get; set; }
        public string source { get; set; }
        public object additionalNotes { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public object coupon { get; set; }
        public object deal { get; set; }
        public object user { get; set; }
        public List<object> bookingItems { get; set; }
        public List<object> bookingUsers { get; set; }
        public List<object> payments { get; set; }
    }
}
