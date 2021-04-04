using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LearnerFitnessAdvisor
    {
        public int UID { get; set; }
        public int CoachID { get; set; }

        public virtual ServingCoach Coach { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
