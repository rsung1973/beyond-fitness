using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BodyPart
    {
        public BodyPart()
        {
            BodySufferings = new HashSet<BodySuffering>();
        }

        public int PartID { get; set; }
        public string Part { get; set; }

        public virtual ICollection<BodySuffering> BodySufferings { get; set; }
    }
}
