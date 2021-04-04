using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MonthlyBranchRevenueIndicator
    {
        public int PeriodID { get; set; }
        public int BranchID { get; set; }
        public int GradeID { get; set; }
        public int RevenueGoal { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual MonthlyRevenueGrade Grade { get; set; }
        public virtual MonthlyBranchIndicator MonthlyBranchIndicator { get; set; }
        public virtual MonthlyIndicator Period { get; set; }
        public virtual MonthlyBranchRevenueGoal MonthlyBranchRevenueGoal { get; set; }
    }
}
