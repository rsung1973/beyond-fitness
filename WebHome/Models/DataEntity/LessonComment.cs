using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonComment
    {
        public int CommentID { get; set; }
        public int SpeakerID { get; set; }
        public int HearerID { get; set; }
        public string Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public int? Status { get; set; }

        public virtual UserProfile Hearer { get; set; }
        public virtual UserProfile Speaker { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
    }
}
