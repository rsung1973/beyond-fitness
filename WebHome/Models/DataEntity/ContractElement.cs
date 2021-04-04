using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractElement
    {
        public int ContractID { get; set; }
        public int OriginalFitnessConsultant { get; set; }
        public int ElementID { get; set; }
        public int RevisionID { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual ServingCoach OriginalFitnessConsultantNavigation { get; set; }
        public virtual CourseContractRevision Revision { get; set; }
    }
}
