using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ExerciseGameItem
    {
        public ExerciseGameItem()
        {
            ExerciseGameRanks = new HashSet<ExerciseGameRank>();
            ExerciseGameResults = new HashSet<ExerciseGameResult>();
        }

        public int ExerciseID { get; set; }
        public string Exercise { get; set; }
        public string Unit { get; set; }
        public bool? Descending { get; set; }

        public virtual ICollection<ExerciseGameRank> ExerciseGameRanks { get; set; }
        public virtual ICollection<ExerciseGameResult> ExerciseGameResults { get; set; }
    }
}
