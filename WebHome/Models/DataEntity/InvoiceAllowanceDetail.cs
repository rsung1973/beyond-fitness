using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAllowanceDetail
    {
        public int AllowanceID { get; set; }
        public int ItemID { get; set; }

        public virtual InvoiceAllowance Allowance { get; set; }
        public virtual InvoiceAllowanceItem Item { get; set; }
    }
}
