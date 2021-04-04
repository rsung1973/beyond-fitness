using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UserRoleDefinition
    {
        public UserRoleDefinition()
        {
            UserRoleAuthorizations = new HashSet<UserRoleAuthorization>();
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleID { get; set; }
        public string SiteMenu { get; set; }
        public string Role { get; set; }

        public virtual ICollection<UserRoleAuthorization> UserRoleAuthorizations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
