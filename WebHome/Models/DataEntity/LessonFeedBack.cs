using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonFeedBack
    {
        public int LessonID { get; set; }
        public int RegisterID { get; set; }
        public string FeedBack { get; set; }
        public DateTime? FeedBackDate { get; set; }
        public string Remark { get; set; }
        public DateTime? RemarkDate { get; set; }
        public int? Status { get; set; }

        public virtual LessonTime Lesson { get; set; }
        public virtual RegisterLesson Register { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
    }
}
