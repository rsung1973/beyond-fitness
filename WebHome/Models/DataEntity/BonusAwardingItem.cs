using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BonusAwardingItem
    {
        public BonusAwardingItem()
        {
            LearnerAwards = new HashSet<LearnerAward>();
        }

        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string SampleUrl { get; set; }
        public int PointValue { get; set; }
        public int? Price { get; set; }
        public bool? ExchangeableOnline { get; set; }
        public int? OrderIndex { get; set; }
        public int? Status { get; set; }

        public virtual BonusAwardingIndication BonusAwardingIndication { get; set; }
        public virtual BonusAwardingLesson BonusAwardingLesson { get; set; }
        public virtual ICollection<LearnerAward> LearnerAwards { get; set; }
    }
}
