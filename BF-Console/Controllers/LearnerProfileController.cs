using System;
using System.Collections.Generic;
using System.Data;
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

using CommonLib.DataAccess;
using CommonLib.MvcExtension;
using Newtonsoft.Json;
using Utility;
using WebHome.Controllers;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
    public class LearnerProfileController : SampleController<UserProfile>
    {
        // GET: LearnerProfile
        public ActionResult ProfileIndex()
        {
            return View();
        }

        public ActionResult LearnerCalendar(DailyBookingQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();
            return View("~/Views/LearnerProfile/Module/LearnerCalendar.cshtml", profile.LoadInstance(models));
        }

        public ActionResult LearnerCalendarEvents(FullCalendarViewModel viewModel, bool? toHtml = false)
        {
            ViewBag.ViewModel = viewModel;

            DataLoadOptions ops = new DataLoadOptions();
            ops.LoadWith<LessonTime>(i => i.GroupingLesson);
            ops.LoadWith<LessonTime>(i => i.RegisterLesson);
            ops.LoadWith<GroupingLesson>(i => i.RegisterLesson);
            ops.LoadWith<RegisterLesson>(i => i.UserProfile);
            ops.LoadWith<RegisterLesson>(i => i.LessonPriceType);
            models.GetDataContext().LoadOptions = ops;


            IQueryable<LessonTime> dataItems = viewModel.LearnerID.Value.PromptLearnerLessons(models);
            IQueryable<UserEvent> eventItems = models.GetTable<UserEvent>()
                .Where(e => !e.SystemEventID.HasValue)
                .Where(e => e.UID == viewModel.LearnerID);
            if (viewModel.DateFrom.HasValue && viewModel.DateTo.HasValue)
            {
                dataItems = dataItems.Where(t => t.ClassTime >= viewModel.DateFrom.Value
                    && t.ClassTime < viewModel.DateTo.Value.AddDays(1));
                eventItems = eventItems.Where(t =>
                    (t.StartDate >= viewModel.DateFrom.Value && t.StartDate < viewModel.DateTo.Value.AddDays(1))
                    || (t.StartDate >= viewModel.DateFrom.Value && t.StartDate < viewModel.DateTo.Value.AddDays(1))
                    || (t.EndDate >= viewModel.DateFrom.Value && t.EndDate < viewModel.DateTo.Value.AddDays(1))
                    || (t.StartDate < viewModel.DateFrom.Value && t.EndDate >= viewModel.DateTo.Value));
            }
            else if (viewModel.DateFrom.HasValue)
            {
                dataItems = dataItems.Where(t => t.ClassTime >= viewModel.DateFrom.Value);
                eventItems = eventItems.Where(t => t.StartDate >= viewModel.DateFrom.Value);
            }
            else if (viewModel.DateTo.HasValue)
            {
                dataItems = dataItems.Where(t => t.ClassTime < viewModel.DateTo.Value.AddDays(1));
                eventItems = eventItems.Where(t => t.EndDate < viewModel.DateTo.Value.AddDays(1));
            }

            var items = dataItems
                .ToList()
                .Select(d => new CalendarEventItem
                {
                    EventTime = d.ClassTime,
                    EventItem = d
                }).ToList();

            items.AddRange(eventItems.Select(v => new CalendarEventItem
            {
                EventTime = v.StartDate,
                EventItem = v
            }));

            if (toHtml == true)
            {

            }
            else
            {
                Response.ContentType = "application/json";
            }
            return View("~/Views/LearnerProfile/Module/LearnerCalendarEvents.cshtml", items);

        }

        public ActionResult LearnerLessonsReviewDetails(DailyBookingQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if(viewModel.KeyID!=null)
            {
                viewModel.LearnerID = viewModel.DecryptKeyValue();
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.LearnerID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            return View("~/Views/LearnerProfile/Module/LearnerLessonsReviewDetails.cshtml", item);
        }

        public ActionResult LoadCompleteExercisePurposeItems(ExercisePurposeViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var items = models.GetTable<PersonalExercisePurposeItem>()
                .Where(p => p.UID == viewModel.UID)
                .Where(p => p.CompleteDate.HasValue)
                .OrderByDescending(p => p.CompleteDate);

            return View("~/Views/LearnerProfile/Module/TrainingMilestoneItems.cshtml", items);
        }

        public ActionResult EditTrainingItem(TrainingItemViewModel viewModel)
        {
            if (!models.GetTable<TrainingExecution>().Any(t => t.ExecutionID == viewModel.ExecutionID))
            {
                return View("~/Views/ConsoleHome/Shared/AlertMessage.cshtml", model: "資料錯誤!!");
            }

            var stage = models.GetTable<TrainingStage>().Where(s => s.StageID == viewModel.StageID).FirstOrDefault();
            if (stage == null)
            {
                return View("~/Views/ConsoleHome/Shared/AlertMessage.cshtml", model: "資料錯誤!!");
            }
            ViewBag.TrainingStage = stage;

            TrainingItem item = models.GetTable<TrainingItem>().Where(x => x.ItemID == viewModel.ItemID).FirstOrDefault();

            if (item != null)
            {
                viewModel.ItemID = item.ItemID;
                viewModel.TrainingID = item.TrainingID;
                viewModel.GoalTurns = item.GoalTurns;
                viewModel.GoalStrength = item.GoalStrength;
                viewModel.ExecutionID = item.ExecutionID;
                viewModel.Description = item.Description;
                viewModel.ActualStrength = item.ActualStrength;
                viewModel.ActualTurns = item.ActualTurns;
                viewModel.Remark = item.Remark;
                viewModel.Repeats = item.Repeats;
                viewModel.DurationInSeconds = item.DurationInMinutes * 60;
                viewModel.BreakInterval = item.BreakIntervalInSecond;

                viewModel.AidID = item.TrainingItemAids.Select(s => s.AidID).ToArray();
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/LearnerProfile/ProfileModal/EditTrainingItem.cshtml", item);

        }

        public ActionResult EditBreakInterval(TrainingItemViewModel viewModel)
        {
            ViewResult result = (ViewResult)EditTrainingItem(viewModel);
            result.ViewName = "~/Views/Training/Module/EditBreakInterval.ascx";
            return result;
        }



    }
}