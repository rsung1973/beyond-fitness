using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        public int TypeID { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
