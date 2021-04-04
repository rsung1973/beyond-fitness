using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CoachMonthlySalary
    {
        public CoachMonthlySalary()
        {
            CoachBranchMonthlyBonus = new HashSet<CoachBranchMonthlyBonu>();
        }

        public int CoachID { get; set; }
        public int SettlementID { get; set; }
        public int? WorkPlace { get; set; }
        public int LevelID { get; set; }
        public decimal GradeIndex { get; set; }
        public int? ManagerBonus { get; set; }
        public int? SpecialBonus { get; set; }
        public int? PerformanceAchievement { get; set; }
        public decimal? AchievementShareRatio { get; set; }

        public virtual ServingCoach Coach { get; set; }
        public virtual ProfessionalLevel Level { get; set; }
        public virtual Settlement Settlement { get; set; }
        public virtual BranchStore WorkPlaceNavigation { get; set; }
        public virtual ICollection<CoachBranchMonthlyBonu> CoachBranchMonthlyBonus { get; set; }
    }
}
