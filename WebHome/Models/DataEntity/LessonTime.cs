using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonTime
    {
        public LessonTime()
        {
            ContractTrustTracks = new HashSet<ContractTrustTrack>();
            LessonFeedBacks = new HashSet<LessonFeedBack>();
            LessonFitnessAssessments = new HashSet<LessonFitnessAssessment>();
            LessonTimeExpansions = new HashSet<LessonTimeExpansion>();
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int LessonID { get; set; }
        public int RegisterID { get; set; }
        public DateTime? ClassTime { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? InvitedCoach { get; set; }
        public int? AttendingCoach { get; set; }
        public int? GroupID { get; set; }
        public int? TrainingBySelf { get; set; }
        public int? BranchID { get; set; }
        public int? HourOfClassTime { get; set; }
        public string Place { get; set; }

        public virtual ServingCoach AttendingCoachNavigation { get; set; }
        public virtual BranchStore Branch { get; set; }
        public virtual GroupingLesson Group { get; set; }
        public virtual DailyWorkingHour HourOfClassTimeNavigation { get; set; }
        public virtual ServingCoach InvitedCoachNavigation { get; set; }
        public virtual RegisterLesson Register { get; set; }
        public virtual FitnessAssessment FitnessAssessment { get; set; }
        public virtual LessonAttendance LessonAttendance { get; set; }
        public virtual LessonPlan LessonPlan { get; set; }
        public virtual LessonTimeSettlement LessonTimeSettlement { get; set; }
        public virtual LessonTrend LessonTrend { get; set; }
        public virtual PreferredLessonTime PreferredLessonTime { get; set; }
        public virtual ICollection<ContractTrustTrack> ContractTrustTracks { get; set; }
        public virtual ICollection<LessonFeedBack> LessonFeedBacks { get; set; }
        public virtual ICollection<LessonFitnessAssessment> LessonFitnessAssessments { get; set; }
        public virtual ICollection<LessonTimeExpansion> LessonTimeExpansions { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
