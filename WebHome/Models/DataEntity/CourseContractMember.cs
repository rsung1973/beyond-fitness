using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractMember
    {
        public CourseContractMember()
        {
            CourseContractSignatures = new HashSet<CourseContractSignature>();
        }

        public int ContractID { get; set; }
        public int UID { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual ICollection<CourseContractSignature> CourseContractSignatures { get; set; }
    }
}
