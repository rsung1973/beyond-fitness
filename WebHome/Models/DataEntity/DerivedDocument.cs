using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DerivedDocument
    {
        public int DocID { get; set; }
        public int SourceID { get; set; }

        public virtual Document Doc { get; set; }
        public virtual Document Source { get; set; }
    }
}
