﻿using System;
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
    public static class TaskExtensionMethods
    {
        public static void ProcessContractTranference(this CourseContractRevision item)
        {
            ThreadPool.QueueUserWorkItem(t =>
            {
                try
                {
                    using (var models = new ModelSource<UserProfile>())
                    {
                        item = models.GetTable<CourseContractRevision>()
                            .Where(r => r.RevisionID == item.RevisionID).First();
                        ///1.變更狀態
                        ///
                        item.SourceContract.Status = (int)Naming.CourseContractStatus.已轉讓;
                        foreach(var lesson in item.SourceContract.RegisterLessonContract)
                        {
                            lesson.RegisterLesson.Attended = (int)Naming.LessonStatus.課程結束;
                        }
                        ///2.回沖繳款餘額
                        ///
                        var balance = item.SourceContract.TotalPaidAmount() - item.SourceContract.TotalCost / item.SourceContract.Lessons * (item.SourceContract.Lessons - item.SourceContract.RemainedLessonCount());
                        Payment balancedPayment = null;
                        if (balance > 0)
                        {
                            var dummyInvoice = models.GetTable<InvoiceItem>().Where(i => i.No == "--").FirstOrDefault();

                            balancedPayment = new Payment
                            {
                                Status = (int)Naming.CourseContractStatus.已生效,
                                ContractPayment = new ContractPayment { },
                                PaymentTransaction = new PaymentTransaction
                                {
                                    BranchID = item.SourceContract.CourseContractExtension.BranchID
                                },
                                PaymentAudit = new Models.DataEntity.PaymentAudit { },
                                PayoffAmount = -balance,
                                PayoffDate = DateTime.Today,
                                Remark = "繳款餘額沖銷",
                                HandlerID = item.CourseContract.AgentID,
                                PaymentType = "現金",
                                TransactionType = (int)Naming.PaymentTransactionType.合約轉讓沖銷
                            };
                            balancedPayment.ContractPayment.ContractID = item.OriginalContract.Value;
                            if (dummyInvoice != null)
                                balancedPayment.InvoiceID = dummyInvoice.InvoiceID;
                            models.GetTable<Payment>().InsertOnSubmit(balancedPayment);

                            models.GetTable<ContractTrustTrack>().InsertOnSubmit(new ContractTrustTrack
                            {
                                ContractID = item.OriginalContract.Value,
                                EventDate = balancedPayment.PayoffDate.Value,
                                Payment = balancedPayment,
                                TrustType = Naming.TrustType.X.ToString()
                            });
                        }

                        models.SubmitChanges();

                        ///3.產生新合約
                        ///
                        var contract = models.InitiateCourseContract(
                            new CourseContractViewModel
                            {
                                ContractType = item.SourceContract.ContractType,
                                Subject = item.SourceContract.Subject,
                                OwnerID = item.CourseContract.CourseContractMember.First().UID,
                                Lessons = item.SourceContract.RemainedLessonCount(),
                                PriceID = item.SourceContract.PriceID,
                                Remark = String.Concat("原合約編號 ",item.SourceContract.ContractNo(),"剩餘上課堂數：",
                                        item.SourceContract.RemainedLessonCount(), " 堂，轉讓至此合約"),
                                FitnessConsultant = item.CourseContract.FitnessConsultant,
                                UID = item.CourseContract.CourseContractMember.Select(m => m.UID).ToArray(),

                            },
                            item.CourseContract.ContractAgent, item.SourceContract.LessonPriceType);
                        contract.CourseContractExtension.RevisionTrackingID = item.RevisionID;

                        ///4.回存轉讓餘額
                        ///
                        if(balancedPayment!=null)
                        {
                            balancedPayment = new Payment
                            {
                                Status = (int)Naming.CourseContractStatus.已生效,
                                ContractPayment = new ContractPayment
                                {
                                    CourseContract = contract
                                },
                                PaymentTransaction = new PaymentTransaction
                                {
                                    BranchID = item.SourceContract.CourseContractExtension.BranchID
                                },
                                PaymentAudit = new Models.DataEntity.PaymentAudit { },
                                PayoffAmount = balance,
                                PayoffDate = DateTime.Today,
                                Remark = "餘額轉讓",
                                HandlerID = item.CourseContract.AgentID,
                                PaymentType = "現金",
                                TransactionType = (int)Naming.PaymentTransactionType.體能顧問費,
                                InvoiceID = balancedPayment.InvoiceID
                            };
                            models.GetTable<Payment>().InsertOnSubmit(balancedPayment);

                            models.GetTable<ContractTrustTrack>().InsertOnSubmit(new ContractTrustTrack
                            {
                                ContractID = contract.ContractID,
                                EventDate = balancedPayment.PayoffDate.Value,
                                Payment = balancedPayment,
                                TrustType = Naming.TrustType.T.ToString()
                            });

                            models.SubmitChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("轉讓失敗:\r\n" + ex);
                }
            });
        }

        public static void ProcessContractMigration(this CourseContractRevision item)
        {
            ThreadPool.QueueUserWorkItem(t =>
            {
                try
                {
                    using (var models = new ModelSource<UserProfile>())
                    {
                        item = models.GetTable<CourseContractRevision>()
                            .Where(r => r.RevisionID == item.RevisionID).First();
                        ///1.變更狀態
                        ///
                        item.SourceContract.Status = (int)Naming.CourseContractStatus.已轉點;
                        foreach (var lesson in item.SourceContract.RegisterLessonContract)
                        {
                            lesson.RegisterLesson.Attended = (int)Naming.LessonStatus.課程結束;
                        }
                        ///2.回沖繳款餘額
                        ///
                        var balance = item.SourceContract.TotalPaidAmount() - item.SourceContract.TotalCost / item.SourceContract.Lessons * (item.SourceContract.Lessons - item.SourceContract.RemainedLessonCount());
                        Payment balancedPayment = null;
                        if (balance > 0)
                        {
                            var dummyInvoice = models.GetTable<InvoiceItem>().Where(i => i.No == "--").FirstOrDefault();

                            balancedPayment = new Payment
                            {
                                Status = (int)Naming.CourseContractStatus.已生效,
                                ContractPayment = new ContractPayment { },
                                PaymentTransaction = new PaymentTransaction
                                {
                                    BranchID = item.SourceContract.CourseContractExtension.BranchID
                                },
                                PaymentAudit = new Models.DataEntity.PaymentAudit { },
                                PayoffAmount = -balance,
                                PayoffDate = DateTime.Today,
                                Remark = "繳款餘額沖銷",
                                HandlerID = item.CourseContract.AgentID,
                                PaymentType = "現金",
                                TransactionType = (int)Naming.PaymentTransactionType.合約轉點沖銷
                            };
                            balancedPayment.ContractPayment.ContractID = item.OriginalContract.Value;
                            if (dummyInvoice != null)
                                balancedPayment.InvoiceID = dummyInvoice.InvoiceID;
                            models.GetTable<Payment>().InsertOnSubmit(balancedPayment);
                        }

                        models.SubmitChanges();

                        ///3.產生新合約
                        ///
                        var contract = models.InitiateCourseContract(
                            new CourseContractViewModel
                            {
                                ContractType = item.SourceContract.ContractType,
                                Subject = item.SourceContract.Subject,
                                OwnerID = item.CourseContract.CourseContractMember.First().UID,
                                Lessons = item.SourceContract.RemainedLessonCount(),
                                PriceID = item.CourseContract.PriceID,
                                Remark = String.Concat("原合約編號 ", item.SourceContract.ContractNo(), "剩餘上課堂數：",
                                        item.SourceContract.RemainedLessonCount(), " 堂(",item.SourceContract.LessonPriceType.ListPrice,"元)，轉點至此合約。"),
                                FitnessConsultant = item.CourseContract.FitnessConsultant,
                                UID = item.CourseContract.CourseContractMember.Select(m => m.UID).ToArray(),

                            },
                            item.CourseContract.ContractAgent, item.CourseContract.LessonPriceType);
                        contract.CourseContractExtension.RevisionTrackingID = item.RevisionID;

                        ///4.回存轉讓餘額
                        ///
                        if (balancedPayment != null)
                        {
                            balancedPayment = new Payment
                            {
                                Status = (int)Naming.CourseContractStatus.已生效,
                                ContractPayment = new ContractPayment
                                {
                                    CourseContract = contract
                                },
                                PaymentTransaction = new PaymentTransaction
                                {
                                    BranchID = item.SourceContract.CourseContractExtension.BranchID
                                },
                                PaymentAudit = new Models.DataEntity.PaymentAudit { },
                                PayoffAmount = balance,
                                PayoffDate = DateTime.Today,
                                Remark = "餘額轉點",
                                HandlerID = item.CourseContract.AgentID,
                                PaymentType = "現金",
                                TransactionType = (int)Naming.PaymentTransactionType.體能顧問費,
                                InvoiceID = balancedPayment.InvoiceID
                            };
                            models.GetTable<Payment>().InsertOnSubmit(balancedPayment);
                            models.SubmitChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("轉點失敗:\r\n" + ex);
                }
            });
        }

        public static void ProcessContractTermination(this CourseContractRevision item)
        {
            ThreadPool.QueueUserWorkItem(t =>
            {
                try
                {
                    using (var models = new ModelSource<UserProfile>())
                    {
                        item = models.GetTable<CourseContractRevision>()
                            .Where(r => r.RevisionID == item.RevisionID).First();
                        ///1.變更狀態
                        ///
                        item.SourceContract.Status = (int)Naming.CourseContractStatus.已終止;
                        foreach (var lesson in item.SourceContract.RegisterLessonContract)
                        {
                            lesson.RegisterLesson.Attended = (int)Naming.LessonStatus.課程結束;
                        }
                        ///2.回沖繳款餘額
                        ///
                        var original = item.SourceContract;
                        var remained = original.RemainedLessonCount();
                        var balance = original.TotalPaidAmount() - (original.Lessons - original.RemainedLessonCount()) 
                                * item.CourseContract.CourseContractExtension.SettlementPrice 
                                * original.CourseContractType.GroupingMemberCount
                                * original.CourseContractType.GroupingLessonDiscount.PercentageOfDiscount / 100;
                        Payment balancedPayment = null;
                        if (balance > 0)
                        {
                            var dummyInvoice = models.GetTable<InvoiceItem>().Where(i => i.No == "--").FirstOrDefault();

                            balancedPayment = new Payment
                            {
                                Status = (int)Naming.CourseContractStatus.已生效,
                                ContractPayment = new ContractPayment { },
                                PaymentTransaction = new PaymentTransaction
                                {
                                    BranchID = item.SourceContract.CourseContractExtension.BranchID
                                },
                                PaymentAudit = new Models.DataEntity.PaymentAudit { },
                                PayoffAmount = -balance,
                                PayoffDate = DateTime.Today,
                                Remark = "繳款餘額沖銷",
                                HandlerID = item.CourseContract.AgentID,
                                PaymentType = "現金",
                                TransactionType = (int)Naming.PaymentTransactionType.合約終止沖銷
                            };
                            balancedPayment.ContractPayment.ContractID = item.OriginalContract.Value;
                            if (dummyInvoice != null)
                                balancedPayment.InvoiceID = dummyInvoice.InvoiceID;
                            models.GetTable<Payment>().InsertOnSubmit(balancedPayment);

                            models.GetTable<ContractTrustTrack>().InsertOnSubmit(new ContractTrustTrack
                            {
                                ContractID = item.OriginalContract.Value,
                                EventDate = balancedPayment.PayoffDate.Value,
                                Payment = balancedPayment,
                                TrustType = Naming.TrustType.S.ToString()
                            });
                        }

                        models.SubmitChanges();

                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("終止失敗:\r\n" + ex);
                }
            });
        }


    }
}