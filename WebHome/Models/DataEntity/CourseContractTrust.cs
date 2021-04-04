using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractTrust
    {
        public int ContractID { get; set; }
        public int CurrentSettlement { get; set; }

        public virtual ContractTrustSettlement C { get; set; }
        public virtual CourseContract Contract { get; set; }
    }
}
