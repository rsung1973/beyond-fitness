using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CoachWorkplace
    {
        public int CoachID { get; set; }
        public int BranchID { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual ServingCoach Coach { get; set; }
    }
}
