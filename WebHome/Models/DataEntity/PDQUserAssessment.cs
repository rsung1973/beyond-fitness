using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQUserAssessment
    {
        public int UID { get; set; }
        public int? GoalID { get; set; }
        public int? LevelID { get; set; }
        public int? StyleID { get; set; }

        public virtual GoalAboutPDQ Goal { get; set; }
        public virtual TrainingLevelAboutPDQ Level { get; set; }
        public virtual StyleAboutPDQ Style { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
