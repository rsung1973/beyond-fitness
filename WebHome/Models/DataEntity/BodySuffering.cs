using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BodySuffering
    {
        public int DiagnosisID { get; set; }
        public int PartID { get; set; }

        public virtual BodyDiagnosis Diagnosis { get; set; }
        public virtual BodyPart Part { get; set; }
    }
}
