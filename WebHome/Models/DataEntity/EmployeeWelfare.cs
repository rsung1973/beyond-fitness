using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class EmployeeWelfare
    {
        public int UID { get; set; }
        public int? MonthlyGiftLessons { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
    }
}
