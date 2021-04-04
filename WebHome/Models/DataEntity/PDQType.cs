using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQType
    {
        public int QuestionID { get; set; }
        public int TypeID { get; set; }

        public virtual PDQQuestion Question { get; set; }
        public virtual LevelExpression Type { get; set; }
    }
}
