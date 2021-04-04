using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class GroupingLesson
    {
        public GroupingLesson()
        {
            EnterpriseCourseMembers = new HashSet<EnterpriseCourseMember>();
            LessonTimes = new HashSet<LessonTime>();
            RegisterLessons = new HashSet<RegisterLesson>();
        }

        public int GroupID { get; set; }

        public virtual ICollection<EnterpriseCourseMember> EnterpriseCourseMembers { get; set; }
        public virtual ICollection<LessonTime> LessonTimes { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
    }
}
