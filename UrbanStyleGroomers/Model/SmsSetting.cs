using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class SmsSetting
    {
        public long Id { get; set; }
        public string NexmoStatus { get; set; }
        public string NexmoKey { get; set; }
        public string NexmoSecret { get; set; }
        public string NexmoFrom { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
