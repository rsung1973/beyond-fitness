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
                contract = models.GetTable<CourseContract>()
                    .Where(c => c.ContractNo == viewModel.ContractNo)
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
                    item.TuitionInstallment.TuitionAchievement.Add(new TuitionAchievement
                    {
                        CoachID = lesson.AdvisorID.Value
                    });
                    models.GetTable<Payment>().InsertOnSubmit(item);
                    item.InvoiceItem = invoice;
                }
                item.TuitionInstallment.RegisterID = lesson.IntuitionCharge.RegisterID;
                item.TuitionInstallment.PayoffAmount = viewModel.PayoffAmount;
                item.TuitionInstallment.PayoffDate = viewModel.PayoffDate;
                item.TuitionInstallment.TuitionAchievement[0].ShareAmount = viewModel.PayoffAmount;

                item.PaymentTransaction.BranchID = viewModel.SellerID.Value;

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
                    //if (invoice != null && (!viewModel.PaymentID.HasValue || invoice.Payment.Any(p => p.PaymentID == viewModel.PaymentID)))
                    //{
                    //    ModelState.AddModelError("InvoiceNo", "發票號碼重複!!");
                    //}
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
                .Where(r => r.RegisterLesson.IntuitionCharge.TuitionInstallment.Count == 0);

            if (branchID.HasValue)
            {
                items = items.Where(r => r.BranchID == branchID);
            }

            return View("~/Views/Payment/Module/UnpaidPISession.ascx", items);
        }

        public ActionResult PaymentAuditSummary()
        {
            return View("~/Views/Payment/Module/PaymentAuditSummary.ascx");
        }

        public ActionResult InquirePayment(PaymentViewModel viewModel)
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

            items = items.Where(p => p.InvoiceItem.TrackCode == trackCode && p.InvoiceItem.No == no);

            return View("~/Views/Payment/Module/PaymentQueryList.ascx", items);
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

            return View("~/Views/Payment/Module/ApproveToVoidPayment.ascx", items);
        }

        public ActionResult ApproveToVoidPaymentView(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();

            var items = models.GetTable<Payment>().Where(v => v.PaymentID == viewModel.PaymentID);
            var item = items.Select(p => p.VoidPayment).FirstOrDefault();

            if (item != null)
            {
                viewModel.Remark = item.Remark;
            }

            ViewBag.ViewModel = viewModel;

            return View("~/Views/Payment/Module/VoidPaymentApprovalView.ascx", items);
        }

        public ActionResult ExecuteVoidPaymentStatus(PaymentViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var item = models.GetTable<VoidPayment>().Where(c => c.VoidID == viewModel.PaymentID).FirstOrDefault();
            if (item != null)
            {
                item.VoidPaymentLevel.Add(new VoidPaymentLevel
                {
                    ExecutorID = profile.UID,
                    LevelDate = DateTime.Now,
                    LevelID = viewModel.Status.Value
                });
                item.Status = viewModel.Status.Value;

                models.SubmitChanges();

                return Json(new { result = true });
            }
            else
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

                bool hasVoid = false;
                foreach (var v in viewModel.VoidID)
                {
                    var item = models.GetTable<Payment>().Where(p => p.PaymentID == v).FirstOrDefault();
                    if (item.VoidPayment == null)
                    {
                        if (item.InvoiceID.HasValue && item.InvoiceItem.InvoiceCancellation == null)
                        {
                            cancelInvoice(item.InvoiceItem);
                            item.VoidPayment = new Models.DataEntity.VoidPayment
                            {
                                HandlerID = profile.UID,
                                Remark = viewModel.Remark,
                                Status = (int)Naming.CourseContractStatus.待審核,
                                VoidDate = DateTime.Now
                            };

                            models.SubmitChanges();
                            hasVoid = true;
                        }
                    }
                }
                if (hasVoid)
                    return Json(new { result = true });
                else
                    return Json(new { result = false,message="作廢收款資料錯誤!!" });

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
    }
}