using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class V_PerformanceShare
    {
        public int PaymentID { get; set; }
        public int? PayoffAmount { get; set; }
        public DateTime? PayoffDate { get; set; }
        public int CoachID { get; set; }
        public int? ShareAmount { get; set; }
    }
}
