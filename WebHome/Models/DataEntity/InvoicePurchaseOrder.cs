using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoicePurchaseOrder
    {
        public int InvoiceID { get; set; }
        public int? UploadID { get; set; }
        public string OrderNo { get; set; }
        public DateTime? PurchaseDate { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
    }
}
