using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceNoIntervalGroup
    {
        public InvoiceNoIntervalGroup()
        {
            InvoiceNoIntervals = new HashSet<InvoiceNoInterval>();
        }

        public int GroupID { get; set; }

        public virtual ICollection<InvoiceNoInterval> InvoiceNoIntervals { get; set; }
    }
}
