using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class VoidPayment
    {
        public VoidPayment()
        {
            ContractTrustTracks = new HashSet<ContractTrustTrack>();
            VoidPaymentLevels = new HashSet<VoidPaymentLevel>();
        }

        public int VoidID { get; set; }
        public int? Status { get; set; }
        public DateTime? VoidDate { get; set; }
        public int? HandlerID { get; set; }
        public string Remark { get; set; }
        public bool? Drawback { get; set; }

        public virtual UserProfile Handler { get; set; }
        public virtual LevelExpression StatusNavigation { get; set; }
        public virtual Payment Void { get; set; }
        public virtual ICollection<ContractTrustTrack> ContractTrustTracks { get; set; }
        public virtual ICollection<VoidPaymentLevel> VoidPaymentLevels { get; set; }
    }
}
