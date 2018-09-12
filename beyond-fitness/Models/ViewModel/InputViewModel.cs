﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHome.Models.Locale;

namespace WebHome.Models.ViewModel
{
    public class InputViewModel
    {
        public String Name { get; set; }
        public String Id { get; set; }
        public String Label { get; set; }
        public String Value { get; set; }
        public String PlaceHolder { get; set; }
        public String ErrorMessage { get; set; }
        public bool? IsValid { get; set; }
        public String InputType { get; set; }
        public Object DefaultValue { get; set; }
        public String ButtonStyle { get; set; }
        public String IconStyle { get; set; }
        public String Href { get; set; }
    }

    public class CourseContractViewModel
    {
        public int? ContractID { get; set; }
        public int? ContractType { get; set; } = 1;
        public DateTime? ContractDate { get; set; }
        public String Subject { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? Expiration { get; set; }
        public int? OwnerID { get; set; }
        public int? SequenceNo { get; set; } = 0;
        public int? Lessons { get; set; }
        public int? PriceID { get; set; }
        public String Remark { get; set; }
        public int? FitnessConsultant { get; set; }
        public int? Status { get; set; }
        public Naming.CourseContractStatus? FromStatus { get; set; }
        public int[] UID { get; set; }
        public int? BranchID { get; set; }
        public int? AgentID { get; set; }
        public String Reason { get; set; }
        public int? MonthExtension { get; set; }
        public int? RevisionID { get; set; }
        public bool? Drawback { get; set; }
        public int? SettlementPrice { get; set; }
        public bool? Renewal { get; set; }
    }

    public class CourseContractQueryViewModel : CourseContractViewModel
    {
        public CourseContractQueryViewModel()
        {
            ContractDateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            ContractDateTo = ContractDateFrom.Value.AddMonths(1).AddDays(-1);
        }
        public String RealName { get; set; }
        public DateTime? ContractDateFrom { get; set; }
        public DateTime? ContractDateTo { get; set; }
        public String ContractNo { get; set; }

    }


    public class ContractMemberViewModel : LearnerViewModel
    {
        public int? UID { get; set; }
        public String EmergencyContactPhone { get; set; }
        public String EmergencyContactPerson { get; set; }
        public String Relationship { get; set; }
        public String AdministrativeArea { get; set; }
        public String IDNo { get; set; }
        public int? OwnerID { get; set; }
        public int? ContractType { get; set; }
        public string Nickname { get; set; }

    }

    public class UserSignatureViewModel
    {
        public int? UID { get; set; }
        public String Signature { get; set; }
    }


    public class CourseContractSignatureViewModel : UserSignatureViewModel
    {
        public int? ContractID { get; set; }
        public String SignatureName { get; set; }
    }

    public class LessonPriceQueryViewModel : LessonPriceViewModel
    {
        public LessonPriceQueryViewModel()
        {
            Status = (int)Naming.LessonSeriesStatus.已停用;
        }
        public int? BranchID { get; set; }
        public int? Year { get; set; } = DateTime.Today.Year;
        public int? DurationInMinutes { get; set; } = 60;
        public int?[] ListPriceSeries { get; set; }
        public int? SeriesID { get; set; }
        public int?[] PriceSeriesID { get; set; }
        public bool?[] ReadOnly { get; set; }
    }

    public class LearnerQueryViewModel : LearnerViewModel
    {
        public String IDNo { get; set; }
        public int? UID { get; set; }
        public int? CoachID { get; set; }
        public int?[] LessonID { get; set; }
    }

    public class PaymentViewModel : InvoiceViewModel
    {
        public int? PaymentID { get; set; }
        //public int? PayoffAmount { get; set; }
        public DateTime? PayoffDate { get; set; } = DateTime.Today;
        public int? Status { get; set; }
        public int? HandlerID { get; set; }
        public string PaymentType { get; set; } //= "現金";
        public int? InvoiceID { get; set; }
        public int? TransactionType { get; set; }
        public String ContractNo { get; set; }
        //public Naming.InvoiceTypeDefinition? InvoiceType { get; set; } = Naming.InvoiceTypeDefinition.二聯式;
        //public string BuyerReceiptNo { get; set; }
        public string InvoiceNo { get; set; }
        //public string Remark { get; set; }
        //public int? SellerID { get; set; }
        public int? RegisterID { get; set; }
        public int? ProductID { get; set; }
        public int? ProductCount { get; set; } = 1;
        public int?[] VoidID { get; set; }
        public bool? Drawback { get; set; }
        public int? ShareAmount { get; set; }
        public int? CoachID { get; set; }
        public int? ContractID { get; set; }
        public bool? InvoiceNow { get; set; }
    }

