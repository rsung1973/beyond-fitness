using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingLevelAboutPDQ
    {
        public TrainingLevelAboutPDQ()
        {
            PDQUserAssessments = new HashSet<PDQUserAssessment>();
        }

        public int LevelID { get; set; }
        public string TrainingLevel { get; set; }

        public virtual ICollection<PDQUserAssessment> PDQUserAssessments { get; set; }
    }
}
