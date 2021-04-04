using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MonthlyCoachRevenueIndicator
    {
        public int PeriodID { get; set; }
        public int CoachID { get; set; }
        public int? AchievementGoal { get; set; }
        public int? CompleteLessonsGoal { get; set; }
        public int? AverageLessonPrice { get; set; }
        public int? LessonTuitionGoal { get; set; }
        public int? BranchID { get; set; }
        public int? ActualCompleteLessonCount { get; set; }
        public int? ActualSharedPIAchievement { get; set; }
        public int? ActualSharedMerchandiseAchievement { get; set; }
        public int? ActualNewContractAchievement { get; set; }
        public int? ActualRenewContractAchievement { get; set; }
        public int? ActualLessonAchievement { get; set; }
        public int? LevelID { get; set; }
        public int? ActualCompleteTSCount { get; set; }
        public int? ActualCompletePICount { get; set; }
        public int? BRCount { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual ServingCoach Coach { get; set; }
        public virtual ProfessionalLevel Level { get; set; }
        public virtual MonthlyIndicator Period { get; set; }
    }
}
