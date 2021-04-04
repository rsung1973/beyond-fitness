using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PaymentOrder
    {
        public int PaymentID { get; set; }
        public int ProductID { get; set; }
        public int ProductCount { get; set; }

        public virtual PaymentTransaction Payment { get; set; }
        public virtual MerchandiseWindow Product { get; set; }
    }
}
