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
    public class AccountingController : SampleController<UserProfile>
    {
        // GET: Accounting
        public ActionResult ReceivableIndex()
        {
            return View();
        }

        public ActionResult InquireAccountsReceivable(CourseContractQueryViewModel viewModel)
        {
            IQueryable<CourseContract> items = models.GetTable<CourseContract>()
                .Where(c => c.SequenceNo == 0 && c.Status == (int)Naming.CourseContractStatus.已生效);

            var profile = HttpContext.GetUser();

            Expression<Func<CourseContract, bool>> queryExpr = c => false;
            bool hasConditon = false;

            viewModel.RealName = viewModel.RealName.GetEfficientString();
            if (viewModel.RealName != null)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.CourseContractMember.Any(m => m.UserProfile.RealName.Contains(viewModel.RealName) || m.UserProfile.Nickname.Contains(viewModel.RealName)));
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
                        queryExpr = queryExpr.Or(c => c.ContractNo.StartsWith(no[0])
                            && c.SequenceNo == seqNo);
                    }
                    else
                    {
                        queryExpr = queryExpr.Or(c => c.ContractNo.StartsWith(viewModel.ContractNo));
                    }
                }
            }


            if (!hasConditon)
            {
                if (viewModel.FitnessConsultant.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.FitnessConsultant == viewModel.FitnessConsultant);
                }
            }

            if (!hasConditon)
            {
                if (viewModel.BranchID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.CourseContractExtension.BranchID == viewModel.BranchID);
                }
            }

            if (!hasConditon)
            {
                if (profile.IsAssistant() || profile.IsAccounting())
                {

                }
                else if (profile.IsManager() || profile.IsViceManager())
                {
                    var coaches = profile.GetServingCoachInSameStore(models);
                    items = items.Join(coaches, c => c.FitnessConsultant, h => h.CoachID, (c, h) => c);
                }
                else if (profile.IsCoach())
                {
                    items = items.Where(c => c.FitnessConsultant == profile.UID);
                }
            }

            if (hasConditon)
            {
                items = items.Where(queryExpr);
            }

            return View("~/Views/Accounting/Module/AccountsReceivableList.ascx", items);
        }

        public ActionResult CreateAccountsReveivableXlsx(CourseContractQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireAccountsReceivable(viewModel);
            IQueryable<CourseContract> items = (IQueryable<CourseContract>)result.Model;

            var details = items.ToArray()
                .Select(item => new
                {
                    合約編號 = item.ContractNo(),
                    分店 = item.CourseContractExtension.BranchStore.BranchName,
                    簽約體能顧問 = item.ServingCoach.UserProfile.FullName(),
                    學員 = item.CourseContractType.IsGroup == true
                        ? String.Join("/", item.CourseContractMember.Select(m => m.UserProfile).ToArray().Select(u => u.FullName()))
                        : item.ContractOwner.FullName(),
                    合約名稱 = String.Concat(item.CourseContractType.TypeName,
                        "(", item.LessonPriceType.DurationInMinutes, " 分鐘)"),
                    生效日 = String.Format("{0:yyyy/MM/dd}", item.EffectiveDate),
                    剩餘堂數 = item.RemainedLessonCount(),
                    購買堂數 = item.Lessons,
                    應付金額 = item.TotalCost ?? 0,
                    已付金額 =  item.TotalPaidAmount() ?? 0,
                    欠款金額 =  (item.TotalCost - item.TotalPaidAmount()) ?? 0,
                    已繳期數 = item.ContractPayment.Count,
                })
                .Where(r =>  r.欠款金額 != 0);


            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Cache-control", "max-age=1");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=({1:yyyy-MM-dd HH-mm-ss}){0}", HttpUtility.UrlEncode("AccountsReceivable.xlsx"), DateTime.Now));

            using (DataSet ds = new DataSet())
            {
                DataTable table = details.ToDataTable();
                table.TableName = "應收帳款催收表";
                ds.Tables.Add(table);

                using (var xls = ds.ConvertToExcel())
                {
                    xls.SaveAs(Response.OutputStream);
                }
            }

            return new EmptyResult();
        }

        public ActionResult AchievementIndex(AchievementQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult TrustIndex(TrustQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }



        public ActionResult InquireAchievement(AchievementQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            IQueryable<LessonTime> items = models.GetTable<LessonTime>()
                //.Where(t => t.LessonAttendance != null)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自由教練預約)
                .Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.內部訓練)
                .Where(t => t.RegisterLesson.RegisterLessonEnterprise==null 
                    || t.RegisterLesson.RegisterLessonEnterprise.EnterpriseCourseContent.EnterpriseLessonType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)

                //.Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.體驗課程)
                //.Where(t => t.RegisterLesson.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.點數兌換課程)
                .Where(t => t.LessonAttendance != null || t.LessonPlan.CommitAttendance.HasValue);

            var profile = HttpContext.GetUser();

            Expression<Func<LessonTime, bool>> queryExpr = c => false;
            bool hasConditon = false;

            if (viewModel.CoachID.HasValue)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.AttendingCoach == viewModel.CoachID);
            }

            if (!hasConditon)
            {
                if (viewModel.BranchID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.RegisterLesson.LessonPriceType.BranchID == viewModel.BranchID);
                }
            }


            if (!hasConditon)
            {
                if (profile.IsAssistant() || profile.IsAccounting())
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

            if (hasConditon)
            {
                items = items.Where(queryExpr);
            }

            if (!String.IsNullOrEmpty(viewModel.AchievementYearMonthFrom))
            {
                viewModel.AchievementDateFrom = DateTime.ParseExact(viewModel.AchievementYearMonthFrom, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
                items = items.Where(c => c.ClassTime >= viewModel.AchievementDateFrom);
            }

            if (!String.IsNullOrEmpty(viewModel.AchievementYearMonthTo))
            {
                viewModel.AchievementDateTo = DateTime.ParseExact(viewModel.AchievementYearMonthTo, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
                items = items.Where(c => c.ClassTime < viewModel.AchievementDateTo.Value.AddMonths(1));
            }

            return View("~/Views/Accounting/Module/LessonAttendanceAchievementList.ascx", items);
        }

        public ActionResult InquireTuitionAchievement(AchievementQueryViewModel viewModel)
        {

            IQueryable<TuitionAchievement> items = models.GetTable<TuitionAchievement>()
                .Where(t => t.Payment.VoidPayment == null);

            var profile = HttpContext.GetUser();

            Expression<Func<TuitionAchievement, bool>> queryExpr = c => false;
            bool hasConditon = false;

            if (!hasConditon)
            {
                if (viewModel.CoachID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.CoachID == viewModel.CoachID);
                }
            }

            if (!hasConditon)
            {
                if (viewModel.BranchID.HasValue)
                {
                    hasConditon = true;
                    queryExpr = queryExpr.Or(c => c.Payment.ContractPayment.CourseContract.CourseContractExtension.BranchID == viewModel.BranchID);
                }
            }

            if (!hasConditon)
            {
                if (profile.IsAssistant() || profile.IsAccounting())
                {

                }
                else if (profile.IsManager() || profile.IsViceManager())
                {
                    var coaches = profile.GetServingCoachInSameStore(models);
                    items = items
                            .Join(coaches, p => p.CoachID, b => b.CoachID, (p, b) => p);
                }
                else if (profile.IsCoach())
                {
                    items = items.Where(p => p.CoachID == profile.UID);
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

            if (!String.IsNullOrEmpty(viewModel.AchievementYearMonthFrom))
            {
                viewModel.AchievementDateFrom = DateTime.ParseExact(viewModel.AchievementYearMonthFrom, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
                items = items.Where(c => c.Payment.PayoffDate >= viewModel.AchievementDateFrom);
            }

            if (!String.IsNullOrEmpty(viewModel.AchievementYearMonthTo))
            {
                viewModel.AchievementDateTo = DateTime.ParseExact(viewModel.AchievementYearMonthTo, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
                items = items.Where(c => c.Payment.PayoffDate < viewModel.AchievementDateTo.Value.AddMonths(1));
            }

            return View("~/Views/Accounting/Module/TuitionAchievementList.ascx", items);

        }

        public ActionResult ListAttendanceAchievement(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireAchievement(viewModel);
            result.ViewName = "~/Views/Accounting/Module/AttendanceAchievementList.ascx";
            return result;
        }

        public ActionResult ListTuitionAchievement(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireTuitionAchievement(viewModel);
            result.ViewName = "~/Views/Accounting/Module/TuitionAchievementItemList.ascx";
            return result;
        }

        public ActionResult InquireContractTrust(TrustQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (String.IsNullOrEmpty(viewModel.TrustYearMonth))
            {
                ModelState.AddModelError("TrustYearMonth", "請輸入查詢月份!!");
            }
            else
            {
                viewModel.TrustDateFrom = DateTime.ParseExact(viewModel.TrustYearMonth, "yyyy/MM", System.Globalization.CultureInfo.CurrentCulture);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            IQueryable<Settlement> items = models.GetTable<Settlement>()
                    .Where(c => c.StartDate == viewModel.TrustDateFrom);

            Expression<Func<ContractTrustTrack, bool>> queryExpr = c => false;
            bool hasConditon = false;

            if (!String.IsNullOrEmpty(viewModel.TrustType))
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.TrustType == viewModel.TrustType);
                if (viewModel.TrustType == Naming.TrustType.N.ToString())
                {
                    queryExpr = queryExpr.Or(c => c.TrustType == Naming.TrustType.V.ToString());
                }
            }

            IQueryable<ContractTrustTrack> trackItems = models.GetTable<ContractTrustTrack>();
            if (hasConditon)
            {
                trackItems = trackItems.Where(queryExpr);
            }

            ViewBag.DataItems = items;
            return View("~/Views/Accounting/Module/ContractTrustList.ascx", items.Join(trackItems, s => s.SettlementID, t => t.SettlementID, (s, t) => t));
        }

        class _TrustTrackReportItem
        {
            public String 處理代碼 { get; set; }
            public String 入會契約編號 { get; set; }
            public String 買受人證號 { get; set; }
            public String 姓名 { get; set; }
            public String 通訊地址 { get; set; }
            public String 電話 { get; set; }
            public String 轉出買受人証號 { get; set; }
            public String 當期信託金額 { get; set; }
            public int? 信託餘額 { get; set; }
            public int? 契約總價金 { get; set; }
            public String 交付信託日期 { get; set; }
            public DateTime? 契約起日 { get; set; }
            public DateTime? 契約迄日 { get; set; }
        }

        public ActionResult CreateTrustTrackXlsx(TrustQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireContractTrust(viewModel);
            IQueryable<ContractTrustTrack> items = (IQueryable<ContractTrustTrack>)result.Model;

            IQueryable<Settlement> settlementItems = (IQueryable<Settlement>)ViewBag.DataItems;
            var summary = settlementItems.ToArray()
                .Select(item => new
                {
                    信託期初金額 = item.ContractTrustSettlement.Sum(s => s.InitialTrustAmount).ToString(),
                    T_轉入 = item.ContractTrustTrack.Where(t => t.TrustType == "T").Sum(t => t.Payment.PayoffAmount).ToString(),
                    B_新增 = item.ContractTrustTrack.Where(t => t.TrustType == "B").Sum(t => t.Payment.PayoffAmount).ToString(),
                    N_返還 = String.Format("({0})", (item.ContractTrustTrack.Where(t => t.TrustType == "N").Select(t => t.LessonTime.RegisterLesson)
                        .Sum(lesson => lesson.LessonPriceType.ListPrice * lesson.GroupingMemberCount * lesson.GroupingLessonDiscount.PercentageOfDiscount / 100) ?? 0)
                        + (item.ContractTrustTrack.Where(t => t.TrustType == "V").Select(t => t.VoidPayment.Payment)
                        .Sum(p => p.PayoffAmount) ?? 0)),
                    S_終止 = String.Format("({0})", -item.ContractTrustTrack.Where(t => t.TrustType == "S").Sum(t => t.Payment.PayoffAmount)),
                    X_轉讓 = String.Format("({0})", -item.ContractTrustTrack.Where(t => t.TrustType == "X").Sum(t => t.Payment.PayoffAmount)),
                    收_付金額 = (item.ContractTrustSettlement.Sum(s => s.TotalTrustAmount) - item.ContractTrustSettlement.Sum(s => s.InitialTrustAmount)).ToString(),
                    信託期末金額 = item.ContractTrustSettlement.Sum(s => s.TotalTrustAmount).ToString(),
                });

            DataTable summaryTable = summary.ToDataTable();
            summaryTable.TableName = "彙總表(差額請領)";

            List<_TrustTrackReportItem> details = new List<_TrustTrackReportItem>();

            foreach (var item in items.GroupBy(t => t.ContractID))
            {
                var contract = models.GetTable<CourseContract>().Where(c => c.ContractID == item.Key).First();
                var settlement = models.GetTable<ContractTrustSettlement>().Where(s => s.ContractID == item.Key && s.SettlementID == item.First().SettlementID).First();

                var amt = item.Where(t => t.TrustType == "B").Sum(t => t.Payment.PayoffAmount);
                if (amt.HasValue && amt > 0)
                {
                    _TrustTrackReportItem reportItem = newReportItem(contract);
                    reportItem.處理代碼 = "B";
                    settlement.InitialTrustAmount += amt.Value;
                    reportItem.當期信託金額 = String.Format("{0:##,###,###,###}", amt);
                    reportItem.信託餘額 = settlement.InitialTrustAmount;
                    details.Add(reportItem);
                }
                amt = item.Where(t => t.TrustType == "T").Sum(t => t.Payment.PayoffAmount);
                if (amt.HasValue && amt > 0)
                {
                    _TrustTrackReportItem reportItem = newReportItem(contract);
                    reportItem.處理代碼 = "T";
                    settlement.InitialTrustAmount += amt.Value;
                    reportItem.當期信託金額 = String.Format("{0:##,###,###,###}", amt); ;
                    reportItem.信託餘額 = settlement.InitialTrustAmount;
                    reportItem.轉出買受人証號 = contract.CourseContractExtension.CourseContractRevision.SourceContract.ContractOwner.UserProfileExtension.IDNo;
                    details.Add(reportItem);
                }

                amt = item.Where(t => t.TrustType == "N").Select(t => t.LessonTime.RegisterLesson)
                         .Sum(lesson => lesson.LessonPriceType.ListPrice * lesson.GroupingMemberCount * lesson.GroupingLessonDiscount.PercentageOfDiscount / 100);
                if (amt.HasValue && amt > 0)
                {
                    _TrustTrackReportItem reportItem = newReportItem(contract);
                    reportItem.處理代碼 = "N";
                    settlement.InitialTrustAmount -= amt.Value;
                    reportItem.當期信託金額 = String.Format("({0:##,###,###,###})", amt); ;
                    reportItem.信託餘額 = settlement.InitialTrustAmount;
                    details.Add(reportItem);
                }

                amt = item.Where(t => t.TrustType == "V").Select(t => t.VoidPayment.Payment)
                         .Sum(p => p.PayoffAmount);
                if (amt.HasValue && amt > 0)
                {
                    _TrustTrackReportItem reportItem = newReportItem(contract);
                    reportItem.處理代碼 = "N";
                    settlement.InitialTrustAmount -= amt.Value;
                    reportItem.當期信託金額 = String.Format("({0:##,###,###,###})", amt); ;
                    reportItem.信託餘額 = settlement.InitialTrustAmount;
                    details.Add(reportItem);
                }

                amt = -item.Where(t => t.TrustType == "X").Sum(t => t.Payment.PayoffAmount);
                if (amt.HasValue && amt > 0)
                {
                    _TrustTrackReportItem reportItem = newReportItem(contract);
                    reportItem.處理代碼 = "X";
                    settlement.InitialTrustAmount -= amt.Value;
                    reportItem.當期信託金額 = String.Format("({0:##,###,###,###})", amt); ;
                    reportItem.信託餘額 = settlement.InitialTrustAmount;
                    details.Add(reportItem);
                }

                amt = -item.Where(t => t.TrustType == "S").Sum(t => t.Payment.PayoffAmount);
                if (amt.HasValue && amt > 0)
                {
                    _TrustTrackReportItem reportItem = newReportItem(contract);
                    reportItem.處理代碼 = "S";
                    settlement.InitialTrustAmount -= amt.Value;
                    reportItem.當期信託金額 = String.Format("({0:##,###,###,###})", amt); ;
                    reportItem.信託餘額 = settlement.InitialTrustAmount;
                    details.Add(reportItem);
                }

            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Cache-control", "max-age=1");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=({1:yyyy-MM-dd HH-mm-ss}){0}", HttpUtility.UrlEncode("TrustReport.xlsx"), DateTime.Now));

            using (DataSet ds = new DataSet())
            {
                DataTable table = details.ToDataTable();
                table.TableName = "預收款信託";
                ds.Tables.Add(table);

                ds.Tables.Add(summaryTable);

                using (var xls = ds.ConvertToExcel())
                {
                    xls.SaveAs(Response.OutputStream);
                }
            }

            return new EmptyResult();
        }

        private _TrustTrackReportItem newReportItem(CourseContract contract)
        {
            return new _TrustTrackReportItem
            {
                處理代碼 = null,
                入會契約編號 = contract.ContractNo(),
                買受人證號 = contract.ContractOwner.UserProfileExtension.IDNo,
                姓名 = contract.ContractOwner.RealName,
                通訊地址 = contract.ContractOwner.Address(),
                電話 = contract.ContractOwner.Phone,
                轉出買受人証號 = null,
                當期信託金額 = null,
                信託餘額 = null,
                契約總價金 = contract.TotalCost,
                交付信託日期 = null,
                契約起日 = contract.ValidFrom.Value.Date,
                契約迄日 = contract.Expiration.Value.Date,
            };
        }

        public ActionResult ExecuteSettlementForLastMonth(DateTime? settlementDate)
        {
            if (!settlementDate.HasValue)
            {
                settlementDate = DateTime.Today;
            }

            DateTime execDate = settlementDate.Value.AddMonths(-1);
            DateTime startDate = new DateTime(execDate.Year, execDate.Month, 1);
            models.ExecuteSettlement(startDate, startDate.AddMonths(1));

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InitializeTrust()
        {
            var items = models.GetTable<RegisterLesson>().Where(r => r.AttendedLessons > 0)
                    .Where(r => r.GroupingLesson.LessonTime.Count > 0);

            foreach(var item in items)
            {
                var firstLesson = item.GroupingLesson.LessonTime.First();
                for (int i = 0; i < item.AttendedLessons; i++)
                {
                    models.GetTable<ContractTrustTrack>().InsertOnSubmit(new ContractTrustTrack
                    {
                        ContractID = item.RegisterLessonContract.ContractID,
                        EventDate = firstLesson.ClassTime.Value,
                        LessonID = firstLesson.LessonID,
                        TrustType = "N"
                    });
                }
            }

            models.SubmitChanges();

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExecuteSettlement(DateTime? startDate,DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue )
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            models.ExecuteSettlement(startDate.Value, endDate.Value);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CreateAchievementXlsx(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireAchievement(viewModel);
            IQueryable<LessonTime> items = (IQueryable<LessonTime>)result.Model;

            var details = items.GroupBy(t => new { CoachID = t.AttendingCoach });
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("姓名", typeof(String)));
            table.Columns.Add(new DataColumn("總終點課數", typeof(int)));
            table.Columns.Add(new DataColumn("總鐘點費用", typeof(int)));
            table.Columns.Add(new DataColumn("等級", typeof(String)));
            table.Columns.Add(new DataColumn("實際抽成費用", typeof(int)));

            foreach (var item in details)
            {
                var r = table.NewRow();
                var coach = models.GetTable<ServingCoach>().Where(u => u.CoachID == item.Key.CoachID).First();
                r[0] = coach.UserProfile.RealName;
                r[1] = item.Count();
                int shares;
                r[2] = models.CalcAchievement(item, out shares);
                r[3] = coach.ProfessionalLevel.LevelName;
                r[4] = shares;
                table.Rows.Add(r);
            }

            table.TableName = "上課統計表";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Cache-control", "max-age=1");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename=({1:yyyy-MM-dd HH-mm-ss}){0}", HttpUtility.UrlEncode("AchievementReport.xlsx"), DateTime.Now));

            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(table);
                table = createTuitionAchievementXlsx(viewModel);
                ds.Tables.Add(table);
                table = createAchievementDetailsXlsx(viewModel);
                ds.Tables.Add(table);
                table = createTuitionAchievementDetailsXlsx(viewModel);
                ds.Tables.Add(table);

                using (var xls = ds.ConvertToExcel())
                {
                    if (viewModel.AchievementDateFrom.HasValue && viewModel.AchievementDateTo.HasValue)
                    {
                        xls.Worksheets.ElementAt(0).Name = "上課統計表" + String.Format("{0:yyyy-MM-dd}~{1:yyyy-MM-dd}", viewModel.AchievementDateFrom, viewModel.AchievementDateTo.Value.AddMonths(1).AddDays(-1));
                        xls.Worksheets.ElementAt(1).Name = "業績統計表" + String.Format("{0:yyyy-MM-dd}~{1:yyyy-MM-dd}", viewModel.AchievementDateFrom, viewModel.AchievementDateTo.Value.AddMonths(1).AddDays(-1));
                    }
                    xls.SaveAs(Response.OutputStream);
                }
            }

            return new EmptyResult();
        }

        private DataTable createAchievementDetailsXlsx(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireAchievement(viewModel);
            IQueryable<LessonTime> items = (IQueryable<LessonTime>)result.Model;


            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("合約編號", typeof(String)));
            table.Columns.Add(new DataColumn("體能顧問", typeof(String)));
            table.Columns.Add(new DataColumn("分店", typeof(String)));
            table.Columns.Add(new DataColumn("學員", typeof(String)));
            table.Columns.Add(new DataColumn("合約名稱", typeof(String)));
            table.Columns.Add(new DataColumn("課程單價", typeof(int)));
            table.Columns.Add(new DataColumn("全價計算堂數", typeof(int)));
            table.Columns.Add(new DataColumn("半價計算堂數", typeof(int)));

            var details = items.Where(t => t.RegisterLesson.RegisterLessonContract != null)
                .GroupBy(t => new
                {
                    t.AttendingCoach,
                    ContractID = t.RegisterLesson.RegisterLessonContract.ContractID
                });

            foreach (var item in details)
            {
                CourseContract contract = models.GetTable<CourseContract>().Where(u => u.ContractID == item.Key.ContractID).First();
                ServingCoach coach = models.GetTable<ServingCoach>().Where(c => c.CoachID == item.Key.AttendingCoach).First();

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
                table.Rows.Add(r);
            }

            table.TableName = "上課統計表-人員明細";

            return table;
        }

        private DataTable createTuitionAchievementXlsx(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireTuitionAchievement(viewModel);
            IQueryable<TuitionAchievement> items = (IQueryable<TuitionAchievement>)result.Model;

            var details = items.GroupBy(t => t.CoachID);

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("姓名", typeof(String)));
            table.Columns.Add(new DataColumn("業績金額", typeof(int)));

            foreach (var item in details)
            {
                var r = table.NewRow();
                var coach = models.GetTable<ServingCoach>().Where(u => u.CoachID == item.Key).First();
                r[0] = coach.UserProfile.RealName;
                r[1] = item.Sum(l => l.ShareAmount);
                table.Rows.Add(r);
            }

            table.TableName = "業績統計表";

            return table;
        }

        private DataTable createTuitionAchievementDetailsXlsx(AchievementQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireTuitionAchievement(viewModel);
            IQueryable<TuitionAchievement> items = (IQueryable<TuitionAchievement>)result.Model;

            var details = items.ToArray().Select(item=> new {
                發票號碼 = item.Payment.InvoiceID.HasValue 
                    ? item.Payment.InvoiceItem.TrackCode + item.Payment.InvoiceItem.No : null,
                分店 = item.Payment.PaymentTransaction.BranchStore.BranchName,
                收款人 = item.Payment.UserProfile.FullName(),
                學員 = item.Payment.TuitionInstallment != null
                        ? item.Payment.TuitionInstallment.IntuitionCharge.RegisterLesson.UserProfile.FullName()
                        : item.Payment.ContractPayment != null
                            ? item.Payment.ContractPayment.CourseContract.ContractOwner.FullName()
                            : "--",
                收款日期 = item.Payment.VoidPayment == null ? String.Format("{0:yyyy/MM/dd}", item.Payment.PayoffDate) : null,
                作廢日期 = item.Payment.VoidPayment != null ? String.Format("{0:yyyy/MM/dd}", item.Payment.VoidPayment.VoidDate) : null,
                業績金額 = item.ShareAmount,
                收款方式 = item.Payment.PaymentType,
                發票類型 = item.Payment.InvoiceID.HasValue
                        ? item.Payment.InvoiceItem.InvoiceType == (int)Naming.InvoiceTypeDefinition.一般稅額計算之電子發票
                            ? "電子發票"
                            : "紙本"
                        : "--",
                發票狀態 = item.Payment.VoidPayment == null
                        ? "已開立"
                        : item.Payment.VoidPayment.Status == (int)Naming.CourseContractStatus.已生效
                            ? "已作廢"
                            : "已開立",
                合約編號 = item.Payment.ContractPayment != null
                    ? item.Payment.ContractPayment.CourseContract.ContractNo()
                    : ((Naming.PaymentTransactionType)item.Payment.TransactionType).ToString(),
                狀態 = ((Naming.CourseContractStatus)item.Payment.Status).ToString()
                    + (item.Payment.PaymentAudit.AuditorID.HasValue ? "" : "(*)")
            });

            DataTable table = details.ToDataTable();
            table.TableName = "業績統計表-人員明細";

            return table;
        }

        public ActionResult ListPaymentHistory(CourseContractQueryViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();

            if (item == null)
            {
                return View("~/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            return View("~/Views/Accounting/Module/ContractPaymentList.ascx", item);
        }


    }
}