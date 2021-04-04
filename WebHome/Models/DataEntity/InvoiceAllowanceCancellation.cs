using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAllowanceCancellation
    {
        public int AllowanceID { get; set; }
        public DateTime? CancelDate { get; set; }
        public string Remark { get; set; }
        public string CancelReason { get; set; }

        public virtual InvoiceAllowance Allowance { get; set; }
    }
}
