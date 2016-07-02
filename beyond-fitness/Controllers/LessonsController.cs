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
    public class LessonsController : SampleController<UserProfile>
    {
        public LessonsController() : base() { }
        // GET: Lessons
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BookingByCoach()
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = new LessonTimeViewModel();
            return View(item);

        }

        [HttpPost]
        public ActionResult BookingByCoach(LessonTimeViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;

            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(item);
            }

            RegisterLesson lesson = models.GetTable<RegisterLesson>().Where(r => r.RegisterID == viewModel.RegisterID).FirstOrDefault();
            if (lesson == null)
            {

                ViewBag.Message = "學員未購買課程!!";
                return View(item);
            }

            if (lesson.Attended == (int)Naming.LessonStatus.課程結束)
            {
                ViewBag.Message = "學員課程已結束!!";
                return View(item);
            }

            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = viewModel.CoachID,
                AttendingCoach = viewModel.CoachID,
                ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                DurationInMinutes = viewModel.Duration
            };

            var users = checkOverlapedBooking(timeItem, lesson);
            if (users.Count() > 0)
            {
                ViewBag.Message = "學員(" + String.Join("、", users.Select(u => u.RealName)) + ")上課時間重複!!";
                return View(item);
            }

            timeItem.RegisterID = lesson.RegisterID;
            if (lesson.RegisterGroupID.HasValue && lesson.GroupingMemberCount > 1)
            {
                timeItem.GroupID = lesson.RegisterGroupID;
            }

            models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();
            if (timeItem.GroupID.HasValue)
            {
                for (int i = 0; i <= (timeItem.DurationInMinutes - 1) / 60; i++)
                {
                    foreach (var regles in lesson.GroupingLesson.RegisterLesson)
                    {
                        timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                        {
                            ClassDate = timeItem.ClassTime.Value.Date,
                            LessonID = timeItem.LessonID,
                            Hour = timeItem.ClassTime.Value.Hour + i,
                            RegisterID = regles.RegisterID
                        });
                    }
                }
            }
            else
            {
                for (int i = 0; i <= (timeItem.DurationInMinutes - 1) / 60; i++)
                {
                    timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                    {
                        ClassDate = timeItem.ClassTime.Value.Date,
                        LessonID = timeItem.LessonID,
                        Hour = timeItem.ClassTime.Value.Hour + i,
                        RegisterID = lesson.RegisterID
                    });
                }
            }

            models.SubmitChanges();

            return Redirect("~/Account/Coach");
        }

        private IEnumerable<UserProfile> checkOverlapedBooking(LessonTime timeItem, RegisterLesson lesson)
        {
            int durationHours = (timeItem.ClassTime.Value.Minute + timeItem.DurationInMinutes.Value + 59) / 60;
            var items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == timeItem.ClassTime.Value.Date
                && t.Hour >= timeItem.ClassTime.Value.Hour
                && t.Hour < (timeItem.ClassTime.Value.Hour + durationHours));

            if (lesson.GroupingMemberCount > 1)
            {
                return items.Select(t => t.RegisterLesson)
                    .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterGroupID == lesson.RegisterGroupID),
                        t => t.UID, l => l.UID, (t, l) => l)
                    .Select(l => l.UserProfile);
            }
            else
            {
                return items.Select(t => t.RegisterLesson.UserProfile)
                    .Where(u => u.UID == lesson.UID);
            }
        }

        public ActionResult Attendee(String userName)
        {
            return View();
        }


        public ActionResult AttendeeSelector(String userName)
        {

            IEnumerable<RegisterLesson> items;
            userName = userName.GetEfficientString();
            if (userName == null)
            {
                items = models.GetTable<RegisterLesson>().Where(l => false);
            }
            else
            {
                items = models.GetTable<RegisterLesson>()
                    .Where(l => l.Attended != (int)Naming.LessonStatus.課程結束
                        && l.UserProfile.RealName.Contains(userName))
                    .Where(l => l.GroupingMemberCount == 1
                        || (l.RegisterGroupID.HasValue && l.GroupingMemberCount == l.GroupingLesson.RegisterLesson.Count()))
                    .OrderBy(l => l.UID).ThenBy(l => l.ClassLevel).ThenBy(l => l.Lessons);
            }

            return View(items);
        }

        public ActionResult BookingEvents(DateTime start, DateTime end)
        {
            return Json(models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate >= start && t.ClassDate <= end)
                .Select(t => t.ClassDate).ToList()
                .GroupBy(t => t)
                .Select(g => new
                {
                    title = g.Count(),
                    start = g.Key.ToString("yyyy-MM-dd")
                }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingList(DateTime lessonDate)
        {
            return View(lessonDate);
        }

        public ActionResult DailyBookingSummary(DateTime lessonDate)
        {
            Dictionary<int, int> index = new Dictionary<int, int>();
            foreach (int idx in Enumerable.Range(8, 15))
            {
                index[idx] = 0;
            }

            var items = models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate == lessonDate)
                .Select(t => t.Hour).ToList()
                .GroupBy(t => t);

            foreach (var item in items)
            {
                index[item.Key] = item.Count();
            }

            return Json(index.Select(i => new object[] { i.Key, i.Value }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingMembers(DateTime lessonDate, int hour)
        {
            var items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == lessonDate
                    && t.Hour == hour)
                .GroupBy(l => l.LessonID).Select(g => g.First());
            return View(items);
        }

        public ActionResult RevokeBooking(int lessonID)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            models.DeleteAny<LessonTime>(l => l.LessonID == lessonID);
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrainingPlan(LessonTimeExpansionViewModel viewModel)
        {

            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).First();

            HttpContext.SetCacheValue(CachingKey.Training, item);
            return View(item);
        }

        public ActionResult DeletePlan()
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            if (item == null)
            {
                ViewBag.Message = "未登記此上課時間!!";
                return RedirectToAction("Coach", "Account");
            }

            models.DeleteAllOnSubmit<TrainingPlan>(p => p.LessonID == item.LessonID);
            models.DeleteAllOnSubmit<LessonPlan>(p => p.LessonID == item.LessonID);
            models.SubmitChanges();

            HttpContext.RemoveCache(CachingKey.Training);
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassDate, hour = item.Hour, registerID = item.RegisterID, lessonID = item.LessonID });
        }

        public ActionResult CompletePlan()
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            if (item == null)
            {
                return RedirectToAction("Coach", "Account");
            }

            HttpContext.RemoveCache(CachingKey.Training);
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassDate, hour = item.Hour, registerID = item.RegisterID, lessonID = item.LessonID });
        }


        public ActionResult CommitPlan(TrainingPlanViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model;
            LessonPlan plan = prepareLessonPlan(out result,out model);

            if (result != null)
                return result;

            plan.Warming = viewModel.Warming;
            plan.RecentStatus = viewModel.RecentStatus;
            plan.EndingOperation = viewModel.EndingOperation;
            plan.Remark = viewModel.Remark;


            //execution.Repeats = viewModel.Repeats;
            //execution.BreakIntervalInSecond = viewModel.BreakInterval;

            //for (int i = 0; i < execution.TrainingItem.Count && i < viewModel.TrainingID.Length; i++)
            //{
            //    var item = execution.TrainingItem[i];
            //    item.TrainingID = (viewModel.TrainingID[i]).Value;
            //    item.GoalStrength = viewModel.GoalStrength[i];
            //    item.GoalTurns = viewModel.GoalTurns[i];
            //    item.Description = viewModel.Description[i];
            //}

            //execution.TrainingPlan.PlanStatus = (int)Naming.DocumentLevelDefinition.正常;

            models.SubmitChanges();
            //HttpContext.RemoveCache(CachingKey.Training);

            //return RedirectToAction("Coach", "Account", new { lessonDate = model.ClassDate, hour = model.Hour, registerID = model.RegisterID, lessonID = model.LessonID });
            ViewBag.Message = "資料更新完成!!";
            return View("TrainingPlan", model);

        }

        private LessonPlan prepareLessonPlan(out ActionResult result,out LessonTimeExpansion model)
        {
            result = null;
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            model = null;
            if (item != null)
            {
                model = models.GetTable<LessonTimeExpansion>()
                    .Where(l => l.ClassDate == item.ClassDate
                        && l.RegisterID == item.RegisterID
                        && l.Hour == item.Hour).FirstOrDefault();
            }

            if (model == null)
            {
                ViewBag.Message = "未登記此上課時間!!";
                result = RedirectToAction("Coach", "Account");
                return null;
            }

            LessonPlan plan = model.LessonTime.LessonPlan;
            if (plan == null)
                plan = model.LessonTime.LessonPlan = new LessonPlan { };

            return plan;
        }

        public ActionResult DeleteTraining(int id)
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            models.DeleteAny<TrainingPlan>(t => t.ExecutionID == id && t.LessonID == item.LessonID);

            LessonTimeExpansion model = models.GetTable<LessonTimeExpansion>()
                .Where(l => l.ClassDate == item.ClassDate
                    && l.RegisterID == item.RegisterID 
                    && l.Hour == item.Hour).First();
            return View("TrainingPlan", model);
        }


        public ActionResult AddTraining(TrainingPlanViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model;
            LessonPlan plan = prepareLessonPlan(out result,out model);

            if (result != null)
                return result;

            plan.Warming = viewModel.Warming;
            plan.RecentStatus = viewModel.RecentStatus;
            plan.EndingOperation = viewModel.EndingOperation;
            plan.Remark = viewModel.Remark;

            models.SubmitChanges();

            HttpContext.RemoveCache(CachingKey.TrainingExecution);
            return View(new TrainingExecution { });
        }


        public ActionResult AddTrainingItem()
        {
            return View();
        }

        public ActionResult EditTraining(int id)
        {

            TrainingExecution execution = models.GetTable<TrainingExecution>().Where(t => t.ExecutionID == id).FirstOrDefault();
            if(execution==null)
            {
                ViewBag.Message = "修改項目不存在!!";
                return View("AddTraining", new TrainingExecution { });
            }
            HttpContext.SetCacheValue(CachingKey.TrainingExecution, execution.ExecutionID);
            return View("AddTraining", execution);
        }


        public ActionResult CommitTrainingItem(TrainingItemViewModel viewModel)
        {
            TrainingExecution execution = prepareTrainingExecution();

            if(!ModelState.IsValid)
            {
                ViewBag.Message = ModelState.ErrorMessage();
                return View("AddTraining", execution);
            }

            execution.TrainingItem.Add(new TrainingItem
            {
                Description = viewModel.Description,
                GoalStrength = viewModel.GoalStrength,
                GoalTurns = viewModel.GoalTurns,
                TrainingID = viewModel.TrainingID.Value
            });

            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.TrainingExecution, execution.ExecutionID);

            return View("AddTraining", execution);
        }

        public ActionResult CreateTrainingItem(TrainingExecutionViewModel viewModel)
        {
            TrainingExecution execution = prepareTrainingExecution();

            if (!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }

            execution.Repeats = viewModel.Repeats;
            execution.BreakIntervalInSecond = viewModel.BreakInterval;

            TrainingItem item = new TrainingItem
            {
                TrainingID = 1
            };
            execution.TrainingItem.Add(item);

            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.TrainingExecution, execution.ExecutionID);

            return View("EditTrainingItem", item);
        }

        private TrainingExecution prepareTrainingExecution(bool createNew = true)
        {
            TrainingExecution execution = models.GetTable<TrainingExecution>()
                .Where(t => t.ExecutionID == (int?)HttpContext.GetCacheValue(CachingKey.TrainingExecution)).FirstOrDefault();

            if (execution == null && createNew)
            {
                LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

                Models.DataEntity.TrainingPlan plan = new Models.DataEntity.TrainingPlan
                {
                    LessonID = item.LessonID.Value,
                    PlanStatus = (int)Naming.DocumentLevelDefinition.暫存
                };
                execution = new TrainingExecution
                {
                    TrainingPlan = plan
                };
                models.GetTable<Models.DataEntity.TrainingPlan>().InsertOnSubmit(plan);
            }

            return execution;
        }

        public ActionResult DeleteTrainingItem(int id)
        {
            TrainingExecution execution = prepareTrainingExecution(false);
            if (execution == null)
            {
                ViewBag.Message = "修改項目不存在!!";
                return View("AddTraining", new TrainingExecution { });
            }

            var item = execution.TrainingItem.Where(i => i.ItemID == id).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "課程項目不存在!!";
                return View("AddTraining", execution);
            }

            execution.TrainingItem.Remove(item);
            models.SubmitChanges();
            return View("AddTraining", execution);
        }

        public ActionResult ValidateToCommitTraining(TrainingExecutionViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }
            return Json(new { result = true });
        }

        public ActionResult CommitTraining(TrainingExecutionViewModel viewModel)
        {
            TrainingExecution execution = prepareTrainingExecution();

            if(execution.TrainingItem.Count==0)
            {
                ViewBag.Message = "尚未設定上課項目!!";
                return View("AddTraining", execution);
            }

            if(!ModelState.IsValid)
            {
                ViewBag.Message = ModelState.ErrorMessage();
                return View("AddTraining", execution);
            }

            execution.Repeats = viewModel.Repeats;
            execution.BreakIntervalInSecond = viewModel.BreakInterval;

            for (int i = 0; i < execution.TrainingItem.Count && i < viewModel.TrainingID.Length; i++)
            {
                var item = execution.TrainingItem[i];
                item.TrainingID = (viewModel.TrainingID[i]).Value;
                item.GoalStrength = viewModel.GoalStrength[i];
                item.GoalTurns = viewModel.GoalTurns[i];
                item.Description = viewModel.Description[i];
            }

            execution.TrainingPlan.PlanStatus = (int)Naming.DocumentLevelDefinition.正常;

            models.SubmitChanges();
            HttpContext.RemoveCache(CachingKey.TrainingExecution);

            LessonTimeExpansion cacheItem = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            var timeItem = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == cacheItem.ClassDate
                && l.RegisterID == cacheItem.RegisterID && l.Hour == cacheItem.Hour).First();

            ViewBag.DataItem = timeItem.LessonTime;
            return View("TrainingPlan",timeItem);

        }

        public ActionResult CompleteTraining()
        {
            LessonTimeExpansion cacheItem = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            var timeItem = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == cacheItem.ClassDate
                && l.RegisterID == cacheItem.RegisterID && l.Hour == cacheItem.Hour).First();

            ViewBag.DataItem = timeItem.LessonTime;
            return View("TrainingPlan", timeItem);

        }

    }
}