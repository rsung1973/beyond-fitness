using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UserSignature
    {
        public int SignatureID { get; set; }
        public int UID { get; set; }
        public string Signature { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
    }
}
