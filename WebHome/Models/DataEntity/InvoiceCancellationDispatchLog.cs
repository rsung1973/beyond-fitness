using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceCancellationDispatchLog
    {
        public int InvoiceID { get; set; }
        public DateTime? DispatchDate { get; set; }
        public int? Status { get; set; }

        public virtual InvoiceCancellation Invoice { get; set; }
    }
}
