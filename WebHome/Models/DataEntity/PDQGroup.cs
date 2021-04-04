using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQGroup
    {
        public PDQGroup()
        {
            PDQQuestions = new HashSet<PDQQuestion>();
            QuestionnaireRequests = new HashSet<QuestionnaireRequest>();
        }

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int? ConclusionID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PDQQuestion Conclusion { get; set; }
        public virtual QuestionnaireGroup QuestionnaireGroup { get; set; }
        public virtual ICollection<PDQQuestion> PDQQuestions { get; set; }
        public virtual ICollection<QuestionnaireRequest> QuestionnaireRequests { get; set; }
    }
}
