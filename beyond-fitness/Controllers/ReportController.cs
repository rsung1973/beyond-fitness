﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;
using WebHome.Security.Authorization;

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
                items = models.GetTable<RegisterLesson>()
                    .Where(r => r.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)
                    .Where(r => r.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自由教練預約)
                    .Where(r => r.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.內部訓練);


                if (viewModel.Payoff == true)
                {
                    Expression<Func<TuitionInstallment,bool>> f = t => t.PayoffDate.HasValue;
                    if (viewModel.DateFrom.HasValue)
                         f = f.And(t => t.PayoffDate >= viewModel.DateFrom);
                    if (viewModel.DateTo.HasValue)
                        f = f.And(t => t.PayoffDate < viewModel.DateTo.Value.AddDays(1));
                    var queryItems = models.GetTable<TuitionInstallment>().Where(f).Select(t => t.IntuitionCharge.RegisterLesson);

                    items = items.Join(queryItems, i => i.RegisterID, q => q.RegisterID, (i, q) => i);
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

            var dataItems = items.OrderByDescending(t => t.ClassTime).Take(2).ToArray();

            if (dataItems.Count() == 0)
            {
                return Content("學員未建立上課資料!!");
            }

            var item = dataItems.Last();

            ViewBag.LessonDate = item.ClassTime;
            ViewBag.EventsUrl = (new UrlHelper(ControllerContext.RequestContext)).Action("VipEvents", "Lessons", new { id = uid });
            return View(item);
        }

        public ActionResult RecentLessons(int uid,int? lessonID,bool? edit)
        {
            var items = models.GetTable<LessonTime>().Where(t => t.RegisterLesson.UID == uid
                    || t.GroupingLesson.RegisterLesson.Any(r => r.UID == uid));


            if (lessonID.HasValue)
            {
                items = items.Where(l => l.LessonID == lessonID);
            }

            var dataItems = items.OrderByDescending(t => t.ClassTime).Take(2).ToArray();

            if (dataItems.Count() == 0)
            {
                return Content("學員未建立上課資料!!");
            }

            var item = dataItems.Last();

            ViewBag.LessonDate = item.ClassTime;
            ViewBag.ByCalendar = true;
            if (edit == true)
                ViewBag.Edit = true;
            return View("~/Views/Lessons/LessonContent.ascx",item);
        }

        public ActionResult LessonContent(int lessonID,bool? edit,bool? learner,int? tabIndex,bool? byCalendar)
        {
            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).FirstOrDefault();

            if (item == null)
            {
                return Content("學員未建立上課資料!!");
            }

            ViewBag.LessonDate = item.ClassTime;
            if (edit == true)
                ViewBag.Edit = true;
            if (learner == true)
                ViewBag.Learner = true;
            ViewBag.TabIndex = tabIndex;
            ViewBag.ByCalendar = byCalendar;
            return View("~/Views/Lessons/LessonContent.ascx", item);
        }

        public ActionResult ListLessonPriceType()
        {
            var items = models.GetTable<LessonPriceType>();
            return View(items);
        }

        public ActionResult StaffAchievement()
        {
            return View();
        }

        [CoachAuthorize]
        public ActionResult CoachAchievement()
        {
            return View();
        }


        public ActionResult ListLessonAttendance(int? coachID,DateTime? dateFrom,DateTime? dateTo,int? month)
        {
            var items = models.GetTable<LessonTime>().Where(t => t.LessonAttendance != null)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自由教練預約)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.內部訓練)
                .Where(t => t.LessonPlan.CommitAttendance.HasValue);

            if(coachID.HasValue)
            {
                items = items.Where(t => t.AttendingCoach == coachID);
            }

            if(dateFrom.HasValue)
            {
                items = items.Where(t => t.ClassTime >= dateFrom);
            }

            if (dateTo.HasValue)
            {
                items = items.Where(t => t.ClassTime < dateTo.Value.AddDays(1));
            }
            else if(month.HasValue)
            {
                dateTo = dateFrom.Value.AddMonths(month.Value);
                items = items.Where(t => t.ClassTime < dateTo);
                dateTo = dateTo.Value.AddDays(-1);
            }


            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            return View(items);

        }

        public ActionResult ListRegisterLesson(int? coachID, DateTime? dateFrom, DateTime? dateTo,int? month)
        {
            IQueryable<RegisterLesson> items = models.GetTable<RegisterLesson>()
                .Where(r => r.AdvisorID.HasValue);

            if (coachID.HasValue)
            {
                items = items.Where(t => t.AdvisorID == coachID);
            }

            IQueryable<TuitionInstallment> installment = models.GetTable<TuitionInstallment>();

            if (dateFrom.HasValue)
            {
                installment = installment.Where(i => i.PayoffDate >= dateFrom);
            }
            if (dateTo.HasValue)
            {
                installment = installment.Where(i => i.PayoffDate < dateTo.Value.AddDays(1));
            }
            else if(month.HasValue)
            {
                dateTo = dateFrom.Value.AddMonths(month.Value);
                installment = installment.Where(i => i.PayoffDate < dateTo);
                dateTo = dateTo.Value.AddDays(-1);
            }

            installment = items.Join(installment, t => t.RegisterID, i => i.RegisterID, (t, i) => i);

            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;

            return View(installment);

        }


    }
}