using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceDetail
    {
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
        public virtual InvoiceProduct Product { get; set; }
    }
}
