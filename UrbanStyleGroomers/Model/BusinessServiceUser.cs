using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class BusinessServiceUser
    {
        public int BusinessServiceId { get; set; }
        public int UserId { get; set; }

        public virtual BusinessService BusinessService { get; set; }
        public virtual User User { get; set; }
    }
}
