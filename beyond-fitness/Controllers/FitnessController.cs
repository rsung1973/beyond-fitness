using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using CommonLib.MvcExtension;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    [Authorize]
    public class FitnessController : SampleController<UserProfile>
    {
        // GET: Fitness
        public ActionResult PersonalStrengthAssessment(int uid,int[] itemID)
        {
            return getAssessmentReportItem(uid, itemID);
        }

        public ActionResult BodyEnergyAssessment(int uid, int[] itemID)
        {
            return getAssessmentReportItem(uid, itemID);
        }


        private ActionResult getAssessmentReportItem(int uid, int[] itemID)
        {
            DataLoadOptions ops = new DataLoadOptions();
            ops.LoadWith<LessonFitnessAssessment>(f => f.LessonFitnessAssessmentReport);
            ops.LoadWith<LessonFitnessAssessmentReport>(f => f.FitnessAssessmentItem);
            models.GetDataContext().LoadOptions = ops;

            var items = models.GetTable<LessonFitnessAssessment>().Where(u => u.UID == uid)
                .Where(u => u.LessonFitnessAssessmentReport.Count(r => itemID.Contains(r.ItemID) && (r.TotalAssessment.HasValue || r.SingleAssessment.HasValue)) > 0);

            if (items.Count() == 0)
                return Content("資料未建立");

            return View(items);
        }
    }
}