using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractTrustTrackBak
    {
        public int TrustID { get; set; }
        public int ContractID { get; set; }
        public DateTime EventDate { get; set; }
        public string TrustType { get; set; }
        public int? SettlementID { get; set; }
        public int? LessonID { get; set; }
        public int? PaymentID { get; set; }
        public int? VoidID { get; set; }
    }
}
