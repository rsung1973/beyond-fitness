using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQQuestion
    {
        public PDQQuestion()
        {
            PDQGroups = new HashSet<PDQGroup>();
            PDQSuggestions = new HashSet<PDQSuggestion>();
            PDQTasks = new HashSet<PDQTask>();
        }

        public int QuestionID { get; set; }
        public string Question { get; set; }
        public int? QuestionType { get; set; }
        public bool? RightAnswer { get; set; }
        public int? QuestionNo { get; set; }
        public int? GroupID { get; set; }
        public int? AskerID { get; set; }

        public virtual UserProfile Asker { get; set; }
        public virtual PDQGroup Group { get; set; }
        public virtual LevelExpression QuestionTypeNavigation { get; set; }
        public virtual PDQQuestionExtension PDQQuestionExtension { get; set; }
        public virtual PDQType PDQType { get; set; }
        public virtual ICollection<PDQGroup> PDQGroups { get; set; }
        public virtual ICollection<PDQSuggestion> PDQSuggestions { get; set; }
        public virtual ICollection<PDQTask> PDQTasks { get; set; }
    }
}
