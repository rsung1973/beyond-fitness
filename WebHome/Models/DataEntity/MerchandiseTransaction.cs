using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MerchandiseTransaction
    {
        public int TransactionID { get; set; }
        public int ProductID { get; set; }

        public virtual MerchandiseWindow Product { get; set; }
        public virtual MerchandiseTransactionType Transaction { get; set; }
    }
}
