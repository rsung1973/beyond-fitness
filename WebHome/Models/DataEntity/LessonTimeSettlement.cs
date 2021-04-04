using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LessonTimeSettlement
    {
        public int LessonID { get; set; }
        public int ProfessionalLevelID { get; set; }
        public decimal? MarkedGradeIndex { get; set; }
        public int? SettlementStatus { get; set; }
        public int? CoachWorkPlace { get; set; }
        public int? SettlementID { get; set; }

        public virtual BranchStore CoachWorkPlaceNavigation { get; set; }
        public virtual LessonTime Lesson { get; set; }
        public virtual ProfessionalLevel ProfessionalLevel { get; set; }
        public virtual Settlement Settlement { get; set; }
    }
}
