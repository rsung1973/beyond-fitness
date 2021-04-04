using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MonthlyBranchIndicator
    {
        public MonthlyBranchIndicator()
        {
            MonthlyBranchRevenueIndicators = new HashSet<MonthlyBranchRevenueIndicator>();
        }

        public int PeriodID { get; set; }
        public int BranchID { get; set; }
        public string RiskPrediction { get; set; }
        public string Strategy { get; set; }
        public string Comment { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual MonthlyIndicator Period { get; set; }
        public virtual ICollection<MonthlyBranchRevenueIndicator> MonthlyBranchRevenueIndicators { get; set; }
    }
}
