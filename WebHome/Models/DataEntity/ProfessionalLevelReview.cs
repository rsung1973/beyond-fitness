using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ProfessionalLevelReview
    {
        public int LevelID { get; set; }
        public int? PromotionID { get; set; }
        public int? DemotionID { get; set; }
        public int CheckLevel { get; set; }

        public virtual ProfessionalLevel Demotion { get; set; }
        public virtual ProfessionalLevel Level { get; set; }
        public virtual ProfessionalLevel Promotion { get; set; }
    }
}
