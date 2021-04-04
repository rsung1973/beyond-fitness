using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MonthlyRevenueGrade
    {
        public MonthlyRevenueGrade()
        {
            MonthlyBranchRevenueIndicators = new HashSet<MonthlyBranchRevenueIndicator>();
            MonthlyRevenueIndicators = new HashSet<MonthlyRevenueIndicator>();
        }

        public int GradeID { get; set; }
        public int IndicatorPercentage { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MonthlyBranchRevenueIndicator> MonthlyBranchRevenueIndicators { get; set; }
        public virtual ICollection<MonthlyRevenueIndicator> MonthlyRevenueIndicators { get; set; }
    }
}
