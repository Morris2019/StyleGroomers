using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Category
    {
        public Category()
        {
            BusinessServices = new HashSet<BusinessService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BusinessService> BusinessServices { get; set; }
    }
}
