using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Page
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
