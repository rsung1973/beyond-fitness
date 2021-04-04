using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ResetPassword
    {
        public Guid ResetID { get; set; }
        public int UID { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
    }
}
