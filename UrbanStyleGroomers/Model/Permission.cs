using System;
using System.Collections.Generic;


namespace UrbanStyleGroomers.Model
{
    public class Permission
    {
        public Permission()
        {
            PermissionRoles = new HashSet<PermissionRole>();
            PermissionUsers = new HashSet<PermissionUser>();
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
        public virtual ICollection<PermissionUser> PermissionUsers { get; set; }
    }
}
