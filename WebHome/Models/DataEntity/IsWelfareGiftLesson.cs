using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class IsWelfareGiftLesson
    {
        public int PriceID { get; set; }

        public virtual LessonPriceType Price { get; set; }
    }
}
