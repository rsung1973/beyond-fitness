using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceNoInterval
    {
        public InvoiceNoInterval()
        {
            InvoiceNoAssignments = new HashSet<InvoiceNoAssignment>();
            VacantInvoiceNos = new HashSet<VacantInvoiceNo>();
        }

        public int IntervalID { get; set; }
        public int TrackID { get; set; }
        public int SellerID { get; set; }
        public int StartNo { get; set; }
        public int EndNo { get; set; }
        public int? GroupID { get; set; }

        public virtual InvoiceNoIntervalGroup Group { get; set; }
        public virtual InvoiceTrackCodeAssignment InvoiceTrackCodeAssignment { get; set; }
        public virtual ICollection<InvoiceNoAssignment> InvoiceNoAssignments { get; set; }
        public virtual ICollection<VacantInvoiceNo> VacantInvoiceNos { get; set; }
    }
}
