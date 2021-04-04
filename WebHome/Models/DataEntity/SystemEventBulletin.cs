using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class SystemEventBulletin
    {
        public SystemEventBulletin()
        {
            UserEvents = new HashSet<UserEvent>();
        }

        public int EventID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}
