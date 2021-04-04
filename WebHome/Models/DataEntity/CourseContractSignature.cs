using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractSignature
    {
        public int SignatureID { get; set; }
        public int ContractID { get; set; }
        public int UID { get; set; }
        public string SignatureName { get; set; }
        public string Signature { get; set; }

        public virtual CourseContractMember CourseContractMember { get; set; }
    }
}
