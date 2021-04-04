using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonPriceProperty
    {
        public int PriceID { get; set; }
        public int PropertyID { get; set; }

        public virtual LessonPriceType Price { get; set; }
        public virtual LevelExpression Property { get; set; }
    }
}
