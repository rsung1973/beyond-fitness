using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PDQTaskItem
    {
        public int TaskID { get; set; }
        public int SuggestionID { get; set; }

        public virtual PDQSuggestion Suggestion { get; set; }
        public virtual PDQTask Task { get; set; }
    }
}
