using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractTrustTrack
    {
        public int TrustID { get; set; }
        public int ContractID { get; set; }
        public DateTime EventDate { get; set; }
        public string TrustType { get; set; }
        public int? SettlementID { get; set; }
        public int? LessonID { get; set; }
        public int? PaymentID { get; set; }
        public int? VoidID { get; set; }
        public int? ReturnAmount { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual LessonTime Lesson { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Settlement Settlement { get; set; }
        public virtual VoidPayment Void { get; set; }
    }
}
