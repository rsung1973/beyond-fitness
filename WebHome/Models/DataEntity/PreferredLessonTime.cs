using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PreferredLessonTime
    {
        public int LessonID { get; set; }
        public int? ApproverID { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public virtual UserProfile Approver { get; set; }
        public virtual LessonTime Lesson { get; set; }
    }
}
