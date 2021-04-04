using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BodyDiagnosis
    {
        public BodyDiagnosis()
        {
            BodySufferings = new HashSet<BodySuffering>();
            DiagnosisAssessments = new HashSet<DiagnosisAssessment>();
        }

        public int DiagnosisID { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public int LearnerID { get; set; }
        public int CoachID { get; set; }
        public int LevelID { get; set; }
        public string Goal { get; set; }
        public string Description { get; set; }

        public virtual UserProfile Coach { get; set; }
        public virtual UserProfile Learner { get; set; }
        public virtual LevelExpression Level { get; set; }
        public virtual ICollection<BodySuffering> BodySufferings { get; set; }
        public virtual ICollection<DiagnosisAssessment> DiagnosisAssessments { get; set; }
    }
}
