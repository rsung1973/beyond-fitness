using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceProduct
    {
        public InvoiceProduct()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
            InvoiceProductItems = new HashSet<InvoiceProductItem>();
        }

        public int ProductID { get; set; }
        public string Brief { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<InvoiceProductItem> InvoiceProductItems { get; set; }
    }
}
