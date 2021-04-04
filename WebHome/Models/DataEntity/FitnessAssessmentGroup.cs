using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class FitnessAssessmentGroup
    {
        public FitnessAssessmentGroup()
        {
            FitnessAssessmentItems = new HashSet<FitnessAssessmentItem>();
        }

        public int GroupID { get; set; }
        public int? MajorID { get; set; }
        public string GroupName { get; set; }

        public virtual FitnessAssessmentItem Major { get; set; }
        public virtual ICollection<FitnessAssessmentItem> FitnessAssessmentItems { get; set; }
    }
}
