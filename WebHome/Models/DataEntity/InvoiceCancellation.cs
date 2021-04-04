using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceCancellation
    {
        public string CancellationNo { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelReason { get; set; }
        public string ReturnTaxDocumentNo { get; set; }
        public string Remark { get; set; }
        public int InvoiceID { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
        public virtual InvoiceCancellationDispatch InvoiceCancellationDispatch { get; set; }
        public virtual InvoiceCancellationDispatchLog InvoiceCancellationDispatchLog { get; set; }
    }
}
