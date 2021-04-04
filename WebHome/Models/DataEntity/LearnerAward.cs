using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LearnerAward
    {
        public LearnerAward()
        {
            BonusExchanges = new HashSet<BonusExchange>();
        }

        public int AwardID { get; set; }
        public int UID { get; set; }
        public int ItemID { get; set; }
        public DateTime AwardDate { get; set; }
        public int ActorID { get; set; }

        public virtual UserProfile Actor { get; set; }
        public virtual BonusAwardingItem Item { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
        public virtual AwardingLesson AwardingLesson { get; set; }
        public virtual AwardingLessonGift AwardingLessonGift { get; set; }
        public virtual ICollection<BonusExchange> BonusExchanges { get; set; }
    }
}
