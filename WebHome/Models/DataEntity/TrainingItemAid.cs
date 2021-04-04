using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TrainingItemAid
    {
        public int ItemID { get; set; }
        public int AidID { get; set; }

        public virtual TrainingAid Aid { get; set; }
        public virtual TrainingItem Item { get; set; }
    }
}
