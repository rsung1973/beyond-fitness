using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceCarrier
    {
        public int InvoiceID { get; set; }
        public string CarrierType { get; set; }
        public string CarrierNo { get; set; }
        public string CarrierNo2 { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
    }
}
