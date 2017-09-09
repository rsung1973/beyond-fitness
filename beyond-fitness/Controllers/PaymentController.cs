using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

using CommonLib.MvcExtension;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Security.Authorization;
using WebHome.Properties;

namespace WebHome.Controllers
{
    [Authorize]
    public class PaymentController : SampleController<UserProfile>
    {
        // GET: Payment
        public ActionResult PaymentIndex()
        {
            return View();
        }
        public ActionResult QueryIndex()
        {
            return View();
        }
        public ActionResult AchievementIndex()
        {
            return View();
        }

        [CoachOrAssistantAuthorize]
        public ActionResult EditPayment(PaymentViewModel viewModel)
        {
            switch ((Naming.PaymentTransactionType?)viewModel.TransactionType)
            {
                case Naming.PaymentTransactionType.自主訓練:
                    return EditPaymentForPISession(viewModel);
                case Naming.PaymentTransactionType.運動商品:
                case Naming.PaymentTransactionType.飲品:
                    return EditPaymentForShopping(viewModel);
                case Naming.PaymentTransactionType.體能顧問費:
                default:
                    return EditPaymentForContract(viewModel);
            }
        }

        public ActionResult LoadMerchandiseOptions(PaymentViewModel viewModel)
        {
            var items = models.GetTable<MerchandiseTransaction>().Where(m => m.TransactionID == viewModel.TransactionType)
                .Select(m => m.MerchandiseWindow)
                .Where(p => p.Status == (int)Naming.MerchandiseStatus.OnSale);
            return View("~/Views/SystemInfo/MerchandiseOptions.ascx", items);
        }

        [CoachOrAssistantAuthorize]
        public ActionResult EditPaymentForPISession(PaymentViewModel viewModel)
        {
            ViewResult result = (ViewResult)EditPaymentForContract(viewModel);
            result.ViewName = "~/Views/Payment/Module/EditPaymentForPISession.ascx";

            return result;
        }

        [CoachOrAssistantAuthorize]
        public ActionResult EditPaymentForShopping(PaymentViewModel viewModel)
        {
            ViewResult result = (ViewResult)EditPaymentForContract(viewModel);
            result.ViewName = "~/Views/Payment/Module/EditPaymentForShopping.ascx";

            return result;
        }


