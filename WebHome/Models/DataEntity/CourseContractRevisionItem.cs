using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractRevisionItem
    {
        public int RevisionID { get; set; }
        public int FitnessConsultant { get; set; }
        public int BranchID { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual ServingCoach FitnessConsultantNavigation { get; set; }
        public virtual CourseContractRevision Revision { get; set; }
    }
}
