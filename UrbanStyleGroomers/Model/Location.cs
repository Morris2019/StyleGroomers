using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public partial class Location
    {
        public Location()
        {
            BusinessServices = new HashSet<BusinessService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BusinessService> BusinessServices { get; set; }
    }
}
