using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class TuitionInstallment
    {
        public int InstallmentID { get; set; }
        public int RegisterID { get; set; }
        public int? PayoffAmount { get; set; }
        public DateTime? PayoffDate { get; set; }

        public virtual Payment Installment { get; set; }
        public virtual IntuitionCharge Register { get; set; }
    }
}
