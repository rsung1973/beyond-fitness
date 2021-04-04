using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ProfessionalLevel
    {
        public ProfessionalLevel()
        {
            CoachMonthlySalaries = new HashSet<CoachMonthlySalary>();
            CoachRatings = new HashSet<CoachRating>();
            LessonTimeSettlements = new HashSet<LessonTimeSettlement>();
            MonthlyCoachRevenueIndicators = new HashSet<MonthlyCoachRevenueIndicator>();
            ProfessionalLevelReviewDemotions = new HashSet<ProfessionalLevelReview>();
            ProfessionalLevelReviewPromotions = new HashSet<ProfessionalLevelReview>();
            ServingCoaches = new HashSet<ServingCoach>();
        }

        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public decimal? GradeIndex { get; set; }
        public int? CategoryID { get; set; }
        public string DisplayName { get; set; }

        public virtual LevelExpression Category { get; set; }
        public virtual ProfessionalLevelReview ProfessionalLevelReviewLevel { get; set; }
        public virtual ICollection<CoachMonthlySalary> CoachMonthlySalaries { get; set; }
        public virtual ICollection<CoachRating> CoachRatings { get; set; }
        public virtual ICollection<LessonTimeSettlement> LessonTimeSettlements { get; set; }
        public virtual ICollection<MonthlyCoachRevenueIndicator> MonthlyCoachRevenueIndicators { get; set; }
        public virtual ICollection<ProfessionalLevelReview> ProfessionalLevelReviewDemotions { get; set; }
        public virtual ICollection<ProfessionalLevelReview> ProfessionalLevelReviewPromotions { get; set; }
        public virtual ICollection<ServingCoach> ServingCoaches { get; set; }
    }
}
