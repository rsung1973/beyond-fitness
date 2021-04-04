using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class V_LearnerFitenessAssessment
    {
        public int UID { get; set; }
        public string Gender { get; set; }
        public int? AthleticLevel { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal Assessment { get; set; }
        public int? Years { get; set; }
    }
}
