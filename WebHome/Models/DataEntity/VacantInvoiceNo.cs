using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class VacantInvoiceNo
    {
        public VacantInvoiceNo()
        {
            InverseNext = new HashSet<VacantInvoiceNo>();
            InversePrev = new HashSet<VacantInvoiceNo>();
        }

        public int VacancyID { get; set; }
        public int IntervalID { get; set; }
        public int InvoiceNo { get; set; }
        public int? PrevID { get; set; }
        public int? NextID { get; set; }

        public virtual InvoiceNoInterval Interval { get; set; }
        public virtual VacantInvoiceNo Next { get; set; }
        public virtual VacantInvoiceNo Prev { get; set; }
        public virtual ICollection<VacantInvoiceNo> InverseNext { get; set; }
        public virtual ICollection<VacantInvoiceNo> InversePrev { get; set; }
    }
}
