using System;
using System.Collections.Generic;
using System.Data;
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

using CommonLib.DataAccess;
using CommonLib.MvcExtension;
using Newtonsoft.Json;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    [Authorize]
    public class InvoiceController : SampleController<UserProfile>
    {
        // GET: Invoice
        public ActionResult InvoiceNoIndex(InvoiceNoViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult InquireInvoiceNoInterval(InvoiceNoViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            IQueryable<InvoiceNoInterval> items = models.GetTable<InvoiceNoInterval>();

            var profile = HttpContext.GetUser();
            bool hasCondition = false;

            if (viewModel.BranchID.HasValue)
            {
                items = items.Where(t => t.InvoiceTrackCodeAssignment.SellerID == viewModel.BranchID);
                hasCondition = true;
            }

            if (viewModel.Year.HasValue)
            {
                items = items.Where(t => t.InvoiceTrackCodeAssignment.InvoiceTrackCode.Year == viewModel.Year);
                hasCondition = true;
            }

            if (viewModel.PeriodNo.HasValue)
            {
                items = items.Where(t => t.InvoiceTrackCodeAssignment.InvoiceTrackCode.PeriodNo == viewModel.PeriodNo);
                hasCondition = true;
            }

            if(viewModel.IntervalID.HasValue)
            {
                if(hasCondition)
                {
                    items = items.Concat(models.GetTable<InvoiceNoInterval>().Where(t => t.IntervalID == viewModel.IntervalID));
                }
                else
                {
                    items = items.Where(t => t.IntervalID == viewModel.IntervalID);
                }
            }

            return View("~/Views/Invoice/Module/TrackCodeNoList.ascx", items);
        }

        public ActionResult EditInvoiceNoInterval(InvoiceNoViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = models.GetTable<InvoiceNoInterval>().Where(t => t.IntervalID == viewModel.IntervalID).FirstOrDefault();
            if(item!=null)
            {
                viewModel.BranchID = item.InvoiceTrackCodeAssignment.SellerID;
                viewModel.StartNo = item.StartNo;
                viewModel.EndNo = item.EndNo;
                viewModel.IntervalID = item.IntervalID;
                viewModel.TrackCode = item.InvoiceTrackCodeAssignment.InvoiceTrackCode.TrackCode;
                viewModel.Year = item.InvoiceTrackCodeAssignment.InvoiceTrackCode.Year;
                viewModel.PeriodNo = item.InvoiceTrackCodeAssignment.InvoiceTrackCode.PeriodNo;
            }
            return View("~/Views/Invoice/Module/EditInvoiceNoInterval.ascx", item);
        }

        public ActionResult DeleteInvoiceNoInterval(InvoiceNoViewModel viewModel)
        {
            try
            {
                var item = models.DeleteAny<InvoiceNoInterval>(t => t.IntervalID == viewModel.IntervalID);
                if (item == null)
                {
                    return Json(new { result = false, message = "資料錯誤!!" }, JsonRequestBehavior.AllowGet);
                }

                models.ExecuteCommand(@"DELETE FROM InvoiceTrackCode
                    WHERE   (TrackID NOT IN
                        (SELECT  TrackID
                            FROM     InvoiceNoInterval)) AND (TrackID = {0})", item.TrackID);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }
        }

        public ActionResult CommitInvoiceNoInterval(InvoiceNoViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (!viewModel.BranchID.HasValue)
            {
                ModelState.AddModelError("BranchID", "請選擇分店!!");
            }

            viewModel.TrackCode = viewModel.TrackCode.GetEfficientString();
            if (viewModel.TrackCode==null || !Regex.IsMatch(viewModel.TrackCode,"[A-Z]{2}"))
            {
                ModelState.AddModelError("TrackCode", "字軌錯誤!!");
            }

            if (!viewModel.Year.HasValue)
            {
                ModelState.AddModelError("Year", "請選擇年份!!");
            }

            if (!viewModel.PeriodNo.HasValue)
            {
                ModelState.AddModelError("PeriodNo", "請選擇期別!!");
            }

            var trackCode = models.GetTable<InvoiceTrackCode>()
                .Where(t => t.TrackCode == viewModel.TrackCode && t.Year == viewModel.Year && t.PeriodNo == viewModel.PeriodNo).FirstOrDefault();

            var table = models.GetTable<InvoiceNoInterval>();
            var item = table.Where(i => i.IntervalID == viewModel.IntervalID).FirstOrDefault();

            if (!viewModel.StartNo.HasValue || !(viewModel.StartNo >= 0 && viewModel.StartNo < 100000000))
            {
                ModelState.AddModelError("StartNo", "起號非8位整數!!");
            }
            else if (!viewModel.EndNo.HasValue || !(viewModel.EndNo >= 0 && viewModel.EndNo < 100000000))
            {
                ModelState.AddModelError("EndNo", "迄號非8位整數!!");
            }
            else if (viewModel.EndNo <= viewModel.StartNo || ((viewModel.EndNo - viewModel.StartNo + 1) % 50 != 0))
            {
                ModelState.AddModelError("StartNo", "不符號碼大小順序與差距為50之倍數原則!!");
            }
            else
            {
                if (item != null)
                {
                    if (item.InvoiceNoAssignment.Count > 0)
                    {
                        ModelState.AddModelError("StartNo", "該區間之號碼已經被使用,不可修改!!!!");
                    }
                    else if (table.Any(t => t.IntervalID != item.IntervalID && t.TrackID == item.TrackID && t.StartNo >= viewModel.EndNo && t.InvoiceNoAssignment.Count > 0
                        && t.SellerID == item.SellerID))
                    {
                        ModelState.AddModelError("StartNo", "違反序時序號原則該區段無法修改!!");
                    }
                    else if (table.Any(t => t.IntervalID != item.IntervalID && t.TrackID == item.TrackID
                        && ((t.EndNo <= viewModel.EndNo && t.EndNo >= viewModel.StartNo) || (t.StartNo <= viewModel.EndNo && t.StartNo >= viewModel.StartNo) || (t.StartNo <= viewModel.StartNo && t.EndNo >= viewModel.StartNo) || (t.StartNo <= viewModel.EndNo && t.EndNo >= viewModel.EndNo))))
                    {
                        ModelState.AddModelError("StartNo", "系統中已存在重疊的區段!!");
                    }
                }
                else
                {
                    if (trackCode != null)
                    {
                        if (table.Any(t => t.TrackID == trackCode.TrackID && t.StartNo >= viewModel.EndNo && t.InvoiceNoAssignment.Count > 0
                            && t.SellerID == viewModel.BranchID))
                        {
                            ModelState.AddModelError("StartNo", "違反序時序號原則該區段無法新增!!");
                        }
                        else if (table.Any(t => t.TrackID == trackCode.TrackID
                            && ((t.EndNo <= viewModel.EndNo && t.EndNo >= viewModel.StartNo) || (t.StartNo <= viewModel.EndNo && t.StartNo >= viewModel.StartNo) || (t.StartNo <= viewModel.StartNo && t.EndNo >= viewModel.StartNo) || (t.StartNo <= viewModel.EndNo && t.EndNo >= viewModel.EndNo))))
                        {
                            ModelState.AddModelError("StartNo", "系統中已存在重疊的區段!!");
                        }
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            if (item == null)
            {
                if(trackCode==null)
                {
                    trackCode = new InvoiceTrackCode
                    {
                        TrackCode = viewModel.TrackCode,
                        PeriodNo = viewModel.PeriodNo.Value,
                        Year = viewModel.Year.Value
                    };
                    models.GetTable<InvoiceTrackCode>().InsertOnSubmit(trackCode);
                }

                var codeAssignment = trackCode.InvoiceTrackCodeAssignment.Where(t => t.SellerID == viewModel.BranchID).FirstOrDefault();
                if (codeAssignment == null)
                {
                    codeAssignment = new InvoiceTrackCodeAssignment
                    {
                        SellerID = viewModel.BranchID.Value,
                        InvoiceTrackCode = trackCode
                    };

                    trackCode.InvoiceTrackCodeAssignment.Add(codeAssignment);
                }

                item = new InvoiceNoInterval { };
                codeAssignment.InvoiceNoInterval.Add(item);
            }

            item.StartNo = viewModel.StartNo.Value;
            item.EndNo = viewModel.EndNo.Value;

            try
            {
                models.SubmitChanges();
                return Json(new { result = true, item.IntervalID });
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message });
            }

        }


    }
}