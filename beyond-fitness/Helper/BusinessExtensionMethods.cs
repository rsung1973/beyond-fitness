using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using Utility;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static class BusinessExtensionMethods
    {
        public static void AttendLesson<TEntity>(this ModelSource<TEntity> models, LessonTime item)
                    where TEntity : class, new()
        {
            if (!item.ContractTrustTrack.Any(t => t.SettlementID.HasValue))
            {
                LessonAttendance attendance = item.LessonAttendance;
                if (attendance == null)
                    attendance = item.LessonAttendance = new LessonAttendance { };
                attendance.CompleteDate = DateTime.Now;

                models.SubmitChanges();
            }

            var lesson = item.RegisterLesson;
            if (lesson.Lessons - (lesson.AttendedLessons ?? 0) <= lesson.GroupingLesson.LessonTime.Count(t => t.LessonAttendance != null))
            {
                foreach (var r in lesson.GroupingLesson.RegisterLesson)
                {
                    r.Attended = (int)Naming.LessonStatus.課程結束;
                }
                models.SubmitChanges();
            }
        }

        public static void CheckLearnerQuestionnaireRequest<TEntity>(this ModelSource<TEntity> models, RegisterLesson item)
            where TEntity : class, new()
        {
            if (item.LessonPriceType.ExcludeQuestionnaire.HasValue)
                return;

            if (item.Lessons <= 10)
                return;

            int countBase;
            //if (item.Lessons <= 10)
            //{
            //    countBase = item.Lessons;
            //}
            //else 
            if (item.Lessons <= 51)
            {
                countBase = item.Lessons / 2;
            }
            else
            {
                countBase = item.Lessons / 3;
            }

            int totalAttendance =
                models.GetTable<LessonTime>().Where(l => l.GroupID == item.RegisterGroupID && l.LessonAttendance != null).Count()
                    + (item.AttendedLessons ?? 0);
            int checkAttendance = totalAttendance;
            bool underCount = true;
            if (item.QuestionnaireRequest.Count > 0)
            {
                var questItem = item.QuestionnaireRequest.OrderByDescending(q => q.QuestionnaireID).First();
                checkAttendance = models.GetTable<LessonTime>().Where(l => l.GroupID == item.RegisterGroupID
                    && l.LessonAttendance != null && l.LessonAttendance.CompleteDate > questItem.RequestDate).Count();
                underCount = totalAttendance < countBase * (item.QuestionnaireRequest.Count + 1);
            }

            if (((item.Lessons - totalAttendance >= countBase && checkAttendance >= countBase) && underCount) || (totalAttendance + 1) == item.Lessons)
            {
                var group = models.GetTable<QuestionnaireGroup>().OrderByDescending(q => q.GroupID).FirstOrDefault();
                if (group != null && !item.QuestionnaireRequest.Any(q => q.PDQTask.Count == 0))
                {
                    models.GetTable<QuestionnaireRequest>().InsertOnSubmit(new QuestionnaireRequest
                    {
                        GroupID = group.GroupID,
                        RegisterID = item.RegisterID,
                        RequestDate = DateTime.Now,
                        UID = item.UID
                    });
                    models.SubmitChanges();
                }
            }
        }

        public static bool CouldMarkToAttendLesson<TEntity>(this ModelSource<TEntity> models, LessonTime item)
            where TEntity : class, new()
        {
            if (models.IsAttendanceOverdue(item))
                return false;

            return !item.LessonFitnessAssessment.Any(f => f.LessonFitnessAssessmentReport.Count(r => r.FitnessAssessmentItem.ItemID == 16) == 0
                    || f.LessonFitnessAssessmentReport.Count(r => r.FitnessAssessmentItem.ItemID == 17) == 0
                    || f.LessonFitnessAssessmentReport.Count(r => r.FitnessAssessmentItem.GroupID == 3) == 0);
        }

        public static bool IsAttendanceOverdue<TEntity>(this ModelSource<TEntity> models, LessonTime item)
            where TEntity : class, new()
        {
            var due = models.GetTable<LessonAttendanceDueDate>().OrderByDescending(d => d.DueDate).FirstOrDefault();
            return due != null && item.ClassTime < due.DueDate;
        }

        public static DailyBookingQueryViewModel InitializeBookingQuery(this HttpContextBase context, string userName, int? branchID, UserProfile item)
        {
            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)context.GetCacheValue(CachingKey.DailyBookingQuery);
            if (viewModel == null)
            {
                viewModel = new DailyBookingQueryViewModel
                {
                    //DateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
                    UserName = userName,
                    MonthInterval = 1,
                    BranchID = branchID,
                    CoachID = item.UID,
                    HasQuery = false
                };
                //viewModel.DateTo = viewModel.DateFrom.Value.AddMonths(1);
                context.SetCacheValue(CachingKey.DailyBookingQuery, viewModel);
            }


            return viewModel;
        }

        public static IEnumerable<CalendarEvent> ToFullCalendarEventMonthView(this IEnumerable<LessonTime> dataItems)
        {
            var today = DateTime.Today;

            IEnumerable<CalendarEvent> items;

            items = dataItems.Where(l => l.RegisterLesson.RegisterLessonEnterprise != null)
                    .GroupBy(t => t.ClassTime.Value.Date)
                    .Select(g => new CalendarEvent
                    {
                        id = "course",
                        title = g.Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "企業方案",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-yellow" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-clock-o"
                    });

            dataItems = dataItems.Where(l => l.RegisterLesson.RegisterLessonEnterprise == null);

            items = items.Concat(dataItems
                .Where(t => !t.TrainingBySelf.HasValue || t.TrainingBySelf == 0)
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.正常
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.已刪除
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.點數兌換課程)
                .GroupBy(t => t.ClassTime.Value.Date)
                .Select(g => new CalendarEvent
                {
                    id = "course",
                    title = g.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "P.T session",
                    allDay = true,
                    className = g.Key < today ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-blue" },
                    icon = /*g.Key < today ? "fa-check" :*/ "fa-clock-o"
                }));

            items = items.Concat(dataItems
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練)
                .GroupBy(t => t.ClassTime.Value.Date)
                .Select(g => new CalendarEvent
                {
                    id = "coach",
                    title = g.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "內部訓練",
                    allDay = true,
                    className = g.Key < today ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-teal" },
                    icon = /*g.Key < today ? "fa-check" :*/ "fa-university"
                }));

            items = items.Concat(dataItems
                .Where(t => t.TrainingBySelf == 1)
                .GroupBy(t => t.ClassTime.Value.Date)
                .Select(g => new CalendarEvent
                {
                    id = "self",
                    title = g.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "P.I session",
                    allDay = true,
                    className = g.Key < today ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-red" },
                    icon = "fa-child" // g.Key < today ? "fa-ckeck" : "fa-clock-o"
                }));

            items = items.Concat(dataItems
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程)
                .GroupBy(t => t.ClassTime.Value.Date)
                .Select(g => new CalendarEvent
                {
                    id = "trial",
                    title = g.Count().ToString(),
                    start = g.Key.ToString("yyyy-MM-dd"),
                    description = "體驗課程",
                    allDay = true,
                    className = g.Key < today ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-pink" },
                    icon = "fa-magic" // g.Key < today ? "fa-ckeck" : "fa-clock-o"
                }));

            return items;
        }

        public static IEnumerable<CalendarEvent> ToFullCalendarCustomizedEventMonthView(this IEnumerable<UserEvent> dataItems)
        {
            var today = DateTime.Today;

            IEnumerable<CalendarEvent> items;
            items = dataItems
                    .Select(g => new CalendarEvent
                    {
                        id = "my" + g.EventID,
                        title = g.Title ?? g.ActivityProgram,
                        start = g.StartDate.ToString("yyyy-MM-dd"),
                        end = g.EndDate.ToString("yyyy-MM-dd"),
                        description = g.Title == null ? "" : g.ActivityProgram,
                        lessonID = g.EventID,
                        allDay = true,
                        className = new string[] { "event", "bg-color-greenLight" },  //g.StartDate < today ? g.EndDate < today ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-pink" },
                        icon = ""   //"fa-magic"
                    });

            return items;
        }

        public static IEnumerable<CalendarEvent> ToFullCalendarEventWeekView(this IQueryable<LessonTime> sourceItems)
        {
            var today = DateTime.Today;

            IEnumerable<CalendarEvent> items;

            IQueryable<LessonTime> dataItems = sourceItems.Where(l => l.RegisterLesson.RegisterLessonEnterprise!=null);
            items = dataItems
                    .Select(g => new CalendarEvent
                    {
                        id = g.LessonID.ToString(),
                        lessonID = g.LessonID,
                        title = String.Join("、", g.GroupingLesson.RegisterLesson.Select(r => r.UserProfile.RealName)),
                        start = String.Format("{0:O}", g.ClassTime),
                        end = String.Format("{0:O}", g.ClassTime.Value.AddMinutes(g.DurationInMinutes.Value)),
                        //description = "自由教練",
                        allDay = false,
                        className = g.LessonAttendance==null ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-grayDark" },
                        editable = g.LessonAttendance == null,
                    });

            dataItems = sourceItems.Where(l => l.RegisterLesson.RegisterLessonEnterprise == null);
            items = items.Concat(dataItems
                .Where(t => !t.TrainingBySelf.HasValue || t.TrainingBySelf == 0)
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.正常
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.已刪除
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.點數兌換課程)
                .Select(g => new CalendarEvent
                {
                    id = g.LessonID.ToString(),
                    lessonID = g.LessonID,
                    title = String.Join("、", g.GroupingLesson.RegisterLesson.Select(r => r.UserProfile.RealName)),
                    start = String.Format("{0:O}", g.ClassTime),
                    end = String.Format("{0:O}", g.ClassTime.Value.AddMinutes(g.DurationInMinutes.Value)),
                    //description = "P.T session",
                    allDay = false,
                    className = g.LessonAttendance == null ? new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-grayDark" },
                    editable = g.LessonAttendance == null,
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null
                }));

            items = items.Concat(dataItems
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練)
                .Select(g => new CalendarEvent
                {
                    id = g.LessonID.ToString(),
                    lessonID = g.LessonID,
                    title = g.AsAttendingCoach.UserProfile.RealName,
                    start = String.Format("{0:O}", g.ClassTime),
                    end = String.Format("{0:O}", g.ClassTime.Value.AddMinutes(g.DurationInMinutes.Value)),
                    //description = "內部訓練",
                    allDay = false,
                    className = g.LessonAttendance == null ? new string[] { "event", "bg-color-teal" } : new string[] { "event", "bg-color-grayDark" },
                    editable = g.LessonAttendance == null,
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null
                }));

            items = items.Concat(dataItems
                .Where(t => t.TrainingBySelf == 1)
                .Select(g => new CalendarEvent
                {
                    id = g.LessonID.ToString(),
                    lessonID = g.LessonID,
                    title = g.RegisterLesson.UserProfile.RealName,
                    start = String.Format("{0:O}", g.ClassTime),
                    end = String.Format("{0:O}", g.ClassTime.Value.AddMinutes(g.DurationInMinutes.Value)),
                    //description = "自主訓練",
                    allDay = false,
                    className = g.LessonAttendance == null ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-grayDark" },
                    editable = g.LessonAttendance == null,
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null
                }));

            items = items.Concat(dataItems
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程)
                .Select(g => new CalendarEvent
                {
                    id = g.LessonID.ToString(),
                    lessonID = g.LessonID,
                    title = g.RegisterLesson.UserProfile.RealName,
                    start = String.Format("{0:O}", g.ClassTime),
                    end = String.Format("{0:O}", g.ClassTime.Value.AddMinutes(g.DurationInMinutes.Value)),
                    //description = "體驗課程",
                    allDay = false,
                    className = g.LessonAttendance == null ? new string[] { "event", "bg-color-pink" } : new string[] { "event", "bg-color-grayDark" },
                    editable = g.LessonAttendance == null,
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null
                }));

            //items = items.Concat(dataItems
            //    .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.正常
            //        || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.已刪除
            //|| t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.點數兌換課程)
            //    .GroupBy(t => t.ClassTime.Value.Date)
            //    .Select(g => new CalendarEvent
            //    {
            //        id = "all",
            //        title = g.Sum(v => v.RegisterLesson.GroupingMemberCount).ToString(),
            //        start = g.Key.ToString("yyyy-MM-dd"),
            //        description = "P.T session",
            //        allDay = true,
            //        className = new string[] { "event", "bg-color-blue" },
            //        icon = /*g.Key < today ? "fa-check" :*/ "fa-clock-o"
            //    }));
            items = items.Concat(dataItems.ToFullCalendarEventMonthView());

            return items;
        }

        public static IEnumerable<CalendarEvent> ToFullCalendarCustomizedEventWeekView(this IEnumerable<UserEvent> dataItems)
        {
            var today = DateTime.Today;

            IEnumerable<CalendarEvent> items;

            items = dataItems
                    .Select(g => new CalendarEvent
                    {
                        id = "my" + g.EventID,
                        title = g.Title ?? g.ActivityProgram,
                        start = String.Format("{0:O}", g.StartDate),
                        end = String.Format("{0:O}", g.EndDate),
                        description = g.Title == null ? "" : g.ActivityProgram,
                        lessonID = g.EventID,
                        allDay = false,
                        editable = true,
                        className = new string[] { "event", "bg-color-greenLight" },  //g.StartDate < today ? g.EndDate < today ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-pink" },
                        icon = ""   //"fa-magic"
                    });

            return items;
        }


        public static IEnumerable<UserProfile> CheckOverlappingBooking<TEntity>(this ModelSource<TEntity> models, LessonTime intendedBooking, LessonTime originalBooking)
                    where TEntity : class, new()
        {
            int durationHours = (intendedBooking.ClassTime.Value.Minute + intendedBooking.DurationInMinutes.Value + 59) / 60;

            var oriUID = originalBooking.GroupingLesson.RegisterLesson.Select(r => r.UID).ToArray();

            var overlappingItems = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == intendedBooking.ClassTime.Value.Date
                    && t.Hour >= intendedBooking.ClassTime.Value.Hour
                    && t.Hour < (intendedBooking.ClassTime.Value.Hour + durationHours)
                    && t.LessonID != originalBooking.LessonID)
                .Select(r => r.RegisterLesson.UserProfile).Where(u => oriUID.Contains(u.UID));

            return overlappingItems;

        }

        public static UserProfile CreateLearner<TEntity>(this ModelSource<TEntity> models, LearnerViewModel viewModel, Naming.RoleID role = Naming.RoleID.Learner)
                    where TEntity : class, new()
        {
            String memberCode;

            while (true)
            {
                memberCode = createMemberCode();
                if (!models.GetTable<UserProfile>().Any(u => u.MemberCode == memberCode))
                {
                    break;
                }
            }

            UserProfile item = new UserProfile
            {
                PID = memberCode,
                MemberCode = memberCode,
                LevelID = (int)Naming.MemberStatusDefinition.ReadyToRegister,
                RealName = viewModel.RealName,
                Phone = viewModel.Phone,
                Birthday = viewModel.Birthday,
                UserProfileExtension = new UserProfileExtension
                {
                    Gender = viewModel.Gender,
                    AthleticLevel = viewModel.AthleticLevel,
                    CurrentTrial = viewModel.CurrentTrial
                }
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = (int)role
            });


            models.GetTable<UserProfile>().InsertOnSubmit(item);
            models.SubmitChanges();

            return item;
        }

        private static String createMemberCode()
        {
            Thread.Sleep(1);
            Random rnd = new Random();

            return (new StringBuilder()).Append((char)((int)'A' + rnd.Next(26)))
                .Append((char)((int)'A' + rnd.Next(26)))
                .Append(rnd.Next(100000000))
                .ToString();
        }

        public static IQueryable<QuestionnaireRequest> GetQuestionnaireRequest<TEntity>(this ModelSource<TEntity> models, UserProfile profile)
            where TEntity : class, new()
        {
            return models.GetTable<QuestionnaireRequest>().Where(q => q.UID == profile.UID
                && q.PDQTask.Count == 0 && !q.Status.HasValue);
        }

        public static int? BonusPoint<TEntity>(this UserProfile item, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<PDQTaskBonus>()
                .Where(t => t.PDQTask.UID == item.UID)
                .Where(t => !t.BonusExchange.Any())
                .Sum(x => x.PDQTask.PDQQuestion.PDQQuestionExtension.BonusPoint);
        }

        public static IEnumerable<PDQTaskBonus> BonusPointList<TEntity>(this UserProfile item, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<PDQTaskBonus>()
                .Where(t => t.PDQTask.UID == item.UID)
                .Where(t => !t.BonusExchange.Any());
        }

        public static IQueryable<LessonTime> GetLessonAttendance<TEntity>(this ModelSource<TEntity> models, int? coachID, DateTime? dateFrom, ref DateTime? dateTo, int? month, int? branchID)
            where TEntity : class, new()
        {
            DateTime? queryDateTo = dateTo;

            var items = models.GetTable<LessonTime>()
                //.Where(t => t.LessonAttendance != null)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自由教練預約)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.內部訓練)
                //.Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.體驗課程)
                //.Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.點數兌換課程)
                .Where(t => t.LessonAttendance != null || t.LessonPlan.CommitAttendance.HasValue);

            if (coachID.HasValue)
            {
                items = items.Where(t => t.AttendingCoach == coachID);
            }

            if (dateFrom.HasValue)
            {
                items = items.Where(t => t.ClassTime >= dateFrom);
            }

            if (queryDateTo.HasValue)
            {
                items = items.Where(t => t.ClassTime < queryDateTo.Value.AddDays(1));
            }
            else if (month.HasValue)
            {
                queryDateTo = dateFrom.Value.AddMonths(month.Value);
                items = items.Where(t => t.ClassTime < queryDateTo);
                queryDateTo = queryDateTo.Value.AddDays(-1);
                dateTo = queryDateTo;
            }

            if (branchID.HasValue)
            {
                items = items.Where(t => t.BranchID == branchID);
            }

            return items;
        }

        public static int CalcAchievement<TEntity>(this ModelSource<TEntity> models, IEnumerable<LessonTime> items)
            where TEntity : class, new()
        {
            var lessons = items.Where(t => t.LessonAttendance != null && t.LessonPlan.CommitAttendance.HasValue)
                .Select(l => l.GroupingLesson)
                        .Join(models.GetTable<RegisterLesson>(), g => g.GroupID, r => r.RegisterGroupID, (g, r) => r)
                        .Where(r => r.IntuitionCharge != null);

            //Utility.Logger.Debug(
            //String.Join("\r\n", lessons.Select(r => r.RegisterID + "\t"
            //    + r.IntuitionCharge.Payment + "\t"
            //    + r.IntuitionCharge.FeeShared + "\t"
            //    + r.LessonPriceType.CoachPayoff + "\t"
            //    + r.LessonPriceType.CoachPayoffCreditCard + "\t"
            //    + r.GroupingLessonDiscount.PercentageOfDiscount + "\t"
            //    )));


            var fullAchievement = lessons.Where(r => r.IntuitionCharge.Payment == "Cash" || r.IntuitionCharge.FeeShared == 0).Sum(l => l.LessonPriceType.CoachPayoff * l.GroupingLessonDiscount.PercentageOfDiscount / 100)
                + lessons.Where(r => r.IntuitionCharge.Payment == "CreditCard" && r.IntuitionCharge.FeeShared == 1).Sum(l => l.LessonPriceType.CoachPayoffCreditCard * l.GroupingLessonDiscount.PercentageOfDiscount / 100);

            //if (items.First().AttendingCoach == 2089)
            //{
            //    Utility.Logger.Debug(items.First().AttendingCoach + "=>" + lessons.Sum(l => l.LessonPriceType.ListPrice * l.GroupingLessonDiscount.PercentageOfDiscount / 100) + ":" + fullAchievement);
            //    foreach(var t in lessons.Where(r => r.IntuitionCharge.Payment == "Cash" || r.IntuitionCharge.FeeShared == 0))
            //    {
            //        Utility.Logger.Debug(t.LessonPriceType.PriceID + ":" + t.LessonPriceType.ListPrice + ":CoachPayoff:" + t.LessonPriceType.CoachPayoff);
            //    }
            //    foreach (var t in lessons.Where(r => r.IntuitionCharge.Payment == "CreditCard" && r.IntuitionCharge.FeeShared == 1))
            //    {
            //        Utility.Logger.Debug(t.LessonPriceType.PriceID + ":" + t.LessonPriceType.ListPrice + ":CoachPayoffCreditCard:" + t.LessonPriceType.CoachPayoffCreditCard);
            //    }

            //}

            lessons = items.Where(t => t.LessonAttendance == null || !t.LessonPlan.CommitAttendance.HasValue)
                .Select(l => l.GroupingLesson)
                        .Join(models.GetTable<RegisterLesson>(), g => g.GroupID, r => r.RegisterGroupID, (g, r) => r)
                        .Where(r => r.IntuitionCharge != null);

            var halfAchievement = lessons.Where(r => r.IntuitionCharge.Payment == "Cash" || r.IntuitionCharge.FeeShared == 0).Sum(l => l.LessonPriceType.CoachPayoff * l.GroupingLessonDiscount.PercentageOfDiscount / 100) / 2
                + lessons.Where(r => r.IntuitionCharge.Payment == "CreditCard" && r.IntuitionCharge.FeeShared == 1).Sum(l => l.LessonPriceType.CoachPayoffCreditCard * l.GroupingLessonDiscount.PercentageOfDiscount / 100) / 2;

            return (fullAchievement ?? 0) + (halfAchievement ?? 0);
        }

        public static int CalcAchievement<TEntity>(this ModelSource<TEntity> models, IEnumerable<LessonTime> items, out int shares)
            where TEntity : class, new()
        {
            shares = 0;
            var lessons = items.Where(t => t.LessonAttendance != null && t.LessonPlan.CommitAttendance.HasValue)
                .Select(l => l.GroupingLesson)
                        .Join(models.GetTable<RegisterLesson>(), g => g.GroupID, r => r.RegisterGroupID, (g, r) => r);

            var fullAchievement = lessons
                .Sum(l => l.LessonPriceType.CoachPayoffCreditCard
                    * l.GroupingLessonDiscount.PercentageOfDiscount / 100);

            lessons = items.Where(t => t.LessonAttendance == null || !t.LessonPlan.CommitAttendance.HasValue)
                .Select(l => l.GroupingLesson)
                        .Join(models.GetTable<RegisterLesson>(), g => g.GroupID, r => r.RegisterGroupID, (g, r) => r);

            var halfAchievement = lessons
                .Sum(l => l.LessonPriceType.CoachPayoffCreditCard
                    * l.GroupingLessonDiscount.PercentageOfDiscount / 100) / 2;

            shares = ((int?)items.Where(t => t.LessonAttendance != null && t.LessonPlan.CommitAttendance.HasValue)
                .Sum(l => l.RegisterLesson.LessonPriceType.CoachPayoffCreditCard
                    * l.RegisterLesson.GroupingMemberCount * l.RegisterLesson.GroupingLessonDiscount.PercentageOfDiscount / 100
                    * l.LessonTimeSettlement.ProfessionalLevel.GradeIndex / 100) ?? 0)
                + ((int?)items.Where(t => t.LessonAttendance == null || !t.LessonPlan.CommitAttendance.HasValue)
                .Sum(l => l.RegisterLesson.LessonPriceType.CoachPayoffCreditCard
                    * l.RegisterLesson.GroupingMemberCount * l.RegisterLesson.GroupingLessonDiscount.PercentageOfDiscount / 100
                    * l.LessonTimeSettlement.ProfessionalLevel.GradeIndex / 100) / 2 ?? 0);

            return (fullAchievement ?? 0) + (halfAchievement ?? 0);
        }


        public static IQueryable<TuitionAchievement> GetTuitionAchievement<TEntity>(this ModelSource<TEntity> models, int? coachID, DateTime? dateFrom, ref DateTime? dateTo, int? month)
            where TEntity : class, new()
        {
            IQueryable<TuitionInstallment> installment = models.GetTable<TuitionInstallment>();
            IQueryable<TuitionAchievement> items;

            DateTime? queryDateTo = dateTo;

            if (dateFrom.HasValue)
            {
                installment = installment.Where(i => i.PayoffDate >= dateFrom);
            }
            if (queryDateTo.HasValue)
            {
                installment = installment.Where(i => i.PayoffDate < queryDateTo.Value.AddDays(1));
            }
            else if (month.HasValue)
            {
                queryDateTo = dateFrom.Value.AddMonths(month.Value);
                installment = installment.Where(i => i.PayoffDate < queryDateTo);
                queryDateTo = queryDateTo.Value.AddDays(-1);
                dateTo = queryDateTo;
            }

            if (coachID.HasValue)
            {
                items = installment.Join(models.GetTable<TuitionAchievement>().Where(c => c.CoachID == coachID),
                    t => t.InstallmentID, i => i.InstallmentID, (t, i) => i);
            }
            else
            {
                items = installment.Join(models.GetTable<TuitionAchievement>(),
                    t => t.InstallmentID, i => i.InstallmentID, (t, i) => i);
            }

            return items;
        }

        public static void CheckProfessionalLeve<TEntity>(this ModelSource<TEntity> models, ServingCoach item)
            where TEntity : class, new()
        {
            if (item.LevelID == (int)Naming.ProfessionLevelDefinition.AFM_1st
                || item.LevelID == (int)Naming.ProfessionLevelDefinition.AFM_2nd
                || item.LevelID == (int)Naming.ProfessionLevelDefinition.FM_1st
                || item.LevelID == (int)Naming.ProfessionLevelDefinition.FM_2nd)
                return;

            DateTime? quarterEnd = new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1);
            DateTime quarterStart = quarterEnd.Value.AddMonths(-3);

            var attendanceCount = models.GetLessonAttendance(item.CoachID, quarterStart, ref quarterEnd, null, null).Count();
            var tuition = models.GetTuitionAchievement(item.CoachID, quarterStart, ref quarterEnd, null);
            var summary = tuition.Sum(t => t.ShareAmount) ?? 0;
            bool qualifiedCert = item.CoachCertificate.Count(c => c.Expiration >= quarterStart) >= 2;

            CoachRating ratingItem = new CoachRating
            {
                AttendanceCount = attendanceCount,
                CoachID = item.CoachID,
                RatingDate = DateTime.Now,
                TuitionSummary = summary
            };
            item.CoachRating.Add(ratingItem);

            if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_5_2nd)
            {
                if (!qualifiedCert || attendanceCount < 270 || summary < 390000)
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_2nd;
                }
                else
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_5_2nd;
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_5_1st)
            {
                if (!qualifiedCert || attendanceCount < 270 || summary < 390000)
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_1st;
                }
                else
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_5_1st;
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_4_2nd)
            {
                if (attendanceCount >= 300 && summary >= 500000)
                {
                    if (qualifiedCert)
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_5_2nd;
                    else
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_2nd;
                }
                else if (!qualifiedCert || !(attendanceCount >= 240 && summary >= 300000))
                {
                    item.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_2nd;
                }
                else
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_2nd;
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_4_1st)
            {
                if (attendanceCount >= 300 && summary >= 500000)
                {
                    if (qualifiedCert)
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_5_1st;
                    else
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_1st;
                }
                else if (!qualifiedCert || !(attendanceCount >= 240 && summary >= 300000))
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_1st;
                }
                else
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_1st;
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_3_2nd)
            {
                if (attendanceCount >= 240 && summary >= 350000)
                {
                    if (qualifiedCert)
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_2nd;
                    else
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_2nd;
                }
                else if (qualifiedCert)
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_2nd;
                }
                else
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_2_2nd;
                }
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_3_1st)
            {
                if (attendanceCount >= 240 && summary >= 350000)
                {
                    if (qualifiedCert)
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_4_1st;
                    else
                        ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_1st;
                }
                else if (qualifiedCert)
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_1st;
                }
                else
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_2_1st;
                }
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_2_2nd)
            {
                //27.5 % LEVEL3(考取兩張國際證照Beyond認可未過期)
                if (qualifiedCert)
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_2nd;
                }
                else
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_2_2nd;
                }
            }
            else if (item.LevelID == (int)Naming.ProfessionLevelDefinition.Level_2_1st)
            {
                if (qualifiedCert)
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_3_1st;
                }
                else
                {
                    ratingItem.LevelID = (int)Naming.ProfessionLevelDefinition.Level_2_1st;
                }
            }
            else
            {
                ratingItem.LevelID = item.LevelID.Value;
            }

            item.LevelID = ratingItem.LevelID;
            models.SubmitChanges();
            //工作滿1.5 年上課抽成 + 1 % .

        }

        public static IQueryable<CourseContract> GetContractInEditingByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            if (agent.IsAssistant())
            {
                return models.GetTable<CourseContract>()
                    .Where(c => c.CourseContractRevision == null)
                    .Where(c => c.Status == (int)Naming.CourseContractStatus.草稿);
            }
            else
            {
                return models.GetTable<CourseContract>()
                    .Where(c => c.CourseContractRevision == null)
                    .Where(c => (c.AgentID == agent.UID || c.FitnessConsultant == agent.UID) && c.Status == (int)Naming.CourseContractStatus.草稿);
            }
        }

        public static UserProfile LoadInstance<TEntity>(this UserProfile profile, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<UserProfile>().Where(u => u.UID == profile.UID).First();
        }

        public static IQueryable<CourseContract> GetApplyingContractByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContract>()
                .Where(c => c.CourseContractRevision == null)
                .Where(c => c.RegisterLessonContract.Count == 0);
            items = models.filterContractByAgent(agent, items);
            return items;
        }

        public static IQueryable<CourseContractRevision> GetApplyingAmendmentByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContractRevision>()
                .Where(c => c.CourseContract.Status < (int)Naming.CourseContractStatus.已生效);
            items = models.filterAmendmentByAgent(agent, items);
            return items;
        }

        public static IQueryable<CourseContract> FilterByBranchStoreManager<TEntity>(this ModelSource<TEntity> models, IQueryable<CourseContract> items, UserProfile agent)
            where TEntity : class, new()
        {
            return items.Join(models.GetTable<CourseContractExtension>()
                    .Join(models.GetTable<BranchStore>()
                            .Where(b => b.ManagerID == agent.UID || b.ViceManagerID == agent.UID),
                        p => p.BranchID, b => b.BranchID, (p, b) => p),
                c => c.ContractID, p => p.ContractID, (c, p) => c);
        }

        public static IQueryable<CourseContract> GetContractToAllowByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContract>()
                .Where(c => c.CourseContractRevision == null)
                .Where(c => c.Status == (int)Naming.CourseContractStatus.待確認);
            if (agent.IsManager() || agent.IsViceManager())
            {
                items = models.FilterByBranchStoreManager(items, agent);
            }
            else if (agent.IsAssistant())
            {

            }
            else
            {
                items = items.Where(c => false);
            }
            return items;
        }

        public static IQueryable<CourseContractRevision> GetAmendmentToAllowByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContractRevision>()
                .Where(c => c.CourseContract.Status == (int)Naming.CourseContractStatus.待確認);
            if (agent.IsManager() || agent.IsViceManager())
            {
                items = items.Join(models.FilterByBranchStoreManager(models.GetTable<CourseContract>(), agent),
                    r => r.OriginalContract, c => c.ContractID, (r, c) => r);
            }
            else if (agent.IsAssistant())
            {

            }
            else
            {
                items = items.Where(c => false);
            }
            return items;
        }

        public static IQueryable<CourseContract> GetContractToSignByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContract>()
                .Where(c => c.CourseContractRevision == null)
                .Where(c => c.Status == (int)Naming.CourseContractStatus.待簽名);
            items = models.filterContractByAgent(agent, items);
            return items;
        }

        public static IQueryable<CourseContractRevision> GetAmendmentToSignByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContractRevision>()
                .Where(c => c.CourseContract.Status == (int)Naming.CourseContractStatus.待簽名);
            items = models.filterAmendmentByAgent(agent, items);
            return items;
        }

        private static IQueryable<CourseContract> filterContractByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent, IQueryable<CourseContract> items)
                        where TEntity : class, new()
        {
            if (agent.IsManager() || agent.IsViceManager())
            {
                items = models.FilterByBranchStoreManager(items, agent);
            }
            else if (agent.IsAssistant())
            {

            }
            else if (agent.IsCoach())
            {
                items = items.Where(c => c.FitnessConsultant == agent.UID);
            }
            else
            {
                items = items.Where(c => false);
            }

            return items;
        }

        private static IQueryable<CourseContractRevision> filterAmendmentByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent, IQueryable<CourseContractRevision> items)
                        where TEntity : class, new()
        {
            if (agent.IsManager() || agent.IsViceManager())
            {
                items = items.Join(models.FilterByBranchStoreManager(models.GetTable<CourseContract>(), agent),
                    r => r.OriginalContract, c => c.ContractID, (r, c) => r);
            }
            else if (agent.IsAssistant())
            {

            }
            else if (agent.IsCoach())
            {
                items = items.Where(c => c.CourseContract.FitnessConsultant == agent.UID);
            }
            else
            {
                items = items.Where(c => false);
            }

            return items;
        }

        public static IQueryable<CourseContract> GetContractToConfirmByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContract>()
                .Where(c => c.CourseContractRevision == null)
                .Where(c => c.Status == (int)Naming.CourseContractStatus.待審核);
            if (agent.IsManager())
            {
                items = models.FilterByBranchStoreManager(items, agent);
            }
            else if (agent.IsViceManager())
            {
                items = models.FilterByBranchStoreManager(items, agent)
                    .Where(c => c.AgentID != agent.UID);
            }
            else if (agent.IsAssistant())
            {

            }
            else
            {
                items = items.Where(c => false);
            }
            return items;
        }

        public static IQueryable<CourseContractRevision> GetAmendmentToConfirmByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile agent)
            where TEntity : class, new()
        {
            var items = models.GetTable<CourseContractRevision>()
                .Where(c => c.CourseContract.Status == (int)Naming.CourseContractStatus.待審核);
            if (agent.IsManager())
            {
                items = items.Join(models.FilterByBranchStoreManager(models.GetTable<CourseContract>(), agent),
                    r => r.OriginalContract, c => c.ContractID, (r, c) => r);
            }
            else if (agent.IsViceManager())
            {
                items = items.Join(models.FilterByBranchStoreManager(models.GetTable<CourseContract>(), agent)
                        .Where(c => c.AgentID != agent.UID),
                    r => r.OriginalContract, c => c.ContractID, (r, c) => r);
            }
            else if (agent.IsAssistant())
            {

            }
            else
            {
                items = items.Where(c => false);
            }
            return items;
        }

        public static String CreateContractPDF(this CourseContract item, bool createNew = false)
        {
            String pdfFile = Path.Combine(GlobalDefinition.ContractPdfPath, item.ContractNo + ".pdf");
            if (createNew == true || !File.Exists(pdfFile))
            {
                String viewUrl = Settings.Default.HostDomain + VirtualPathUtility.ToAbsolute("~/CourseContract/ViewContract") + "?pdf=1&contractID=" + item.ContractID;
                viewUrl.ConvertHtmlToPDF(pdfFile, 20);
            }
            return pdfFile;
        }

        public static String CreateContractAmendmentPDF(this CourseContractRevision item, bool createNew = false)
        {
            String pdfFile = Path.Combine(GlobalDefinition.ContractPdfPath, item.CourseContract.ContractNo + ".pdf");
            if (createNew == true || !File.Exists(pdfFile))
            {
                String viewUrl = Settings.Default.HostDomain + VirtualPathUtility.ToAbsolute("~/CourseContract/ViewContractAmendment") + "?pdf=1&revisionID=" + item.RevisionID;
                viewUrl.ConvertHtmlToPDF(pdfFile, 20);
            }
            return pdfFile;
        }

        public static String CreateInvoicePDF(this InvoiceItem item, bool createNew = false)
        {
            String storePath = Path.Combine(GlobalDefinition.InvoicePdfPath, item.InvoiceDate.Value.Year.ToString());
            if (!Directory.Exists(storePath))
                Directory.CreateDirectory(storePath);
            String pdfFile = Path.Combine(storePath, item.TrackCode + item.No + ".pdf");
            if (createNew == true || !File.Exists(pdfFile))
            {
                String viewUrl = Settings.Default.HostDomain + VirtualPathUtility.ToAbsolute("~/Invoice/PrintInvoice") + "?invoiceID=" + item.InvoiceID;
                viewUrl.ConvertHtmlToPDF(pdfFile, 20);
            }
            return pdfFile;
        }

        public static String CreateQueuedInvoicePDF(this UserProfile item)
        {
            String storePath = Path.Combine(GlobalDefinition.InvoicePdfPath, DateTime.Today.ToString("yyyy-MM-dd"));
            if (!Directory.Exists(storePath))
                Directory.CreateDirectory(storePath);
            String pdfFile = Path.Combine(storePath, item.UID + "-" + DateTime.Now.Ticks + ".pdf");
            if (!File.Exists(pdfFile))
            {
                String viewUrl = Settings.Default.HostDomain + VirtualPathUtility.ToAbsolute("~/Invoice/PrintInvoice") + "?uid=" + item.UID + "&t=" + DateTime.Now.Ticks;
                viewUrl.ConvertHtmlToPDF(pdfFile, 20);
            }
            return pdfFile;
        }

        public static bool CheckLearnerDiscount<TEntity>(this ModelSource<TEntity> models, IEnumerable<int> uid)
            where TEntity : class, new()
        {
            return models.GetTable<CourseContractMember>().Where(m => uid.Contains(m.UID))
                            .Any(m => m.CourseContract.Status == (int)Naming.CourseContractStatus.已生效);
        }

        public static IQueryable<ServingCoach> GetServingCoachInSameStore<TEntity>(this UserProfile profile, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<ServingCoach>()
                .Join(models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID)
                                            .Join(models.GetTable<CoachWorkplace>(),
                                                b => b.BranchID, w => w.BranchID, (b, w) => w),
                                            s => s.CoachID, w => w.CoachID, (s, w) => s);
        }

        public static IQueryable<PaymentAudit> GetPaymentToAuditByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile profile)
            where TEntity : class, new()
        {
            IQueryable<PaymentAudit> items;
            if (profile.IsAssistant())
            {
                items = models.GetTable<PaymentAudit>().Where(p => !p.AuditorID.HasValue);
            }
            else if (profile.IsManager() || profile.IsViceManager())
            {
                var payment = models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID)
                    .SelectMany(b => b.PaymentTransaction)
                    .Select(p => p.Payment);

                items = payment.Select(p => p.PaymentAudit)
                    .Where(a => !a.AuditorID.HasValue);
            }
            else
            {
                items = models.GetTable<PaymentAudit>().Where(f => false);
            }
            return items;
        }

        public static IQueryable<VoidPayment> GetVoidPaymentToApproveByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile profile)
            where TEntity : class, new()
        {
            IQueryable<VoidPayment> items;
            if (profile.IsAssistant())
            {
                items = models.GetTable<VoidPayment>().Where(p => p.Status == (int)Naming.CourseContractStatus.待審核);
            }
            else if (profile.IsManager() || profile.IsViceManager())
            {
                var payment = models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID)
                    .SelectMany(b => b.PaymentTransaction)
                    .Select(p => p.Payment);

                items = payment.Select(p => p.VoidPayment)
                        .Where(p => p.Status == (int)Naming.CourseContractStatus.待審核);
            }
            else
            {
                items = models.GetTable<VoidPayment>().Where(f => false);
            }

            return items;
        }

        public static IQueryable<VoidPayment> GetVoidPaymentToEditByAgent<TEntity>(this ModelSource<TEntity> models, UserProfile profile)
            where TEntity : class, new()
        {
            IQueryable<VoidPayment> items;
            if (profile.IsAssistant())
            {
                items = models.GetTable<VoidPayment>().Where(p => p.Status == (int)Naming.CourseContractStatus.草稿);
            }
            else if (profile.IsManager() || profile.IsViceManager())
            {
                items = models.GetTable<VoidPayment>().Where(p => false);
            }
            else
            {
                items = models.GetTable<VoidPayment>().Where(p => p.Status == (int)Naming.CourseContractStatus.草稿)
                        .Where(p => p.HandlerID == profile.UID);
            }

            return items;
        }

        public static CourseContract InitiateCourseContract<TEntity>(this ModelSource<TEntity> models, CourseContractViewModel viewModel, UserProfile profile, LessonPriceType lessonPrice)
            where TEntity : class, new()
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item == null)
            {
                item = new CourseContract
                {
                    //AgentID = profile.UID,  //lessonPrice.BranchStore.ManagerID.Value,
                    CourseContractExtension = new Models.DataEntity.CourseContractExtension
                    {
                        BranchID = lessonPrice.BranchID.Value
                    }
                };
                models.GetTable<CourseContract>().InsertOnSubmit(item);
            }

            item.AgentID = profile.UID;
            item.Status = (int)Naming.CourseContractStatus.待簽名;  //  (int)checkInitialStatus(viewModel, profile);
            item.CourseContractLevel.Add(new CourseContractLevel
            {
                LevelDate = DateTime.Now,
                ExecutorID = profile.UID,
                LevelID = item.Status
            });

            item.ContractType = viewModel.ContractType.Value;
            item.ContractDate = DateTime.Now;
            item.Subject = viewModel.Subject;
            item.ValidFrom = DateTime.Today;
            item.Expiration = DateTime.Today.AddMonths(18);
            item.OwnerID = viewModel.OwnerID.Value;
            item.SequenceNo = 0;// viewModel.SequenceNo;
            item.Lessons = viewModel.Lessons;
            item.PriceID = viewModel.PriceID.Value;
            item.Remark = viewModel.Remark;
            item.FitnessConsultant = viewModel.FitnessConsultant.Value;
            //item.Status = viewModel.Status;
            if (viewModel.UID != null && viewModel.UID.Length > 0)
            {
                models.DeleteAllOnSubmit<CourseContractMember>(m => m.ContractID == item.ContractID);
                item.CourseContractMember.AddRange(viewModel.UID.Select(u => new CourseContractMember
                {
                    UID = u
                }));
            }
            models.SubmitChanges();

            item.TotalCost = item.Lessons * item.LessonPriceType.ListPrice;
            if (item.CourseContractType.GroupingLessonDiscount != null)
            {
                item.TotalCost = item.TotalCost * item.CourseContractType.GroupingLessonDiscount.GroupingMemberCount * item.CourseContractType.GroupingLessonDiscount.PercentageOfDiscount / 100;
            }
            models.SubmitChanges();

            foreach (var uid in viewModel.UID)
            {
                models.ExecuteCommand("update UserProfileExtension set CurrentTrial = null where UID = {0}", uid);
            }

            return item;
        }

        public static LessonPriceType CurrentTrialLessonPrice<TEntity>(this ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.體驗課程).FirstOrDefault();
        }

        public static LessonPriceType CurrentPISessionPrice<TEntity>(this ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.自主訓練).FirstOrDefault();
        }

        public static void ExecuteSettlement<TEntity>(this ModelSource<TEntity> models, DateTime startDate,DateTime endExclusiveDate)
            where TEntity : class, new()
        {
            var items = models.GetTable<ContractTrustTrack>().Where(t => t.EventDate >= startDate && t.EventDate < endExclusiveDate)
                    .Where(t => !t.SettlementID.HasValue);

            if(items.Count()>0)
            {
                Settlement settlement = new Settlement
                {
                    SettlementDate = DateTime.Now,
                    StartDate = startDate,
                    EndExclusiveDate = endExclusiveDate
                };
                models.GetTable<Settlement>().InsertOnSubmit(settlement);
                models.SubmitChanges();

                models.ExecuteCommand(@"INSERT INTO ContractTrustSettlement
                       (ContractID, SettlementID, TotalTrustAmount, InitialTrustAmount)
                        SELECT  s.ContractID, {0}, t.TotalTrustAmount, t.TotalTrustAmount
                        FROM     (SELECT  a.ContractID, MAX(a.SettlementID) AS SettlementID
                        FROM     ContractTrustSettlement AS a INNER JOIN
                                       CourseContract AS b ON a.ContractID = b.ContractID
                        WHERE   (b.Status = {1})
                        GROUP BY a.ContractID) AS s INNER JOIN ContractTrustSettlement AS t 
                            ON s.ContractID = t.ContractID AND s.SettlementID = t.SettlementID",
                    settlement.SettlementID, (int)Naming.CourseContractStatus.已生效);

                models.ExecuteCommand(@"UPDATE CourseContractTrust
                        SET        CurrentSettlement = {0}
                        FROM     CourseContractTrust INNER JOIN
                                       CourseContract ON CourseContractTrust.ContractID = CourseContract.ContractID
                        WHERE   (CourseContract.Status = {1})", settlement.SettlementID, (int)Naming.CourseContractStatus.已生效);

                models.ExecuteCommand(@"INSERT INTO CourseContractTrust
                                           (ContractID, CurrentSettlement)
                            SELECT  a.ContractID, b.SettlementID
                            FROM     CourseContract AS a INNER JOIN
                                           ContractTrustSettlement AS b ON a.ContractID = b.ContractID
                            WHERE   (b.SettlementID = {0}) AND (a.ContractID NOT IN
                                               (SELECT  ContractID
                                               FROM     CourseContractTrust)) AND (a.Status = {1})", 
                    settlement.SettlementID, (int)Naming.CourseContractStatus.已生效);

                foreach (var item in items.GroupBy(t => t.ContractID))
                {
                    var contract = models.GetTable<CourseContract>().Where(t => t.ContractID == item.Key).First();
                    ContractTrustSettlement contractTrustSettlement;

                    if (contract.CourseContractTrust == null)
                    {
                        contract.CourseContractTrust = new CourseContractTrust
                        {
                            CurrentSettlement = settlement.SettlementID
                        };
                        contractTrustSettlement = new ContractTrustSettlement
                        {
                            ContractID = contract.ContractID,
                            SettlementID = settlement.SettlementID,
                            InitialTrustAmount = 0,
                            TotalTrustAmount = 0
                        };

                        models.GetTable<ContractTrustSettlement>().InsertOnSubmit(contractTrustSettlement);
                    }
                    else
                    {
                        var currentTrustSettlement = contract.CourseContractTrust.ContractTrustSettlement;
                        if(currentTrustSettlement.SettlementID!=settlement.SettlementID)
                        {
                            contractTrustSettlement = new ContractTrustSettlement
                            {
                                ContractID = contract.ContractID,
                                SettlementID = settlement.SettlementID,
                                InitialTrustAmount = currentTrustSettlement.TotalTrustAmount,
                                TotalTrustAmount = currentTrustSettlement.TotalTrustAmount
                            };

                            models.GetTable<ContractTrustSettlement>().InsertOnSubmit(contractTrustSettlement);
                        }
                        else
                        {
                            contractTrustSettlement = currentTrustSettlement;
                        }
                    }

                    foreach (var trust in item)
                    {
                        trust.SettlementID = settlement.SettlementID;
                        switch (trust.TrustType)
                        {
                            case "B":
                            case "T":
                                contractTrustSettlement.TotalTrustAmount += trust.Payment.PayoffAmount ?? 0;
                                break;

                            case "N":
                                var lesson = trust.LessonTime.RegisterLesson;
                                contractTrustSettlement.TotalTrustAmount -= (lesson.LessonPriceType.ListPrice * lesson.GroupingMemberCount * lesson.GroupingLessonDiscount.PercentageOfDiscount / 100 ?? 0);
                                break;

                            case "X":
                            case "S":
                                contractTrustSettlement.TotalTrustAmount -= trust.Payment.PayoffAmount ?? 0;
                                break;
                        }
                    }

                    models.SubmitChanges();
                }

            }
        }

        public static IEnumerable<UserProfile> CheckOverlapedBooking<TEntity>(this ModelSource<TEntity> models, LessonTime timeItem, RegisterLesson lesson, LessonTime source = null)
            where TEntity : class, new()
        {
            int durationHours = (timeItem.ClassTime.Value.Minute + timeItem.DurationInMinutes.Value + 59) / 60;
            IQueryable<LessonTimeExpansion> items;
            if (source == null)
            {
                items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == timeItem.ClassTime.Value.Date
                    && t.Hour >= timeItem.ClassTime.Value.Hour
                    && t.Hour < (timeItem.ClassTime.Value.Hour + durationHours));
            }
            else
            {
                items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == timeItem.ClassTime.Value.Date
                    && t.Hour >= timeItem.ClassTime.Value.Hour
                    && t.Hour < (timeItem.ClassTime.Value.Hour + durationHours)
                    && t.LessonID != source.LessonID);
            }

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


    }
}