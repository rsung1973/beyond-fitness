using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class Organization
    {
        public Organization()
        {
            EnterpriseCourseContracts = new HashSet<EnterpriseCourseContract>();
            InvoiceAllowanceBuyers = new HashSet<InvoiceAllowanceBuyer>();
            InvoiceAllowanceSellers = new HashSet<InvoiceAllowanceSeller>();
            InvoiceBuyers = new HashSet<InvoiceBuyer>();
            InvoiceItems = new HashSet<InvoiceItem>();
            InvoiceSellers = new HashSet<InvoiceSeller>();
            InvoiceTrackCodeAssignments = new HashSet<InvoiceTrackCodeAssignment>();
        }

        public string ContactName { get; set; }
        public string Fax { get; set; }
        public string LogoURL { get; set; }
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string ReceiptNo { get; set; }
        public string Phone { get; set; }
        public string ContactFax { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMobilePhone { get; set; }
        public string RegAddr { get; set; }
        public string UndertakerName { get; set; }
        public string Addr { get; set; }
        public string EnglishName { get; set; }
        public string EnglishAddr { get; set; }
        public string EnglishRegAddr { get; set; }
        public string ContactEmail { get; set; }
        public string UndertakerPhone { get; set; }
        public string UndertakerFax { get; set; }
        public string UndertakerMobilePhone { get; set; }
        public string InvoiceSignature { get; set; }
        public string UndertakerID { get; set; }
        public string ContactTitle { get; set; }
        public string TaxNo { get; set; }

        public virtual BranchStore BranchStore { get; set; }
        public virtual ICollection<EnterpriseCourseContract> EnterpriseCourseContracts { get; set; }
        public virtual ICollection<InvoiceAllowanceBuyer> InvoiceAllowanceBuyers { get; set; }
        public virtual ICollection<InvoiceAllowanceSeller> InvoiceAllowanceSellers { get; set; }
        public virtual ICollection<InvoiceBuyer> InvoiceBuyers { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual ICollection<InvoiceSeller> InvoiceSellers { get; set; }
        public virtual ICollection<InvoiceTrackCodeAssignment> InvoiceTrackCodeAssignments { get; set; }
    }
}
