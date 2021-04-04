using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAllowanceDispatchLog
    {
        public int AllowanceID { get; set; }
        public DateTime? DispatchDate { get; set; }
        public int? Status { get; set; }

        public virtual InvoiceAllowance Allowance { get; set; }
    }
}
