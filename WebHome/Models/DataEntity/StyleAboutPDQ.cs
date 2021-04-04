using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class StyleAboutPDQ
    {
        public StyleAboutPDQ()
        {
            PDQUserAssessments = new HashSet<PDQUserAssessment>();
        }

        public int StyleID { get; set; }
        public string Style { get; set; }

        public virtual ICollection<PDQUserAssessment> PDQUserAssessments { get; set; }
    }
}
