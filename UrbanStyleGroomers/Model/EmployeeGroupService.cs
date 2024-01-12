using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public partial class EmployeeGroupService
    {
        public long Id { get; set; }
        public int? EmployeeGroupsId { get; set; }
        public int? BusinessServiceId { get; set; }

        public virtual BusinessService BusinessService { get; set; }
        public virtual EmployeeGroup EmployeeGroups { get; set; }
    }
}
