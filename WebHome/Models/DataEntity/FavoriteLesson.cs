using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class FavoriteLesson
    {
        public int ExecutionID { get; set; }
        public int UID { get; set; }

        public virtual TrainingPlan Execution { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
