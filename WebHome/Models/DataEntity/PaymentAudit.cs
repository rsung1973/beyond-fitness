using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PaymentAudit
    {
        public int PaymentID { get; set; }
        public int? AuditorID { get; set; }
        public DateTime? AuditDate { get; set; }

        public virtual UserProfile Auditor { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