        [CoachOrAssistantAuthorize]
        public ActionResult EditPaymentForContract(PaymentViewModel viewModel)
        {
            var item = models.GetTable<Payment>().Where(c => c.PaymentID == viewModel.PaymentID).FirstOrDefault();
            if (item != null)
            {
                viewModel.PayoffAmount = item.PayoffAmount;
                viewModel.PayoffDate = item.PayoffDate;
                viewModel.Status = item.Status;
                viewModel.HandlerID = item.HandlerID;
                viewModel.PaymentType = item.PaymentType;
                viewModel.InvoiceID = item.InvoiceID;
                viewModel.TransactionType = item.TransactionType;
                viewModel.BuyerReceiptNo = item.InvoiceItem.InvoiceBuyer.IsB2C() ? null : item.InvoiceItem.InvoiceBuyer.ReceiptNo;
                viewModel.Remark = item.Remark;
                viewModel.InvoiceNo = item.InvoiceItem.TrackCode + item.InvoiceItem.No;
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Payment/Module/EditPaymentForContract.ascx", item);
        }

        public ActionResult CommitPaymentForContract(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            CourseContract contract = null;
            viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
            if (viewModel.ContractNo == null)
            {
                ModelState.AddModelError("ContractNo", "請輸入合約編號!!");
            }
            else
            {
                var no = viewModel.ContractNo.Split('-');
                int seqNo = 0;
                if (no.Length > 1)
                {
                    int.TryParse(no[1], out seqNo);
                }
                contract = models.GetTable<CourseContract>()
                    .Where(c => c.ContractNo == no[0] && c.SequenceNo == seqNo)
                    .Where(c => c.Status >= (int)Naming.CourseContractStatus.已生效)
                    .FirstOrDefault();

                if (contract == null)
                {
                    ModelState.AddModelError("ContractNo", "合約編號錯誤!!");
                }
            }

            if (!viewModel.PayoffDate.HasValue)
            {
                ModelState.AddModelError("PayoffDate", "請選擇收款日期!!");
            }

            var invoice = checkInvoiceNo(viewModel);

            if (!viewModel.PayoffAmount.HasValue || viewModel.PayoffAmount <= 0)
            {
                ModelState.AddModelError("PayoffAmount", "請輸入收款金額!!");
            }

            if (!viewModel.SellerID.HasValue)
            {
                ModelState.AddModelError("SellerID", "請選擇分店!!");
            }

            if (contract!=null && contract.ContractPayment.Where(p => p.Payment.VoidPayment == null)
                .Sum(p => p.Payment.PayoffAmount) + viewModel.PayoffAmount > contract.TotalCost)
            {
                ModelState.AddModelError("PayoffAmount", "含本次收款金額已大於總收費金額!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            try
            {
                Payment item = models.GetTable<Payment>()
                    .Where(p => p.PaymentID == viewModel.PaymentID).FirstOrDefault();

                if (item == null)
                {
                    item = new Payment
                    {
                        Status = (int)Naming.CourseContractStatus.已生效,
                        ContractPayment = new ContractPayment { },
                        PaymentTransaction = new PaymentTransaction
                        { },
                        PaymentAudit = new Models.DataEntity.PaymentAudit { }
                    };
                    models.GetTable<Payment>().InsertOnSubmit(item);
                    item.InvoiceItem = invoice;
                }

                preparePayment(viewModel, profile, item);

                item.ContractPayment.ContractID = contract.ContractID;
                item.PaymentTransaction.BranchID = contract.LessonPriceType.BranchID.Value;

                models.SubmitChanges();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }

        private void preparePayment(PaymentViewModel viewModel, UserProfile profile, Payment item)
        {
            item.PayoffAmount = viewModel.PayoffAmount;
            item.PayoffDate = viewModel.PayoffDate;
            item.Remark = viewModel.Remark;
            item.HandlerID = profile.UID;
            item.PaymentType = viewModel.PaymentType;
            if (item.InvoiceItem == null)
            {
                item.InvoiceItem = createPaperInvoice(viewModel);
            }
            item.TransactionType = viewModel.TransactionType;
            item.InvoiceItem.InvoiceType = (byte)viewModel.InvoiceType;
            item.InvoiceItem.InvoiceBuyer.ReceiptNo = viewModel.BuyerReceiptNo;
            item.InvoiceItem.InvoiceAmountType.TotalAmount = viewModel.PayoffAmount;
        }

        public ActionResult CommitPaymentForPISession(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (!viewModel.RegisterID.HasValue)
            {
                ModelState.AddModelError("RegisterID", "請選擇收款自主訓練!!");
            }

            if (!viewModel.PayoffDate.HasValue)
            {
                ModelState.AddModelError("PayoffDate", "請選擇收款日期!!");
            }

            var invoice = checkInvoiceNo(viewModel);

            if (!viewModel.PayoffAmount.HasValue || viewModel.PayoffAmount <= 0)
            {
                ModelState.AddModelError("PayoffAmount", "請輸入收款金額!!");
            }

            if (!viewModel.SellerID.HasValue)
            {
                ModelState.AddModelError("SellerID", "請選擇分店!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            try
            {
                var lesson = models.GetTable<RegisterLesson>().Where(r => r.RegisterID == viewModel.RegisterID).First();

                Payment item = models.GetTable<Payment>()
                    .Where(p => p.PaymentID == viewModel.PaymentID).FirstOrDefault();

                if (item == null)
                {
                    item = new Payment
                    {
                        Status = (int)Naming.CourseContractStatus.已生效,
                        TuitionInstallment = new TuitionInstallment
                        {

                        },
                        PaymentTransaction = new PaymentTransaction
                        { },
                        PaymentAudit = new Models.DataEntity.PaymentAudit { }
                    };
                    item.TuitionAchievement.Add(new TuitionAchievement
                    {
                        CoachID = lesson.AdvisorID.Value,
                        ShareAmount = viewModel.PayoffAmount
                    });
                    models.GetTable<Payment>().InsertOnSubmit(item);
                    item.InvoiceItem = invoice;
                }
                item.TuitionInstallment.RegisterID = lesson.IntuitionCharge.RegisterID;
                item.TuitionInstallment.PayoffAmount = viewModel.PayoffAmount;
                item.TuitionInstallment.PayoffDate = viewModel.PayoffDate;

                item.PaymentTransaction.BranchID = viewModel.SellerID.Value;

                preparePayment(viewModel, profile, item);

                models.SubmitChanges();
                models.AttendLesson(lesson.LessonTime.First());
                                
                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }

        public ActionResult CommitPaymentForShopping(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (!viewModel.ProductID.HasValue)
            {
                ModelState.AddModelError("RegisterID", "請選擇品項!!");
            }

            if (!viewModel.ProductCount.HasValue)
            {
                ModelState.AddModelError("RegisterID", "請輸入數量!!");
            }

            if (!viewModel.PayoffDate.HasValue)
            {
                ModelState.AddModelError("PayoffDate", "請選擇收款日期!!");
            }

            var invoice = checkInvoiceNo(viewModel);

            if (!viewModel.PayoffAmount.HasValue || viewModel.PayoffAmount <= 0)
            {
                ModelState.AddModelError("PayoffAmount", "請輸入收款金額!!");
            }

            if (!viewModel.SellerID.HasValue)
            {
                ModelState.AddModelError("SellerID", "請選擇分店!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            try
            {

                Payment item = models.GetTable<Payment>()
                    .Where(p => p.PaymentID == viewModel.PaymentID).FirstOrDefault();

                if (item == null)
                {
                    item = new Payment
                    {
                        Status = (int)Naming.CourseContractStatus.已生效,
                        PaymentTransaction = new PaymentTransaction
                        { },
                        PaymentAudit = new Models.DataEntity.PaymentAudit { }
                    };
                    models.GetTable<Payment>().InsertOnSubmit(item);
                    item.InvoiceItem = invoice;
                }
                else
                {
                    models.DeleteAllOnSubmit<PaymentOrder>(p => p.PaymentID == item.PaymentID);
                }

                item.PaymentTransaction.BranchID = viewModel.SellerID.Value;
                item.PaymentTransaction.PaymentOrder.Add(new PaymentOrder
                {
                    ProductID = viewModel.ProductID.Value,
                    ProductCount = viewModel.ProductCount.Value
                });

                preparePayment(viewModel, profile, item);

                models.SubmitChanges();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }

        private InvoiceItem checkInvoiceNo(PaymentViewModel viewModel)
        {
            String trackCode, no;
            viewModel.InvoiceNo = viewModel.InvoiceNo.GetEfficientString();
            if (viewModel.InvoiceType != Naming.InvoiceTypeDefinition.一般稅額計算之電子發票)
            {
                if (viewModel.InvoiceNo == null || !Regex.IsMatch(viewModel.InvoiceNo, "[A-Za-z]{2}[0-9]{8}"))
                {
                    ModelState.AddModelError("InvoiceNo", "請輸入正確發票號碼!!");
                }
                else
                {
                    trackCode = viewModel.InvoiceNo.Substring(0, 2).ToUpper();
                    no = viewModel.InvoiceNo.Substring(2);
                    var invoice = models.GetTable<InvoiceItem>().Where(i => i.TrackCode == trackCode && i.No == no).FirstOrDefault();
                    if (invoice != null && invoice.Payment.Any(p => p.VoidPayment != null))
                    {
                        ModelState.AddModelError("InvoiceNo", "發票號碼重複!!");
                    }
                    return invoice;
                }
            }
            return null;
        }

        protected InvoiceItem createPaperInvoice(PaymentViewModel viewModel)
        {
            InvoiceItem item;
            item = new InvoiceItem
            {
                Document = new Document
                {
                    DocDate = DateTime.Now,
                    DocType = (int)Naming.DocumentTypeDefinition.E_Invoice
                },
                InvoiceType = (byte)Naming.InvoiceTypeDefinition.二聯式,
                SellerID = viewModel.SellerID.Value,
                InvoiceSeller = new InvoiceSeller
                {
                },
                InvoiceBuyer = new InvoiceBuyer { },
                InvoiceAmountType = new InvoiceAmountType
                {
                }
            };

            if (viewModel.InvoiceType != Naming.InvoiceTypeDefinition.一般稅額計算之電子發票)
            {
                item.TrackCode = viewModel.InvoiceNo.Substring(0, 2).ToUpper();
                item.No = viewModel.InvoiceNo.Substring(2);
            }

            return item;
        }

        public ActionResult ListUnpaidPISession(int? branchID)
        {
            var profile = HttpContext.GetUser();
            var items = models.GetTable<LessonTime>().Where(r => r.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自主訓練)
                .Where(r => r.RegisterLesson.IntuitionCharge.TuitionInstallment.Count == 0
                    || !r.RegisterLesson.IntuitionCharge.TuitionInstallment.Any(t => t.Payment.VoidPayment == null || t.Payment.VoidPayment.Status != (int)Naming.CourseContractStatus.已生效));

            if (branchID.HasValue)
            {
                items = items.Where(r => r.BranchID == branchID);
            }

            return View("~/Views/Payment/Module/UnpaidPISession.ascx", items);
        }

        public ActionResult PaymentAuditSummary()
        {
            var profile = HttpContext.GetUser();
            if (profile.IsAssistant() || profile.IsManager() || profile.IsViceManager())
            {
                return View("~/Views/Payment/Module/PaymentAuditSummary.ascx");
            }
            else
            {
                return View("~/Views/Payment/Module/PaymentAuditSummaryCoachView.ascx");
            }
        }

        public ActionResult ListPaymentByInvoice(PaymentViewModel viewModel)
        {
            IQueryable<Payment> items = models.GetTable<Payment>();
            if (viewModel.InvoiceNo == null || !Regex.IsMatch(viewModel.InvoiceNo, "[A-Za-z]{2}[0-9]{8}"))
            {
                ModelState.AddModelError("InvoiceNo", "請輸入正確發票號碼!!");
            }

            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            string trackCode = viewModel.InvoiceNo.Substring(0, 2).ToUpper();
            string no = viewModel.InvoiceNo.Substring(2);

            items = items
                .Where(p=>p.Status==(int)Naming.CourseContractStatus.已生效)
                .Where(p => p.InvoiceItem.TrackCode == trackCode && p.InvoiceItem.No == no);

            if (items.Count(p => p.VoidPayment != null) > 0)
            {
                ModelState.AddModelError("InvoiceNo", "發票已作廢或作廢審核中!!");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            return View("~/Views/Payment/Module/PaymentQueryList.ascx", items);
        }

        public ActionResult ListContractByContractNo(PaymentViewModel viewModel)
        {
            IQueryable<CourseContract> items = models.GetTable<CourseContract>()
                    .Where(c => c.Status == (int)Naming.CourseContractStatus.已生效);

            viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
            if (viewModel.ContractNo != null)
            {
                var no = viewModel.ContractNo.Split('-');
                int seqNo = 0;
                if (no.Length > 1)
                {
                    int.TryParse(no[1], out seqNo);
                }
                items = items.Where(c => c.ContractNo == no[0]
                    && c.SequenceNo == seqNo);
            }
            else
            {
                items = items.Where(c => false);
            }

            return View("~/Views/Payment/Module/ContractQueryList.ascx", items);

        }


        public ActionResult AuditPayment(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();

            IQueryable<Payment> items = models.GetPaymentToAuditByAgent(profile)
                .Select(a => a.Payment)
                .Where(p => p.TransactionType == viewModel.TransactionType);

            ViewBag.ViewModel = viewModel;

            return View("~/Views/Payment/Module/AuditPayment.ascx", items);
        }

        public ActionResult ApproveToVoidPayment(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();

            IQueryable<Payment> items = models.GetVoidPaymentToApproveByAgent(profile)
                .Select(a => a.Payment)
                .Where(p => p.TransactionType == viewModel.TransactionType);

            ViewBag.ViewModel = viewModel;
            ViewBag.ViewAction = "待審核";

            return View("~/Views/Payment/Module/ApproveToVoidPayment.ascx", items);
        }

        public ActionResult EditToVoidPayment(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();

            IQueryable<Payment> items = models.GetVoidPaymentToEditByAgent(profile)
                .Select(a => a.Payment)
                .Where(p => p.TransactionType == viewModel.TransactionType);

            ViewBag.ViewModel = viewModel;
            ViewBag.ViewAction = "草稿";

            return View("~/Views/Payment/Module/ApproveToVoidPayment.ascx", items);
        }



        public ActionResult ApproveToVoidPaymentView(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();

            var items = models.GetTable<Payment>().Where(v => v.PaymentID == viewModel.PaymentID)
                            .Join(models.GetTable<Payment>(), p => p.InvoiceID, v => v.InvoiceID, (p, v) => v);
            var item = items.Select(p => p.VoidPayment).FirstOrDefault();

            if (item != null)
            {
                viewModel.Remark = item.Remark;
                viewModel.Status = item.Status;
            }

            ViewBag.ViewModel = viewModel;

            return View("~/Views/Payment/Module/VoidPaymentApprovalView.ascx", items);
        }

        public ActionResult EditToVoidPaymentView(PaymentViewModel viewModel)
        {
            ViewResult result = (ViewResult)ApproveToVoidPaymentView(viewModel);
            result.ViewName = "~/Views/Payment/Module/VoidPaymentEditingView.ascx";
            return result;
        }

        public ActionResult ExecuteVoidPaymentStatus(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var items = models.GetTable<Payment>().Where(v => v.PaymentID == viewModel.PaymentID)
                            .Join(models.GetTable<Payment>(), p => p.InvoiceID, v => v.InvoiceID, (p, v) => v)
                        .Select(p => p.VoidPayment);

            if (items.Count() > 0)
            {
                try
                {
                    foreach (var item in items)
                    {
                        item.VoidPaymentLevel.Add(new VoidPaymentLevel
                        {
                            ExecutorID = profile.UID,
                            LevelDate = DateTime.Now,
                            LevelID = viewModel.Status.Value
                        });
                        item.Status = viewModel.Status.Value;
                        item.Drawback = viewModel.Drawback;
                        if (viewModel.Status == (int)Naming.CourseContractStatus.待審核)
                        {
                            item.Remark = viewModel.Remark;
                        }

                    }

                    models.SubmitChanges();

                    if (viewModel.Status == (int)Naming.CourseContractStatus.已生效)
                    {
                        foreach (var item in items)
                        {
                            withdrawAttendance(item);
                        }
                    }

                    return Json(new { result = true });
                }
                catch(Exception ex)
                {
                    Logger.Error(ex);
                    return Json(new { result = false, message = ex.Message });
                }
            }

            return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
        }

        private void withdrawAttendance(VoidPayment item)
        {
            if (item.Payment.TransactionType == (int)Naming.PaymentTransactionType.自主訓練)
            {
                models.ExecuteCommand(@"
                            DELETE FROM LessonAttendance
                            FROM     LessonAttendance INNER JOIN
                                           LessonTime ON LessonAttendance.LessonID = LessonTime.LessonID
                            WHERE   (LessonTime.RegisterID = {0})", item.Payment.TuitionInstallment.IntuitionCharge.RegisterLesson.RegisterID);
            }
        }

        public ActionResult CancelVoidPayment(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var items = models.GetTable<Payment>().Where(v => v.PaymentID == viewModel.PaymentID)
                            .Join(models.GetTable<Payment>(), p => p.InvoiceID, v => v.InvoiceID, (p, v) => v);

            if (items.Count() > 0)
            {
                try
                {
                    foreach (var item in items.ToList())
                    {
                        models.DeleteAnyOnSubmit<InvoiceCancellation>(c => c.InvoiceID == item.InvoiceID);
                        models.DeleteAnyOnSubmit<Document>(d => models.GetTable<DerivedDocument>()
                            .Where(r => r.SourceID == item.InvoiceID)
                            .Select(r => r.DocID).Contains(d.DocID));
                        models.DeleteAnyOnSubmit<VoidPayment>(v => v.VoidID == item.PaymentID);
                        models.SubmitChanges();
                    }
                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return Json(new { result = false, message = ex.Message });
                }
            }

            return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
        }


        public ActionResult VoidPayment(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View("~/Views/Payment/Module/VoidPayment.ascx");
        }

        public ActionResult CommitToVoidPayment(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (viewModel.VoidID==null || viewModel.VoidID.Length==0)
            {
                ModelState.AddModelError("InvoiceNo", "發票資料無效!!");
            }

            try
            {
                List<VoidPayment> items = new List<VoidPayment>();
                foreach (var v in viewModel.VoidID)
                {
                    var item = models.GetTable<Payment>().Where(p => p.PaymentID == v).FirstOrDefault();
                    if (item.VoidPayment == null)
                    {
                        if (item.InvoiceID.HasValue && item.InvoiceItem.InvoiceCancellation == null)
                        {
                            cancelInvoice(item.InvoiceItem);
                        }

                        item.VoidPayment = new Models.DataEntity.VoidPayment
                        {
                            HandlerID = profile.UID,
                            Remark = viewModel.Remark,
                            Status = (int)Naming.CourseContractStatus.待審核,
                            VoidDate = DateTime.Now
                        };

                        if (profile.IsManager())
                        {
                            item.VoidPayment.Status = (int)Naming.CourseContractStatus.已生效;
                        }
                        items.Add(item.VoidPayment);
                    }
                }

                if (items.Count>0)
                {
                    models.SubmitChanges();

                    if (profile.IsManager())
                    {
                        foreach (var item in items)
                        {
                            withdrawAttendance(item);
                        }
                        return Json(new { result = true, message = "作廢收款資料已生效!!" });
                    }
                    return Json(new { result = true, message = "作廢收款資料已送交審核!!" });
                }
                else
                    return Json(new { result = false, message = "作廢收款資料錯誤!!" });

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }

        [CoachOrAssistantAuthorize]
        public ActionResult CommitToAuditPayment(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            try
            {
                Payment item = models.GetTable<Payment>()
                    .Where(p => p.PaymentID == viewModel.PaymentID).FirstOrDefault();

                if (item == null)
                {
                    return Json(new { result = false,message="收款資料錯誤!!" });
                }

                item.PaymentAudit.AuditDate = DateTime.Now;
                item.PaymentAudit.AuditorID = profile.UID;

                models.SubmitChanges();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }

        private void cancelInvoice(InvoiceItem invoice)
        {
            invoice.InvoiceCancellation = new InvoiceCancellation
            {
                InvoiceItem = invoice,
                CancellationNo = invoice.TrackCode + invoice.No,
                Remark = "作廢收款",
                //ReturnTaxDocumentNo = invoice.ReturnTaxDocumentNumber,
                CancelDate = DateTime.Now
            };

            var doc = new DerivedDocument
            {
                Document = new Document
                {
                    DocType = (int)Naming.DocumentTypeDefinition.E_InvoiceCancellation,
                    DocDate = DateTime.Now
                },
                SourceID = invoice.InvoiceID
            };

            models.GetTable<DerivedDocument>().InsertOnSubmit(doc);
        }

        public ActionResult InquirePayment(PaymentQueryViewModel viewModel)
        {
            IQueryable<Payment> items = models.GetTable<Payment>()
                .Where(p => p.Status.HasValue);

            var profile = HttpContext.GetUser();

            Expression<Func<Payment, bool>> queryExpr = c => false;
            bool hasConditon = false;

            viewModel.UserName = viewModel.UserName.GetEfficientString();
            if (viewModel.UserName != null)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.ContractPayment.CourseContract.CourseContractMember.Any(m => m.UserProfile.RealName.Contains(viewModel.UserName) || m.UserProfile.Nickname.Contains(viewModel.UserName)));
                queryExpr = queryExpr.Or(c => c.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName));
                queryExpr = queryExpr.Or(c => c.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.Nickname.Contains(viewModel.UserName));
            }

            if(!hasConditon)
            {
                viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
                if (viewModel.ContractNo != null)
                {
                    hasConditon = true;
                    var no = viewModel.ContractNo.Split('-');
                    int seqNo;
                    if (no.Length > 1)
                    {
                        int.TryParse(no[1], out seqNo);
                        queryExpr = queryExpr.Or(c => c.ContractPayment.CourseContract.ContractNo.StartsWith(no[0])
                            && c.ContractPayment.CourseContract.SequenceNo == seqNo);
                    }
                    else
                    {
                        queryExpr = queryExpr.Or(c => c.ContractPayment.CourseContract.ContractNo.StartsWith(viewModel.ContractNo));
                    }
                }
            }

            if(!hasConditon)
            {
                if (viewModel.BranchID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.PaymentTransaction.BranchID == viewModel.BranchID);
                }
            }

            if(!hasConditon)
            {
                if (viewModel.HandlerID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.HandlerID == viewModel.HandlerID);
                }
            }

            if (!hasConditon)
            {
                if (profile.IsAssistant())
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

            if (viewModel.Status.HasValue)
            {
                //items = items.Where(c => c.Status == viewModel.Status);
                //items = items.Where(c => c.VoidPayment.Status == viewModel.Status);
                items = items.Where(c => c.Status == viewModel.Status
                    || c.VoidPayment.Status == viewModel.Status);
            }

            if (viewModel.InvoiceType.HasValue)
                items = items.Where(c => c.InvoiceItem.InvoiceType == (byte)viewModel.InvoiceType);

            if (viewModel.IsCancelled == true)
            {
                if (!viewModel.Status.HasValue || viewModel.Status == (int)Naming.CourseContractStatus.已生效)
                    items = items.Where(c => c.InvoiceItem.InvoiceCancellation != null);
                else
                    items = items.Where(f => false);
            }
            else if (viewModel.IsCancelled == false)
            {
                if (viewModel.Status.HasValue && viewModel.Status != (int)Naming.CourseContractStatus.已生效)
                    items = items.Where(f => false);
                //items = items.Where(c => c.InvoiceItem.InvoiceCancellation == null);
            }

            viewModel.InvoiceNo = viewModel.InvoiceNo.GetEfficientString();
            if (viewModel.InvoiceNo != null && Regex.IsMatch(viewModel.InvoiceNo, "[A-Za-z]{2}[0-9]{8}"))
            {
                String trackCode = viewModel.InvoiceNo.Substring(0, 2).ToUpper();
                String no = viewModel.InvoiceNo.Substring(2);
                items = items.Where(c => c.InvoiceItem.TrackCode == trackCode
                    && c.InvoiceItem.No == no);
            }

            viewModel.BuyerReceiptNo = viewModel.BuyerReceiptNo.GetEfficientString();
            if(viewModel.BuyerReceiptNo!=null)
            {
                items = items.Where(c => c.InvoiceItem.InvoiceBuyer.ReceiptNo == viewModel.BuyerReceiptNo);
            }

            if (viewModel.PayoffDateFrom.HasValue)
                items = items.Where(c => c.PayoffDate >= viewModel.PayoffDateFrom);

            if (viewModel.PayoffDateTo.HasValue)
                items = items.Where(c => c.PayoffDate < viewModel.PayoffDateTo.Value.AddDays(1));

            return View("~/Views/Payment/Module/PaymentList.ascx", items);

        }

        public ActionResult InquirePaymentForAchievement(PaymentQueryViewModel viewModel)
        {
            IQueryable<Payment> items = models.GetTable<Payment>()
                .Where(p => p.Status.HasValue)
                .Where(p => p.TransactionType == (int)Naming.PaymentTransactionType.自主訓練
                    || p.TransactionType == (int)Naming.PaymentTransactionType.體能顧問費)
                .Where(c => c.InvoiceItem.InvoiceCancellation == null);

            
            var profile = HttpContext.GetUser();

            Expression<Func<Payment, bool>> queryExpr = c => false;
            bool hasConditon = false;

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
                    int seqNo;
                    if (no.Length > 1)
                    {
                        int.TryParse(no[1], out seqNo);
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
                }
            }

            if (!hasConditon)
            {
                if (profile.IsAssistant())
                {

                }
                else if (profile.IsManager())
                {
                    var branches = models.GetTable<BranchStore>().Where(b => b.ManagerID == profile.UID || b.ViceManagerID == profile.UID);
                    items = items
                        .Join(models.GetTable<PaymentTransaction>()
                            .Join(branches, p => p.BranchID, b => b.BranchID, (p, b) => p),
                            c => c.PaymentID, h => h.PaymentID, (c, h) => c);
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

            if (viewModel.PayoffDateFrom.HasValue)
                items = items.Where(c => c.PayoffDate >= viewModel.PayoffDateFrom);

            if (viewModel.PayoffDateTo.HasValue)
                items = items.Where(c => c.PayoffDate < viewModel.PayoffDateTo.Value.AddDays(1));

            return View("~/Views/Payment/Module/PaymentAchievementList.ascx", items);

        }


        [CoachOrAssistantAuthorize]
        public ActionResult EditPaymentAchievement(PaymentViewModel viewModel)
        {
            var item = models.GetTable<Payment>().Where(c => c.PaymentID == viewModel.PaymentID).FirstOrDefault();
            if (item == null)
            {
                return View("~/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Payment/Module/EditPaymentAchievement.ascx", item);
        }

        public ActionResult LoadCoachAchievement(PaymentViewModel viewModel)
        {
            var item = models.GetTable<Payment>().Where(c => c.PaymentID == viewModel.PaymentID).FirstOrDefault();
            if (item == null)
            {
                return View("~/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Payment/Module/PaymentCoachAchievement.ascx", item);
        }


        [CoachOrAssistantAuthorize]
        public ActionResult DeleteCoachAchievement(PaymentViewModel viewModel)
        {
            try
            {
                var item = models.DeleteAny<TuitionAchievement>(d => d.CoachID == viewModel.CoachID && d.InstallmentID == viewModel.PaymentID);
                if (item != null)
                {
                    return Json(new { result = true });
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return Json(new { result = false,message = "資料錯誤!!" });
        }


        [CoachOrAssistantAuthorize]
        public ActionResult CommitToApplyCoachAchievement(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (!viewModel.ShareAmount.HasValue || viewModel.ShareAmount <= 0)
            {
                ModelState.AddModelError("ShareAmount", "請輸入業績分潤金額");
            }

            if (!viewModel.CoachID.HasValue)
            {
                ModelState.AddModelError("CoachID", "請選擇體能顧問");
            }

            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            try
            {
                Payment item = models.GetTable<Payment>()
                    .Where(p => p.PaymentID == viewModel.PaymentID).FirstOrDefault();

                if (item == null)
                {
                    return Json(new { result = false, message = "收款資料錯誤!!" });
                }

                if (item.TuitionAchievement.Where(t => t.CoachID != viewModel.CoachID).Sum(t => t.ShareAmount) + viewModel.ShareAmount > item.PayoffAmount)
                {
                    return Json(new { result = false, message = "所屬體能顧問業績總額大於付款金額!!" });
                }
                
                var achievement = item.TuitionAchievement.Where(t => t.CoachID == viewModel.CoachID).FirstOrDefault();

                if (achievement==null)
                {
                    achievement = new TuitionAchievement
                    {
                        CoachID = viewModel.CoachID.Value,
                        Payment = item
                    };
                    item.TuitionAchievement.Add(achievement);
                }

                achievement.ShareAmount = viewModel.ShareAmount;
                models.SubmitChanges();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }


        [CoachOrAssistantAuthorize]
        public ActionResult ApplyPaymentAchievement(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var item = models.GetTable<Payment>().Where(c => c.PaymentID == viewModel.PaymentID).FirstOrDefault();
            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Payment/Module/ApplyPaymentAchievement.ascx", item);
        }


    }
}