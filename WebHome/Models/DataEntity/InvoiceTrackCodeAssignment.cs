using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceTrackCodeAssignment
    {
        public InvoiceTrackCodeAssignment()
        {
            InvoiceNoIntervals = new HashSet<InvoiceNoInterval>();
        }

        public int TrackID { get; set; }
        public int SellerID { get; set; }

        public virtual Organization Seller { get; set; }
        public virtual InvoiceTrackCode Track { get; set; }
        public virtual ICollection<InvoiceNoInterval> InvoiceNoIntervals { get; set; }
    }
}
