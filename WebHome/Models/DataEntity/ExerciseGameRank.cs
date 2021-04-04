using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ExerciseGameRank
    {
        public int UID { get; set; }
        public int ExerciseID { get; set; }
        public int? RecordID { get; set; }
        public int? RankingScore { get; set; }
        public int? Rank { get; set; }

        public virtual ExerciseGameItem Exercise { get; set; }
        public virtual ExerciseGameResult Record { get; set; }
        public virtual ExerciseGameContestant UIDNavigation { get; set; }
    }
}
