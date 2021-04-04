using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingItem
    {
        public TrainingItem()
        {
            TrainingItemAids = new HashSet<TrainingItemAid>();
        }

        public int ItemID { get; set; }
        public int? TrainingID { get; set; }
        public string GoalTurns { get; set; }
        public string GoalStrength { get; set; }
        public int ExecutionID { get; set; }
        public string Description { get; set; }
        public string ActualStrength { get; set; }
        public string ActualTurns { get; set; }
        public string Remark { get; set; }
        public string BreakIntervalInSecond { get; set; }
        public string Repeats { get; set; }
        public int? Sequence { get; set; }
        public string ExecutionFeedBack { get; set; }
        public DateTime? ExecutionFeedBackDate { get; set; }
        public decimal? DurationInMinutes { get; set; }
        public int? PurposeID { get; set; }

        public virtual TrainingExecution Execution { get; set; }
        public virtual PersonalExercisePurposeItem Purpose { get; set; }
        public virtual TrainingType Training { get; set; }
        public virtual ICollection<TrainingItemAid> TrainingItemAids { get; set; }
    }
}
