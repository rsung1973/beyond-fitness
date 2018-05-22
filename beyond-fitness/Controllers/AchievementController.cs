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
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Officer,(int)Naming.RoleID.ViceManager, (int)Naming.RoleID.Assistant,
        (int)Naming.RoleID.Accounting, (int)Naming.RoleID.Manager })]
    public class AchievementController : SampleController<UserProfile>
    {
        // GET: Achievement
        public ActionResult LessonIndex(AchievementQueryViewModel viewModel)
        {
            viewModel.AchievementDateFrom = viewModel.AchievementDateTo = DateTime.Today;
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult InquireLesson(AchievementQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (!viewModel.AchievementDateFrom.HasValue)
            {
                ModelState.AddModelError("AchievementDateFrom", "請選擇查詢起日");
            }

            if (!viewModel.AchievementDateTo.HasValue)
            {
                ModelState.AddModelError("AchievementDateTo", "請選擇查詢迄日");
            }

            if(viewModel.AchievementDateFrom.HasValue && viewModel.AchievementDateTo.HasValue 
                && (viewModel.AchievementDateTo.Value-viewModel.AchievementDateFrom.Value).TotalDays>31)
            {
                ModelState.AddModelError("AchievementDateFrom", "查詢區間只能是一個月內");
                ModelState.AddModelError("AchievementDateTo", "查詢區間只能是一個月內");
            }

            if (viewModel.ByCoachID == null || viewModel.ByCoachID.Length == 0)
            {
                ModelState.AddModelError("ByCoachID", "請勾選一位");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            IQueryable<LessonTime> items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= viewModel.AchievementDateFrom)
                .Where(t => t.ClassTime < viewModel.AchievementDateTo.Value.AddDays(1));

            if(viewModel.ByCoachID!=null && viewModel.ByCoachID.Length>0)
            {
                items = items.Where(t => viewModel.ByCoachID.Contains(t.AttendingCoach));
            }

            if(viewModel.CoachID.HasValue)
            {
                items = items.Where(t => t.AttendingCoach == viewModel.CoachID);
            }

            if (viewModel.ClassTime.HasValue)
            {
                items = items.Where(t => t.ClassTime >= viewModel.ClassTime && t.ClassTime < viewModel.ClassTime.Value.AddDays(1));
            }

            if (viewModel.QueryType.HasValue)
                items = items.ByLessonQueryType(viewModel.QueryType);

            return View("~/Views/Achievement/Module/DailyLessonList.ascx", items);
        }

        public ActionResult ShowLessonList(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireLesson(viewModel);
            IQueryable<LessonTime> items = (IQueryable<LessonTime>)result.Model;
            if (items == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            return View("~/Views/CoachFacet/Module/DailyBookingList.ascx", items);
        }

        public ActionResult CheckLearnerAttendance(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)ShowLearnerToComplete(viewModel);
            IQueryable<LessonTime> items = (IQueryable<LessonTime>)result.Model;
            if (items == null)
            {
                return result;
            }

            int[] scope = new int[]
                {
                    (int)Naming.LessonPriceStatus.一般課程,
                    //(int)Naming.LessonPriceStatus.企業合作方案,
                    (int)Naming.LessonPriceStatus.已刪除,
                    (int)Naming.LessonPriceStatus.點數兌換課程
                };

            items = items.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                                                || (l.RegisterLesson.RegisterLessonEnterprise != null
                                                && (new int?[] { (int)Naming.LessonPriceStatus.一般課程, (int)Naming.LessonPriceStatus.團體學員課程 })
                                                    .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status)));

            foreach (var item in items)
            {
                item.LessonPlan.CommitAttendance = DateTime.Now;
            }

            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult InquireBarChart(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireLesson(viewModel);
            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return new EmptyResult { };

            Response.ContentType = "application/json";
            return View("~/Views/Achievement/Module/LessonBarChartData.ascx", items);
        }

        public ActionResult InquireCoachLesson(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireLesson(viewModel);
            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return new EmptyResult { };

            return View("~/Views/Achievement/Module/CoachLessonList.ascx", items);
        }

        public ActionResult InquireCoachBarChart(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireLesson(viewModel);
            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return new EmptyResult { };

            Response.ContentType = "application/json";
            return View("~/Views/Achievement/Module/CoachLessonBarChartData.ascx", items);
        }

        class _ClassTimeGroupKey
        {
            public DateTime? ClassTime { get; set; }
            public int? Hour { get; set; }
        }
        public ActionResult CreateLessonListXlsx(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireLesson(viewModel);
            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
            {
                ViewBag.GoBack = true;
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            DataTable dailyTable = new DataTable();
            dailyTable.Columns.Add(new DataColumn("日期", typeof(DateTime)));
            dailyTable.Columns.Add(new DataColumn("P.T已完成", typeof(int)));
            dailyTable.Columns.Add(new DataColumn("P.T未完成", typeof(int)));
            dailyTable.Columns.Add(new DataColumn("P.I已完成", typeof(int)));
            dailyTable.Columns.Add(new DataColumn("P.I未完成", typeof(int)));
            dailyTable.Columns.Add(new DataColumn("S.T", typeof(int)));
            dailyTable.Columns.Add(new DataColumn("體驗課程", typeof(int)));
            dailyTable.Columns.Add(new DataColumn("內部訓練", typeof(int)));


            DataTable dailyCoachTable = new DataTable();
            dailyCoachTable.Columns.Add(new DataColumn("日期", typeof(DateTime)));
            dailyCoachTable.Columns.Add(new DataColumn("體能顧問", typeof(String)));
            dailyCoachTable.Columns.Add(new DataColumn("P.T已完成", typeof(int)));
            dailyCoachTable.Columns.Add(new DataColumn("P.T未完成", typeof(int)));
            dailyCoachTable.Columns.Add(new DataColumn("P.I已完成", typeof(int)));
            dailyCoachTable.Columns.Add(new DataColumn("P.I未完成", typeof(int)));
            dailyCoachTable.Columns.Add(new DataColumn("S.T", typeof(int)));
            dailyCoachTable.Columns.Add(new DataColumn("體驗課程", typeof(int)));
            dailyCoachTable.Columns.Add(new DataColumn("內部訓練", typeof(int)));

            int[] scope = new int[] {
                        (int)Naming.LessonPriceStatus.一般課程,
                        //(int)Naming.LessonPriceStatus.企業合作方案,
                        (int)Naming.LessonPriceStatus.已刪除,
                        (int)Naming.LessonPriceStatus.點數兌換課程 };

            Expression<Func<LessonTime, _ClassTimeGroupKey>> groupBy;
            bool byHour = false;
            if (viewModel.AchievementDateFrom == viewModel.AchievementDateTo)
            {
                byHour = true;
                groupBy = l => new _ClassTimeGroupKey
                {
                    ClassTime = l.ClassTime.Value.Date,
                    Hour = l.HourOfClassTime
                };
            }
            else
            {
                groupBy = l => new _ClassTimeGroupKey
                {
                    ClassTime = l.ClassTime.Value.Date,
                    Hour = 0
                };
            }

            foreach (var g in items.GroupBy(groupBy)
                .OrderBy(g => g.Key.ClassTime).ThenBy(g=>g.Key.Hour))
            {
                var r = dailyTable.NewRow();
                r[0] = byHour ? $"{g.Key.ClassTime:yyyy/MM/dd} {g.Key.Hour:00}:00" : $"{g.Key.ClassTime:yyyy/MM/dd}";
                var lessons = g.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                            || (l.RegisterLesson.RegisterLessonEnterprise != null
                            && (new int?[] { (int)Naming.LessonPriceStatus.一般課程, (int)Naming.LessonPriceStatus.團體學員課程 })
                                .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status)));
                r[1] = lessons.Where(l => l.LessonAttendance != null).Count();
                r[2] = lessons.Where(l => l.LessonAttendance == null).Count();
                lessons = g.Where(l => l.TrainingBySelf == 1);
                r[3] = lessons.Where(l => l.LessonAttendance != null).Count();
                r[4] = lessons.Where(l => l.LessonAttendance == null).Count();
                r[5] = g.Where(l => l.TrainingBySelf == 2).Count();
                r[6] = g.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.體驗課程
                            || (l.RegisterLesson.RegisterLessonEnterprise != null && l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status == (int)Naming.LessonPriceStatus.體驗課程)).Count();
                r[7] = g.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.內部訓練).Count();
                dailyTable.Rows.Add(r);

                foreach (var c in g.GroupBy(l => l.AttendingCoach))
                {
                    var coach = models.GetTable<ServingCoach>().Where(s => s.CoachID == c.Key).First();
                    var t = dailyCoachTable.NewRow();
                    t[0] = byHour ? $"{g.Key.ClassTime:yyyy/MM/dd} {g.Key.Hour:00}:00" : $"{g.Key.ClassTime:yyyy/MM/dd}";
                    t[1] = coach.UserProfile.FullName();
                    var lessonItems = c.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                                || (l.RegisterLesson.RegisterLessonEnterprise != null
                                && (new int?[] { (int)Naming.LessonPriceStatus.一般課程, (int)Naming.LessonPriceStatus.團體學員課程 })
                                    .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status)));
                    t[2] = lessonItems.Where(l => l.LessonAttendance != null).Count();
                    t[3] = lessonItems.Where(l => l.LessonAttendance == null).Count();
                    lessonItems = c.Where(l => l.TrainingBySelf == 1);
                    t[4] = lessonItems.Where(l => l.LessonAttendance != null).Count();
                    t[5] = lessonItems.Where(l => l.LessonAttendance == null).Count();
                    t[6] = c.Where(l => l.TrainingBySelf == 2).Count();
                    t[7] = c.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.體驗課程
                                || (l.RegisterLesson.RegisterLessonEnterprise != null && l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status == (int)Naming.LessonPriceStatus.體驗課程)).Count();
                    t[8] = c.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.內部訓練).Count();
                    dailyCoachTable.Rows.Add(t);
                }
            }

            DataTable coachTable = new DataTable();
            coachTable.Columns.Add(new DataColumn("體能顧問", typeof(String)));
            coachTable.Columns.Add(new DataColumn("P.T已完成", typeof(int)));
            coachTable.Columns.Add(new DataColumn("P.T未完成", typeof(int)));
            coachTable.Columns.Add(new DataColumn("P.I已完成", typeof(int)));
            coachTable.Columns.Add(new DataColumn("P.I未完成", typeof(int)));
            coachTable.Columns.Add(new DataColumn("S.T", typeof(int)));
            coachTable.Columns.Add(new DataColumn("體驗課程", typeof(int)));
            coachTable.Columns.Add(new DataColumn("內部訓練", typeof(int)));

            foreach (var c in items.GroupBy(l => l.AttendingCoach))
            {
                var coach = models.GetTable<ServingCoach>().Where(s => s.CoachID == c.Key).First();
                var r = coachTable.NewRow();
                r[0] = coach.UserProfile.FullName();
                var lessons = c.Where(l => scope.Contains(l.RegisterLesson.LessonPriceType.Status.Value)
                            || (l.RegisterLesson.RegisterLessonEnterprise != null
                            && (new int?[] { (int)Naming.LessonPriceStatus.一般課程, (int)Naming.LessonPriceStatus.團體學員課程 })
                                .Contains(l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status)));
                r[1] = lessons.Where(l => l.LessonAttendance != null).Count();
                r[2] = lessons.Where(l => l.LessonAttendance == null).Count();
                lessons = c.Where(l => l.TrainingBySelf == 1);
                r[3] = lessons.Where(l => l.LessonAttendance != null).Count();
                r[4] = lessons.Where(l => l.LessonAttendance == null).Count();
                r[5] = c.Where(l => l.TrainingBySelf == 2).Count();
                r[6] = c.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.體驗課程
                            || (l.RegisterLesson.RegisterLessonEnterprise != null && l.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status == (int)Naming.LessonPriceStatus.體驗課程)).Count();
                r[7] = c.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.LessonPriceStatus.內部訓練).Count();
                coachTable.Rows.Add(r);
            }

            DataTable achievement = models.CreateLessonAchievementDetails(items.Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.內部訓練));

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Cache-control", "max-age=1");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=({1:yyyy-MM-dd HH-mm-ss}){0}", HttpUtility.UrlEncode("LessonReport.xlsx"), DateTime.Now));

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(dailyTable);
                ds.Tables.Add(dailyCoachTable);
                ds.Tables.Add(coachTable);
                ds.Tables.Add(achievement);

                using (var xls = ds.ConvertToExcel())
                {
                    if (viewModel.AchievementDateFrom.HasValue && viewModel.AchievementDateTo.HasValue)
                    {
                        xls.Worksheets.ElementAt(0).Name = "上課統計表（日）" + String.Format("{0:yyyy-MM-dd}~{1:yyyy-MM-dd}", viewModel.AchievementDateFrom, viewModel.AchievementDateTo);
                        xls.Worksheets.ElementAt(1).Name = "上課體能顧問彙總表" + String.Format("{0:yyyy-MM-dd}~{1:yyyy-MM-dd}", viewModel.AchievementDateFrom, viewModel.AchievementDateTo);
                        xls.Worksheets.ElementAt(2).Name = "上課彙總表" + String.Format("{0:yyyy-MM-dd}~{1:yyyy-MM-dd}", viewModel.AchievementDateFrom, viewModel.AchievementDateTo);
                    }
                    xls.SaveAs(Response.OutputStream);
                }
            }

            dailyTable.Dispose();
            dailyCoachTable.Dispose();
            coachTable.Dispose();
            achievement.Dispose();

            return new EmptyResult();
        }

        public ActionResult ShowLearnerToComplete(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireLesson(viewModel);
            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");

            items = items.GetLearnerUncheckedLessons();

            return View("~/Views/Achievement/Module/LearnerToComplete.ascx", items);
        }

        public ActionResult InquireLearnerToCompleteBarChart(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)ShowLearnerToComplete(viewModel);
            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return new EmptyResult { };

            Response.ContentType = "application/json";
            return View("~/Views/Achievement/Module/LearnerToCompleteBarChartData.ascx", items);
        }


    }
}