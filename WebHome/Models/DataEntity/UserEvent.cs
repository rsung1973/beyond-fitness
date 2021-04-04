using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UserEvent
    {
        public UserEvent()
        {
            GroupEvents = new HashSet<GroupEvent>();
        }

        public int EventID { get; set; }
        public int UID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ActivityProgram { get; set; }
        public string Accompanist { get; set; }
        public int? BranchID { get; set; }
        public int? EventType { get; set; }
        public int? SystemEventID { get; set; }
        public string Place { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual SystemEventBulletin SystemEvent { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
    }
}
