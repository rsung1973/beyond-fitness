using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
    public static class ReportExtensionMethods
    {
        public static DataTable CreateLessonAchievementDetails<TEntity>(this ModelSource<TEntity> models,IQueryable<LessonTime> items)
            where TEntity : class, new()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("合約編號", typeof(String)));
            table.Columns.Add(new DataColumn("體能顧問", typeof(String)));
            table.Columns.Add(new DataColumn("分店", typeof(String)));
            table.Columns.Add(new DataColumn("學員", typeof(String)));
            table.Columns.Add(new DataColumn("合約名稱", typeof(String)));
            table.Columns.Add(new DataColumn("課程單價", typeof(int)));
            table.Columns.Add(new DataColumn("全價計算堂數", typeof(int)));
            table.Columns.Add(new DataColumn("半價計算堂數", typeof(int)));
            table.Columns.Add(new DataColumn("上課地點", typeof(String)));

            var details = items.Where(t => t.RegisterLesson.RegisterLessonContract != null)
                .GroupBy(t => new
                {
                    t.AttendingCoach,
                    ContractID = t.RegisterLesson.RegisterLessonContract.ContractID,
                    t.BranchID
                });

            foreach (var item in details)
            {
                CourseContract contract = models.GetTable<CourseContract>().Where(u => u.ContractID == item.Key.ContractID).First();
                ServingCoach coach = models.GetTable<ServingCoach>().Where(c => c.CoachID == item.Key.AttendingCoach).First();
                var branch = models.GetTable<BranchStore>().Where(b => b.BranchID == item.Key.BranchID).First();

                var r = table.NewRow();
                r[0] = contract.ContractNo();
                r[1] = coach.UserProfile.FullName();
                r[2] = contract.CourseContractExtension.BranchStore.BranchName;

                if (contract.CourseContractType.IsGroup == true)
                {
                    r[3] = String.Join("/", contract.CourseContractMember.Select(m => m.UserProfile).ToArray().Select(m => m.FullName()));
                }
                else
                {
                    r[3] = contract.ContractOwner.FullName();
                }

                r[4] = contract.CourseContractType.TypeName
                    + " (" + contract.LessonPriceType.DurationInMinutes + " 分鐘)";
                r[5] = contract.LessonPriceType.ListPrice;

                var halfCount = item.Count(t => t.LessonAttendance == null || !t.LessonPlan.CommitAttendance.HasValue);
                r[6] = item.Count() - halfCount;
                r[7] = halfCount;
                r[8] = branch.BranchName;
                table.Rows.Add(r);
            }

            var enterprise = items.Where(t => t.RegisterLesson.RegisterLessonEnterprise != null)
                .GroupBy(t => new
                {
                    t.AttendingCoach,
                    ContractID = t.RegisterLesson.RegisterLessonEnterprise.ContractID,
                    t.RegisterID
                });

            foreach (var item in enterprise)
            {
                EnterpriseCourseContract contract = models.GetTable<EnterpriseCourseContract>().Where(u => u.ContractID == item.Key.ContractID).First();
                ServingCoach coach = models.GetTable<ServingCoach>().Where(c => c.CoachID == item.Key.AttendingCoach).First();
                RegisterLesson lesson = models.GetTable<RegisterLesson>().Where(g => g.RegisterID == item.Key.RegisterID).First();

                var r = table.NewRow();

                r[0] = contract.ContractNo;
                r[1] = coach.UserProfile.FullName();
                r[2] = contract.BranchStore.BranchName;

                if (lesson.GroupingMemberCount > 1)
                {
                    r[3] = String.Join("/", lesson.GroupingLesson.RegisterLesson.Select(s => s.UserProfile).ToArray().Select(m => m.FullName()));
                }
                else
                {
                    r[3] = lesson.UserProfile.FullName();
                }

                r[4] = contract.Subject;
                r[5] = contract.EnterpriseCourseContent.OrderByDescending(c => c.ListPrice).First().ListPrice;
                var halfCount = item.Count(t => t.LessonAttendance == null || !t.LessonPlan.CommitAttendance.HasValue);
                r[6] = item.Count() - halfCount;
                r[7] = halfCount;
                table.Rows.Add(r);
            }

            var others = items.Where(t => t.RegisterLesson.RegisterLessonContract == null && t.RegisterLesson.RegisterLessonEnterprise == null);
            foreach (var item in others)
            {
                var r = table.NewRow();
                r[0] = "--";
                r[1] = item.AsAttendingCoach.UserProfile.FullName();
                r[2] = item.BranchStore.BranchName;

                r[3] = item.RegisterLesson.UserProfile.FullName();

                r[4] = item.RegisterLesson.LessonPriceType.Description
                    + " (" + item.RegisterLesson.LessonPriceType.DurationInMinutes + " 分鐘)";
                r[5] = item.RegisterLesson.LessonPriceType.ListPrice;
                var halfCount = item.LessonAttendance == null || item.LessonPlan.CommitAttendance.HasValue ? 1 : 0;
                r[6] = 1 - halfCount;
                r[7] = halfCount;
                r[8] = item.BranchStore.BranchName;
                table.Rows.Add(r);
            }

            table.TableName = "上課統計表-人員明細";

            return table;
        }

    }
}