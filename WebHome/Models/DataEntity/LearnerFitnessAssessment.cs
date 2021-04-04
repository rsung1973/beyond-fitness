using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LearnerFitnessAssessment
    {
        public LearnerFitnessAssessment()
        {
            LearnerFitnessAssessmentResults = new HashSet<LearnerFitnessAssessmentResult>();
        }

        public int AssessmentID { get; set; }
        public int UID { get; set; }
        public DateTime AssessmentDate { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
        public virtual ICollection<LearnerFitnessAssessmentResult> LearnerFitnessAssessmentResults { get; set; }
    }
}
