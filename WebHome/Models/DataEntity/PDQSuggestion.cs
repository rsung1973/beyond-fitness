using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQSuggestion
    {
        public PDQSuggestion()
        {
            PDQTaskItems = new HashSet<PDQTaskItem>();
            PDQTasks = new HashSet<PDQTask>();
        }

        public int SuggestionID { get; set; }
        public int QuestionID { get; set; }
        public string Suggestion { get; set; }
        public bool? RightAnswer { get; set; }

        public virtual PDQQuestion Question { get; set; }
        public virtual ICollection<PDQTaskItem> PDQTaskItems { get; set; }
        public virtual ICollection<PDQTask> PDQTasks { get; set; }
    }
}
