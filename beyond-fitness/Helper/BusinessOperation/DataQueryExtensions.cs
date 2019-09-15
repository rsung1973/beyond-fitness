﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WebHome.Helper.BusinessOperation
{
    public static class DataQueryExtensions
    {
        public static IQueryable<CourseContract> InquireContract<TEntity>(this CourseContractQueryViewModel viewModel, SampleController<TEntity> controller, out String alertMessage)
            where TEntity : class, new()
        {
            alertMessage = null;
            var ModelState = controller.ModelState;
            var ViewBag = controller.ViewBag;
            var HttpContext = controller.HttpContext;
            var models = controller.DataSource;

            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            ViewBag.ViewModel = viewModel;

            bool hasConditon = false;
            ViewBag.ViewModel = viewModel;

            IQueryable<CourseContract> items;

            if (viewModel.ContractQueryMode == Naming.ContractServiceMode.ContractOnly)
            {
                if (viewModel.Status >= (int)Naming.CourseContractStatus.已生效)
                {
                    items = models.PromptOriginalContract();
                }
                else if (viewModel.PayoffMode.HasValue)
                {
                    items = models.PromptAccountingContract();
                }
                else
                {
                    items = models.PromptContract();
                }
            }
            else if (viewModel.ContractQueryMode == Naming.ContractServiceMode.ServiceOnly)
            {
                items = models.PromptContractService();
            }
            else
            {
                items = models.GetTable<CourseContract>();
            }

            if (viewModel.PayoffMode == Naming.ContractPayoffMode.Unpaid)
            {
                hasConditon = true;
                items = items
                    .FilterByToPay(models);
            }
            else if (viewModel.PayoffMode == Naming.ContractPayoffMode.Paid)
            {
                hasConditon = true;
                items = items.Where(c => c.ContractPayment.Any());
            }

            if (viewModel.IncludeTotalUnpaid == true)
            {
                Expression<Func<CourseContract, bool>> expr = c => true;
                Expression<Func<CourseContract, bool>> defaultExpr = expr;
                if (viewModel.PayoffDueFrom.HasValue)
                {
                    hasConditon = true;
                    expr = expr.And(c => c.PayoffDue >= viewModel.PayoffDueFrom);
                }

                if (viewModel.PayoffDueTo.HasValue)
                {
                    hasConditon = true;
                    expr = expr.And(c => c.PayoffDue < viewModel.PayoffDueTo);
                }

                if (defaultExpr != expr)
                {
                    expr = expr.Or(c => !c.PayoffDue.HasValue);
                    items = items.Where(expr);
                }
            }
            else
            {
                if (viewModel.PayoffDueFrom.HasValue)
                {
                    hasConditon = true;
                    items = items.Where(c => c.PayoffDue >= viewModel.PayoffDueFrom);
                }

                if (viewModel.PayoffDueTo.HasValue)
                {
                    hasConditon = true;
                    items = items.Where(c => c.PayoffDue < viewModel.PayoffDueTo);
                }
            }

            if (viewModel.Status.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.Status == viewModel.Status);
            }

            if (viewModel.FitnessConsultant.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.FitnessConsultant == viewModel.FitnessConsultant);
            }

            if (viewModel.ManagerID.HasValue)
            {
                hasConditon = true;
                items = items.FilterByBranchStoreManager(models, viewModel.ManagerID);
            }

            if (viewModel.OfficerID.HasValue)
            {
                hasConditon = true;
            }

            if (viewModel.IsExpired == true)
            {
                hasConditon = true;
                items = items.FilterByExpired(models);
            }
            else if (viewModel.IsExpired == false)
            {
                hasConditon = true;
                items = items.Where(c => c.Expiration >= DateTime.Today);
            }

            if (viewModel.EffectiveDateFrom.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.EffectiveDate >= viewModel.EffectiveDateFrom);
            }

            if (viewModel.EffectiveDateTo.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.EffectiveDate < viewModel.EffectiveDateTo);
            }

            if (viewModel.ExpirationFrom.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.Expiration >= viewModel.ExpirationFrom);
            }

            if (viewModel.ExpirationTo.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.Expiration < viewModel.ExpirationTo.Value);
            }

            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }
            if (viewModel.ContractID.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.ContractID == viewModel.ContractID);
            }

            if (viewModel.AlarmCount.HasValue)
            {
                hasConditon = true;
                items = items.FilterByAlarmedContract(models, viewModel.AlarmCount.Value);
            }

            if (viewModel.InstallmentID.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.InstallmentID == viewModel.InstallmentID);
            }

            if (viewModel.UnpaidExpiring == true)
            {
                hasConditon = true;
                items = items.FilterByUnpaidContract(models)
                        .Where(p => p.PayoffDue < DateTime.Today);
            }

            if (viewModel.Unpaid == true)
            {
                hasConditon = true;
                items = items.FilterByUnpaidContract(models);
            }

            if (hasConditon)
            {

            }
            else
            {
                items = items.Where(c => false);
            }

            //if (viewModel.ContractType.HasValue)
            //    items = items.Where(c => c.ContractType == viewModel.ContractType);
            return items;
        }

        public static IQueryable<CourseContract> InquireContractByCustom<TEntity>(this CourseContractQueryViewModel viewModel, SampleController<TEntity> controller, out String alertMessage)
                where TEntity : class, new()
        {
            alertMessage = null;
            var ModelState = controller.ModelState;
            var ViewBag = controller.ViewBag;
            var HttpContext = controller.HttpContext;
            var models = controller.DataSource;

            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();
            bool hasConditon = false;

            IQueryable<CourseContract> items;

            if (viewModel.ContractQueryMode == Naming.ContractServiceMode.ContractOnly)
            {
                items = models.PromptContract();
            }
            else if (viewModel.ContractQueryMode == Naming.ContractServiceMode.ServiceOnly)
            {
                items = models.PromptContractService();
            }
            else
            {
                items = models.GetTable<CourseContract>();
            }

            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }
            if (viewModel.ContractID.HasValue)
            {
                items = items.Where(c => c.ContractID == viewModel.ContractID);
            }

            if (viewModel.ContractDateFrom.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.ContractDate >= viewModel.ContractDateFrom);
            }

            if (viewModel.ContractDateTo.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.ContractDate < viewModel.ContractDateTo.Value);
            }

            Expression<Func<CourseContract, bool>> queryExpr = c => false;
            bool subCondition = false;

            viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
            if (viewModel.ContractNo != null)
            {
                subCondition = true;
                var no = viewModel.ContractNo.Split('-');
                if (no.Length > 1)
                {
                    int.TryParse(no[1], out int seqNo);
                    queryExpr = queryExpr.Or(c => c.ContractNo.StartsWith(no[0])
                        && c.SequenceNo == seqNo);
                }
                else
                {
                    queryExpr = queryExpr.Or(c => c.ContractNo.StartsWith(viewModel.ContractNo));
                }
            }

            viewModel.RealName = viewModel.RealName.GetEfficientString();
            if (viewModel.RealName != null)
            {
                subCondition = true;
                queryExpr = queryExpr.Or(c => c.CourseContractMember.Any(m => m.UserProfile.RealName.Contains(viewModel.RealName) || m.UserProfile.Nickname.Contains(viewModel.RealName)));
            }

            if (hasConditon)
            {
                if (subCondition)
                {
                    items = items.Where(queryExpr);
                }
                else
                {
                    if (viewModel.FitnessConsultant.HasValue)
                    {
                        hasConditon = true;
                        items = items.Where(c => c.FitnessConsultant == viewModel.FitnessConsultant);
                    }

                    if (viewModel.ManagerID.HasValue)
                    {
                        hasConditon = true;
                        items = items.FilterByBranchStoreManager(models, viewModel.ManagerID);
                    }

                    if (viewModel.OfficerID.HasValue)
                    {
                        hasConditon = true;
                    }
                }
            }
            else
            {
                if (subCondition)
                {
                    items = items.Where(queryExpr);
                }
                else
                {
                    //items = items.Where(c => false);
                    //return Json(new { result = false, message = "請設定查詢條件!!" });
                    ModelState.AddModelError("RealName", "請輸入查詢學生姓名(暱稱)!!");
                    ModelState.AddModelError("ContractNo", "請輸入查詢合約編號!!");
                    ModelState.AddModelError("ContractDateFrom", "請輸入查詢合約起日!!");
                    ModelState.AddModelError("ContractDateTo", "請輸入查詢合約迄日!!");
                    ViewBag.ModelState = ModelState;
                    return null;
                }
            }

            return items;
        }

        public static IQueryable<Payment> InquirePaymentByCustom<TEntity>(this PaymentQueryViewModel viewModel, SampleController<TEntity> controller, out String alertMessage)
                where TEntity : class, new()
        {
            alertMessage = null;
            var ModelState = controller.ModelState;
            var ViewBag = controller.ViewBag;
            var HttpContext = controller.HttpContext;
            var models = controller.DataSource;

            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();
            bool hasConditon = false;

            IQueryable<Payment> items = models.GetTable<Payment>()
                .Where(p => p.TransactionType.HasValue)
                .Where(p => p.PayoffAmount > 0)
                .Where(p => p.Status.HasValue);

            IQueryable<VoidPayment> voidItems = models.GetTable<VoidPayment>();
            IQueryable<InvoiceItem> invoiceItems = models.GetTable<InvoiceItem>();
            Expression<Func<Payment, bool>> queryExpr = c => false;

            if(viewModel.PaymentID.HasValue)
            {
                items = items.Where(p => p.PaymentID == viewModel.PaymentID);
            }

            if (viewModel.BypassCondition == true)
            {
                hasConditon = true;
                queryExpr = c => true;
            }

            viewModel.UserName = viewModel.UserName.GetEfficientString();
            if (viewModel.UserName != null)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.ContractPayment.CourseContract.CourseContractMember.Any(m => m.UserProfile.RealName.Contains(viewModel.UserName) || m.UserProfile.Nickname.Contains(viewModel.UserName)));
                queryExpr = queryExpr.Or(c => c.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName));
                queryExpr = queryExpr.Or(c => c.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.Nickname.Contains(viewModel.UserName));
            }

            if (!hasConditon)
            {
                viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
                if (viewModel.ContractNo != null)
                {
                    hasConditon = true;
                    var no = viewModel.ContractNo.Split('-');
                    if (no.Length > 1)
                    {
                        int.TryParse(no[1], out int seqNo);
                        queryExpr = queryExpr.Or(c => c.ContractPayment.CourseContract.ContractNo.StartsWith(no[0])
                            && c.ContractPayment.CourseContract.SequenceNo == seqNo);
                    }
                    else
                    {
                        queryExpr = queryExpr.Or(c => c.ContractPayment.CourseContract.ContractNo.StartsWith(viewModel.ContractNo));
                    }
                }
            }

            if (!hasConditon)
            {
                if (viewModel.BranchID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.PaymentTransaction.BranchID == viewModel.BranchID);
                }
            }

            if (!hasConditon)
            {
                if (viewModel.HandlerID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.HandlerID == viewModel.HandlerID);
                    voidItems = voidItems.Where(v => v.HandlerID == viewModel.HandlerID);
                }
            }

            if (!hasConditon)
            {
                if (profile.IsAssistant() || profile.IsAccounting() || profile.IsServitor() || profile.IsOfficer())
                {

                }
                else if (profile.IsManager() || profile.IsViceManager())
                {
                    var branches = models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID);
                    items = items
                        .Join(models.GetTable<PaymentTransaction>()
                            .Join(branches, p => p.BranchID, b => b.BranchID, (p, b) => p),
                            c => c.PaymentID, h => h.PaymentID, (c, h) => c);
                }
                else if (profile.IsCoach())
                {
                    items = items.Where(p => p.HandlerID == profile.UID);
                    voidItems = voidItems.Where(v => v.HandlerID == profile.UID);
                }
                else
                {
                    items = items.Where(p => false);
                }
            }

            if (hasConditon)
            {
                items = items.Where(queryExpr);
            }

            if (viewModel.TransactionType.HasValue)
                items = items.Where(c => c.TransactionType == viewModel.TransactionType);

            if (viewModel.CompoundType != null)
            {
                viewModel.CompoundType = viewModel.CompoundType.Where(t => t.HasValue).ToArray();
                if (viewModel.CompoundType.Length > 0)
                {
                    items = items.Where(c => viewModel.CompoundType.Contains((Naming.PaymentTransactionType?)c.TransactionType));
                }
            }

            if (viewModel.Status.HasValue)
            {
                //items = items.Where(c => c.Status == viewModel.Status);
                //items = items.Where(c => c.VoidPayment.Status == viewModel.Status);
                items = items.Where(c => c.Status == viewModel.Status
                    || c.VoidPayment.Status == viewModel.Status);
            }

            if (viewModel.InvoiceType.HasValue)
            {
                invoiceItems = invoiceItems.Where(c => c.InvoiceType == (byte)viewModel.InvoiceType);
            }


            viewModel.InvoiceNo = viewModel.InvoiceNo.GetEfficientString();
            if (viewModel.InvoiceNo != null && Regex.IsMatch(viewModel.InvoiceNo, "[A-Za-z]{2}[0-9]{8}"))
            {
                String trackCode = viewModel.InvoiceNo.Substring(0, 2).ToUpper();
                String no = viewModel.InvoiceNo.Substring(2);
                invoiceItems = invoiceItems.Where(c => c.TrackCode == trackCode
                    && c.No == no);

                //allowanceItems = allowanceItems.Where(a => a.AllowanceNumber == viewModel.InvoiceNo);
                //voidItems = voidItems
                //    .Join(models.GetTable<Payment>()
                //        .Join(models.GetTable<InvoiceItem>().Where(i => i.TrackCode == trackCode && i.No == no),
                //            p => p.InvoiceID, i => i.InvoiceID, (p, i) => p),
                //        v => v.VoidID, p => p.PaymentID, (v, p) => v);
            }

            viewModel.BuyerReceiptNo = viewModel.BuyerReceiptNo.GetEfficientString();
            if (viewModel.BuyerReceiptNo != null)
            {
                invoiceItems = invoiceItems.Where(c => c.InvoiceBuyer.ReceiptNo == viewModel.BuyerReceiptNo);
            }

            Expression<Func<InvoiceItem, bool>> queryInv = f => true;
            bool dateQuery = false;
            Expression<Func<Payment, bool>> dateItems = p => true;
            IQueryable<InvoiceAllowance> allowanceItems = models.GetTable<InvoiceAllowance>();
            IQueryable<InvoiceCancellation> cancelledItems = models.GetTable<InvoiceCancellation>();

            if (viewModel.PayoffDateFrom.HasValue)
            {
                dateQuery = true;
                dateItems = dateItems.And(p => p.PayoffDate >= viewModel.PayoffDateFrom);
                queryInv = queryInv.And(i => i.InvoiceDate >= viewModel.PayoffDateFrom);
                cancelledItems = cancelledItems.Where(i => i.CancelDate >= viewModel.PayoffDateFrom);
                allowanceItems = allowanceItems.Where(a => a.AllowanceDate >= viewModel.PayoffDateFrom);
                voidItems = voidItems.Where(v => v.VoidDate >= viewModel.PayoffDateFrom);
            }

            if (viewModel.PayoffDateTo.HasValue)
            {
                dateQuery = true;
                dateItems = dateItems.And(i => i.PayoffDate < viewModel.PayoffDateTo.Value.AddDays(1));
                queryInv = queryInv.And(i => i.InvoiceDate < viewModel.PayoffDateTo.Value.AddDays(1));
                cancelledItems = cancelledItems.Where(i => i.CancelDate < viewModel.PayoffDateTo.Value.AddDays(1));
                allowanceItems = allowanceItems.Where(a => a.AllowanceDate < viewModel.PayoffDateTo.Value.AddDays(1));
                voidItems = voidItems.Where(v => v.VoidDate < viewModel.PayoffDateTo.Value.AddDays(1));
            }

            IQueryable<Payment> result = items.Join(invoiceItems, p => p.InvoiceID, i => i.InvoiceID, (p, i) => p);
            if (dateQuery)
            {
                result = result.Join(models.GetTable<Payment>().Where(dateItems), p => p.PaymentID, d => d.PaymentID, (p, d) => p)
                    .Union(result.Join(models.GetTable<InvoiceItem>().Where(queryInv), p => p.InvoiceID, i => i.InvoiceID, (p, i) => p))
                    .Union(result.Join(allowanceItems, p => p.AllowanceID, a => a.AllowanceID, (p, a) => p)
                    .Union(result.Join(voidItems, p => p.PaymentID, c => c.VoidID, (p, c) => p)));
            }

            if (viewModel.IsCancelled == true)
            {
                result = result
                    .Where(p => p.VoidPayment != null /*|| p.AllowanceID.HasValue*/);

            }
            else if (viewModel.IsCancelled == false)
            {
                result = items
                    .Where(p => p.VoidPayment == null /*&& !p.AllowanceID.HasValue*/);
            }

            if(viewModel.HasAllowance == true)
            {
                result = result.Where(p => p.AllowanceID.HasValue);
            }

            return result;
        }
        public static IQueryable<Payment> InquirePayment<TEntity>(this PaymentQueryViewModel queryViewModel, SampleController<TEntity> controller, out String alertMessage)
                where TEntity : class, new()
        {
            alertMessage = null;
            var ModelState = controller.ModelState;
            var ViewBag = controller.ViewBag;
            var HttpContext = controller.HttpContext;
            var models = controller.DataSource;

            ViewBag.ViewModel = queryViewModel;
            PaymentQueryViewModel viewModel = (PaymentQueryViewModel)queryViewModel.Duplicate();
            var profile = HttpContext.GetUser();

            IQueryable<Payment> items = models.PromptIncomePayment();

            IQueryable<VoidPayment> voidItems = models.GetTable<VoidPayment>();
            IQueryable<InvoiceItem> invoiceItems = models.GetTable<InvoiceItem>();

            if (viewModel.PaymentID.HasValue)
            {
                items = items.Where(p => p.PaymentID == viewModel.PaymentID);
            }

            IQueryable<Payment> buildQueryByUserName(IQueryable<Payment> queryItems,String userName)
            {
                IQueryable<UserProfile> users = models.GetTable<UserProfile>().Where(c => c.RealName.Contains(userName)
                        || c.Nickname.Contains(userName));

                IQueryable<ContractPayment> cp = users.Join(models.GetTable<CourseContractMember>(), u => u.UID, m => m.UID, (u, m) => m)
                                                    .Join(models.GetTable<CourseContract>(), m => m.ContractID, c => c.ContractID, (m, c) => c)
                                                    .Join(models.GetTable<ContractPayment>(), c => c.ContractID, p => p.ContractID, (m, p) => p);
                IQueryable<TuitionInstallment> ti = users.Join(models.GetTable<RegisterLesson>(), u => u.UID, r => r.UID, (u, r) => r)
                                                    .Join(models.GetTable<TuitionInstallment>(), r => r.RegisterID, t => t.RegisterID, (r, t) => t);

                queryItems = queryItems.Join(cp, p => p.PaymentID, c => c.PaymentID, (p, c) => p)
                    .Union(queryItems.Join(ti, p => p.PaymentID, c => c.InstallmentID, (p, c) => p));

                return queryItems;
            }

            viewModel.UserName = viewModel.UserName.GetEfficientString();
            if (viewModel.UserName != null)
            {
                items = buildQueryByUserName(items,viewModel.UserName);
            }

            IQueryable<Payment> buildQueryByContractNo(IQueryable<Payment> queryItems, String contractNo)
            {
                IQueryable<CourseContract> ci = models.GetTable<CourseContract>();
                var no = contractNo.Split('-');
                if (no.Length > 1)
                {
                    int.TryParse(no[1], out int seqNo);
                    ci = ci.Where(c => c.ContractNo.StartsWith(no[0])
                        && c.SequenceNo == seqNo);
                }
                else
                {
                    ci = ci.Where(c => c.ContractNo.StartsWith(contractNo));
                }

                IQueryable<ContractPayment> cp = ci.Join(models.GetTable<ContractPayment>(), c => c.ContractID, p => p.ContractID, (m, p) => p);
                queryItems =queryItems.Join(cp, p => p.PaymentID, c => c.PaymentID, (p, c) => p);

                return queryItems;
            }


            viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
            if (viewModel.ContractNo != null)
            {
                items = buildQueryByContractNo(items, viewModel.ContractNo);
            }

            if (viewModel.BranchID.HasValue)
            {
                items = items.Where(c => c.PaymentTransaction.BranchID == viewModel.BranchID);
            }

            if (viewModel.HandlerID.HasValue)
            {
                items = items.Where(c => c.HandlerID == viewModel.HandlerID);
                voidItems = voidItems.Where(v => v.HandlerID == viewModel.HandlerID);
            }


            if (viewModel.TransactionType.HasValue)
                items = items.Where(c => c.TransactionType == viewModel.TransactionType);

            if (viewModel.CompoundType != null)
            {
                viewModel.CompoundType = viewModel.CompoundType.Where(t => t.HasValue).ToArray();
                if (viewModel.CompoundType.Length > 0)
                {
                    items = items.Where(c => viewModel.CompoundType.Contains((Naming.PaymentTransactionType?)c.TransactionType));
                }
            }

            if (viewModel.Status.HasValue)
            {
                //items = items.Where(c => c.Status == viewModel.Status);
                //items = items.Where(c => c.VoidPayment.Status == viewModel.Status);
                items = items.Where(c => c.Status == viewModel.Status
                    || c.VoidPayment.Status == viewModel.Status);
            }

            if (viewModel.InvoiceType.HasValue)
            {
                invoiceItems = invoiceItems.Where(c => c.InvoiceType == (byte)viewModel.InvoiceType);
            }

            if (viewModel.HasInvoicePrinted.HasValue)
            {
                if (viewModel.HasInvoicePrinted == true)
                {
                    invoiceItems = invoiceItems
                        .Where(i => i.Document.DocumentPrintLog.Any());
                }
                else if (viewModel.HasInvoicePrinted == false)
                {
                    invoiceItems = invoiceItems
                        .Where(i => i.InvoiceCancellation == null)
                        .Where(i => i.PrintMark == "Y")
                        .Where(i => !i.Document.DocumentPrintLog.Any());
                }
            }

            IQueryable<Payment> buildQueryByInvoiceNo(IQueryable<Payment> queryItems, String invoiceNo)
            {
                if (Regex.IsMatch(invoiceNo, "[A-Za-z]{2}[0-9]{8}"))
                {
                    String trackCode = invoiceNo.Substring(0, 2).ToUpper();
                    String no = invoiceNo.Substring(2);

                    queryItems = queryItems.Join(models.GetTable<InvoiceItem>()
                                                    .Where(c => c.TrackCode == trackCode && c.No == no),
                                                p => p.InvoiceID, i => i.InvoiceID, (p, i) => p);
                    return queryItems;
                }
                else
                {
                    return models.GetTable<Payment>().Where(p => false);
                }
            }


            viewModel.InvoiceNo = viewModel.InvoiceNo.GetEfficientString();
            if (viewModel.InvoiceNo != null && Regex.IsMatch(viewModel.InvoiceNo, "[A-Za-z]{2}[0-9]{8}"))
            {
                String trackCode = viewModel.InvoiceNo.Substring(0, 2).ToUpper();
                String no = viewModel.InvoiceNo.Substring(2);
                invoiceItems = invoiceItems.Where(c => c.TrackCode == trackCode
                    && c.No == no);

                //allowanceItems = allowanceItems.Where(a => a.AllowanceNumber == viewModel.InvoiceNo);
                //voidItems = voidItems
                //    .Join(models.GetTable<Payment>()
                //        .Join(models.GetTable<InvoiceItem>().Where(i => i.TrackCode == trackCode && i.No == no),
                //            p => p.InvoiceID, i => i.InvoiceID, (p, i) => p),
                //        v => v.VoidID, p => p.PaymentID, (v, p) => v);
            }

            viewModel.BuyerReceiptNo = viewModel.BuyerReceiptNo.GetEfficientString();
            if (viewModel.BuyerReceiptNo != null)
            {
                invoiceItems = invoiceItems.Where(c => c.InvoiceBuyer.ReceiptNo == viewModel.BuyerReceiptNo);
            }

            bool dateQuery = false;
            IQueryable<InvoiceAllowance> allowanceItems = models.GetTable<InvoiceAllowance>();
            IQueryable<InvoiceCancellation> cancelledItems = models.GetTable<InvoiceCancellation>();

            if (viewModel.PayoffDateFrom.HasValue)
            {
                dateQuery = true;
                items = items.Where(p => p.PayoffDate >= viewModel.PayoffDateFrom);
                invoiceItems = invoiceItems.Where(i => i.InvoiceDate >= viewModel.PayoffDateFrom);
                cancelledItems = cancelledItems.Where(i => i.CancelDate >= viewModel.PayoffDateFrom);
                allowanceItems = allowanceItems.Where(a => a.AllowanceDate >= viewModel.PayoffDateFrom);
                voidItems = voidItems.Where(v => v.VoidDate >= viewModel.PayoffDateFrom);
            }

            if (viewModel.PayoffDateTo.HasValue)
            {
                dateQuery = true;
                items = items.Where(i => i.PayoffDate < viewModel.PayoffDateTo.Value.AddDays(1));
                invoiceItems = invoiceItems.Where(i => i.InvoiceDate < viewModel.PayoffDateTo.Value.AddDays(1));
                cancelledItems = cancelledItems.Where(i => i.CancelDate < viewModel.PayoffDateTo.Value.AddDays(1));
                allowanceItems = allowanceItems.Where(a => a.AllowanceDate < viewModel.PayoffDateTo.Value.AddDays(1));
                voidItems = voidItems.Where(v => v.VoidDate < viewModel.PayoffDateTo.Value.AddDays(1));
            }

            IQueryable<Payment> result = items.Join(invoiceItems, p => p.InvoiceID, i => i.InvoiceID, (p, i) => p);
            if (viewModel.ShareFor.HasValue)
            {
                result = result.Where(p => p.VoidPayment == null)
                            .Where(p => p.TuitionAchievement.Any(t => t.CoachID == viewModel.ShareFor));
            }
            else if (viewModel.IncomeOnly == true)
            {

            }
            else if (dateQuery)
            {
                result = result
                    .Union(result.Join(allowanceItems, p => p.AllowanceID, a => a.AllowanceID, (p, a) => p)
                    .Union(result.Join(voidItems, p => p.PaymentID, c => c.VoidID, (p, c) => p)));
            }

            if (viewModel.IsCancelled == true)
            {
                result = result
                    .Where(p => p.VoidPayment != null /*|| p.AllowanceID.HasValue*/);

            }
            else if (viewModel.IsCancelled == false)
            {
                result = items
                    .Where(p => p.VoidPayment == null /*&& !p.AllowanceID.HasValue*/);
            }

            if (viewModel.HasAllowance == true)
            {
                result = result.Where(p => p.AllowanceID.HasValue);
            }

            if (viewModel.CustomQuery != null)
            {
                result = buildQueryByUserName(result, viewModel.CustomQuery)
                            .Union(buildQueryByContractNo(result, viewModel.CustomQuery))
                            .Union(buildQueryByInvoiceNo(result, viewModel.CustomQuery));
            }

            if (viewModel.HasShare.HasValue)
            {
                IQueryable<TuitionAchievement> shareItems = models.GetTable<TuitionAchievement>();
                if (viewModel.HasShare == true)
                {
                    result = result.Where(t => shareItems.Any(s => s.InstallmentID == t.PaymentID));
                }
                else
                {
                    result = result.Where(t => !shareItems.Any(s => s.InstallmentID == t.PaymentID));
                }
            }

            if(viewModel.RelatedID.HasValue)
            {
                result = result.Where(p => p.HandlerID == viewModel.RelatedID)
                            .Union(result.Where(p => p.ContractPayment.CourseContract.FitnessConsultant == viewModel.RelatedID))
                            .Union(result.Where(p => p.TuitionInstallment.IntuitionCharge.RegisterLesson.LessonTime.Any(l => l.AttendingCoach == viewModel.RelatedID)));
            }

            return result;
        }

        public static IQueryable<LessonTime> InquireAchievement<TEntity>(this AchievementQueryViewModel viewModel, SampleController<TEntity> controller, out String alertMessage)
                where TEntity : class, new()
        {
            alertMessage = null;
            var ModelState = controller.ModelState;
            var ViewBag = controller.ViewBag;
            var HttpContext = controller.HttpContext;
            var models = controller.DataSource;

            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();
            if (!viewModel.AchievementDateFrom.HasValue)
            {
                if (!String.IsNullOrEmpty(viewModel.AchievementYearMonthFrom))
                {
                    viewModel.AchievementDateFrom = DateTime.ParseExact(viewModel.AchievementYearMonthFrom, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
                }
                else
                {
                    ModelState.AddModelError("AchievementYearMonthFrom", "請選擇查詢月份");
                }
            }

            if (!viewModel.AchievementDateTo.HasValue)
            {

                if (!String.IsNullOrEmpty(viewModel.AchievementYearMonthTo))
                {
                    viewModel.AchievementDateTo = DateTime.ParseExact(viewModel.AchievementYearMonthTo, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
                }
            }

            IQueryable<LessonTime> items = models.GetTable<LessonTime>().AllCompleteLesson();

            bool hasConditon = false;
            if (viewModel.BypassCondition == true)
            {
                hasConditon = true;
            }

            if (viewModel.CoachID.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.AttendingCoach == viewModel.CoachID);
            }

            if (viewModel.ByCoachID != null && viewModel.ByCoachID.Length > 0)
            {
                items = items.Where(c => viewModel.ByCoachID.Contains(c.AttendingCoach));
            }

            //if (!hasConditon)
            //{
            if (viewModel.BranchID.HasValue)
            {
                hasConditon = true;
                items = items.Where(c => c.BranchID == viewModel.BranchID);
            }
            //}


            if (!hasConditon)
            {
                if (profile.IsAssistant() || profile.IsAccounting() || profile.IsOfficer())
                {

                }
                else if (profile.IsManager() || profile.IsViceManager())
                {
                    var coaches = profile.GetServingCoachInSameStore(models);
                    items = items.Join(coaches, c => c.AttendingCoach, h => h.CoachID, (c, h) => c);
                }
                else if (profile.IsCoach())
                {
                    items = items.Where(c => c.AttendingCoach == profile.UID);
                }
            }

            if (viewModel.AchievementDateFrom.HasValue)
            {
                items = items.Where(c => c.ClassTime >= viewModel.AchievementDateFrom);

                if (!viewModel.AchievementDateTo.HasValue)
                    viewModel.AchievementDateTo = viewModel.AchievementDateFrom;

            }

            if (viewModel.AchievementDateTo.HasValue)
            {
                items = items.Where(c => c.ClassTime < viewModel.AchievementDateTo.Value.AddMonths(1));
            }

            return items;
        }

    }
}