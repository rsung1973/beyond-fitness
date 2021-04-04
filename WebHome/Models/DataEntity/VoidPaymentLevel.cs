using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class VoidPaymentLevel
    {
        public int VoidID { get; set; }
        public DateTime LevelDate { get; set; }
        public int ExecutorID { get; set; }
        public int LevelID { get; set; }

        public virtual UserProfile Executor { get; set; }
        public virtual LevelExpression Level { get; set; }
        public virtual VoidPayment Void { get; set; }
    }
}
