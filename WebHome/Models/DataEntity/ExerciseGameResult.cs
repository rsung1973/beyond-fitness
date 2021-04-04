using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ExerciseGameResult
    {
        public ExerciseGameResult()
        {
            ExerciseGameRanks = new HashSet<ExerciseGameRank>();
        }

        public int TestID { get; set; }
        public int UID { get; set; }
        public int ExerciseID { get; set; }
        public decimal Score { get; set; }
        public DateTime TestDate { get; set; }

        public virtual ExerciseGameItem Exercise { get; set; }
        public virtual ExerciseGameContestant UIDNavigation { get; set; }
        public virtual ICollection<ExerciseGameRank> ExerciseGameRanks { get; set; }
    }
}
