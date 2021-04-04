using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceTrackCode
    {
        public InvoiceTrackCode()
        {
            InvoiceTrackCodeAssignments = new HashSet<InvoiceTrackCodeAssignment>();
        }

        public int TrackID { get; set; }
        public string TrackCode { get; set; }
        public short Year { get; set; }
        public short PeriodNo { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }

        public virtual ICollection<InvoiceTrackCodeAssignment> InvoiceTrackCodeAssignments { get; set; }
    }
}
