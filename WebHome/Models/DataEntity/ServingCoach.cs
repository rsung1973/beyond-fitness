using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ServingCoach
    {
        public ServingCoach()
        {
            CoachCertificates = new HashSet<CoachCertificate>();
            CoachMonthlySalaries = new HashSet<CoachMonthlySalary>();
            CoachRatings = new HashSet<CoachRating>();
            CoachWorkplaces = new HashSet<CoachWorkplace>();
            ContractElements = new HashSet<ContractElement>();
            CourseContractRevisionItems = new HashSet<CourseContractRevisionItem>();
            CourseContracts = new HashSet<CourseContract>();
            LearnerFitnessAdvisors = new HashSet<LearnerFitnessAdvisor>();
            LessonTimeAttendingCoachNavigations = new HashSet<LessonTime>();
            LessonTimeInvitedCoachNavigations = new HashSet<LessonTime>();
            MonthlyCoachRevenueIndicators = new HashSet<MonthlyCoachRevenueIndicator>();
            RegisterLessons = new HashSet<RegisterLesson>();
            TuitionAchievements = new HashSet<TuitionAchievement>();
        }

        public int CoachID { get; set; }
        public string Description { get; set; }
        public int? LevelID { get; set; }
        public DateTime? EmploymentDate { get; set; }

        public virtual UserProfile Coach { get; set; }
        public virtual ProfessionalLevel Level { get; set; }
        public virtual ICollection<CoachCertificate> CoachCertificates { get; set; }
        public virtual ICollection<CoachMonthlySalary> CoachMonthlySalaries { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
        public virtual ICollection<CoachWorkplace> CoachWorkplaces { get; set; }
        public virtual ICollection<ContractElement> ContractElements { get; set; }
        public virtual ICollection<CourseContractRevisionItem> CourseContractRevisionItems { get; set; }
        public virtual ICollection<CourseContract> CourseContracts { get; set; }
        public virtual ICollection<LearnerFitnessAdvisor> LearnerFitnessAdvisors { get; set; }
        public virtual ICollection<LessonTime> LessonTimeAttendingCoachNavigations { get; set; }
        public virtual ICollection<LessonTime> LessonTimeInvitedCoachNavigations { get; set; }
        public virtual ICollection<MonthlyCoachRevenueIndicator> MonthlyCoachRevenueIndicators { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
        public virtual ICollection<TuitionAchievement> TuitionAchievements { get; set; }
    }
}
