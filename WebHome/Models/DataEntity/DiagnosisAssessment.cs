using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DiagnosisAssessment
    {
        public int DiagnosisID { get; set; }
        public int ItemID { get; set; }
        public decimal? Assessment { get; set; }
        public string Judgement { get; set; }
        public string DiagnosisAction { get; set; }
        public string Description { get; set; }
        public decimal? AdditionalAssessment { get; set; }

        public virtual BodyDiagnosis Diagnosis { get; set; }
        public virtual FitnessAssessmentItem Item { get; set; }
    }
}
