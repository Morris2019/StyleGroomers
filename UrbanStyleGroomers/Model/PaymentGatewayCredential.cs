using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class PaymentGatewayCredential
    {
        public int Id { get; set; }
        public string PaypalClientId { get; set; }
        public string PaypalSecret { get; set; }
        public string StripeClientId { get; set; }
        public string StripeSecret { get; set; }
        public string StripeWebhookSecret { get; set; }
        public string StripeStatus { get; set; }
        public string PaypalStatus { get; set; }
        public string PaypalMode { get; set; }
        public string OfflinePayment { get; set; }
        public string ShowPaymentOptions { get; set; }
        public string RazorpayKey { get; set; }
        public string RazorpaySecret { get; set; }
        public string RazorpayStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
