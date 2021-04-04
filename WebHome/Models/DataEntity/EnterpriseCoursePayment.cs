using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class EnterpriseCoursePayment
    {
        public int PaymentID { get; set; }
        public int ContractID { get; set; }

        public virtual EnterpriseCourseContract Contract { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
