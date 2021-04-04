using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class EnterpriseLessonType
    {
        public EnterpriseLessonType()
        {
            EnterpriseCourseContents = new HashSet<EnterpriseCourseContent>();
        }

        public int TypeID { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EnterpriseCourseContent> EnterpriseCourseContents { get; set; }
    }
}
