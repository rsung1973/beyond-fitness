using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAllowance
    {
        public InvoiceAllowance()
        {
            InvoiceAllowanceDetails = new HashSet<InvoiceAllowanceDetail>();
            Payments = new HashSet<Payment>();
        }

        public int AllowanceID { get; set; }
        public string AllowanceNumber { get; set; }
        public byte? AllowanceType { get; set; }
        public DateTime? AllowanceDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public int? InvoiceID { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }

        public virtual Document Allowance { get; set; }
        public virtual InvoiceItem Invoice { get; set; }
        public virtual InvoiceAllowanceBuyer InvoiceAllowanceBuyer { get; set; }
        public virtual InvoiceAllowanceCancellation InvoiceAllowanceCancellation { get; set; }
        public virtual InvoiceAllowanceDispatch InvoiceAllowanceDispatch { get; set; }
        public virtual InvoiceAllowanceDispatchLog InvoiceAllowanceDispatchLog { get; set; }
        public virtual InvoiceAllowanceSeller InvoiceAllowanceSeller { get; set; }
        public virtual ICollection<InvoiceAllowanceDetail> InvoiceAllowanceDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
