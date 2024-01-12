using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class LtmTranslation
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string Locale { get; set; }
        public string Group { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
