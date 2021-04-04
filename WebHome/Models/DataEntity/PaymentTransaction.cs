using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PaymentTransaction
    {
        public PaymentTransaction()
        {
            PaymentOrders = new HashSet<PaymentOrder>();
        }

        public int PaymentID { get; set; }
        public int BranchID { get; set; }

        public virtual BranchStore Branch { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<PaymentOrder> PaymentOrders { get; set; }
    }
}
