using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CoachBranchMonthlyBonu
    {
        public int CoachID { get; set; }
        public int SettlementID { get; set; }
        public int BranchID { get; set; }
        public int? Tuition { get; set; }
        public int? AttendanceBonus { get; set; }
        public decimal? AchievementAttendanceCount { get; set; }
        public int? BranchTotalAttendanceCount { get; set; }
        public int? BranchTotalTuition { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual CoachMonthlySalary CoachMonthlySalary { get; set; }
    }
}
