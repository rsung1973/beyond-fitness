using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractInstallment
    {
        public ContractInstallment()
        {
            CourseContracts = new HashSet<CourseContract>();
        }

        public int InstallmentID { get; set; }
        public int Installments { get; set; }

        public virtual ICollection<CourseContract> CourseContracts { get; set; }
    }
}
