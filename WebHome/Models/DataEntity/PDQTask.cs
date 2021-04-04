using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQTask
    {
        public PDQTask()
        {
            PDQTaskItems = new HashSet<PDQTaskItem>();
        }

        public int TaskID { get; set; }
        public int UID { get; set; }
        public int? SuggestionID { get; set; }
        public int? QuestionID { get; set; }
        public string PDQAnswer { get; set; }
        public bool? YesOrNo { get; set; }
        public DateTime? TaskDate { get; set; }
        public int? QuestionnaireID { get; set; }

        public virtual PDQQuestion Question { get; set; }
        public virtual QuestionnaireRequest Questionnaire { get; set; }
        public virtual PDQSuggestion Suggestion { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual PDQTaskBonu PDQTaskBonu { get; set; }
        public virtual ICollection<PDQTaskItem> PDQTaskItems { get; set; }
    }
}
