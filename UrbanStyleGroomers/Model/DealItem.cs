using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public partial class DealItem
    {
        public int Id { get; set; }
        public int? DealId { get; set; }
        public int? BusinessServiceId { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double DiscountAmount { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual BusinessService BusinessService { get; set; }
        public virtual Deal Deal { get; set; }
    }
}
