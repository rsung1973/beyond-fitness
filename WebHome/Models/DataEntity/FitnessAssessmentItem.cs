using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class FitnessAssessmentItem
    {
        public FitnessAssessmentItem()
        {
            DiagnosisAssessments = new HashSet<DiagnosisAssessment>();
            FitnessAssessmentGroups = new HashSet<FitnessAssessmentGroup>();
            FitnessDiagnoses = new HashSet<FitnessDiagnosis>();
            LearnerFitnessAssessmentResults = new HashSet<LearnerFitnessAssessmentResult>();
            LessonFitnessAssessmentReports = new HashSet<LessonFitnessAssessmentReport>();
        }

        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public int? GroupID { get; set; }
        public bool? UseSingleSide { get; set; }
        public bool? UseCustom { get; set; }

        public virtual FitnessAssessmentGroup Group { get; set; }
        public virtual ICollection<DiagnosisAssessment> DiagnosisAssessments { get; set; }
        public virtual ICollection<FitnessAssessmentGroup> FitnessAssessmentGroups { get; set; }
        public virtual ICollection<FitnessDiagnosis> FitnessDiagnoses { get; set; }
        public virtual ICollection<LearnerFitnessAssessmentResult> LearnerFitnessAssessmentResults { get; set; }
        public virtual ICollection<LessonFitnessAssessmentReport> LessonFitnessAssessmentReports { get; set; }
    }
}
