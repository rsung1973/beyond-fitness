using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractType
    {
        public CourseContractType()
        {
            CourseContracts = new HashSet<CourseContract>();
        }

        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string ContractCode { get; set; }
        public bool? IsGroup { get; set; }
        public int? GroupingMemberCount { get; set; }

        public virtual GroupingLessonDiscount GroupingMemberCountNavigation { get; set; }
        public virtual ICollection<CourseContract> CourseContracts { get; set; }
    }
}
