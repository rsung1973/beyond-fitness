using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PersonalExercisePurposeItem
    {
        public PersonalExercisePurposeItem()
        {
            TrainingItems = new HashSet<TrainingItem>();
        }

        public int ItemID { get; set; }
        public int UID { get; set; }
        public string PurposeItem { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int? NoticeStatus { get; set; }
        public DateTime? InitialDate { get; set; }

        public virtual PersonalExercisePurpose UIDNavigation { get; set; }
        public virtual ICollection<TrainingItem> TrainingItems { get; set; }
    }
}
