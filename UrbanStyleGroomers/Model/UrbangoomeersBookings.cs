using System;
namespace UrbanStyleGroomers.Model
{
    public class UrbangoomeersBookings
    {
        public string deal_id { get; set; }
        public string deal_quantity { get; set; }
        public string coupon_id { get; set; }
        public string user_id { get; set; }
        public string date_time { get; set; }
        public string status { get; set; }
        public string payment_gateway { get; set; }
        public string original_amount { get; set; }
        public string discount { get; set; }
        public string coupon_discount { get; set; }
        public string discount_percent { get; set; }
        public string tax_name { get; set; }
        public string tax_percent { get; set; }
        public string tax_amount { get; set; }
        public string amount_to_pay { get; set; }
        public string payment_status { get; set; }
        public string source { get; set; }
        public string additional_notes { get; set; }
        //public List<object> links { get; set; }
    }
}
