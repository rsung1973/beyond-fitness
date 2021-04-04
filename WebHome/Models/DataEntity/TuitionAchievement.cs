using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TuitionAchievement
    {
        public int InstallmentID { get; set; }
        public int CoachID { get; set; }
        public int? ShareAmount { get; set; }
        public DateTime? CommitShare { get; set; }
        public int? CoachWorkPlace { get; set; }

        public virtual ServingCoach Coach { get; set; }
        public virtual BranchStore CoachWorkPlaceNavigation { get; set; }
        public virtual Payment Installment { get; set; }
    }
}
