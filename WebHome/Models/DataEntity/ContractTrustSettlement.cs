using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractTrustSettlement
    {
        public ContractTrustSettlement()
        {
            CourseContractTrusts = new HashSet<CourseContractTrust>();
        }

        public int ContractID { get; set; }
        public int SettlementID { get; set; }
        public int TotalTrustAmount { get; set; }
        public int InitialTrustAmount { get; set; }
        public int? BookingTrustAmount { get; set; }
        public int? CurrentLiableAmount { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual Settlement Settlement { get; set; }
        public virtual ICollection<CourseContractTrust> CourseContractTrusts { get; set; }
    }
}
