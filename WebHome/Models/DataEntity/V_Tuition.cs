using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class V_Tuition
    {
        public int LessonID { get; set; }
        public DateTime? ClassTime { get; set; }
        public int? AttendingCoach { get; set; }
        public int? GroupID { get; set; }
        public int? BranchID { get; set; }
        public int? CoachAttendance { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? CommitAttendance { get; set; }
        public int PriceID { get; set; }
        public int? PriceStatus { get; set; }
        public int? ELStatus { get; set; }
        public decimal? AchievementIndex { get; set; }
        public decimal? TuitionIndex { get; set; }
        public int RegisterID { get; set; }
        public int GroupingMemberCount { get; set; }
        public int? ListPrice { get; set; }
        public int? PercentageOfDiscount { get; set; }
        public int ProfessionalLevelID { get; set; }
        public decimal? MarkedGradeIndex { get; set; }
        public string BranchName { get; set; }
        public string OfficeLocation { get; set; }
        public int? EnterpriseRegisterID { get; set; }
        public int? EnterpriseListPrice { get; set; }
        public int? WorkPlace { get; set; }
        public int? EnterpriseContractID { get; set; }
        public int? ContractID { get; set; }
        public int? SettlementStatus { get; set; }
        public int? CoachWorkPlace { get; set; }
        public int? SettlementID { get; set; }
    }
}
