using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class Payment
    {
        public Payment()
        {
            ContractTrustTracks = new HashSet<ContractTrustTrack>();
            TuitionAchievements = new HashSet<TuitionAchievement>();
        }

        public int PaymentID { get; set; }
        public int? PayoffAmount { get; set; }
        public DateTime? PayoffDate { get; set; }
        public int? Status { get; set; }
        public int? HandlerID { get; set; }
        public string PaymentType { get; set; }
        public int? InvoiceID { get; set; }
        public int? TransactionType { get; set; }
        public string Remark { get; set; }
        public int? AllowanceID { get; set; }
        public int? AdjustmentAmount { get; set; }

        public virtual InvoiceAllowance Allowance { get; set; }
        public virtual UserProfile Handler { get; set; }
        public virtual InvoiceItem Invoice { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual ContractPayment ContractPayment { get; set; }
        public virtual EnterpriseCoursePayment EnterpriseCoursePayment { get; set; }
        public virtual PaymentAudit PaymentAudit { get; set; }
        public virtual PaymentTransaction PaymentTransaction { get; set; }
        public virtual TuitionInstallment TuitionInstallment { get; set; }
        public virtual VoidPayment VoidPayment { get; set; }
        public virtual ICollection<ContractTrustTrack> ContractTrustTracks { get; set; }
        public virtual ICollection<TuitionAchievement> TuitionAchievements { get; set; }
    }
}
