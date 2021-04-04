using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DailyWorkingHour
    {
        public DailyWorkingHour()
        {
            LessonTimes = new HashSet<LessonTime>();
        }

        public int Hour { get; set; }

        public virtual ICollection<LessonTime> LessonTimes { get; set; }
    }
}
