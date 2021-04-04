using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonTimeExpansion
    {
        public DateTime ClassDate { get; set; }
        public int Hour { get; set; }
        public int RegisterID { get; set; }
        public int? LessonID { get; set; }

        public virtual LessonTime Lesson { get; set; }
        public virtual RegisterLesson Register { get; set; }
    }
}
