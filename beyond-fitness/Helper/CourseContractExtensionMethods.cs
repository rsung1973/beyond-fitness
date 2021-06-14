﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using CommonLib.DataAccess;
using MessagingToolkit.QRCode.Codec;
using Utility;
using WebHome.Controllers;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static class CourseContractExtensionMethods
    {
        public static IQueryable<CourseContract> PromptExpiringContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            //var revisionID = models.GetTable<CourseContractRevision>().Where(r => r.Reason == "展延")
            //    .Select(r => r.RevisionID);
            //var items = models.GetTable<CourseContract>()
            //    .Where(c => !c.RegisterLessonContract.Any(r => r.RegisterLesson.Attended == (int)Naming.LessonStatus.課程結束))
            //    .Where(c => c.Expiration < DateTime.Today.AddMonths(1))
            //    .Where(c => !revisionID.Any(r => r == c.ContractID));
            //return items;
            return models.PromptEffectiveContract()
                .Where(c => c.Expiration >= DateTime.Today)
                .Where(c => c.Expiration < DateTime.Today.AddMonths(1));
        }

        public static IQueryable<CourseContract> PromptUnpaidContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.PromptEffectiveContract().FilterByUnpaidContract(models);
        }

        public static IQueryable<CourseContract> FilterByUnpaidContract<TEntity>(this IQueryable<CourseContract> contractItems, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            var items = models.GetTable<ContractPayment>()
                    .Join(models.GetTable<Payment>().Where(p => p.VoidPayment == null),
                        c => c.PaymentID, p => p.PaymentID, (c, p) => c);

            return contractItems
                .Join(models.GetTable<CourseContractExtension>().Where(t => t.Version.HasValue),
                    c => c.ContractID, t => t.ContractID, (c, t) => c)
                .Where(c => !items.Any(t => t.ContractID == c.ContractID));
        }

        public static IQueryable<CourseContract> PromptUnpaidExpiredContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.GetTable<CourseContract>().FilterByUnpaidExpiredContract(models);
        }

        public static IQueryable<CourseContract> FilterByUnpaidExpiredContract<TEntity>(this IQueryable<CourseContract> contractItems, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return contractItems.Where(c => c.Status == (int)Naming.CourseContractStatus.已終止)
                .Where(c => c.Subject == "已自動終止");
        }


        public static IQueryable<CourseContract> PromptUnpaidExpiringContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.PromptUnpaidContract()
                .Where(c => c.PayoffDue < DateTime.Today);
        }

        public static IQueryable<CourseContract> PromptEffectiveContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            //var revisionID = models.GetTable<CourseContractRevision>().Where(r => r.Reason == "展延")
            //    .Select(r => r.RevisionID);
            var items = models.GetTable<CourseContract>()
                .Where(c => c.CourseContractRevision == null)
                .Where(c => c.Status == (int)Naming.CourseContractStatus.已生效);
            return items;
            //return models.PromptOriginalContract().FilterByEffective(models);
        }

        public static IQueryable<CourseContract> PromptAccountingContract<TEntity>(this ModelSource<TEntity> models)
        where TEntity : class, new()
        {
            var items = models.GetTable<CourseContract>()
                .Where(c => c.CourseContractRevision == null)
                .Where(c => c.Status == (int)Naming.CourseContractStatus.已生效
                    || c.Status == (int)Naming.CourseContractStatus.已過期);
            return items;
        }


        public static IQueryable<CourseContract> FilterByEffective<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return items
                    .Where(c => c.Status == (int)Naming.CourseContractStatus.已生效);
        }

        public static IQueryable<CourseContract> PromptOriginalContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            //var contractID = models.GetTable<RegisterLessonContract>()
            //                    .Join(models.GetTable<RegisterLesson>().Where(r => r.Attended != (int)Naming.LessonStatus.課程結束),
            //                        c => c.RegisterID, r => r.RegisterID, (c, r) => c.ContractID);
            //return models.GetTable<CourseContract>()
            //        .Where(c => contractID.Contains(c.ContractID));
            return models.PromptRegisterLessonContract()
                        .Where(c => c.Status >= (int)Naming.CourseContractStatus.已生效);
        }

        public static IQueryable<CourseContract> PromptRegisterLessonContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.GetTable<CourseContract>()
                    .Where(c => c.RegisterLessonContract.Any());
        }

        public static IQueryable<CourseContract> PromptContractService<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.GetTable<CourseContract>()
                .Join(models.GetTable<CourseContractRevision>(), c => c.ContractID, r => r.RevisionID, (c, r) => c);
        }

        public static IQueryable<CourseContractRevision> PromptEffectiveRevision<TEntity>(this ModelSource<TEntity> models, DateTime? dateFrom, DateTime? dateTo)
                where TEntity : class, new()
        {
            IQueryable<CourseContract> items = models.GetTable<CourseContract>();
            if (dateFrom.HasValue)
                items = items.Where(c => c.EffectiveDate >= dateFrom);
            if (dateTo.HasValue)
                items = items.Where(c => c.EffectiveDate < dateTo);
            return models.GetTable<CourseContractRevision>()
                .Join(models.PromptEffectiveContract(), r => r.OriginalContract, c => c.ContractID, (r, c) => r)
                .Join(items, r => r.RevisionID, c => c.ContractID, (r, c) => r);
        }


        public static IQueryable<CourseContract> FilterByExpiringContract<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            var revisionID = models.GetTable<CourseContractRevision>().Where(r => r.Reason == "展延")
                .Select(r => r.RevisionID);
            return items
                    .Where(c => !c.RegisterLessonContract.Any(r => r.RegisterLesson.Attended == (int)Naming.LessonStatus.課程結束))
                    .Where(c => c.Expiration < DateTime.Today.AddMonths(1))
                    .Where(c => !revisionID.Any(r => r == c.ContractID));
        }

        public static IQueryable<CourseContract> FilterByUserRoleScope<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models, UserProfile profile)
                where TEntity : class, new()
        {
            if (profile.IsAssistant() || profile.IsOfficer())
            {
                return items;
            }
            else if (profile.IsManager() || profile.IsViceManager())
            {
                return models.FilterByBranchStoreManager(items, profile);
            }
            else if (profile.IsCoach())
            {
                return items.Where(c => c.FitnessConsultant == profile.UID);
            }
            else
            {
                return items.Where(c => false);
            }
        }

        public static IQueryable<CourseContractRevision> FilterByUserRoleScope<TEntity>(this IQueryable<CourseContractRevision> items, ModelSource<TEntity> models, UserProfile profile)
                where TEntity : class, new()
        {
            if (profile.IsManager() || profile.IsViceManager())
            {
                items = items.Join(models.FilterByBranchStoreManager(models.GetTable<CourseContract>(), profile),
                    r => r.OriginalContract, c => c.ContractID, (r, c) => r);
            }
            else if (profile.IsAssistant() || profile.IsOfficer())
            {

            }
            else if (profile.IsCoach())
            {
                items = items.Where(c => c.CourseContract.FitnessConsultant == profile.UID);
            }
            else
            {
                items = items.Where(c => false);
            }

            return items;
        }

        public static IQueryable<CourseContract> FilterByExpired<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return items.Where(c => c.Status == (int)Naming.CourseContractStatus.已過期);
        }

        public static IQueryable<CourseContract> FilterByToPay<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models)
                where TEntity : class, new()
        {

            //var dataItems = items.GroupJoin(models.GetTable<ContractPayment>()
            //                        .Join(models.GetTable<Payment>()
            //                            .Where(p => p.VoidPayment == null || p.AllowanceID.HasValue),
            //                            c => c.PaymentID, p => p.PaymentID, (c, p) => new { c.ContractID, p.PayoffAmount }),
            //                        t => t.ContractID, a => a.ContractID, (t, a) => new { Contract = t, TotalPaidAmount = a.Sum(s => s.PayoffAmount) })
            //                    .Where(t => !t.TotalPaidAmount.HasValue || t.Contract.TotalCost > t.TotalPaidAmount)
            //                    .Select(t => t.Contract);

            var dataItems = items.QueryContractPayment(models)
                    .Where(t => !t.TotalPaidAmount.HasValue || t.Contract.TotalCost > t.TotalPaidAmount)
                    .Select(t => t.Contract);

            return dataItems;
        }

        public static IQueryable<CourseContractPayment> QueryContractPayment<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models)
                where TEntity : class, new()
        {

            var dataItems = items.GroupJoin(models.GetTable<ContractPayment>()
                                    .Join(models.GetTable<Payment>().FilterByEffective(),
                                        c => c.PaymentID, p => p.PaymentID, (c, p) => new { c.ContractID, p.PayoffAmount }),
                                    t => t.ContractID, a => a.ContractID, (t, a) => new CourseContractPayment { Contract = t, TotalPaidAmount = a.Sum(s => s.PayoffAmount) });
            return dataItems;
        }

        public static bool IsContractService(this CourseContract item)
        {
            return item.CourseContractRevision != null;
        }

        public static String ContractCurrentStatus(this CourseContract item)
        {
            return item.IsContractService()
                        ? ((Naming.ContractServiceStatus)item.Status).ToString()
                        : item.Status == (int)Naming.ContractQueryStatus.已終止
                            ? item.Subject ?? ((Naming.ContractQueryStatus)item.Status).ToString()
                            : ((Naming.ContractQueryStatus)item.Status).ToString();
        }

        public static bool IsEditable<TEntity>(this CourseContract item, ModelSource<TEntity> models, UserProfile agent)
                where TEntity : class, new()
        {
            return item.Status == (int)Naming.CourseContractStatus.草稿
                    && (item.AgentID == agent.UID || item.FitnessConsultant == agent.UID);
            //return models.GetContractInEditingByAgent(agent).Any(c => c.ContractID == item.ContractID); ;

        }

        public static bool IsSignable<TEntity>(this CourseContract item, ModelSource<TEntity> models, UserProfile agent)
                where TEntity : class, new()
        {
            return item.Status == (int)Naming.CourseContractStatus.待簽名
                && agent.IsCoach();
            //return models.GetContractToSignByAgent(agent).Any(c => c.ContractID == item.ContractID);
        }

        public static bool IsApprovable<TEntity>(this CourseContract item, ModelSource<TEntity> models, UserProfile agent)
                where TEntity : class, new()
        {
            return models.GetContractToConfirmByAgent(agent).Any(c => c.ContractID == item.ContractID);
        }

        public static bool IsServiceApprovable<TEntity>(this CourseContract item, ModelSource<TEntity> models, UserProfile agent)
                where TEntity : class, new()
        {
            return models.GetAmendmentToAllowByAgent(agent).Any(c => c.RevisionID == item.ContractID);
        }

        public static bool IsServiceSignable<TEntity>(this CourseContract item, ModelSource<TEntity> models, UserProfile agent)
                where TEntity : class, new()
        {
            return item.IsSignable(models, agent);
            //return models.GetAmendmentToSignByAgent(agent).Any(c => c.RevisionID == item.ContractID);
        }

        public static bool IsPayable<TEntity>(this CourseContract item, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.PromptAccountingContract()
                    .FilterByToPay(models)
                    .Any(c => c.ContractID == item.ContractID);
        }

        public static bool CanApplyContractAmendment<TEntity>(this CourseContract item, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return (item.Status == (int)Naming.CourseContractStatus.已生效
                        || item.Status == (int)Naming.CourseContractStatus.已過期)
                    && !item.IsContractServiceInProgress(models);
        }

        public static bool IsContractServiceInProgress<TEntity>(this CourseContract item, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.GetTable<CourseContractRevision>().Any(r => r.OriginalContract == item.ContractID && r.CourseContract.Status < (int)Naming.CourseContractStatus.已生效);
        }


        public static IQueryable<LessonPriceType> PromptEffectiveLessonPrice<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            return models.GetTable<LessonPriceType>()
                .Where(l => l.Status == (int)Naming.LessonSeriesStatus.已啟用)
                .Where(l => l.LowerLimit.HasValue && (!l.SeriesID.HasValue || l.CurrentPriceSeries.Status == (int)Naming.LessonSeriesStatus.已啟用));
        }

        public static IQueryable<UserProfile> PromptContractMembers<TEntity>(this int[] uid, ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            IQueryable<UserProfile> items = models.GetTable<UserProfile>();

            if (uid != null && uid.Length > 0)
            {
                items = items.Where(u => uid.Contains(u.UID));
            }
            else
            {
                items = items.Where(u => false);
            }
            return items;
        }

        public static IQueryable<CourseContract> FilterByAlarmedContract<TEntity>(this IQueryable<CourseContract> items, ModelSource<TEntity> models, int alarmCount)
            where TEntity : class, new()
        {
            var c0 = items.Where(c => c.ContractType == (int)Naming.ContractTypeDefinition.CFA);
            var c1 = items.Where(c => c.ContractType != (int)Naming.ContractTypeDefinition.CFA);

            var c2 = c0.Select(c => new { Contract = c, RemainedCount = c.Lessons - c.RegisterLessonContract.Sum(l => l.RegisterLesson.LessonTime.Count()) })
                        .Concat(c1.Select(c => new { Contract = c, RemainedCount = c.Lessons - c.RegisterLessonContract.First().RegisterLesson.GroupingLesson.LessonTime.Count }));
            return c2.Where(c => c.RemainedCount <= alarmCount).Select(c => c.Contract);
        }

        public static IQueryable<CourseContract> PromptAlarmedContract<TEntity>(this ModelSource<TEntity> models, int alarmCount)
            where TEntity : class, new()
        {
            return models.PromptEffectiveContract().FilterByAlarmedContract(models, alarmCount);
        }

        public static void ClearUnpaidOverdueContract<TEntity>(this ModelSource<TEntity> models)
                    where TEntity : class, new()
        {
            DateTime checkDate = DateTime.Today.FirstDayOfMonth();
            var items = models.PromptUnpaidContract()
                                .Where(c => c.PayoffDue.Value.AddMonths(1) < checkDate)
                                .Where(c => c.CourseContractExtension.Version.HasValue);

            foreach(var item in items)
            {
                item.Status = (int)Naming.CourseContractStatus.已終止;
                item.ValidTo = checkDate;
                item.Subject = "已自動終止";
            }
            models.SubmitChanges();
        }

        public static LessonPriceType ContractOriginalSeriesPrice<TEntity>(this CourseContract item, ModelSource<TEntity> models)
                    where TEntity : class, new()
        {

            var seriesItem = models.GetTable<V_LessonUnitPrice>()
                    .Where(p => p.DurationInMinutes == item.LessonPriceType.DurationInMinutes)
                    .Where(p => p.BranchID == item.LessonPriceType.BranchID)
                    .Join(models.GetTable<LessonPriceType>(),
                        p => p.PriceID, s => s.PriceID, (p, s) => s)
                    .OrderByDescending(s => s.PriceID);

            return seriesItem.FirstOrDefault();
        }

        public static IQueryable<CourseContract> PartialEffective<TEntity>(this CourseContract item, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            return models.GetTable<CourseContract>().Where(c => c.InstallmentID == item.InstallmentID)
                    .Where(c => c.Status >= (int)Naming.CourseContractStatus.已生效);
        }



    }
}