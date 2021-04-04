using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractExtension
    {
        public int ContractID { get; set; }
        public int BranchID { get; set; }
        public int? RevisionTrackingID { get; set; }
        public int? SettlementPrice { get; set; }
        public string PaymentMethod { get; set; }
        public int? Version { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual CourseContract Contract { get; set; }
        public virtual CourseContractRevision RevisionTracking { get; set; }
    }
}
