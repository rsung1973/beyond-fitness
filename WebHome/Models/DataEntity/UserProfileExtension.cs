using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UserProfileExtension
    {
        public int UID { get; set; }
        public string Gender { get; set; }
        public int? AthleticLevel { get; set; }
        public int? CurrentTrial { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string EmergencyContactPerson { get; set; }
        public string Relationship { get; set; }
        public string IDNo { get; set; }
        public string AdministrativeArea { get; set; }
        public string Signature { get; set; }
        public bool? RegisterStatus { get; set; }
        public string LineID { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
    }
}
