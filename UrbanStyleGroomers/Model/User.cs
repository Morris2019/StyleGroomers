using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class User
    {
        public User()
        {
            BookingUsers = new HashSet<BookingUser>();
            Bookings = new HashSet<Booking>();
            BusinessServiceUsers = new HashSet<BusinessServiceUser>();
            CouponUsers = new HashSet<CouponUser>();
            TodoItems = new HashSet<TodoItem>();
        }

        public int Id { get; set; }
        public int? GroupId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CallingCode { get; set; }
        public string Mobile { get; set; }
        public int MobileVerified { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string RememberToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<BookingUser> BookingUsers { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<BusinessServiceUser> BusinessServiceUsers { get; set; }
        public virtual ICollection<CouponUser> CouponUsers { get; set; }
        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}
