using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonPriceSeries
    {
        public LessonPriceSeries()
        {
            LessonPriceTypes = new HashSet<LessonPriceType>();
        }

        public int PriceID { get; set; }
        public int? Year { get; set; }
        public int? PeriodNo { get; set; }
        public int? Status { get; set; }

        public virtual LessonPriceType Price { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual ICollection<LessonPriceType> LessonPriceTypes { get; set; }
    }
}
