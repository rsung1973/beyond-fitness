using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class RegisterLesson
    {
        public RegisterLesson()
        {
            AwardingLessonGifts = new HashSet<AwardingLessonGift>();
            AwardingLessons = new HashSet<AwardingLesson>();
            LessonFeedBacks = new HashSet<LessonFeedBack>();
            LessonTimeExpansions = new HashSet<LessonTimeExpansion>();
            LessonTimes = new HashSet<LessonTime>();
            QuestionnaireRequests = new HashSet<QuestionnaireRequest>();
            TrainingPlans = new HashSet<TrainingPlan>();
        }

        public int RegisterID { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Lessons { get; set; }
        public int UID { get; set; }
        public int? ClassLevel { get; set; }
        public int? Attended { get; set; }
        public int? RegisterGroupID { get; set; }
        public int GroupingMemberCount { get; set; }
        public int? AdvisorID { get; set; }
        public int? AttendedLessons { get; set; }
        public int? BranchID { get; set; }
        public bool? MasterRegistration { get; set; }

        public virtual ServingCoach Advisor { get; set; }
        public virtual LevelExpression AttendedNavigation { get; set; }
        public virtual BranchStore Branch { get; set; }
        public virtual LessonPriceType ClassLevelNavigation { get; set; }
        public virtual GroupingLessonDiscount GroupingMemberCountNavigation { get; set; }
        public virtual GroupingLesson RegisterGroup { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual IntuitionCharge IntuitionCharge { get; set; }
        public virtual RegisterLessonContract RegisterLessonContract { get; set; }
        public virtual RegisterLessonEnterprise RegisterLessonEnterprise { get; set; }
        public virtual ICollection<AwardingLessonGift> AwardingLessonGifts { get; set; }
        public virtual ICollection<AwardingLesson> AwardingLessons { get; set; }
        public virtual ICollection<LessonFeedBack> LessonFeedBacks { get; set; }
        public virtual ICollection<LessonTimeExpansion> LessonTimeExpansions { get; set; }
        public virtual ICollection<LessonTime> LessonTimes { get; set; }
        public virtual ICollection<QuestionnaireRequest> QuestionnaireRequests { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
    }
}
