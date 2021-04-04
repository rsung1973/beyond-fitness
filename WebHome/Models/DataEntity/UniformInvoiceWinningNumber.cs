using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UniformInvoiceWinningNumber
    {
        public UniformInvoiceWinningNumber()
        {
            InvoiceWinningNumbers = new HashSet<InvoiceWinningNumber>();
        }

        public int WinningID { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public int Rank { get; set; }
        public string WinningNO { get; set; }
        public string PrizeType { get; set; }
        public int? Bonus { get; set; }

        public virtual ICollection<InvoiceWinningNumber> InvoiceWinningNumbers { get; set; }
    }
}
