using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceDonation
    {
        public int InvoiceID { get; set; }
        public string AgencyCode { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
    }
}
