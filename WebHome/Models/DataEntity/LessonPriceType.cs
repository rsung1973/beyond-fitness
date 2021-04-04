using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonPriceType
    {
        public LessonPriceType()
        {
            BonusAwardingLessons = new HashSet<BonusAwardingLesson>();
            CourseContracts = new HashSet<CourseContract>();
            LessonPriceProperties = new HashSet<LessonPriceProperty>();
            RegisterLessons = new HashSet<RegisterLesson>();
        }

        public int PriceID { get; set; }
        public string Description { get; set; }
        public int? ListPrice { get; set; }
        public int? Status { get; set; }
        public int? UsageType { get; set; }
        public int? CoachPayoff { get; set; }
        public int? CoachPayoffCreditCard { get; set; }
        public int? ExcludeQuestionnaire { get; set; }
        public int? LowerLimit { get; set; }
        public int? UpperBound { get; set; }
        public int? BranchID { get; set; }
        public int? DiscountedPrice { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? SeriesID { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual LessonPriceSeries Series { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual UsageType UsageTypeNavigation { get; set; }
        public virtual IsInternalLesson IsInternalLesson { get; set; }
        public virtual IsWelfareGiftLesson IsWelfareGiftLesson { get; set; }
        public virtual LessonPriceSeries LessonPriceSeries { get; set; }
        public virtual ICollection<BonusAwardingLesson> BonusAwardingLessons { get; set; }
        public virtual ICollection<CourseContract> CourseContracts { get; set; }
        public virtual ICollection<LessonPriceProperty> LessonPriceProperties { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
    }
}
