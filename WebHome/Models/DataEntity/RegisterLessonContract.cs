using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class RegisterLessonContract
    {
        public int RegisterID { get; set; }
        public int ContractID { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual RegisterLesson Register { get; set; }
    }
}
