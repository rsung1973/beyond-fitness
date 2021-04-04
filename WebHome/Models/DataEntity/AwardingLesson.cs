using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class AwardingLesson
    {
        public int AwardID { get; set; }
        public int RegisterID { get; set; }

        public virtual LearnerAward Award { get; set; }
        public virtual RegisterLesson Register { get; set; }
    }
}
