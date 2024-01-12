using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class UniversalSearch
    {
        public long Id { get; set; }
        public string SearchableId { get; set; }
        public string SearchableType { get; set; }
        public string Title { get; set; }
        public string RouteName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
