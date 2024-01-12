using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class PermissionUser
    {
        public int PermissionId { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }

        public virtual Permission Permission { get; set; }
    }
}
