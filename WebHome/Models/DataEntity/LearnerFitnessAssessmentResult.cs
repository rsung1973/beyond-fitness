using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LearnerFitnessAssessmentResult
    {
        public int AssessmentID { get; set; }
        public int ItemID { get; set; }
        public decimal Assessment { get; set; }

        public virtual LearnerFitnessAssessment AssessmentNavigation { get; set; }
        public virtual FitnessAssessmentItem Item { get; set; }
    }
}
