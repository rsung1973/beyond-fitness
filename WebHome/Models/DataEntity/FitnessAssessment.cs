using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class FitnessAssessment
    {
        public int LessonID { get; set; }
        public int? Flexibility { get; set; }
        public int? Cardiopulmonary { get; set; }
        public int? Strength { get; set; }
        public int? Endurance { get; set; }
        public int? ExplosiveForce { get; set; }
        public int? SportsPerformance { get; set; }

        public virtual LessonTime Lesson { get; set; }
    }
}
