using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UserRoleAuthorization
    {
        public int UID { get; set; }
        public int RoleID { get; set; }

        public virtual UserRoleDefinition Role { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
