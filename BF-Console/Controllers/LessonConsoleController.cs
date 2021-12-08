﻿using System;
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
using WebHome.Helper.BusinessOperation;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
    public class LessonConsoleController : SampleController<UserProfile>
    {
        // GET: LessonConsole
        public ActionResult ProcessCrossBranch(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if(viewModel.KeyID!=null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }
            var item = models.GetTable<LessonTime>().Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();
            return View("~/Views/LessonConsole/ProcessModal/ProcessCrossBranch.cshtml", item);
        }

        public ActionResult ShowTodayLessons(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (!viewModel.ClassTimeStart.HasValue)
            {
                viewModel.ClassTimeStart = DateTime.Today;
            }

            var profile = HttpContext.GetUser();
            if (!viewModel.BranchID.HasValue)
            {
                if (profile.IsManager() || profile.IsViceManager())
                {
                    var branch = models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID)
                            .FirstOrDefault();
                    if (branch != null)
                    {
                        viewModel.BranchID = branch.BranchID;
                        viewModel.BranchName = branch.BranchName;
                    }
                }
            }

            return View("~/Views/LessonConsole/ProcessModal/TodayLessons.cshtml", profile.LoadInstance(models));
        }

        public ActionResult CommitCrossBranch(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser();
            LessonTime item = profile.PreferredLessonTimeToApprove(models)
                    .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" });
            }

            item.PreferredLessonTime.ApprovalDate = DateTime.Now;
            item.PreferredLessonTime.ApproverID = profile.UID;
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult LessonContentDetails(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            LessonTime item = models.GetTable<LessonTime>()
                    .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            return View("~/Views/LessonConsole/Module/LessonContentDetails.cshtml", item);

        }

        public ActionResult ViewTrainingExecution(LessonTimeBookingViewModel viewModel)
        {
            ActionResult result = LessonContentDetails(viewModel);
            if (result is JsonResult)
                return result;

            ViewBag.Learner = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();
            if (ViewBag.Learner == null)
            {
                return Json(new { result = false, message = "資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            ((ViewResult)result).ViewName = "~/Views/LearnerProfile/ProfileModal/ViewTrainingExecution.cshtml";
            return result;
        }


        public ActionResult CloneLearnerTrainingPlan(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            LessonTime item = models.GetTable<LessonTime>()
                .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<RegisterLesson> lessonItems = item.GroupingLesson.RegisterLesson;
            if (lessonItems.Count() < 2)
            {
                return Json(new { result = false, message = "課程非一對多!!" }, JsonRequestBehavior.AllowGet);
            }

            if (viewModel.CopyFrom.HasValue)
            {
                if (!viewModel.UID.HasValue)
                {
                    return Json(new { result = false, message = "未指定同步目標之學員!!" }, JsonRequestBehavior.AllowGet);
                }

                var source = item.AssertLearnerTrainingPlan(models, viewModel.CopyFrom.Value);
                var target = item.AssertLearnerTrainingPlan(models, viewModel.UID.Value);
                models.CloneTrainingPlan(source, target);
            }
            else
            {
                if (!viewModel.UID.HasValue)
                {
                    return Json(new { result = false, message = "未指定同步來源之學員!!" }, JsonRequestBehavior.AllowGet);
                }

                var source = item.AssertLearnerTrainingPlan(models, viewModel.UID.Value);
                foreach (var lesson in lessonItems.Where(r => r.UID != viewModel.UID.Value))
                {
                    var target = item.AssertLearnerTrainingPlan(models, lesson.UID);
                    models.CloneTrainingPlan(source, target);
                }
            }
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloneCoachPITrainingPlan(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            LessonTime item = models.GetTable<LessonTime>()
                .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            var lessonItems = models.GetTable<LessonTime>().Where(l => l.GroupID == item.GroupID && l.LessonID != item.LessonID);
            if (lessonItems.Count() == 0)
            {
                return Json(new { result = false, message = "課程非一對多!!" }, JsonRequestBehavior.AllowGet);
            }

            if (viewModel.CopyFrom.HasValue)
            {
                var sourceLesson = item.GroupingLesson.LessonTime.Where(l => l.RegisterLesson.UID == viewModel.CopyFrom).FirstOrDefault();

                if (sourceLesson != null)
                {
                    var target = item.AssertTrainingPlan(models);
                    models.CloneTrainingPlan(sourceLesson.AssertTrainingPlan(models), target);

                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var source = item.AssertTrainingPlan(models);
                foreach (var lesson in lessonItems)
                {
                    var target = lesson.AssertTrainingPlan(models);
                    models.CloneTrainingPlan(source, target);
                }
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { result = false,message = "複製失敗!!" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SelectToCloneCoachPITrainingPlan(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            LessonTime item = models.GetTable<LessonTime>()
                .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            var lessonItems = models.GetTable<LessonTime>().Where(l => l.GroupID == item.GroupID && l.LessonID != item.LessonID);
            if (lessonItems.Count() < 1)
            {
                return Json(new { result = false, message = "課程非多人團體課!!" }, JsonRequestBehavior.AllowGet);
            }

            var items = lessonItems.Where(l => l.LessonID != item.LessonID)
                        .Select(l => l.RegisterLesson.UserProfile);

            return View("~/Views/LessonConsole/Module/SelectToCloneTrainingPlan.cshtml", items);

        }

        public ActionResult SelectToCloneLearnerTrainingPlan(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            LessonTime item = models.GetTable<LessonTime>()
                .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            var lessonItems = item.GroupingLesson.RegisterLesson;
            if (lessonItems.Count() < 2)
            {
                return Json(new { result = false, message = "課程非一對多!!" }, JsonRequestBehavior.AllowGet);
            }

            var items = models.GetTable<RegisterLesson>().Where(r => r.RegisterGroupID == item.GroupID)
                            .Where(l => l.UID != viewModel.UID)
                            .Select(l => l.UserProfile);

            return View("~/Views/LessonConsole/Module/SelectToCloneTrainingPlan.cshtml", items);

        }

        public ActionResult CopyTrainingPlan(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            LessonTime copyTo = models.GetTable<LessonTime>()
                .Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            if (copyTo == null)
            {
                return Json(new { result = false, message = "課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            LessonTime copyFrom = models.GetTable<LessonTime>()
                .Where(l => l.LessonID == viewModel.CopyFrom).FirstOrDefault();

            if (copyTo == null)
            {
                return Json(new { result = false, message = "複製來源課程資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            var source = copyFrom.AssertLearnerTrainingPlan(models, viewModel.UID.Value);
            var target = copyTo.AssertLearnerTrainingPlan(models, viewModel.UID.Value);
            models.CloneTrainingPlan(source, target, false);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SwitchLessonFavorite(TrainingExecutionViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (models.GetTable<FavoriteLesson>().Any(f => f.ExecutionID == viewModel.ExecutionID && f.UID == viewModel.UID))
            {
                models.ExecuteCommand("delete FavoriteLesson where ExecutionID = {0} and UID = {1}", viewModel.ExecutionID, viewModel.UID);
            }
            else
            {
                models.ExecuteCommand(@"
                        insert FavoriteLesson (ExecutionID,UID)
                        SELECT        TrainingPlan.ExecutionID, UserProfile.UID
                        FROM            TrainingPlan CROSS JOIN
                                                 UserProfile
                        WHERE        (TrainingPlan.ExecutionID = {0}) AND (UserProfile.UID = {1})", viewModel.ExecutionID, viewModel.UID);
            }

            return Json(new { result = true, message = models.GetTable<FavoriteLesson>().Where(f => f.ExecutionID == viewModel.ExecutionID).Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InquireLesson(LessonQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            IQueryable<LessonTime> items = models.GetTable<LessonTime>();
            IQueryable<LessonTime> coachPI;
            if (viewModel.LearnerID.HasValue)
            {
                items = viewModel.LearnerID.Value.PromptLearnerLessons(models);
                coachPI = viewModel.LearnerID.Value.PromptCoachPILessons(models);
            }
            else
            {
                coachPI = models.GetTable<LessonTime>().Where(l => false);
            }

            if (viewModel.CoachID.HasValue)
                items = items.Where(t => t.AttendingCoach == viewModel.CoachID);

            if (viewModel.QueryStart.HasValue)
            {
                items = items.Where(t => t.ClassTime >= viewModel.QueryStart && t.ClassTime < viewModel.QueryStart.Value.AddMonths(1));
                coachPI = coachPI.Where(t => t.ClassTime >= viewModel.QueryStart && t.ClassTime < viewModel.QueryStart.Value.AddMonths(1));
            }

            if (viewModel.ClassTime.HasValue)
            {
                items = items.Where(t => t.ClassTime >= viewModel.ClassTime && t.ClassTime < viewModel.ClassTime.Value.AddDays(1));
                coachPI = coachPI.Where(t => t.ClassTime >= viewModel.ClassTime && t.ClassTime < viewModel.ClassTime.Value.AddDays(1));
            }

            return View("~/Views/LearnerProfile/Module/LessonItems.cshtml", items.Union(coachPI));
        }

        public ActionResult ShowLessonList(LessonOverviewQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            var items = viewModel.InquireLesson(models);
            return View("~/Views/LessonConsole/Module/LessonList.cshtml", items);
        }

        public ActionResult ProcessLesson(LessonOverviewQueryViewModel viewModel)
        {
            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            var item = models.GetTable<LessonTime>().Where(c => c.LessonID == viewModel.LessonID).FirstOrDefault();
            if (item == null)
            {
                return View("~/Views/ConsoleHome/Shared/JsAlert.cshtml", model: "資料錯誤!!");
            }

            return View("~/Views/LessonConsole/Module/ProcessLesson.cshtml", item);
        }

        public ActionResult SelectCoachWithBranch(ServingCoachQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            viewModel.SelectablePartial = "~/Views/LessonConsole/Module/SelectBranch.cshtml";
            var items = models.PromptEffectiveCoach();
            if (viewModel.WorkPlace.HasValue)
            {
                items = items.Where(s => s.CoachWorkplace.Any(w => w.BranchID == viewModel.WorkPlace));
            }
            return View("~/Views/ContractConsole/ContractModal/SelectCoach.cshtml", items);
        }

        public ActionResult RemoteLessonFeedback(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            try
            {
                IQueryable<LessonTime> items = models.GetTable<LessonTime>();
                if (viewModel.UID.HasValue)
                {
                    items = items.Where(l => l.GroupingLesson.RegisterLesson.Any(r => r.UID == viewModel.UID));
                }
                models.RegisterRemoteFeedbackLesson(items);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CommitPlace(LessonTimeViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            var item = models.GetTable<LessonTime>().Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();
            if (item == null)
            {
                return View("~/Views/ConsoleHome/Shared/AlertMessage.cshtml", model: "課程資料錯誤!!");
            }

            viewModel.Place = viewModel.Place.GetEfficientString();
            if (viewModel.Place == null)
            {
                return Json(new { result = false, message = "Unfinished？!" });
            }

            if (!viewModel.Place.ValidateMeetingRoom(item.BranchStore, models))
            {
                return Json(new { result = false, message = "請輸入正確會議室連結!!" });
            }

            item.Place = viewModel.Place;
            models.SubmitChanges();

            return Json(new { result = true });
        }

    }
}