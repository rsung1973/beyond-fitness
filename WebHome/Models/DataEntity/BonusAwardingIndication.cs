using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BonusAwardingIndication
    {
        public int ItemID { get; set; }
        public string Indication { get; set; }

        public virtual BonusAwardingItem Item { get; set; }
    }
}
