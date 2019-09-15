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
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static class PaymentExtensionMethods
    {
        public static IQueryable<Payment> FilterByUserRoleScope<TEntity>(this IQueryable<Payment> items, ModelSource<TEntity> models, UserProfile profile)
            where TEntity : class, new()
        {
            if (profile.IsAssistant() || profile.IsOfficer())
            {
                return items;
            }
            else if (profile.IsManager() || profile.IsViceManager())
            {
                //var payment = models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID)
                //    .SelectMany(b => b.PaymentTransaction)
                //    .Select(p => p.Payment);

                return items
                        .Join(models.GetTable<PaymentTransaction>()
                            .Join(models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID),
                                t => t.BranchID, b => b.BranchID, (t, b) => t),
                            p => p.PaymentID, t => t.PaymentID, (p, t) => p);
            }
            else
            {
                return items.Where(p => p.HandlerID == profile.UID);
            }
        }

        public static IQueryable<Payment> FilterByAccountingPayment<TEntity>(this IQueryable<Payment> items, ModelSource<TEntity> models)
                    where TEntity : class, new()
        {
            return items
                .Where(p => p.TransactionType == (int)Naming.PaymentTransactionType.體能顧問費
                    || p.TransactionType == (int)Naming.PaymentTransactionType.合約轉讓餘額
                    || p.TransactionType == (int)Naming.PaymentTransactionType.合約轉點餘額)
                .FilterByEffective();
        }

        public static IEnumerable<Payment> FilterByAccountingPayment(this IEnumerable<Payment> items)
        {
            return items
                .Where(p => p.TransactionType == (int)Naming.PaymentTransactionType.體能顧問費
                    || p.TransactionType == (int)Naming.PaymentTransactionType.合約轉讓餘額
                    || p.TransactionType == (int)Naming.PaymentTransactionType.合約轉點餘額)
                .FilterByEffective();
        }

        public static IQueryable<InvoiceAllowance> ExtractPaymentAllowance<TEntity>(this IQueryable<Payment> items, ModelSource<TEntity> models)
                    where TEntity : class, new()
        {
            return items.Join(models.GetTable<InvoiceAllowance>(),
                            p => p.AllowanceID, a => a.AllowanceID, (p, a) => a);
        }

        public static IQueryable<Payment> ExtractVoidPayment<TEntity>(this IQueryable<Payment> items, ModelSource<TEntity> models,bool currentVoid = false)
                    where TEntity : class, new()
        {
            if (currentVoid)
            {
                return items.Where(p => p.InvoiceItem.InvoiceCancellation != null);
            }
            else
            {
                return items.Where(p => p.InvoiceItem.InvoiceCancellation != null)
                    .Join(models.GetTable<VoidPayment>(),
                                p => p.PaymentID, a => a.VoidID, (p, a) => p);
            }
        }

        public static IQueryable<Payment> PromptIncomePayment<TEntity>(this ModelSource<TEntity> models)
            where TEntity : class, new()
        {

            return models.GetTable<Payment>()
                    .Where(p => p.TransactionType == (int)Naming.PaymentTransactionType.自主訓練
                        || p.TransactionType == (int)Naming.PaymentTransactionType.運動商品
                        || p.TransactionType == (int)Naming.PaymentTransactionType.食飲品
                        || p.TransactionType == (int)Naming.PaymentTransactionType.體能顧問費);
        }

        public static IQueryable<TuitionAchievement> GetPaymentAchievement<TEntity>(this IQueryable<Payment> items, ModelSource<TEntity> models,IQueryable<TuitionAchievement> filterItems = null)
            where TEntity : class, new()
        {
            if(filterItems==null)
            {
                filterItems = models.GetTable<TuitionAchievement>();
            }
            return items
                //.FilterByEffective()
                .Join(filterItems, 
                    p => p.PaymentID, t => t.InstallmentID, (p, t) => t);
        }

        public static IQueryable<LessonTime> GetUnpaidPISession<TEntity>(this PaymentQueryViewModel viewModel, ModelSource<TEntity> models)
            where TEntity : class, new()
        {
            var items = models.GetTable<LessonTime>()
                .Where(r => r.ClassTime < DateTime.Today.AddDays(1))
                .Where(r => r.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自主訓練)
                .Where(r => r.RegisterLesson.IntuitionCharge.TuitionInstallment.Count == 0
                    || !r.RegisterLesson.IntuitionCharge.TuitionInstallment.Any(t => t.Payment.VoidPayment == null || t.Payment.VoidPayment.Status != (int)Naming.CourseContractStatus.已生效));

            if (viewModel.BranchID.HasValue)
            {
                items = items.Where(r => r.BranchID == viewModel.BranchID);
            }

            return items;
        }

    }
}