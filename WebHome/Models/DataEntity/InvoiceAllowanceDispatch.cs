using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAllowanceDispatch
    {
        public int AllowanceID { get; set; }

        public virtual InvoiceAllowance Allowance { get; set; }
    }
}
