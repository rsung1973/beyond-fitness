using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContract
    {
        public CourseContract()
        {
            ContractElements = new HashSet<ContractElement>();
            ContractMonthlySummaries = new HashSet<ContractMonthlySummary>();
            ContractPayments = new HashSet<ContractPayment>();
            ContractTrustSettlements = new HashSet<ContractTrustSettlement>();
            ContractTrustTracks = new HashSet<ContractTrustTrack>();
            CourseContractLevels = new HashSet<CourseContractLevel>();
            CourseContractMembers = new HashSet<CourseContractMember>();
            CourseContractRevisionOriginalContractNavigations = new HashSet<CourseContractRevision>();
            RegisterLessonContracts = new HashSet<RegisterLessonContract>();
        }

        public int ContractID { get; set; }
        public int ContractType { get; set; }
        public DateTime? ContractDate { get; set; }
        public string Subject { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? Expiration { get; set; }
        public int OwnerID { get; set; }
        public int? SequenceNo { get; set; }
        public int? Lessons { get; set; }
        public int PriceID { get; set; }
        public string Remark { get; set; }
        public int FitnessConsultant { get; set; }
        public int Status { get; set; }
        public int AgentID { get; set; }
        public string ContractNo { get; set; }
        public int? TotalCost { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public bool? Installment { get; set; }
        public bool? Renewal { get; set; }
        public int? InstallmentID { get; set; }
        public DateTime? PayoffDue { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? Entrusted { get; set; }
        public int? SupervisorID { get; set; }

        public virtual UserProfile Agent { get; set; }
        public virtual CourseContractType ContractTypeNavigation { get; set; }
        public virtual ServingCoach FitnessConsultantNavigation { get; set; }
        public virtual ContractInstallment InstallmentNavigation { get; set; }
        public virtual UserProfile Owner { get; set; }
        public virtual LessonPriceType Price { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual UserProfile Supervisor { get; set; }
        public virtual CourseContractExtension CourseContractExtension { get; set; }
        public virtual CourseContractRevision CourseContractRevisionRevision { get; set; }
        public virtual CourseContractTrust CourseContractTrust { get; set; }
        public virtual ICollection<ContractElement> ContractElements { get; set; }
        public virtual ICollection<ContractMonthlySummary> ContractMonthlySummaries { get; set; }
        public virtual ICollection<ContractPayment> ContractPayments { get; set; }
        public virtual ICollection<ContractTrustSettlement> ContractTrustSettlements { get; set; }
        public virtual ICollection<ContractTrustTrack> ContractTrustTracks { get; set; }
        public virtual ICollection<CourseContractLevel> CourseContractLevels { get; set; }
        public virtual ICollection<CourseContractMember> CourseContractMembers { get; set; }
        public virtual ICollection<CourseContractRevision> CourseContractRevisionOriginalContractNavigations { get; set; }
        public virtual ICollection<RegisterLessonContract> RegisterLessonContracts { get; set; }
    }
}
