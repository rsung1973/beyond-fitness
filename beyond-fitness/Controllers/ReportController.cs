using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebHome.Models.DataEntity;
using WebHome.Models.ViewModel;
using WebHome.Helper;
using System.Threading;
using System.Text;
using WebHome.Models.Locale;
using Utility;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Web.Security;

namespace WebHome.Controllers
{
    [Authorize]
    public class ReportController : SampleController<UserProfile>
    {
        public ActionResult LearnerPayment(LearnerPaymentViewModel viewModel)
        {
            IQueryable<RegisterLesson> items = models.GetTable<RegisterLesson>().Where(r => false);
            ViewBag.ViewModel = viewModel;

            if (viewModel.HasQuery == true)
            {
                items = models.GetTable<RegisterLesson>().Where(r => r.Lessons != r.IntuitionCharge.TuitionInstallment.Count(t => t.PayoffDate.HasValue));

                if (viewModel.Payoff == true)
                {
                    items = items.Where(r => r.IntuitionCharge.TuitionInstallment.Any(t => t.PayoffDate.HasValue));
                }
                else if (viewModel.Payoff == false)
                {
                    items = items.Where(r => r.IntuitionCharge.TuitionInstallment.Count == 0
                        || r.IntuitionCharge.TuitionInstallment.Any(t => !t.PayoffDate.HasValue));
                }

                if(!String.IsNullOrEmpty(viewModel.UserName))
                {
                    items = items.Where(r => r.UserProfile.RealName.Contains(viewModel.UserName));
                }

                if(viewModel.CoachID.HasValue)
                {
                    items = items.Where(r => r.AdvisorID == viewModel.CoachID);
                }
            }

            return View(items);
        }

        public ActionResult LearnerRecentLessons(int uid,int? lessonID,bool? cloneLesson)
        {
            var item = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Title = "檢視課程記錄";
                ViewBag.Message = "學員資料錯誤!!";
                return View("MessageModal");
            }

            ViewBag.LessonID = lessonID;
            if (cloneLesson == true)
                ViewBag.CloneLesson = true;
            return View(item);
        }

        public ActionResult RecentLessonsCalendar(int uid,int? lessonID)
        {
            var items = models.GetTable<LessonTime>().Where(t => t.RegisterLesson.UID == uid
                    || t.GroupingLesson.RegisterLesson.Any(r => r.UID == uid));

            if(lessonID.HasValue)
            {
                items = items.Where(l => l.LessonID == lessonID);
            }

            var dataItems = items.OrderByDescending(t => t.LessonID).Take(2).ToArray();

            if (dataItems.Count() == 0)
            {
                return Content("學員未建立上課資料!!");
            }

            var item = dataItems.Last();

            ViewBag.LessonDate = item.ClassTime;
            ViewBag.EventsUrl = (new UrlHelper(ControllerContext.RequestContext)).Action("VipEvents", "Lessons", new { id = uid });
            return View(item);
        }

        public ActionResult RecentLessons(int uid,int? lessonID)
        {
            var items = models.GetTable<LessonTime>().Where(t => t.RegisterLesson.UID == uid
                    || t.GroupingLesson.RegisterLesson.Any(r => r.UID == uid));


            if (lessonID.HasValue)
            {
                items = items.Where(l => l.LessonID == lessonID);
            }

            var dataItems = items.OrderByDescending(t => t.LessonID).Take(2).ToArray();

            if (dataItems.Count() == 0)
            {
                return Content("學員未建立上課資料!!");
            }

            var item = dataItems.Last();

            ViewBag.LessonDate = item.ClassTime;
            return View("~/Views/Lessons/LessonContent.ascx",item);
        }

        public ActionResult LessonContent(int lessonID,bool? edit)
        {
            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).FirstOrDefault();

            if (item == null)
            {
                return Content("學員未建立上課資料!!");
            }

            ViewBag.LessonDate = item.ClassTime;
            if (edit == true)
                ViewBag.Edit = true;
            return View("~/Views/Lessons/LessonContent.ascx", item);
        }

    }
}