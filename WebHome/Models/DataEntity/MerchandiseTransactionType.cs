using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MerchandiseTransactionType
    {
        public MerchandiseTransactionType()
        {
            InverseCategorySource = new HashSet<MerchandiseTransactionType>();
            MerchandiseTransactions = new HashSet<MerchandiseTransaction>();
        }

        public int TransactionID { get; set; }
        public string TransactionType { get; set; }
        public int? CategorySourceID { get; set; }

        public virtual MerchandiseTransactionType CategorySource { get; set; }
        public virtual ICollection<MerchandiseTransactionType> InverseCategorySource { get; set; }
        public virtual ICollection<MerchandiseTransaction> MerchandiseTransactions { get; set; }
    }
}
