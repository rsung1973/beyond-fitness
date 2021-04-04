using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingStageItem
    {
        public int TrainingID { get; set; }
        public int StageID { get; set; }

        public virtual TrainingStage Stage { get; set; }
        public virtual TrainingType Training { get; set; }
    }
}
