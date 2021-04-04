using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceItem
    {
        public InvoiceItem()
        {
            InvoiceAllowances = new HashSet<InvoiceAllowance>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
            Payments = new HashSet<Payment>();
        }

        public int InvoiceID { get; set; }
        public string No { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string CheckNo { get; set; }
        public string Remark { get; set; }
        public string BuyerRemark { get; set; }
        public string CustomsClearanceMark { get; set; }
        public string TaxCenter { get; set; }
        public DateTime? PermitDate { get; set; }
        public string PermitWord { get; set; }
        public string PermitNumber { get; set; }
        public string Category { get; set; }
        public string RelateNumber { get; set; }
        public byte? InvoiceType { get; set; }
        public string GroupMark { get; set; }
        public string DonateMark { get; set; }
        public int? SellerID { get; set; }
        public int? DonationID { get; set; }
        public string RandomNo { get; set; }
        public string TrackCode { get; set; }
        public byte? BondedAreaConfirm { get; set; }
        public string PrintMark { get; set; }

        public virtual Document Invoice { get; set; }
        public virtual Organization Seller { get; set; }
        public virtual InvoiceAmountType InvoiceAmountType { get; set; }
        public virtual InvoiceBuyer InvoiceBuyer { get; set; }
        public virtual InvoiceCancellation InvoiceCancellation { get; set; }
        public virtual InvoiceCarrier InvoiceCarrier { get; set; }
        public virtual InvoiceDonation InvoiceDonation { get; set; }
        public virtual InvoiceItemDispatch InvoiceItemDispatch { get; set; }
        public virtual InvoiceItemDispatchLog InvoiceItemDispatchLog { get; set; }
        public virtual InvoiceNoAssignment InvoiceNoAssignment { get; set; }
        public virtual InvoicePurchaseOrder InvoicePurchaseOrder { get; set; }
        public virtual InvoiceSeller InvoiceSeller { get; set; }
        public virtual InvoiceWinningNumber InvoiceWinningNumber { get; set; }
        public virtual ICollection<InvoiceAllowance> InvoiceAllowances { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
