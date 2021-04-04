using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceNoAssignment
    {
        public int InvoiceID { get; set; }
        public int? IntervalID { get; set; }
        public int? InvoiceNo { get; set; }

        public virtual InvoiceNoInterval Interval { get; set; }
        public virtual InvoiceItem Invoice { get; set; }
    }
}
