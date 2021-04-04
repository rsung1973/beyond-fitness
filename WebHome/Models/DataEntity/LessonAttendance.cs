using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonAttendance
    {
        public int LessonID { get; set; }
        public DateTime CompleteDate { get; set; }

        public virtual LessonTime Lesson { get; set; }
    }
}
