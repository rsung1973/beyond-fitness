using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ContractPayment
    {
        public int PaymentID { get; set; }
        public int ContractID { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
