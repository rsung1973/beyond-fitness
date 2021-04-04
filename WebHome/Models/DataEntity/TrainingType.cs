using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingType
    {
        public TrainingType()
        {
            TrainingItems = new HashSet<TrainingItem>();
        }

        public int TrainingID { get; set; }
        public string BodyParts { get; set; }
        public bool? BreakMark { get; set; }
        public int? OrderIndex { get; set; }

        public virtual TrainingStageItem TrainingStageItem { get; set; }
        public virtual ICollection<TrainingItem> TrainingItems { get; set; }
    }
}
