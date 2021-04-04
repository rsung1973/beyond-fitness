using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceSeller
    {
        public int InvoiceID { get; set; }
        public string ReceiptNo { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int? SellerID { get; set; }
        public string CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string CustomerName { get; set; }
        public string Fax { get; set; }
        public string PersonInCharge { get; set; }
        public string RoleRemark { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
        public virtual Organization Seller { get; set; }
    }
}
