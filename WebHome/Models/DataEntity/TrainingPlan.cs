using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingPlan
    {
        public TrainingPlan()
        {
            FavoriteLessons = new HashSet<FavoriteLesson>();
        }

        public int LessonID { get; set; }
        public int ExecutionID { get; set; }
        public int? PlanStatus { get; set; }
        public int? RegisterID { get; set; }

        public virtual LessonTime Lesson { get; set; }
        public virtual LevelExpression PlanStatusNavigation { get; set; }
        public virtual RegisterLesson Register { get; set; }
        public virtual TrainingExecution TrainingExecution { get; set; }
        public virtual ICollection<FavoriteLesson> FavoriteLessons { get; set; }
    }
}
