using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingExecution
    {
        public TrainingExecution()
        {
            TrainingExecutionStages = new HashSet<TrainingExecutionStage>();
            TrainingItems = new HashSet<TrainingItem>();
        }

        public int ExecutionID { get; set; }
        public string BreakIntervalInSecond { get; set; }
        public string Repeats { get; set; }
        public int? ActualBreakIntervalInSecond { get; set; }
        public int? ActualRepeats { get; set; }
        public string Conclusion { get; set; }
        public string ExecutionFeedBack { get; set; }
        public DateTime? ExecutionFeedBackDate { get; set; }
        public string Emphasis { get; set; }

        public virtual TrainingPlan Execution { get; set; }
        public virtual ICollection<TrainingExecutionStage> TrainingExecutionStages { get; set; }
        public virtual ICollection<TrainingItem> TrainingItems { get; set; }
    }
}
