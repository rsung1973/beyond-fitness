using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DocumentPrintLog
    {
        public int LogID { get; set; }
        public int DocID { get; set; }
        public DateTime PrintDate { get; set; }
        public int UID { get; set; }

        public virtual Document Doc { get; set; }
        public virtual UserProfile UIDNavigation { get; set; }
    }
}
