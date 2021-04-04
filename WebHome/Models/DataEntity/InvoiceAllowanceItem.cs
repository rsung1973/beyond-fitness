using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceAllowanceItem
    {
        public InvoiceAllowanceItem()
        {
            InvoiceAllowanceDetails = new HashSet<InvoiceAllowanceDetail>();
        }

        public int ItemID { get; set; }
        public short? No { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? Piece { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Tax { get; set; }
        public int? ProductItemID { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ItemNo { get; set; }
        public short? OriginalSequenceNo { get; set; }
        public string PieceUnit { get; set; }
        public string OriginalDescription { get; set; }
        public byte? TaxType { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitCost2 { get; set; }
        public decimal? Piece2 { get; set; }
        public decimal? Amount2 { get; set; }
        public string PieceUnit2 { get; set; }
        public string Remark { get; set; }

        public virtual InvoiceProductItem ProductItem { get; set; }
        public virtual ICollection<InvoiceAllowanceDetail> InvoiceAllowanceDetails { get; set; }
    }
}
