using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractMonthlySummary
    {
        public int ContractID { get; set; }
        public DateTime SettlementDate { get; set; }
        public int TotalPrepaid { get; set; }
        public int TotalLessonCost { get; set; }
        public int RemainedAmount { get; set; }
        public int? TotalAllowanceAmount { get; set; }

        public virtual CourseContract Contract { get; set; }
    }
}
