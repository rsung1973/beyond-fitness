using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class InvoiceWinningNumber
    {
        public string WinningNO { get; set; }
        public int Year { get; set; }
        public byte MonthFrom { get; set; }
        public byte MonthTo { get; set; }
        public string WinningType { get; set; }
        public int InvoiceID { get; set; }
        public string TrackCode { get; set; }
        public string DataType { get; set; }
        public string BonusDescription { get; set; }
        public int? WinningID { get; set; }
        public DateTime? DownloadDate { get; set; }

        public virtual InvoiceItem Invoice { get; set; }
        public virtual UniformInvoiceWinningNumber Winning { get; set; }
    }
}
