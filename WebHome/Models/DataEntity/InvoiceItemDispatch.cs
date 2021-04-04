using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceItemDispatch
    {
        public int InvoiceID { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
    }
}
