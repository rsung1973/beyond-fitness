using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonTrend
    {
        public int LessonID { get; set; }
        public int? ActionLearning { get; set; }
        public int? PostureRedress { get; set; }
        public int? Training { get; set; }
        public int? Counseling { get; set; }

        public virtual LessonTime Lesson { get; set; }
    }
}
