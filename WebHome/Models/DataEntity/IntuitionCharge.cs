using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class IntuitionCharge
    {
        public IntuitionCharge()
        {
            TuitionInstallments = new HashSet<TuitionInstallment>();
        }

        public int RegisterID { get; set; }
        public string Payment { get; set; }
        public int? FeeShared { get; set; }
        public int? ByInstallments { get; set; }

        public virtual RegisterLesson Register { get; set; }
        public virtual ICollection<TuitionInstallment> TuitionInstallments { get; set; }
    }
}
