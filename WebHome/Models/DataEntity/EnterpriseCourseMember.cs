using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class EnterpriseCourseMember
    {
        public int ContractID { get; set; }
        public int UID { get; set; }
        public int? GroupID { get; set; }

        public virtual EnterpriseCourseContract Contract { get; set; }
        public virtual GroupingLesson Group { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
