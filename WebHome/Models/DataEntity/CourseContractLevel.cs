using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CourseContractLevel
    {
        public int LogID { get; set; }
        public int ContractID { get; set; }
        public DateTime LevelDate { get; set; }
        public int ExecutorID { get; set; }
        public int LevelID { get; set; }

        public virtual CourseContract Contract { get; set; }
        public virtual UserProfile Executor { get; set; }
        public virtual LevelExpression Level { get; set; }
    }
}
