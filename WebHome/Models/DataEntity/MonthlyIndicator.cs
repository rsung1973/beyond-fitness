using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MonthlyIndicator
    {
        public MonthlyIndicator()
        {
            MonthlyBranchIndicators = new HashSet<MonthlyBranchIndicator>();
            MonthlyBranchRevenueIndicators = new HashSet<MonthlyBranchRevenueIndicator>();
            MonthlyCoachRevenueIndicators = new HashSet<MonthlyCoachRevenueIndicator>();
            MonthlyRevenueIndicators = new HashSet<MonthlyRevenueIndicator>();
        }

        public int PeriodID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndExclusiveDate { get; set; }

        public virtual ICollection<MonthlyBranchIndicator> MonthlyBranchIndicators { get; set; }
        public virtual ICollection<MonthlyBranchRevenueIndicator> MonthlyBranchRevenueIndicators { get; set; }
        public virtual ICollection<MonthlyCoachRevenueIndicator> MonthlyCoachRevenueIndicators { get; set; }
        public virtual ICollection<MonthlyRevenueIndicator> MonthlyRevenueIndicators { get; set; }
    }
}
