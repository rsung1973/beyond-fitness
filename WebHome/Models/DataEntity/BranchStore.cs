using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BranchStore
    {
        public BranchStore()
        {
            CoachBranchMonthlyBonus = new HashSet<CoachBranchMonthlyBonu>();
            CoachMonthlySalaries = new HashSet<CoachMonthlySalary>();
            CoachWorkplaces = new HashSet<CoachWorkplace>();
            CourseContractExtensions = new HashSet<CourseContractExtension>();
            CourseContractRevisionItems = new HashSet<CourseContractRevisionItem>();
            EnterpriseCourseContracts = new HashSet<EnterpriseCourseContract>();
            LessonPriceTypes = new HashSet<LessonPriceType>();
            LessonTimeSettlements = new HashSet<LessonTimeSettlement>();
            LessonTimes = new HashSet<LessonTime>();
            MonthlyBranchIndicators = new HashSet<MonthlyBranchIndicator>();
            MonthlyBranchRevenueIndicators = new HashSet<MonthlyBranchRevenueIndicator>();
            MonthlyCoachRevenueIndicators = new HashSet<MonthlyCoachRevenueIndicator>();
            PaymentTransactions = new HashSet<PaymentTransaction>();
            RegisterLessons = new HashSet<RegisterLesson>();
            TuitionAchievements = new HashSet<TuitionAchievement>();
            UserEvents = new HashSet<UserEvent>();
        }

        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public int? ManagerID { get; set; }
        public int? ViceManagerID { get; set; }

        public virtual Organization Branch { get; set; }
        public virtual UserProfile Manager { get; set; }
        public virtual UserProfile ViceManager { get; set; }
        public virtual ICollection<CoachBranchMonthlyBonu> CoachBranchMonthlyBonus { get; set; }
        public virtual ICollection<CoachMonthlySalary> CoachMonthlySalaries { get; set; }
        public virtual ICollection<CoachWorkplace> CoachWorkplaces { get; set; }
        public virtual ICollection<CourseContractExtension> CourseContractExtensions { get; set; }
        public virtual ICollection<CourseContractRevisionItem> CourseContractRevisionItems { get; set; }
        public virtual ICollection<EnterpriseCourseContract> EnterpriseCourseContracts { get; set; }
        public virtual ICollection<LessonPriceType> LessonPriceTypes { get; set; }
        public virtual ICollection<LessonTimeSettlement> LessonTimeSettlements { get; set; }
        public virtual ICollection<LessonTime> LessonTimes { get; set; }
        public virtual ICollection<MonthlyBranchIndicator> MonthlyBranchIndicators { get; set; }
        public virtual ICollection<MonthlyBranchRevenueIndicator> MonthlyBranchRevenueIndicators { get; set; }
        public virtual ICollection<MonthlyCoachRevenueIndicator> MonthlyCoachRevenueIndicators { get; set; }
        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
        public virtual ICollection<TuitionAchievement> TuitionAchievements { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}
