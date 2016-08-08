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
using System.Linq.Expressions;

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
        public ActionResult BookingByCoach(int? coachID)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = new LessonTimeViewModel();
            if (coachID.HasValue)
                ViewBag.DefaultCoach = coachID;
            return View(item);

        }

        public ActionResult FreeAgentClockIn(int? coachID)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new { result = false, message = "連線已中斷，請重新登入!!" });
            }

            if (item.IsFreeAgent() && item.UID != coachID)
            {
                return Json(new { result = false, message = "自由教練資料錯誤!!" });
            }

            var items = models.GetTable<LessonTime>()
                .Where(l => l.ClassTime >= DateTime.Today && l.ClassTime < DateTime.Today.AddDays(1))
                .Where(l => l.AttendingCoach == coachID)
                .Where(l => l.LessonAttendance == null);

            if (items.Count() <= 0)
            {
                return Json(new { result = false, message = "本日無自由教練課程!!" });
            }

            foreach (var lesson in items)
            {
                lesson.LessonAttendance = new LessonAttendance
                {
                    CompleteDate = DateTime.Now
                };
            }
            models.SubmitChanges();

            return Json(new { result = true });

        }

        [HttpGet]
        public ActionResult BookingByFreeAgent(int? coachID)
        {
            return BookingByCoach(coachID);
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
                //ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration,
                TrainingBySelf = viewModel.TrainingBySelf
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
                for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
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
                for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
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

            return RedirectToAction("Coach", "Account", new { lessonDate = viewModel.ClassDate, message = "上課時間預約完成!!" });
        }

        public ActionResult BookingByFreeAgent(LessonTimeViewModel viewModel)
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

            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = viewModel.CoachID,
                AttendingCoach = viewModel.CoachID,
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration,
                RegisterLesson = new RegisterLesson
                {
                    UID = viewModel.UID.Value,
                    RegisterDate = DateTime.Now,
                    GroupingMemberCount = 1,
                    Lessons = 1
                }
            };

            models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();

            for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
            {
                timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                {
                    ClassDate = timeItem.ClassTime.Value.Date,
                    LessonID = timeItem.LessonID,
                    Hour = timeItem.ClassTime.Value.Hour + i,
                    RegisterID = timeItem.RegisterID
                });
            }

            models.SubmitChanges();
            if (item.IsFreeAgent())
                return RedirectToAction("FreeAgent", "Account");
            else
                return RedirectToAction("Coach", "Account");
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
            //return Json(models.GetTable<LessonTimeExpansion>()
            //    .Where(t => t.ClassDate >= start && t.ClassDate <= end)
            //    .Select(t => t.ClassDate).ToList()
            //    .GroupBy(t => t)
            //    .Select(g => new
            //    {
            //        title = g.Count(),
            //        start = g.Key.ToString("yyyy-MM-dd")
            //    }), JsonRequestBehavior.AllowGet);
            //return Json(models.GetTable<LessonTime>()
            //        .Where(t => t.ClassTime >= start && t.ClassTime < end.AddDays(1))
            //        .Select(t => t.ClassTime).ToList()
            //        .Select(t => t.Value.Date)
            //        .GroupBy(t => t)
            //        .Select(g => new
            //        {
            //            title = g.Count(),
            //            start = g.Key.ToString("yyyy-MM-dd")
            //        }), JsonRequestBehavior.AllowGet);

            var today = DateTime.Today;

            var items = models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1))
                .Where(t=>!t.LessonTime.TrainingBySelf.HasValue || t.LessonTime.TrainingBySelf==0)
                .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                .GroupBy(t => t.ClassDate)
                .Select(g => new _calendarEvent
                {
                    id = "course",
                    title = g.Distinct().Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "課程訓練",
                    allDay = true,
                    className = g.Key < today ? new string[] {"event","bg-color-greenLight"} : new string[] { "event", "bg-color-blue" },
                    icon = g.Key<today ? "fa-check" : "fa-clock-o"
                });

            items = items.Concat(models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1))
                .Where(t => t.LessonTime.TrainingBySelf == 1)
                .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                .GroupBy(t => t.ClassDate)
                .Select(g => new _calendarEvent
                {
                    id = "self",
                    title = g.Distinct().Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "自主訓練",
                    allDay = true,
                    className = g.Key < today ? new string[] { "event", "bg-color-greenLight" } : new string[] { "event", "bg-color-red" },
                    icon = g.Key < today ? "fa-ckeck" : "fa-clock-o"
                }));


            return Json(items, JsonRequestBehavior.AllowGet);


        }

        class _calendarEvent
        {
            public String id { get; set; }
            public String title { get; set; }
            public String start { get; set; }
            public String description { get; set; }
            public bool allDay { get; set; }
            public String[] className { get; set; }
            public String icon { get; set; }
        }

        public ActionResult VipEvents(DateTime start, DateTime end)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }

            return Json(models.GetTable<LessonTime>()
                    .Where(t => t.ClassTime >= start && t.ClassTime < end.AddDays(1))
                    //.Where(l => l.TrainingPlan != null)
                    //.Where(l => l.LessonPlan != null)
                    .Where(t => t.RegisterLesson.UID == item.UID).ToArray()
                    .Select(g => new
                    {
                        title = "運動嘍",
                        start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
                        color= "#5BC0DE",   // an option!
                        textColor= "#FFFFFF", // an option!          
                        lessonID = g.LessonID
                    }), JsonRequestBehavior.AllowGet);

        }

        public ActionResult VipEvent(DateTime lessonDate)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            var items = models.GetTable<LessonTime>().Where(t => t.ClassTime >= lessonDate
                && t.ClassTime < lessonDate.AddDays(1)
                && t.RegisterLesson.UID == profile.UID);

            if (items.Count() == 0)
            {
                return Json(new { result = false, message = "課程資料不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            if(items.Count()>1)
            {
                return View("SelectVipDay", items);
            }
            else
            {
                return View("ToVipDay", items.First());
            }

        }

        public ActionResult VipDay(int id)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == id
                && t.RegisterLesson.UID == profile.UID).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Vip", "Account");
            }

            ViewBag.LessonTimeExpansion = item.LessonTimeExpansion.First();
            return View(item);

        }

        public ActionResult Feedback(int id,FeedBackViewModel viewModel)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == id
                && t.RegisterLesson.UID == profile.UID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料不存在!!" });
            }

            item.LessonPlan.FeedBack = viewModel.FeedBack;
            for(int i=0;i<item.TrainingPlan.Count && i<viewModel.ExecutionFeedBack.Length;i++)
            {
                item.TrainingPlan[i].TrainingExecution.ExecutionFeedBack = viewModel.ExecutionFeedBack[i];
            }
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult DailyBookingListJson(DateTime lessonDate,DateTime? endQueryDate)
        {
            Expression<Func<LessonTimeExpansion, bool>> queryExpr = l => true;

            if(endQueryDate.HasValue)
            {
                queryExpr = queryExpr.And(l => l.ClassDate >= lessonDate && l.ClassDate < endQueryDate.Value.AddDays(1));
            }
            else
            {
                queryExpr = l => l.ClassDate == lessonDate;
            }

            var items = models.GetTable<LessonTimeExpansion>().Where(queryExpr)
                .GroupBy(l => new { ClassDate = l.ClassDate, Hour = l.Hour });

            return Json(new { data = items
                .Select(g=>new
                {
                    timezone = String.Format("{0:00}:00 - {1:00}:00",g.Key.Hour,g.Key.Hour+1),
                    count = g.Count(),
                    booktime = "--",
                    hour = g.Key.Hour,
                    function = ""
                })
                .ToArray() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingList(DateTime lessonDate, DateTime? endQueryDate)
        {
            ViewBag.EndQueryDate = endQueryDate;
            return View(lessonDate);
        }


        public ActionResult DailyTodoList(DateTime lessonDate, DateTime? endQueryDate)
        {
            ViewBag.EndQueryDate = endQueryDate;
            return View(lessonDate);
        }

        [HttpGet]
        public ActionResult QueryVip()
        {
            ViewBag.ViewModel = new DailyBookingQueryViewModel
            {
                DateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                MonthInterval = 1
            };
            return View();
        }


        public ActionResult QueryVip(DailyBookingQueryViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTime> items = models.GetTable<LessonTime>();

            HttpContext.SetCacheValue(CachingKey.DailyBookingQuery, viewModel);

            if (viewModel.DateFrom.HasValue)
            {
                items = items.Where(l => l.ClassTime >= viewModel.DateFrom.Value);
                if (viewModel.MonthInterval.HasValue)
                {
                    items = items.Where(l => l.ClassTime < viewModel.DateFrom.Value.AddMonths(viewModel.MonthInterval.Value));
                }
            }
            if (viewModel.DateTo.HasValue)
                items = items.Where(l => l.ClassTime < viewModel.DateTo.Value.AddDays(1));

            if (viewModel.CoachID.HasValue)
                items = items.Where(l => l.AttendingCoach == viewModel.CoachID);

            if (!string.IsNullOrEmpty(viewModel.UserName))
            {
                items = items.Where(l => l.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName));
            }

            if (item.IsFreeAgent())
            {
                items = items.Where(l => l.AttendingCoach == item.UID);
            }

            return View();
        }

        public ActionResult QueryBookingList()
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTime> items = queryBookingLessons(item);

            ViewBag.DataItems = items.Join(models.GetTable<LessonTimeExpansion>(),
                t => t.LessonID, l => l.LessonID, (t, l) => l);

            return View("DailyBookingList");
        }

        private IQueryable<LessonTime> queryBookingLessons(UserProfile item)
        {
            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);

            IQueryable<LessonTime> items = models.GetTable<LessonTime>();

            if (viewModel.DateFrom.HasValue)
            {
                items = items.Where(l => l.ClassTime >= viewModel.DateFrom.Value);
                if (viewModel.MonthInterval.HasValue)
                {
                    items = items.Where(l => l.ClassTime < viewModel.DateFrom.Value.AddMonths(viewModel.MonthInterval.Value));
                }
            }
            if (viewModel.DateTo.HasValue)
                items = items.Where(l => l.ClassTime < viewModel.DateTo.Value.AddDays(1));

            if (viewModel.CoachID.HasValue)
                items = items.Where(l => l.AttendingCoach == viewModel.CoachID);

            if (!string.IsNullOrEmpty(viewModel.UserName))
            {
                items = items.Where(l => l.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName));
            }

            if (item.IsFreeAgent())
            {
                items = items.Where(l => l.AttendingCoach == item.UID);
            }

            return items;
        }

        public ActionResult QueryBookingPlot()
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTime> lessons = queryBookingLessons(item);

            var items = lessons.Join(models.GetTable<LessonTimeExpansion>(),
                t => t.LessonID, l => l.LessonID, (t, l) => l)
                .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                .GroupBy(t => t.ClassDate);

            return Json(items.Select(g => new
            {
                x = g.Key.ToString("MM/dd"),
                y = g.Distinct().Count()
            }).ToArray(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult DailyBookingQuery(DailyBookingQueryViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTime> items = models.GetTable<LessonTime>();

            HttpContext.SetCacheValue(CachingKey.DailyBookingQuery, viewModel);

            if (viewModel.DateFrom.HasValue)
            {
                items = items.Where(l => l.ClassTime >= viewModel.DateFrom.Value);
                if (viewModel.MonthInterval.HasValue)
                {
                    items = items.Where(l => l.ClassTime < viewModel.DateFrom.Value.AddMonths(viewModel.MonthInterval.Value));
                }
            }
            if(viewModel.DateTo.HasValue)
                items = items.Where(l => l.ClassTime < viewModel.DateTo.Value.AddDays(1));

            if (viewModel.CoachID.HasValue)
                items = items.Where(l => l.AttendingCoach == viewModel.CoachID);

            if(!string.IsNullOrEmpty(viewModel.UserName))
            {
                items = items.Where(l => l.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName));
            }

            if(item.IsFreeAgent())
            {
                items = items.Where(l => l.AttendingCoach == item.UID);
            }

            ViewBag.DataItems = items;
            return View(DateTime.Today);
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

        public ActionResult DailyBookingPlot(DateTime lessonDate)
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

            return Json(index.Select(i => new
            {
                x = i.Key,
                y = i.Value
            }).ToArray(), JsonRequestBehavior.AllowGet);
        }


        class GraphDataItem
        {
            public DateTime? ClassTime { get; set; }
            public int ActionLearning { get; set; }
            public int PostureRedress { get; set; }
            public int Training { get; set; }
            public int Cardiopulmonary { get; set; }
            public int Endurance { get; set; }
            public int ExplosiveForce { get; set; }
            public int Flexibility { get; set; }
            public int SportsPerformance { get; set; }
            public int Strength { get; set; }
        }

        GraphDataItem calcAverage(LessonTime item)
        {
            var trend = item.LessonTrend;
            decimal total = trend.ActionLearning.Value + trend.PostureRedress.Value + trend.Training.Value;
            var r = new GraphDataItem
            {
                ClassTime = item.ClassTime.Value,
                ActionLearning = (int)Math.Round(trend.ActionLearning.Value * 100m / total),
                PostureRedress = (int)Math.Round(trend.PostureRedress.Value * 100m / total)
            };
            r.Training = 100 - r.ActionLearning - r.PostureRedress;
            return r;
        }

        GraphDataItem fitnessAverage(LessonTime item)
        {
            var fitness = item.FitnessAssessment;
            decimal total = fitness.Cardiopulmonary.Value 
                + fitness.Endurance.Value 
                + fitness.ExplosiveForce.Value
                + fitness.Flexibility.Value
                + fitness.SportsPerformance.Value 
                + fitness.Strength.Value;

            var r = new GraphDataItem
            {
                ClassTime = item.ClassTime.Value,
                Cardiopulmonary = (int)Math.Round(fitness.Cardiopulmonary.Value * 100m / total),
                Endurance = (int)Math.Round(fitness.Endurance.Value * 100m / total),
                ExplosiveForce = (int)Math.Round(fitness.ExplosiveForce.Value * 100m / total),
                Flexibility = (int)Math.Round(fitness.Flexibility.Value * 100m / total),
                SportsPerformance = (int)Math.Round(fitness.SportsPerformance.Value * 100m / total)
            };

            r.Strength = 100
                - r.Cardiopulmonary
                - r.Endurance
                - r.ExplosiveForce
                - r.Flexibility
                - r.SportsPerformance;

            return r;
        }


        public ActionResult FitnessGraph(DateTime start, DateTime end)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { });
            }

            DateTime startDate = start < end ? new DateTime(start.Year, start.Month, 1) : new DateTime(end.Year, end.Month, 1);
            DateTime endDate = start >= end ? start : end;

            var items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= startDate && t.ClassTime < endDate.AddDays(1))
                .Where(t => t.RegisterLesson.UID == profile.UID)
                .Where(t => t.LessonAttendance != null).ToArray()
                .Select(t => fitnessAverage(t)).ToArray();

            var idx = Enumerable.Range(0, items.Length);
            int section = items.Length >= 12 ? (items.Length + 11) / 12 : 1;

            return Json(
                new
                {
                    data = new object[]
                    {
                        new
                        {
                            label = "柔軟度",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].Flexibility
                            }).ToArray()
                        },
                        new
                        {
                            label = "心肺",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].Cardiopulmonary
                            }).ToArray()
                        },
                        new
                        {
                            label = "肌力",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].Strength
                            }).ToArray()
                        },
                        new
                        {
                            label = "肌耐力",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].Endurance
                            }).ToArray()
                        },
                        new
                        {
                            label = "爆發力",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].ExplosiveForce
                            }).ToArray()
                        },
                        new
                        {
                            label = "運動表現",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].SportsPerformance
                            }).ToArray()
                        }
                    },
                    ticks = idx.Select(g => new object[]
                        {
                            g,
                            //g%section==0 ? (g+1).ToString() : ""
                            ""
                        }).ToArray()
                }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TrendGraph(DateTime start, DateTime end)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { });
            }

            DateTime startDate = start < end ? new DateTime(start.Year, start.Month, 1) : new DateTime(end.Year, end.Month, 1);
            DateTime endDate = start >= end ? start : end;

            var items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= startDate && t.ClassTime < endDate.AddDays(1))
                .Where(t => t.RegisterLesson.UID == profile.UID)
                .Where(t => t.LessonAttendance != null).ToArray()
                .Select(t => calcAverage(t)).ToArray();

            var idx = Enumerable.Range(0, items.Length);
            int section = items.Length >= 12 ? (items.Length + 11) / 12 : 1;

            return Json(
                new
                {
                    data = new object[]
                    {
                        new
                        {
                            label = "動作學習",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].ActionLearning
                            }).ToArray()
                        },
                        new
                        {
                            label = "姿勢矯正",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].PostureRedress
                            }).ToArray()
                        },
                        new
                        {
                            label = "訓練",
                            data = idx.Select(g=>new object[]
                            {
                                g,
                                items[g].Training
                            }).ToArray()
                        }
                    },
                    ticks = idx.Select(g => new object[]
                        {
                            g,
                            //g%section==0 ? (g+1).ToString() : ""
                            ""
                        }).ToArray()
                }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TrendGraphAverage(DateTime start, DateTime end)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { });
            }

            DateTime startDate = start < end ? new DateTime(start.Year,start.Month,1) : new DateTime(end.Year, end.Month, 1);
            DateTime endDate = start >= end ? start : end;

            var items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= startDate && t.ClassTime < endDate.AddDays(1))
                .Where(t => t.RegisterLesson.UID == profile.UID)
                .Where(t => t.LessonAttendance != null).ToArray()
                .Select(t=> calcAverage(t))
                .GroupBy(t => new { Year = t.ClassTime.Value.Year, Month = t.ClassTime.Value.Month })
                .Select(g => new
                {
                    Key = g.Key,
                    ActonLearning = (g.Sum(v => v.ActionLearning) + g.Count() / 2) / g.Count(),
                    PostureRedress = (g.Sum(v => v.PostureRedress) + g.Count() / 2) / g.Count(),
                    Training = (g.Sum(v => v.Training) + g.Count() / 2) / g.Count()
                });

            return Json(
                new
                {
                    data = new object[]
                    {
                        new
                        {
                            label = "動作學習",
                            data = items.Select(g=>new object[]
                            {
                                (g.Key.Year-startDate.Year)*12+g.Key.Month-startDate.Month,
                                g.ActonLearning
                            }).ToArray() },
                        new
                        {
                            label = "姿勢矯正",
                            data = items.Select(g => new object[]
                            {
                                (g.Key.Year-startDate.Year)*12+g.Key.Month-startDate.Month,
                                g.PostureRedress
                            }).ToArray()
                        },
                        new
                        {
                            label = "訓練",
                            data = items.Select(g => new object[]
                            {
                                (g.Key.Year-startDate.Year)*12+g.Key.Month-startDate.Month,
                                g.Training
                            }).ToArray()
                        }
                    },
                    ticks = Enumerable.Range(0, (end.Year - start.Year) * 12 + end.Month - start.Month + 1)
                        .Select(g => new object[]
                        {
                            g,
                            String.Format("{0:00}",(start.Month-1+g)%12+1)
                        }).ToArray()
                }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DailyBookingMembers(DateTime lessonDate, int? hour)
        {
            IQueryable<LessonTimeExpansion> items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == lessonDate);
            if(hour.HasValue)
            {
                items = items.Where(t => t.Hour == hour);
            }
                
            return View(items.GroupBy(l => l.LessonID).Select(g => g.First()));
        }

        public ActionResult DailyBookingMembersByFreeAgent(DateTime lessonDate, int? hour)
        {
            return DailyBookingMembers(lessonDate, hour);
        }

        public ActionResult DailyBookingMembersByQuery(DateTime lessonDate, int? hour)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTimeExpansion> items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == lessonDate);
            if (hour.HasValue)
            {
                items = items.Where(t => t.Hour == hour);
            }

            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);
            if(viewModel!=null)
            {
                if (viewModel.CoachID.HasValue)
                    items = items.Where(l => l.LessonTime.AttendingCoach == viewModel.CoachID);
                if (!string.IsNullOrEmpty(viewModel.UserName))
                {
                    items = items.Where(l => l.LessonTime.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName));
                }
                if(item.IsFreeAgent())
                {
                    items = items.Where(l => l.LessonTime.AttendingCoach == item.UID);
                }
            }

            return View("DailyBookingMembers",items.GroupBy(l => l.LessonID).Select(g => g.First()));
        }

        public ActionResult RevokeBooking(int lessonID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "課程資料不存在!!";
                return RedirectToAction("Coach", "Account", new { message = "課程資料不存在!!" });
            }
            //else if (item.LessonPlan != null || item.TrainingPlan.Count > 0)
            //{
            //    ViewBag.Message = "請先刪除預編課程!!";
            //    return RedirectToAction("Coach", "Account", new { lessonDate = lessonDate, message= "請先刪除預編課程!!" });
            //}

            models.DeleteAny<LessonTime>(l => l.LessonID == lessonID);
            if (item.RegisterLesson.UserProfile.LevelID == (int)Naming.MemberStatusDefinition.Anonymous)
            {
                models.DeleteAny<RegisterLesson>(l => l.RegisterID == item.RegisterID);
            }

            ViewBag.Message = "課程預約已取消!!";
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassTime.Value.Date ,message = "課程預約已取消!!" });

        }

        public ActionResult RevokeBookingByFreeAgent(int lessonID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "課程資料不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            models.DeleteAny<LessonTime>(l => l.LessonID == lessonID);
            models.DeleteAny<RegisterLesson>(l => l.RegisterID == item.RegisterID);

            return Json(new { result = true });

        }

        public ActionResult PreviewLesson(LessonTimeExpansionViewModel viewModel)
        {
            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).First();

            return View(item);

        }

        public ActionResult DailyTrendPie(LessonTimeExpansionViewModel viewModel)
        {
            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).First();

            var trend = item.LessonTime.LessonTrend;

            if (trend == null)
                return Json(new object[] { },JsonRequestBehavior.AllowGet);

            return Json(new object[] {
                new {
                    label = "動作學習",
                    data = trend.ActionLearning
                },
                new {
                    label = "姿勢矯正",
                    data = trend.PostureRedress
                },
                new {
                    label = "訓練",
                    data = trend.Training
                }
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DailyFitnessPie(LessonTimeExpansionViewModel viewModel)
        {
            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).First();

            var fitness = item.LessonTime.FitnessAssessment;

            if (fitness == null)
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);

            return Json(new object[] {
                new {
                    label = "柔軟度",
                    data = fitness.Flexibility
                },
                new {
                    label = "心肺",
                    data = fitness.Cardiopulmonary
                },
                new {
                    label = "肌力",
                    data = fitness.Strength
                },
                new {
                    label = "肌耐力",
                    data = fitness.Endurance
                },
                new {
                    label = "爆發力",
                    data = fitness.ExplosiveForce
                },
                new {
                    label = "運動表現",
                    data = fitness.SportsPerformance
                }
            }, JsonRequestBehavior.AllowGet);

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
            model.RegisterLesson.UserProfile.RecentStatus = viewModel.RecentStatus;
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


        public virtual ActionResult AddTraining(TrainingPlanViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model;
            LessonPlan plan = prepareLessonPlan(out result,out model);

            if (result != null)
                return result;

            plan.Warming = viewModel.Warming;
            plan.RecentStatus = viewModel.RecentStatus;
            model.RegisterLesson.UserProfile.RecentStatus = viewModel.RecentStatus;
            plan.EndingOperation = viewModel.EndingOperation;
            plan.Remark = viewModel.Remark;

            models.SubmitChanges();

            HttpContext.RemoveCache(CachingKey.TrainingExecution);
            return View("AddTraining",new TrainingExecution { });
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
                TrainingID = viewModel.TrainingID.Value,
                ActualStrength = viewModel.GoalStrength,
                ActualTurns = viewModel.GoalTurns,
                Remark = viewModel.Remark
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
                return Json(new { result = false, message = "修改項目不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            var item = execution.TrainingItem.Where(i => i.ItemID == id).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "課程項目不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            execution.TrainingItem.Remove(item);
            models.SubmitChanges();

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
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
                item.ActualStrength = item.GoalStrength = viewModel.GoalStrength[i];
                item.ActualTurns = item.GoalTurns = viewModel.GoalTurns[i];
                item.Description = viewModel.Description[i];
                item.Remark = viewModel.Remark[i];
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

        public virtual ActionResult CompleteTraining()
        {
            LessonTimeExpansion cacheItem = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            var timeItem = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == cacheItem.ClassDate
                && l.RegisterID == cacheItem.RegisterID && l.Hour == cacheItem.Hour).First();

            ViewBag.DataItem = timeItem.LessonTime;
            return View("TrainingPlan", timeItem);

        }

        public ActionResult QueryModal()
        {
            return View();
        }

    }
}