using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MonthlyRevenueGoal
    {
        public int PeriodID { get; set; }
        public int GradeID { get; set; }
        public decimal? CustomIndicatorPercentage { get; set; }
        public int? CustomRevenueGoal { get; set; }
        public int? ActualLessonAchievement { get; set; }
        public int? ActualSharedAchievement { get; set; }
        public int? ActualCompleteLessonCount { get; set; }
        public int? AchievementGoal { get; set; }
        public int? CompleteLessonsGoal { get; set; }
        public int? LessonTuitionGoal { get; set; }
        public int? NewContractCount { get; set; }
        public int? RenewContractCount { get; set; }
        public int? ActualCompleteTSCount { get; set; }
        public int? ActualCompletePICount { get; set; }
        public int? NewContractSubtotal { get; set; }
        public int? RenewContractSubtotal { get; set; }

        public virtual MonthlyRevenueIndicator MonthlyRevenueIndicator { get; set; }
    }
}
