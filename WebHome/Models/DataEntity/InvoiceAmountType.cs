using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAmountType
    {
        public int InvoiceID { get; set; }
        public byte? TaxType { get; set; }
        public decimal? SalesAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string TotalAmountInChinese { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? Adjustment { get; set; }
        public decimal? OriginalCurrencyAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public int? CurrencyID { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
    }
}
