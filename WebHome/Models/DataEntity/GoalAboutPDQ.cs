using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class GoalAboutPDQ
    {
        public GoalAboutPDQ()
        {
            PDQUserAssessments = new HashSet<PDQUserAssessment>();
        }

        public int GoalID { get; set; }
        public string Goal { get; set; }

        public virtual ICollection<PDQUserAssessment> PDQUserAssessments { get; set; }
    }
}
