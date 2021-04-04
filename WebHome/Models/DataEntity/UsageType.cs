using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UsageType
    {
        public UsageType()
        {
            LessonPriceTypes = new HashSet<LessonPriceType>();
        }

        public int UsageID { get; set; }
        public string Usage { get; set; }

        public virtual ICollection<LessonPriceType> LessonPriceTypes { get; set; }
    }
}
