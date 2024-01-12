using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanStyleGroomers.Model
{
    public class UsersBookings
    {
        public User GetUsers { get; set; }
        public BookingItem GetBookingItem { get; set; }
        public Booking GetBooking { get; set; }
        public BookingUser GetBookingUser { get; set; }
        public BusinessService GetBusinessService { get; set; }
        public BusinessServiceLocation GetBusinessServiceLocation { get; set; }
        public BusinessServiceUser GetBusinessServiceUser { get; set; }
        public Location GetLocation { get; set; }
    }
}
