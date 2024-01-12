using System;
using System.Collections.Generic;



namespace UrbanStyleGroomers.Model
{
    public class PermissionRole
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
