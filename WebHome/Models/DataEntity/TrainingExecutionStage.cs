using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingExecutionStage
    {
        public int ExecutionID { get; set; }
        public int StageID { get; set; }
        public decimal? TotalMinutes { get; set; }

        public virtual TrainingExecution Execution { get; set; }
        public virtual TrainingStage Stage { get; set; }
    }
}
