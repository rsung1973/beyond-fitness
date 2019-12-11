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
    public class ConsoleHomeController : SampleController<UserProfile>
    {
        static ConsoleHomeController()
        {
            BusinessExtensionMethods.ContractViewUrl = item => 
            {
                return $"{Settings.Default.HostDomain}{VirtualPathUtility.ToAbsolute("~/CommonHelper/ViewContract")}?pdf=1&contractID={item.ContractID}&t={DateTime.Now.Ticks}";
            };

            BusinessExtensionMethods.ContractServiceViewUrl = item =>
            {
                return $"{Settings.Default.HostDomain}{VirtualPathUtility.ToAbsolute("~/CommonHelper/ViewContractService")}?pdf=1&revisionID={item.RevisionID}&t={DateTime.Now.Ticks}";
            };

        }

        public const String InputErrorView = "~/Views/ConsoleHome/Shared/ReportInputError.cshtml";

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult Index(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (!viewModel.ClassTimeStart.HasValue)
            {
                viewModel.ClassTimeStart = DateTime.Today;
            }

            var profile = HttpContext.GetUser();
            if(!viewModel.BranchID.HasValue)
            {
                var branch = models.GetTable<CoachWorkplace>().Where(c => c.CoachID == profile.UID)
                        .Select(c => c.BranchStore).FirstOrDefault();
                if (branch != null)
                {
                    viewModel.BranchID = branch.BranchID;
                    viewModel.BranchName = branch.BranchName;
                }
            }

            return View(profile.LoadInstance(models));
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult CrossBranchIndex(ExerciseBillboardQueryViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            return View(profile.LoadInstance(models));
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer })]
        public ActionResult CoachBonusIndex(AchievementQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (!viewModel.AchievementDateFrom.HasValue)
            {
                viewModel.AchievementDateFrom = DateTime.Today.FirstDayOfMonth();
            }

            viewModel.AchievementDateTo = viewModel.AchievementDateFrom.Value.AddMonths(1);
            ViewBag.DataItems = viewModel.InquireMonthlySalary(models);

            var profile = HttpContext.GetUser();
            return View("~/Views/BonusCredit/CoachBonusIndex.cshtml", profile.LoadInstance(models));
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult Calendar(DailyBookingQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();
            viewModel.KeyID = profile.UID.EncryptKey();
            return View(profile.LoadInstance(models));
        }

        public ActionResult ContractIndex(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            viewModel.ContractDateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            viewModel.ContractDateTo = viewModel.ContractDateFrom.Value.AddMonths(1).AddDays(-1);

            var profile = HttpContext.GetUser();
            viewModel.KeyID = profile.UID.EncryptKey();
            return View(profile.LoadInstance(models));
        }

        public ActionResult ReportIndex(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            viewModel.KeyID = profile.UID.EncryptKey();
            return View("~/Views/ConsoleHome/ReportIndex.cshtml", profile.LoadInstance(models));

        }

        public ActionResult EditBlogArticle(BlogArticleQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.DocID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser().LoadInstance(models);

            var item = models.GetTable<BlogArticle>().Where(b => b.DocID == viewModel.DocID).FirstOrDefault();
            if (item != null)
            {
                viewModel.AuthorID = item.AuthorID;
                viewModel.Title = item.Title;
                viewModel.DocDate = item.Document.DocDate;
                viewModel.TagID = item.BlogTag.Select(t => (int?)t.CategoryID).ToArray();
            }
            ViewBag.BlogArticle = item;
            return View(profile);
        }

        public ActionResult BlogIndex(BlogArticleQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var profile = HttpContext.GetUser();
            viewModel.KeyID = profile.UID.EncryptKey();
            return View(profile.LoadInstance(models));
        }

        public ActionResult BlogArticleList(BlogArticleQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)BlogIndex(viewModel);

            var items = models.GetTable<BlogArticle>()
                .Where(b => b.BlogTag.Any(c => c.CategoryID == viewModel.CategoryID));

            viewModel.RecordCount = items.Count();
            viewModel.PageSize = viewModel.PageSize ?? 12;
            viewModel.PagingSize = viewModel.PagingSize ?? 5;
            viewModel.CurrentIndex = viewModel.CurrentIndex ?? 0;
            ViewBag.DataItems = items;

            result.ViewName = "BlogArticleList";
            return result;
        }


        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult EditCourseContract(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }
            viewModel.Version = Naming.ContractVersion.Ver2019;
            var profile = HttpContext.GetUser();
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                viewModel.AgentID = item.AgentID;
                viewModel.ContractType = item.ContractType;
                viewModel.ContractDate = item.ContractDate;
                viewModel.Subject = item.Subject;
                viewModel.ValidFrom = item.ValidFrom;
                viewModel.Expiration = item.Expiration;
                viewModel.OwnerID = item.OwnerID;
                viewModel.SequenceNo = item.SequenceNo;
                viewModel.Lessons = item.Lessons;
                viewModel.PriceID = item.PriceID;
                viewModel.DurationInMinutes = item.LessonPriceType.DurationInMinutes;
                var priceType = item.LessonPriceType;
                viewModel.PriceName = $"{priceType.PriceTypeBundle()}{(priceType.LessonPriceProperty.Any(p => p.PropertyID == (int)Naming.LessonPriceFeature.舊會員續約) ? "(舊會員續約)" : null)}{string.Format("{0,5:##,###,###,###}", priceType.ListPrice)}";
                viewModel.Remark = item.Remark;
                viewModel.FitnessConsultant = item.FitnessConsultant;
                viewModel.Status = item.Status;
                viewModel.UID = item.CourseContractMember.Select(m => m.UID).ToArray();
                viewModel.BranchID = item.CourseContractExtension.BranchID;
                viewModel.Renewal = item.Renewal;
                viewModel.TotalCost = item.TotalCost;
                if (item.InstallmentID.HasValue)
                {
                    viewModel.InstallmentPlan = true;
                    viewModel.Installments = item.ContractInstallment.Installments;
                }
                viewModel.UID = item.CourseContractMember.Select(m => m.UID).ToArray();
                if (item.CourseContractExtension.PaymentMethod != null)
                {
                    viewModel.PaymentMethod = item.CourseContractExtension.PaymentMethod.Split('/');
                }
            }
            else
            {
                viewModel.UID = new int[] { };
                viewModel.AgentID = profile.UID;
                if(profile.IsCoach())
                {
                    viewModel.FitnessConsultant = profile.UID;
                }
                viewModel.ContractType = null;
            }
            if(profile.IsManager() || profile.IsViceManager())
            {
                viewModel.ManagerID = profile.UID;
            }
            ViewBag.ViewModel = viewModel;
            return View(profile.LoadInstance(models));
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult SignCourseContract(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser();

            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();

            if (item == null)
            {
                ViewBag.GoBack = true;
                return View("~/Views/ConsoleHome/Shared/JsAlert.cshtml", model: "合約資料錯誤!!");
            }

            ViewBag.DataItem = item;

            return View(profile.LoadInstance(models));
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult SignContractService(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);

            if (ViewBag.DataItem is CourseContract item)
            {
                result.ViewName = "SignContractService";
            }

            return result;
        }

        public ActionResult CalendarEventItems(FullCalendarViewModel viewModel)
        {
            ViewResult result = (ViewResult)CalendarEvents(viewModel, true);
            result.ViewName = "~/Views/ConsoleHome/Module/EventItems.cshtml";
            return result;
        }


        public ActionResult CalendarEvents(FullCalendarViewModel viewModel,bool? toHtml = false)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.UID = viewModel.DecryptKeyValue();
            }

            DataLoadOptions ops = new DataLoadOptions();
            ops.LoadWith<LessonTime>(i => i.GroupingLesson);
            ops.LoadWith<LessonTime>(i => i.RegisterLesson);
            ops.LoadWith<GroupingLesson>(i => i.RegisterLesson);
            ops.LoadWith<RegisterLesson>(i => i.UserProfile);
            ops.LoadWith<RegisterLesson>(i => i.LessonPriceType);
            models.GetDataContext().LoadOptions = ops;


            IQueryable<LessonTime> learnerLessons = models.PromptLearnerLessons();
            IQueryable<LessonTime> coachPI = models.PromptCoachPILessons();
            IQueryable<UserEvent> eventItems = models.GetTable<UserEvent>().Where(e => !e.SystemEventID.HasValue);
            if (viewModel.DateFrom.HasValue && viewModel.DateTo.HasValue)
            {
                learnerLessons = learnerLessons.Where(t => t.ClassTime >= viewModel.DateFrom.Value
                    && t.ClassTime < viewModel.DateTo.Value.AddDays(1));
                coachPI = coachPI.Where(t => t.ClassTime >= viewModel.DateFrom.Value
                    && t.ClassTime < viewModel.DateTo.Value.AddDays(1));
                eventItems = eventItems.Where(t =>
                    (t.StartDate >= viewModel.DateFrom.Value && t.StartDate < viewModel.DateTo.Value.AddDays(1))
                    || (t.StartDate >= viewModel.DateFrom.Value && t.StartDate < viewModel.DateTo.Value.AddDays(1))
                    || (t.EndDate >= viewModel.DateFrom.Value && t.EndDate < viewModel.DateTo.Value.AddDays(1))
                    || (t.StartDate < viewModel.DateFrom.Value && t.EndDate >= viewModel.DateTo.Value));
            }
            else if (viewModel.DateFrom.HasValue)
            {
                learnerLessons = learnerLessons.Where(t => t.ClassTime >= viewModel.DateFrom.Value);
                coachPI = coachPI.Where(t => t.ClassTime >= viewModel.DateFrom.Value);
                eventItems = eventItems.Where(t => t.StartDate >= viewModel.DateFrom.Value);
            }
            else if (viewModel.DateTo.HasValue)
            {
                learnerLessons = learnerLessons.Where(t => t.ClassTime < viewModel.DateTo.Value.AddDays(1));
                coachPI = coachPI.Where(t => t.ClassTime < viewModel.DateTo.Value.AddDays(1));
                eventItems = eventItems.Where(t => t.EndDate < viewModel.DateTo.Value.AddDays(1));
            }
            if (viewModel.BranchID.HasValue)
            {
                learnerLessons = learnerLessons.Where(t => t.BranchID == viewModel.BranchID);
                coachPI = coachPI.Where(t => t.BranchID == viewModel.BranchID);
                eventItems = eventItems.Where(t => t.BranchID == viewModel.BranchID);
            }
            if (viewModel.UID.HasValue)
            {
                learnerLessons = learnerLessons.Where(t => t.AttendingCoach == viewModel.UID
                                        || t.RegisterLesson.UID == viewModel.UID);
                coachPI = coachPI.Where(t => t.RegisterLesson.UID == viewModel.UID);
                eventItems = eventItems.Where(t => t.UID == viewModel.UID
                    || t.GroupEvent.Any(g => g.UID == viewModel.UID));
            }
            else
            {
                eventItems = eventItems.Where(f => false);
            }

            var items = learnerLessons
                .Select(d => new CalendarEventItem
                {
                    EventTime = d.ClassTime,
                    EventItem = d
                }).ToList();

            items.AddRange(coachPI.GroupBy(l => l.GroupID)
                .ToList()
                .Select(d => new CalendarEventItem
                {
                    EventTime = d.First().ClassTime,
                    EventItem = d.First()
                }));

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
            return View("~/Views/ConsoleHome/Module/CalendarEvents.cshtml", items);

        }



        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult ExerciseBillboard()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View(profile);
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult InquireExerciseBillboard(ExerciseBillboardQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if(!viewModel.StartDate.HasValue)
            {
                viewModel.StartDate = DateTime.Today.FirstDayOfMonth();
            }
            if(!viewModel.EndDate.HasValue)
            {
                viewModel.EndDate = DateTime.Today;
            }

            IQueryable<RegisterLesson> lessons = models.PromptMemberExerciseRegisterLesson();
            if(viewModel.BranchID.HasValue)
            {
                lessons = lessons.Join(models.GetTable<CoachWorkplace>().Where(w => w.BranchID == viewModel.BranchID), 
                    r => r.UID, w => w.CoachID, (r, w) => r);
            }

            IQueryable<LessonTime> items = lessons.TotalRegisterLessonItems(models)
                    .Where(l => l.LessonAttendance != null)
                    .Where(l => l.ClassTime >= viewModel.StartDate)
                    .Where(l => l.ClassTime < viewModel.EndDate.Value.AddDays(1));

            return View("~/Views/ConsoleHome/Module/CurrentExerciseBillboard.cshtml", items);
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult InquireExerciseBillboardLastMonth(ExerciseBillboardQueryViewModel viewModel)
        {
            var queryDate = DateTime.Today.FirstDayOfMonth();
            viewModel.StartDate = queryDate.AddMonths(-1);
            viewModel.EndDate = queryDate.AddDays(-1);
            ViewResult result = (ViewResult)InquireExerciseBillboard(viewModel);
            result.ViewName = "~/Views/ConsoleHome/Module/ExerciseBillboard.cshtml";
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult GetExerciseBillboardDetails(ExerciseBillboardQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireExerciseBillboard(viewModel);

            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return new EmptyResult { };

            Response.ContentType = "application/json";
            return View("~/Views/ConsoleHome/Module/ExerciseBillboardDetails.cshtml", items);
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult ApplyContractService(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                result.ViewName = "ApplyContractService";
            }
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult ReassignFitnessConsultant(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                result.ViewName = "ReassignFitnessConsultant";
            }
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult PostponeContractExpiration(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                result.ViewName = "PostponeContractExpiration";
            }
            viewModel.Version = (Naming.ContractVersion?)item.CourseContractExtension.Version;
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult TransferContract(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                result.ViewName = "TransferContract";
            }
            viewModel.Version = (Naming.ContractVersion?)item.CourseContractExtension.Version;
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult TerminateContract(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                result.ViewName = "TerminateContract";
            }
            viewModel.Version = (Naming.ContractVersion?)item.CourseContractExtension.Version;
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult QuickTerminateContract(CourseContractQueryViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                if (profile.IsCoach())
                    viewModel.FitnessConsultant = profile.UID;
                else
                    viewModel.FitnessConsultant = item.FitnessConsultant;
                viewModel.OperationMode = Naming.OperationMode.快速終止;
                viewModel.Status = (int)Naming.CourseContractStatus.待確認;
                result.ViewName = "QuickTerminateContract";
            }
            return result;
        }

        public ActionResult DailyLessonsBarChart(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (!viewModel.ClassTimeStart.HasValue)
            {
                viewModel.ClassTimeStart = DateTime.Today;
            }

            var profile = HttpContext.GetUser();
            return View("~/Views/ConsoleHome/Module/TodayLessonsBarChartC3.cshtml", profile.LoadInstance(models));
        }

        public ActionResult ShowLessonSummary(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (!viewModel.ClassTimeStart.HasValue)
            {
                viewModel.ClassTimeStart = DateTime.Today;
            }

            var profile = HttpContext.GetUser();
            return View("~/Views/ConsoleHome/Module/AboutLessonSummary.cshtml", profile.LoadInstance(models));
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach, (int)Naming.RoleID.Servitor })]
        public ActionResult LearnerProfile(DailyBookingQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (viewModel.KeyID != null)
            {
                viewModel.LearnerID = viewModel.DecryptKeyValue();
            }

            ViewBag.DataItem = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.LearnerID).First();

            return View(profile.LoadInstance(models));
        }

        public ActionResult LessonTrainingContent(DailyBookingQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            ViewBag.DataItem = models.GetTable<LessonTime>().Where(u => u.LessonID == viewModel.LessonID).First();
            ViewBag.Learner = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.LearnerID).First();

            return View(profile.LoadInstance(models));
        }

        public ActionResult EditLearnerCharacter(LearnerCharacterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (viewModel.KeyID != null)
            {
                viewModel.UID = viewModel.DecryptKeyValue();
            }

            UserProfile item = ViewBag.DataItem = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).First();
            QuestionnaireRequest quest = models.GetTable<QuestionnaireRequest>()
                    .Where(r => r.UID == viewModel.UID)
                    .Where(r => r.QuestionnaireID == viewModel.QuestionnaireID).FirstOrDefault();

            if (quest == null)
            {
                quest = item.UID.AssertQuestionnaire(models, Naming.QuestionnaireGroup.身體心靈密碼);
                viewModel.QuestionnaireID = quest.QuestionnaireID;
            }

            ViewBag.CurrentQuestionnaire = quest;

            return View(profile.LoadInstance(models));
        }

        public ActionResult EditPaymentForContract(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)SignCourseContract(viewModel);
            CourseContract item = (CourseContract)ViewBag.DataItem;
            if (item != null)
            {
                result.ViewName = "~/Views/PaymentConsole/EditPaymentForContract.cshtml";
            }
            return result;

        }

        public ActionResult EditPaymentForPISession(PaymentQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)PaymentIndex(viewModel);
            result.ViewName = "~/Views/PaymentConsole/EditPaymentForPISession.cshtml";
            return result;
        }

        public ActionResult EditPaymentForShopping(PaymentQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)PaymentIndex(viewModel);
            result.ViewName = "~/Views/PaymentConsole/EditPaymentForShopping.cshtml";
            return result;
        }



        public ActionResult PaymentIndex(PaymentQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var profile = HttpContext.GetUser();
            viewModel.ScrollToView = false;
            return View(profile.LoadInstance(models));
        }

        public ActionResult InquirePaymentIndex(PaymentQueryViewModel viewModel)
        {
            if (viewModel.KeyID != null)
            {
                viewModel.PaymentID = viewModel.DecryptKeyValue();
            }

            IQueryable<Payment> items = viewModel.InquirePayment(this, out string alertMessage);
            ViewBag.DataItems = items;

            var profile = HttpContext.GetUser();
            viewModel.ScrollToView = true;

            return View("~/Views/ConsoleHome/PaymentIndex.cshtml", profile.LoadInstance(models));
        }

        public ActionResult VoidPayment(PaymentQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.PaymentID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser();

            var item = models.GetTable<Payment>().Where(c => c.PaymentID == viewModel.PaymentID).FirstOrDefault();

            if (item == null)
            {
                ViewBag.GoBack = true;
                return View("~/Views/ConsoleHome/Shared/JsAlert.cshtml", model: "收款資料錯誤!!");
            }

            ViewBag.DataItem = item;

            return View(profile.LoadInstance(models));
        }

        public ActionResult ApplyPaymentAchievement(PaymentQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)VoidPayment(viewModel);
            if (ViewBag.DataItem is Payment item)
            {
                result.ViewName = "~/Views/PaymentConsole/ApplyPaymentAchievement.cshtml";
            }

            return result;
        }

        public ActionResult AchievementOverview(MonthlyIndicatorQueryViewModel viewModel)
        {
            if (viewModel.KeyID != null)
            {
                viewModel.PeriodID = viewModel.DecryptKeyValue();
            }

            if(!viewModel.Year.HasValue || !viewModel.Month.HasValue)
            {
                viewModel.Year = DateTime.Today.Year;
                viewModel.Month = DateTime.Today.Month;
            }

            var item = viewModel.GetAlmostMonthlyIndicator(models, true);

            if (item == null)
            {
                return View("~/Views/ConsoleHome/Shared/JsGoback.cshtml", model: "資料尚未設定!!");
            }

            //if (item == null)
            //{
            //    item = models.InitializeMonthlyIndicator(viewModel.Year.Value, viewModel.Month.Value);
            //}

            //if (viewModel.Year == DateTime.Today.Year && viewModel.Month == DateTime.Today.Month)
            //{
            //    item.UpdateMonthlyAchievement(this);
            //    item.UpdateMonthlyAchievementGoal(models);
            //}

            ViewBag.ViewModel = viewModel;
            ViewBag.DataItem = item;

            var profile = HttpContext.GetUser();
            return View("~/Views/BusinessConsole/AchievementOverview.cshtml", profile.LoadInstance(models));
        }

        public ActionResult ApplyAchievementGoal(MonthlyIndicatorQueryViewModel viewModel)
        {
            if (viewModel.KeyID != null)
            {
                viewModel.PeriodID = viewModel.DecryptKeyValue();
            }

            if (!viewModel.Year.HasValue || !viewModel.Month.HasValue)
            {
                viewModel.Year = DateTime.Today.Year;
                viewModel.Month = DateTime.Today.Month;
            }

            var item = viewModel.AssertMonthlyIndicator(models);

            ViewBag.ViewModel = viewModel;
            ViewBag.DataItem = item;

            var profile = HttpContext.GetUser();
            return View("~/Views/BusinessConsole/ApplyAchievementGoal.cshtml", profile.LoadInstance(models));
        }

        public ActionResult AchievementReview(MonthlyIndicatorQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)AchievementOverview(viewModel);
            if (ViewBag.DataItem != null)
            {
                result.ViewName = "~/Views/BusinessConsole/AchievementReview.cshtml";
            }
            return result;
        }

        public ActionResult InvoiceOverview(InvoiceQueryViewModel viewModel)
        {
            if(viewModel.Year.HasValue && viewModel.Month.HasValue)
            {
                viewModel.DateFrom = new DateTime(viewModel.Year.Value, viewModel.Month.Value, 1);
                if (viewModel.DateFrom.Value.Month % 2 == 0)
                {
                    viewModel.DateFrom = viewModel.DateFrom.Value.AddMonths(-1);
                }
                viewModel.DateTo = viewModel.DateFrom.Value.AddMonths(2);
            }
            else if (!viewModel.DateFrom.HasValue || !viewModel.DateTo.HasValue)
            {
                viewModel.DateFrom = DateTime.Today.FirstDayOfMonth();
                if (viewModel.DateFrom.Value.Month % 2 == 0)
                {
                    viewModel.DateFrom = viewModel.DateFrom.Value.AddMonths(-1);
                }
                viewModel.DateTo = viewModel.DateFrom.Value.AddMonths(2);
            }

            var items = models.GetTable<InvoiceItem>()
                .Where(i => i.InvoiceType == (int)Naming.InvoiceTypeDefinition.一般稅額計算之電子發票)
                .Where(i => i.InvoiceDate >= viewModel.DateFrom && i.InvoiceDate < viewModel.DateTo);

            ViewBag.ViewModel = viewModel;
            ViewBag.DataItems = items;

            var cancellationItems = models.GetTable<InvoiceCancellation>().Where(i => i.CancelDate >= viewModel.DateFrom && i.CancelDate < viewModel.DateTo);
            ViewBag.CancellationItems = cancellationItems;

            var allowanceItems = models.GetTable<InvoiceAllowance>().Where(a => a.AllowanceDate >= viewModel.DateFrom && a.AllowanceDate < viewModel.DateTo);
            ViewBag.AllowanceItems = allowanceItems;

            var profile = HttpContext.GetUser();
            return View("~/Views/InvoiceConsole/InvoiceOverview.cshtml", profile.LoadInstance(models));
        }

        public ActionResult InvoiceNoIndex(InvoiceQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.TrackID = viewModel.DecryptKeyValue();
            }

            IQueryable<InvoiceTrackCode> items = models.GetTable<InvoiceTrackCode>();

            if (viewModel.TrackID.HasValue)
            {
                items = items.Where(t => t.TrackID == viewModel.TrackID);
            }
            else
            {
                if (!viewModel.DateFrom.HasValue)
                {
                    viewModel.DateFrom = DateTime.Today.FirstDayOfMonth();
                    if (viewModel.DateFrom.Value.Month % 2 == 0)
                    {
                        viewModel.DateFrom = viewModel.DateFrom.Value.AddMonths(-1);
                    }
                }
                items = items.Where(t => t.Year == viewModel.DateFrom.Value.Year)
                            .Where(t => t.PeriodNo == viewModel.TrackPeriodNo);
            }

            viewModel.TrackCode = viewModel.TrackCode.GetEfficientString();
            if (viewModel.TrackCode != null)
            {
                items = items.Where(t => t.TrackCode == viewModel.TrackCode);
            }

            ViewBag.DataItems = items;

            var profile = HttpContext.GetUser();
            return View("~/Views/InvoiceConsole/InvoiceNoIndex.cshtml", profile.LoadInstance(models));
        }

    }
}