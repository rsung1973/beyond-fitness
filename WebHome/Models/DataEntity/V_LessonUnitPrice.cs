using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class V_LessonUnitPrice
    {
        public int? Year { get; set; }
        public int? PeriodNo { get; set; }
        public int PriceID { get; set; }
        public string Description { get; set; }
        public int? ListPrice { get; set; }
        public int? Status { get; set; }
        public int? UsageType { get; set; }
        public int? CoachPayoff { get; set; }
        public int? CoachPayoffCreditCard { get; set; }
        public int? ExcludeQuestionnaire { get; set; }
        public int? LowerLimit { get; set; }
        public int? UpperBound { get; set; }
        public int? BranchID { get; set; }
        public int? DiscountedPrice { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? SeriesID { get; set; }
    }
}
