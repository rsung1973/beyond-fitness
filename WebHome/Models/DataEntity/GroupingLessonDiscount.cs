using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class GroupingLessonDiscount
    {
        public GroupingLessonDiscount()
        {
            CourseContractTypes = new HashSet<CourseContractType>();
            RegisterLessons = new HashSet<RegisterLesson>();
        }

        public int GroupingMemberCount { get; set; }
        public int? PercentageOfDiscount { get; set; }

        public virtual ICollection<CourseContractType> CourseContractTypes { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
    }
}
