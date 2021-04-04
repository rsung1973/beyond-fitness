using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BonusAwardingLesson
    {
        public int ItemID { get; set; }
        public int PriceID { get; set; }

        public virtual BonusAwardingItem Item { get; set; }
        public virtual LessonPriceType Price { get; set; }
    }
}
