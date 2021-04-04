using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceCancellationDispatch
    {
        public int InvoiceID { get; set; }

        public virtual InvoiceCancellation Invoice { get; set; }
    }
}
