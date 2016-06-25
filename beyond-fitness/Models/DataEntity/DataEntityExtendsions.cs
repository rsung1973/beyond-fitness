using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHome.Models.DataEntity
{
    public static class DataEntityExtendsions
    {
    }

    public partial class UserProfile
    {
        public UserRole CurrentUserRole
        {
            get
            {
                return this.UserRole[0];
            }
        }
    }
}