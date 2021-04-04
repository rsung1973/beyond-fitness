using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BonusExchange
    {
        public int AwardID { get; set; }
        public int TaskID { get; set; }

        public virtual LearnerAward Award { get; set; }
        public virtual PDQTaskBonu Task { get; set; }
    }
}
