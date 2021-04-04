using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQQuestionExtension
    {
        public int QuestionID { get; set; }
        public int? BonusPoint { get; set; }
        public int? Status { get; set; }
        public DateTime? CreationTime { get; set; }
        public int? AwardingAction { get; set; }

        public virtual PDQQuestion Question { get; set; }
    }
}
