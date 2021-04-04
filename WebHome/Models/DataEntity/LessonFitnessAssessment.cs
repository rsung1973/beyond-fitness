using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonFitnessAssessment
    {
        public LessonFitnessAssessment()
        {
            LessonFitnessAssessmentReports = new HashSet<LessonFitnessAssessmentReport>();
        }

        public int AssessmentID { get; set; }
        public int? LessonID { get; set; }
        public int UID { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public DateTime? FeedBackDate { get; set; }
        public string Remark { get; set; }
        public string FeedBack { get; set; }

        public virtual LessonTime Lesson { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual ICollection<LessonFitnessAssessmentReport> LessonFitnessAssessmentReports { get; set; }
    }
}
