using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class EnterpriseCourseContent
    {
        public EnterpriseCourseContent()
        {
            RegisterLessonEnterprises = new HashSet<RegisterLessonEnterprise>();
        }

        public int ContractID { get; set; }
        public int TypeID { get; set; }
        public int? Lessons { get; set; }
        public int? ListPrice { get; set; }
        public int? DurationInMinutes { get; set; }

        public virtual EnterpriseCourseContract Contract { get; set; }
        public virtual EnterpriseLessonType Type { get; set; }
        public virtual ICollection<RegisterLessonEnterprise> RegisterLessonEnterprises { get; set; }
    }
}
