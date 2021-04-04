using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonFitnessAssessmentReport
    {
        public int AssessmentID { get; set; }
        public int ItemID { get; set; }
        public decimal? TotalAssessment { get; set; }
        public decimal? SingleAssessment { get; set; }
        public int? ByTimes { get; set; }
        public bool? BySingleSide { get; set; }
        public string ByCustom { get; set; }

        public virtual LessonFitnessAssessment Assessment { get; set; }
        public virtual FitnessAssessmentItem Item { get; set; }
    }
}
