using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingAid
    {
        public TrainingAid()
        {
            TrainingItemAids = new HashSet<TrainingItemAid>();
        }

        public int AidID { get; set; }
        public string ItemName { get; set; }
        public int? StageID { get; set; }
        public int? Status { get; set; }

        public virtual TrainingStage Stage { get; set; }
        public virtual ICollection<TrainingItemAid> TrainingItemAids { get; set; }
    }
}
