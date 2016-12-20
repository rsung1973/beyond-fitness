using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;

namespace WebHome.Helper
{
    public static class BusinessExtensionMethods
    {
        public static void AttendLesson<TEntity>(this ModelSource<TEntity> models, LessonTime item)
                    where TEntity : class, new()
        {
            LessonAttendance attendance = item.LessonAttendance;
            if (attendance == null)
                attendance = item.LessonAttendance = new LessonAttendance { };
            attendance.CompleteDate = DateTime.Now;

            models.SubmitChanges();

            if (item.RegisterLesson.GroupingMemberCount > 1)
            {
                var group = item.GroupingLesson;
                var lesson = group.RegisterLesson.First();
                if (lesson.Lessons - (lesson.AttendedLessons ?? 0) <= group.LessonTime.Count(t => t.LessonAttendance != null))
                {
                    foreach (var r in group.RegisterLesson)
                    {
                        r.Attended = (int)Naming.LessonStatus.課程結束;
                    }
                    models.SubmitChanges();
                }
            }
            else
            {
                var lesson = item.RegisterLesson;
                if (lesson.Lessons <= lesson.LessonTime.Count(t => t.LessonAttendance != null))
                {
                    lesson.Attended = (int)Naming.LessonStatus.課程結束;
                    models.SubmitChanges();
                }
            }
        }

        public static bool CouldMarkToAttendLesson<TEntity>(this ModelSource<TEntity> models, LessonTime item)
            where TEntity : class, new()
        {
            if(models.IsAttendanceOverdue(item))
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


    }
}