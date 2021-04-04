using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CoachRating
    {
        public int RatingID { get; set; }
        public int CoachID { get; set; }
        public DateTime RatingDate { get; set; }
        public int? AttendanceCount { get; set; }
        public int? TuitionSummary { get; set; }
        public int LevelID { get; set; }

        public virtual ServingCoach Coach { get; set; }
        public virtual ProfessionalLevel Level { get; set; }
    }
}