    public class PaymentQueryViewModel : PaymentViewModel
    {
        public string UserName { get; set; }
        public int? BranchID { get; set; }
        public bool? IsCancelled { get; set; }
        public DateTime? PayoffDateFrom { get; set; }
        public DateTime? PayoffDateTo { get; set; }

    }

    public class AchievementQueryViewModel
    {
        public AchievementQueryViewModel()
        {
            AchievementDateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            AchievementDateTo = AchievementDateFrom;
            AchievementYearMonthTo = AchievementYearMonthFrom = String.Format("{0:yyyy/MM}", AchievementDateFrom);
        }
        public int? BranchID { get; set; }
        public int? CoachID { get; set; }
        public DateTime? AchievementDateFrom { get; set; }
        public DateTime? AchievementDateTo { get; set; }
        public String AchievementYearMonthFrom { get; set; }
        public String AchievementYearMonthTo { get; set; }
        public int?[] ByCoachID { get; set; }
        public DateTime? ClassTime { get; set; }
        public Naming.LessonQueryType? QueryType { get; set; }
        public Naming.QueryIntervalDefinition? QueryInterval { get; set; }
    }

    public class QuestionnaireQueryViewModel : AchievementQueryViewModel
    {
        public QuestionnaireQueryViewModel() : base()
        {
            AchievementDateFrom = null;
        }
        public String UserName { get; set; }
        public int? Status { get; set; }
    }

    public class TrustQueryViewModel
    {
        public TrustQueryViewModel()
        {
            TrustDateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            TrustDateTo = TrustDateFrom;
            TrustYearMonth = String.Format("{0:yyyy/MM}", TrustDateFrom);
        }
        public int? BranchID { get; set; }
        public String TrustType { get; set; }
        public DateTime? TrustDateFrom { get; set; }
        public DateTime? TrustDateTo { get; set; }
        public String TrustYearMonth { get; set; }
        public String ContractNo { get; set; }

    }

    public class EnterpriseContractViewModel
    {
        public int? ContractID { get; set; }
        public DateTime? ContractDate { get; set; }
        public String Subject { get; set; }
        public String Remark { get; set; }
        public int[] UID { get; set; }

        public int? CompanyID { get; set; }
        public String CompanyName { get; set; }
        public String ReceiptNo { get; set; }
        public int?[] EnterprisePriceID { get; set; }
        public int?[] EnterpriseLessons { get; set; }
        public int?[] EnterpriseDurationInMinutes { get; set; }
        public int?[] EnterpriseListPrice { get; set; }
        public int? TotalCost { get; set; }
        public int? BranchID { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? Expiration { get; set; }
    }

    public class EnterpriseProgramItemViewModel
    {
        public int? PriceID { get; set; }
        public int? Lessons { get; set; }
        public int? ContractType { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? ListPrice { get; set; }
        public String Description { get; set; }
    }

    public class EnterpriseGroupMemberViewModel
    {
        public int? ContractID { get; set; }
        public int? GroupUID { get; set; }
        public int? UID { get; set; }
    }

    public class InvoiceNoViewModel
    {
        public int? BranchID { get; set; }
        public short? Year { get; set; }
        public short? PeriodNo { get; set; }
        public int? IntervalID { get; set; }
        public String TrackCode { get; set; }
        public int? StartNo { get; set; }
        public int? EndNo { get; set; }
        public int?[] BookletCount { get; set; }
        public int?[] BookletBranchID { get; set; }
        public int? GroupID { get; set; }


    }

    public class InvoiceQueryViewModel
    {
        public int? HandlerID { get; set; }
        public bool? IsPrinted { get; set; }
        public int? BranchID { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public String InvoiceNo { get; set; }
        public int?[] InvoiceID { get; set; }
        public int? UID { get; set; }
        public int? DispatchStatus { get; set; }
        public int?[] AllowanceID { get; set; }
        public Naming.DocumentTypeDefinition? DocType { get; set; }
        public int? InvoiceType { get; set; }

    }

    public class AwardQueryViewModel
    {
        public string UserName { get; set; }
        public int? ActorID { get; set; }
        public int? ItemID { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

    }

    public class ExerciseGameViewModel
    {
        public int? UID { get; set; }
        public int? Status { get; set; }
        public decimal? Score { get; set; }
        public DateTime? TestDate { get; set; }
        public int? ExerciseID { get; set; }
        public int? TestID { get; set; }
    }

    public class DailyQuestionViewModel : DailyQuestionQueryViewModel
    {
        public int? SuggestionID { get; set; }
        public String Question { get; set; }
    }

    public class PromotionQueryViewModel
    {

    }

}