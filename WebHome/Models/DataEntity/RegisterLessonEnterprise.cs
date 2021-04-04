using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class RegisterLessonEnterprise
    {
        public int RegisterID { get; set; }
        public int? ContractID { get; set; }
        public int? TypeID { get; set; }

        public virtual EnterpriseCourseContract Contract { get; set; }
        public virtual EnterpriseCourseContent EnterpriseCourseContent { get; set; }
        public virtual RegisterLesson Register { get; set; }
    }
}
