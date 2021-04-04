using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DiagnosisJudgement
    {
        public int JudgementID { get; set; }
        public int FitnessID { get; set; }
        public decimal? RangeStart { get; set; }
        public decimal? RangeEnd { get; set; }
        public string Judgement { get; set; }

        public virtual FitnessDiagnosis Fitness { get; set; }
    }
}
