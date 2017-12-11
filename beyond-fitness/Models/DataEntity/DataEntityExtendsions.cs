﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public static String FullName(this UserProfile item, bool mask = false)
        {
            if(mask)
            {
                return item.Nickname == null ? item.RealName.MaskedName() : item.RealName.MaskedName() + "(" + item.Nickname + ")";
            }
            return item.Nickname == null ? item.RealName : item.RealName + "(" + item.Nickname + ")";
        }

        public static String MaskedName(this String name)
        {
            if (name == null || name.Length < 2)
                return name;
            StringBuilder sb = new StringBuilder(name);
            sb[1] = '○';
            return sb.ToString();
        }

        public static String Address(this UserProfile item)
        {
            return item.UserProfileExtension.AdministrativeArea + item.Address;
        }


        public static String ContractNo(this CourseContract contract)
        {
            return contract.ContractNo != null ? String.Format("{0}-{1:00}", contract.ContractNo, contract.SequenceNo) : "--";
        }

        public static int? TotalPaidAmount(this CourseContract contract)
        {
            return contract.ContractPayment
                .Select(c => c.Payment)
                .Where(p => p.TransactionType == (int)Naming.PaymentTransactionType.體能顧問費
                    || p.TransactionType == (int)Naming.PaymentTransactionType.合約轉讓餘額
                    || p.TransactionType == (int)Naming.PaymentTransactionType.合約轉點餘額)
                .Where(p => p.VoidPayment == null)
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

        public static String LessonTypeStatus(this int? status)
        {
            switch(status)
            {
                case (int)Naming.LessonPriceStatus.自主訓練:
                    return "P.I.session";
                case (int)Naming.LessonPriceStatus.一般課程:
                case (int)Naming.LessonPriceStatus.團體學員課程:
                case (int)Naming.LessonPriceStatus.已刪除:
                    return "P.T.session";
                default:
                    return ((Naming.LessonPriceStatus)status).ToString();
            }
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