using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class PersonalExercisePurpose
    {
        public PersonalExercisePurpose()
        {
            PersonalExercisePurposeItems = new HashSet<PersonalExercisePurposeItem>();
        }

        public int UID { get; set; }
        public string Purpose { get; set; }
        public string PowerAbility { get; set; }
        public int? Flexibility { get; set; }
        public int? Cardiopulmonary { get; set; }
        public int? MuscleStrength { get; set; }
        public string AbilityStyle { get; set; }
        public int? AbilityLevel { get; set; }

        public virtual UserProfile UIDNavigation { get; set; }
        public virtual ICollection<PersonalExercisePurposeItem> PersonalExercisePurposeItems { get; set; }
    }
}
