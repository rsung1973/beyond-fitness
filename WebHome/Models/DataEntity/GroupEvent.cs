using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class GroupEvent
    {
        public int EventID { get; set; }
        public int UID { get; set; }

        public virtual UserEvent Event { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
