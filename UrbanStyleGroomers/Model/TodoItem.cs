using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class TodoItem
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int Position { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
