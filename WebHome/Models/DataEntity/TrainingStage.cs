using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingStage
    {
        public TrainingStage()
        {
            TrainingAids = new HashSet<TrainingAid>();
            TrainingExecutionStages = new HashSet<TrainingExecutionStage>();
            TrainingStageItems = new HashSet<TrainingStageItem>();
        }

        public int StageID { get; set; }
        public string Stage { get; set; }

        public virtual ICollection<TrainingAid> TrainingAids { get; set; }
        public virtual ICollection<TrainingExecutionStage> TrainingExecutionStages { get; set; }
        public virtual ICollection<TrainingStageItem> TrainingStageItems { get; set; }
    }
}
