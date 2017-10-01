using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHome.Models.Locale;

namespace WebHome.Models.DataEntity
{
    public static class DataEntityExtendsions
    {
        public static String YearsOld(this UserProfile item)
        {
            return item.Birthday.HasValue ? (DateTime.Today.Year - item.Birthday.Value.Year).ToString()  : "--";
        }

        public static int CurrentYearsOld(this UserProfile item)
        {
            return item.Birthday.HasValue ? (int)((DateTime.Today - item.Birthday.Value).TotalDays / 365) : 999;
        }


        public static String CurrentLessonStatus(this LessonTime item)
        {
            return item.TrainingPlan.Count == 0
                                    ? item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自由教練預約
                                        ? item.LessonAttendance == null
                                            ? "已預約"
                                            : "已完成課程"
                                        : "已預約"
                                    : item.LessonAttendance != null
                                        ? "已完成課程"
                                        : "編輯課程內容中";
        }

        public static int RemainedLessonCount(this RegisterLesson item)
        {
            //return item.Lessons
            //        - item.GroupingLesson.LessonTime.Count(/*l=>l.LessonAttendance!= null*/);
            return item.Lessons - (item.AttendedLessons ?? 0)
                    - (item.RegisterGroupID.HasValue ? item.GroupingLesson.LessonTime.Count : item.LessonTime.Count);
        }

        public static int? RemainedLessonCount(this CourseContract item)
        {
            return item.RegisterLessonContract.Count > 0
                    ? item.CourseContractType.ContractCode == "CFA"
                        ? item.Lessons
                            - item.RegisterLessonContract.Sum(c => c.RegisterLesson.LessonTime.Count())
                            - item.RegisterLessonContract.Sum(c => c.RegisterLesson.AttendedLessons)
                        : item.RegisterLessonContract.First().RegisterLesson.RemainedLessonCount()
                    : item.Lessons.Value;
        }


        public static String FullName(this UserProfile item)
        {
            return item.Nickname==null ? item.RealName : item.RealName + "(" + item.Nickname + ")";
        }

        public static String ContractNo(this CourseContract contract)
        {
            return contract.ContractNo != null ? String.Format("{0}-{1:00}", contract.ContractNo, contract.SequenceNo) : "--";
        }

        public static int? TotalPaidAmount(this CourseContract contract)
        {
            return contract.ContractPayment
                .Select(c => c.Payment)
                .Where(p => p.VoidPayment == null && p.TransactionType==(int)Naming.PaymentTransactionType.體能顧問費)
                .Sum(c => c.PayoffAmount);
        }

        public static LessonPriceType OriginalSeriesPrice(this CourseContract item)
        {
            return item.LessonPriceType.SeriesID.HasValue
                ? item.LessonPriceType
                    .CurrentPriceSeries.AllLessonPrice
                        .Where(p => p.DurationInMinutes == item.LessonPriceType.DurationInMinutes)
                        .OrderBy(p => p.LowerLimit).FirstOrDefault()
                : null;
        }

    }

    public partial class UserProfile
    {
        public UserRole CurrentUserRole
        {
            get
            {
                return this.UserRole[0];
            }
        }

        public int? DailyQuestionID { get; set; }

    }
}