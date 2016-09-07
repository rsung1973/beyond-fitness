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
using WebHome.Models.Timeline;

namespace WebHome.Controllers
{
    [Authorize]
    public class ActivityController : SampleController<UserProfile>
    {
        // GET: Activity
        public ActionResult TimeLine(int uid)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u=>u.UID==uid).FirstOrDefault();
            List<TimelineEvent> items = new List<TimelineEvent>();
            if (profile != null)
            {
                contructTimeline(profile, items);
            }

            return View(items);
        }

        private void contructTimeline(UserProfile profile, List<TimelineEvent> items)
        {
            ///1. fetch all reserved lessons
            ///
            var lessons = models.GetTable<LessonTime>().Where(t => t.LessonAttendance == null)
                    .Where(t => t.RegisterLesson.UID == profile.UID);
            items.AddRange(lessons.Select(t => new LessonEvent
            {
                EventTime = t.ClassTime.Value,
                Lesson = t
            }));

            ///2. fetch top 5 attended lessons
            ///
            lessons = models.GetTable<LessonTime>().Where(t => t.LessonAttendance != null)
                    .Where(t => t.RegisterLesson.UID == profile.UID)
                    .OrderByDescending(t => t.LessonID).Take(5);
            items.AddRange(lessons.Select(t => new LessonEvent
            {
                EventTime = t.ClassTime.Value,
                Lesson = t
            }));

            ///3. add monthly review
            ///
            items.Add(new LearnerMonthlyReviewEvent
            {
                EventTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            });

            ///4. add birthday
            ///
            if (profile.Birthday.HasValue)
            {
                DateTime birth = new DateTime(DateTime.Today.Year, profile.Birthday.Value.Month, 1);
                DateTime birthDate = birth.AddDays(profile.Birthday.Value.Day - 1);

                if (birth.Month == birthDate.Month)
                {
                    items.Add(new BirthdayEvent
                    {
                        EventTime = birthDate
                    });
                }
            }
        }

        public ActionResult ListReservedLessons(int uid)
        {

            return View();
        }
    }
}