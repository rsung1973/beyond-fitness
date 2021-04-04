using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class ProfessionalCertificate
    {
        public ProfessionalCertificate()
        {
            CoachCertificates = new HashSet<CoachCertificate>();
        }

        public int CertificateID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CoachCertificate> CoachCertificates { get; set; }
    }
}
