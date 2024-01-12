using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class TaxSetting
    {
        public int Id { get; set; }
        public string TaxName { get; set; }
        public double Percent { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
