using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ExerciseGameContestant
    {
        public ExerciseGameContestant()
        {
            ExerciseGameRanks = new HashSet<ExerciseGameRank>();
            ExerciseGameResults = new HashSet<ExerciseGameResult>();
        }

        public int UID { get; set; }
        public int? Status { get; set; }
        public int? TotalScope { get; set; }
        public int? Rank { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
        public virtual ExerciseGamePersonalRank ExerciseGamePersonalRank { get; set; }
        public virtual ICollection<ExerciseGameRank> ExerciseGameRanks { get; set; }
        public virtual ICollection<ExerciseGameResult> ExerciseGameResults { get; set; }
    }
}
