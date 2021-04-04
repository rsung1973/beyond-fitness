using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class MerchandiseWindow
    {
        public MerchandiseWindow()
        {
            MerchandiseTransactions = new HashSet<MerchandiseTransaction>();
            PaymentOrders = new HashSet<PaymentOrder>();
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int? UnitPrice { get; set; }
        public int? Status { get; set; }
        public string SampleUrl { get; set; }

        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual ICollection<MerchandiseTransaction> MerchandiseTransactions { get; set; }
        public virtual ICollection<PaymentOrder> PaymentOrders { get; set; }
    }
}
