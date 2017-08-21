﻿using System;
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
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    [CoachOrAssistantAuthorize]
    public class ClassFacetController : SampleController<UserProfile>
    {
        // GET: ClassFacet
        public ActionResult ClassIndex(int? lessonID)
        {
            var item = models.GetTable<LessonTime>().Where(i => i.LessonID == lessonID).First();
            return View(item);
        }
        public ActionResult ShowUndone(int? lessonID,int? direction)
        {
            var current = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).FirstOrDefault();
            if (current != null)
            {
                LessonTime item;
                if (direction < 0)
                {
                    item = models.GetTable<LessonTime>().Where(t => t.AttendingCoach == current.AttendingCoach && t.ClassTime < current.ClassTime)
                        .Where(t => t.LessonAttendance == null)
                        .OrderByDescending(t => t.ClassTime).FirstOrDefault();
                }
                else
                {
                    item = models.GetTable<LessonTime>().Where(t => t.AttendingCoach == current.AttendingCoach && t.ClassTime > current.ClassTime)
                        .Where(t => t.LessonAttendance == null)
                        .OrderBy(t => t.ClassTime).FirstOrDefault();
                }
                if (item != null)
                    return View("~/Views/ClassFacet/Module/WidgetGrid.ascx", item);
            }

            return new EmptyResult();

        }

        public ActionResult RebookingByCoach(int id)
        {

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == id).FirstOrDefault();
            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "修改上課時間資料不存在!!");
            }

            ViewBag.ViewModel = new LessonTimeViewModel
            {
                LessonID = item.LessonID,
                ClassDate = item.ClassTime.Value,
                CoachID = item.AttendingCoach.Value,
                Duration = item.DurationInMinutes.Value,
                TrainingBySelf = item.TrainingBySelf,
                BranchID = item.BranchID.Value
            };

            return View("~/Views/ClassFacet/Module/RebookingByCoach.ascx", item);

        }


        public ActionResult UpdateBookingByCoach(LessonTimeViewModel viewModel)
        {

            ViewBag.ViewModel = viewModel;

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();
            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "修改上課時間資料不存在!!");
            }

            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = item.InvitedCoach,
                AttendingCoach = item.AttendingCoach,
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration
            };

            var users = models.CheckOverlappingBooking(timeItem, item);
            if (users.Count() > 0)
            {
                ViewBag.Message = "學員(" + String.Join("、", users.Select(u => u.RealName)) + ")上課時間重複!!";
                return View("~/Views/Shared/JsAlert.ascx");
            }

            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            models.DeleteAll<LessonTimeExpansion>(t => t.LessonID == item.LessonID);

            var changeCoach = item.AttendingCoach != viewModel.CoachID;
            item.InvitedCoach = viewModel.CoachID;
            item.AttendingCoach = viewModel.CoachID;
            item.ClassTime = viewModel.ClassDate;
            item.DurationInMinutes = timeItem.DurationInMinutes;
            item.BranchID = viewModel.BranchID;
            //item.TrainingBySelf = viewModel.TrainingBySelf;

            models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();
            if (item.RegisterLesson.GroupingMemberCount > 1)
            {
                for (int i = 0; i <= (item.DurationInMinutes + item.ClassTime.Value.Minute - 1) / 60; i++)
                {
                    foreach (var regles in item.RegisterLesson.GroupingLesson.RegisterLesson)
                    {
                        timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                        {
                            ClassDate = item.ClassTime.Value.Date,
                            LessonID = item.LessonID,
                            Hour = item.ClassTime.Value.Hour + i,
                            RegisterID = regles.RegisterID
                        });
                    }
                }
            }
            else
            {
                for (int i = 0; i <= (item.DurationInMinutes + item.ClassTime.Value.Minute - 1) / 60; i++)
                {
                    timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                    {
                        ClassDate = item.ClassTime.Value.Date,
                        LessonID = item.LessonID,
                        Hour = item.ClassTime.Value.Hour + i,
                        RegisterID = item.RegisterID
                    });
                }
            }

            models.SubmitChanges();

            if (item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自主訓練)
            {
                models.ExecuteCommand("update TuitionInstallment set PayoffDate = {0} where RegisterID = {1} ", item.ClassTime, item.RegisterID);
            }

            return Json(new { result = true, message = "上課時間修改完成!!",
                changeCoach = changeCoach,
                classTime = String.Format("{0:yyyy/MM/dd H:mm}", item.ClassTime) + "-" + String.Format("{0:H:mm}", item.ClassTime.Value.AddMinutes(item.DurationInMinutes.Value)) });

        }


        public ActionResult EditRecentStatus(int uid)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "學員資料錯誤!!");
            }

            return View("~/Views/ClassFacet/Module/EditRecentStatus.ascx", profile);
        }

        public ActionResult CommitRecentStatus(int uid, String recentStatus)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile == null)
            {
                return Json(new { result = false, message = "學員資料錯誤!!" });
            }

            profile.RecentStatus = recentStatus;

            models.SubmitChanges();
            return Json(new { result = true, message = "資料更新完成!!" });

        }

        public ActionResult ShowLessonWidget(int lessonID, int? registerID)
        {
            var model = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).First();
            ViewBag.RegisterID = registerID;
            return View("~/Views/ClassFacet/Module/LessonWidget.ascx", model);
        }

        public ActionResult LessonContent(int lessonID, int registerID)
        {
            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).First();
            var model = item.GroupingLesson.RegisterLesson.Where(r => r.RegisterID == registerID).First();

            ViewBag.LessonTime = item;
            return View("~/Views/ClassFacet/Module/LessonContent.ascx", model);
        }

        public ActionResult LearnerRecentLessons(int uid, int? lessonID)
        {
            var item = models.GetTable<LessonTime>().Where(u => u.LessonID == lessonID)
                .Where(l => l.GroupingLesson.RegisterLesson.Any(r => r.UID == uid))
                .FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "課程資料錯誤!!");
            }

            ViewBag.LessonTime = item;
            return View("~/Views/ClassFacet/Module/LearnerRecentLessons.ascx", item.GroupingLesson.RegisterLesson.Where(r => r.UID == uid).First());
        }

        public ActionResult LearnerAssessment(int uid, int? lessonID)
        {
            var item = models.GetTable<LessonFitnessAssessment>().Where(u => u.UID == uid)
                .OrderByDescending(u => u.AssessmentID)
                .FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "體能分析表資料錯誤!!");
            }

            return View("~/Views/ClassFacet/Module/LearnerAssessment.ascx", item);
        }

        public ActionResult BookingByCoach(FullCalendarViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View("~/Views/ClassFacet/Module/BookingByCoach.ascx");
        }


    }


}