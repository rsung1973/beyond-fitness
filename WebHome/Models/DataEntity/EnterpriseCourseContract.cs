using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class EnterpriseCourseContract
    {
        public EnterpriseCourseContract()
        {
            EnterpriseCourseContents = new HashSet<EnterpriseCourseContent>();
            EnterpriseCourseMembers = new HashSet<EnterpriseCourseMember>();
            EnterpriseCoursePayments = new HashSet<EnterpriseCoursePayment>();
            RegisterLessonEnterprises = new HashSet<RegisterLessonEnterprise>();
        }

        public int ContractID { get; set; }
        public int CompanyID { get; set; }
        public string Remark { get; set; }
        public string Subject { get; set; }
        public string ContractNo { get; set; }
        public int? TotalCost { get; set; }
        public int? BranchID { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? Expiration { get; set; }
        public int GroupingMemberCount { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual Organization Company { get; set; }
        public virtual ICollection<EnterpriseCourseContent> EnterpriseCourseContents { get; set; }
        public virtual ICollection<EnterpriseCourseMember> EnterpriseCourseMembers { get; set; }
        public virtual ICollection<EnterpriseCoursePayment> EnterpriseCoursePayments { get; set; }
        public virtual ICollection<RegisterLessonEnterprise> RegisterLessonEnterprises { get; set; }
    }
}
