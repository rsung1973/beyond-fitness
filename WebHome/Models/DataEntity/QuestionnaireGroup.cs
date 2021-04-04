using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class QuestionnaireGroup
    {
        public int GroupID { get; set; }

        public virtual PDQGroup Group { get; set; }
    }
}
