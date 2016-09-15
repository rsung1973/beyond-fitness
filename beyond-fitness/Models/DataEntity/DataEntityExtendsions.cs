using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHome.Models.DataEntity
{
    public static class DataEntityExtendsions
    {
        public static String YearsOld(this UserProfile item)
        {
            return item.Birthday.HasValue ? (DateTime.Today.Year - item.Birthday.Value.Year).ToString()  : "--";
        }

        public static int? BonusPoint(this UserProfile item)
        {
            return item.PDQTask.Where(t => t.SuggestionID.HasValue)
                .Select(t => t.PDQSuggestion)
                .Where(q => q.RightAnswer == true)
                .Select(q => q.PDQQuestion)
                .Where(q => q.GroupID == 6 && q.PDQQuestionExtension != null)
                .Select(q => q.PDQQuestionExtension)
                .Sum(x => x.BonusPoint);
        }

    }

    public partial class UserProfile
    {
        public UserRole CurrentUserRole
        {
            get
            {
                return this.UserRole[0];
            }
        }

        public int? DailyQuestionID { get; set; }

    }
}