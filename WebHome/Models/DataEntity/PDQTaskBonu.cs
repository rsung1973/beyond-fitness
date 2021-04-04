using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQTaskBonu
    {
        public PDQTaskBonu()
        {
            BonusExchanges = new HashSet<BonusExchange>();
        }

        public int TaskID { get; set; }
        public int? BonusPoint { get; set; }

        public virtual PDQTask Task { get; set; }
        public virtual ICollection<BonusExchange> BonusExchanges { get; set; }
    }
}
