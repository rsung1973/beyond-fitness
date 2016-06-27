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
    public class LessonsController : SampleController<UserProfile>
    {
        public LessonsController() : base() { }
        // GET: Lessons
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
                ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                DurationInMinutes = viewModel.Duration
            };

            var users = checkOverlapedBooking(timeItem, lesson);
            if(users.Count()>0)
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
                        timeExpansion.InsertOnSubmit(new LessonTimeExpansion {
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
                .Select(t=>t.ClassDate).ToList()
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
            foreach(int idx in  Enumerable.Range(8, 15))
            {
                index[idx] = 0;
            }

            var items = models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate == lessonDate)
                .Select(t => t.Hour).ToList()
                .GroupBy(t => t);

            foreach(var item in items)
            {
                index[item.Key] = item.Count();
            }

            return Json(index.Select(i => new object[] { i.Key , i.Value }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingMembers(DateTime lessonDate,int hour)
        {
            var items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == lessonDate
                    && t.Hour == hour)
                .GroupBy(l => l.LessonID).Select(g => g.First());
            return View(items);
        }

    }
}