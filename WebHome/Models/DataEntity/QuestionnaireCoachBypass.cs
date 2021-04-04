using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class QuestionnaireCoachBypass
    {
        public int QuestionnaireID { get; set; }
        public int UID { get; set; }

        public virtual QuestionnaireRequest Questionnaire { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
