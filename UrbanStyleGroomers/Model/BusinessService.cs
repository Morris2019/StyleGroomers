using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class BusinessService
    {
        public BusinessService()
        {
            BookingItems = new HashSet<BookingItem>();
            BusinessServiceUsers = new HashSet<BusinessServiceUser>();
            DealItems = new HashSet<DealItem>();
            EmployeeGroupServices = new HashSet<EmployeeGroupService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Time { get; set; }
        public string TimeType { get; set; }
        public double Discount { get; set; }
        public string DiscountType { get; set; }
        public int? CategoryId { get; set; }
        public int LocationId { get; set; }
        public string Image { get; set; }
        public string DefaultImage { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<BookingItem> BookingItems { get; set; }
        public virtual ICollection<BusinessServiceUser> BusinessServiceUsers { get; set; }
        public virtual ICollection<DealItem> DealItems { get; set; }
        public virtual ICollection<EmployeeGroupService> EmployeeGroupServices { get; set; }
    }
}
