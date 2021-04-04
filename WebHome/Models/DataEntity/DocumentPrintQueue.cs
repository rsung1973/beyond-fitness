using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DocumentPrintQueue
    {
        public int DocID { get; set; }
        public int? UID { get; set; }
        public DateTime? SubmitDate { get; set; }

        public virtual Document Doc { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
