using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class CoachCertificate
    {
        public int CoachID { get; set; }
        public int CertificateID { get; set; }
        public DateTime? Expiration { get; set; }

        public virtual ProfessionalCertificate Certificate { get; set; }
        public virtual ServingCoach Coach { get; set; }
    }
}
