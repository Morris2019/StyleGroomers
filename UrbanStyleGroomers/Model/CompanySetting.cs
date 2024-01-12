using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public partial class CompanySetting
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string Website { get; set; }
        public string Timezone { get; set; }
        public string Locale { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CurrencyId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string PurchaseCode { get; set; }
        public DateTime? SupportedUntil { get; set; }
        public string MultiTaskUser { get; set; }
        public string BookingPerDay { get; set; }
        public string EmployeeSelection { get; set; }
        public string DisableSlot { get; set; }
        public string BookingTimeType { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
