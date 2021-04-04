using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonPlan
    {
        public int LessonID { get; set; }
        public string Remark { get; set; }
        public string Warming { get; set; }
        public string RecentStatus { get; set; }
        public string EndingOperation { get; set; }
        public string FeedBack { get; set; }
        public DateTime? FeedBackDate { get; set; }
        public DateTime? CommitAttendance { get; set; }

        public virtual LessonTime Lesson { get; set; }
    }
}
