using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class QuestionnaireRequest
    {
        public QuestionnaireRequest()
        {
            PDQTasks = new HashSet<PDQTask>();
        }

        public int QuestionnaireID { get; set; }
        public DateTime? RequestDate { get; set; }
        public int GroupID { get; set; }
        public int UID { get; set; }
        public int? Status { get; set; }
        public int? RegisterID { get; set; }
        public int? PartID { get; set; }

        public virtual PDQGroup Group { get; set; }
        public virtual RegisterLesson Register { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual QuestionnaireCoachBypass QuestionnaireCoachBypass { get; set; }
        public virtual ICollection<PDQTask> PDQTasks { get; set; }
    }
}
