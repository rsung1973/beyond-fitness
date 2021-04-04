using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ExerciseGamePersonalRank
    {
        public int UID { get; set; }
        public int TotalScope { get; set; }
        public int Rank { get; set; }

        public virtual ExerciseGameContestant UIDNavigation { get; set; }
    }
}
