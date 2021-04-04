using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class Settlement
    {
        public Settlement()
        {
            CoachMonthlySalaries = new HashSet<CoachMonthlySalary>();
            ContractTrustSettlements = new HashSet<ContractTrustSettlement>();
            ContractTrustTracks = new HashSet<ContractTrustTrack>();
            LessonTimeSettlements = new HashSet<LessonTimeSettlement>();
        }

        public int SettlementID { get; set; }
        public DateTime SettlementDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndExclusiveDate { get; set; }

        public virtual ICollection<CoachMonthlySalary> CoachMonthlySalaries { get; set; }
        public virtual ICollection<ContractTrustSettlement> ContractTrustSettlements { get; set; }
        public virtual ICollection<ContractTrustTrack> ContractTrustTracks { get; set; }
        public virtual ICollection<LessonTimeSettlement> LessonTimeSettlements { get; set; }
    }
}
