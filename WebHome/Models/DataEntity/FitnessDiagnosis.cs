using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class FitnessDiagnosis
    {
        public FitnessDiagnosis()
        {
            DiagnosisJudgements = new HashSet<DiagnosisJudgement>();
        }

        public int FitnessID { get; set; }
        public int ItemID { get; set; }
        public string Gender { get; set; }
        public int? YearsOldStart { get; set; }
        public int? YearsOldEnd { get; set; }

        public virtual FitnessAssessmentItem Item { get; set; }
        public virtual ICollection<DiagnosisJudgement> DiagnosisJudgements { get; set; }
    }
}
