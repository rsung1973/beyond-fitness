using System;
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
        public int[] UID { get; set; }
        public int? BranchID { get; set; }
        public int? AgentID { get; set; }
        public String Reason { get; set; }
        public int? MonthExtension { get; set; }
        public int? RevisionID { get; set; }
        public bool? Drawback { get; set; }
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
    }

    public class PaymentViewModel
    {
        public int? PaymentID { get; set; }
        public int? PayoffAmount { get; set; }
        public DateTime? PayoffDate { get; set; } = DateTime.Today;
        public int? Status { get; set; }
        public int? HandlerID { get; set; }
        public string PaymentType { get; set; } = "現金";
        public int? InvoiceID { get; set; }
        public int? TransactionType { get; set; }
        public String ContractNo { get; set; }
        public Naming.InvoiceTypeDefinition? InvoiceType { get; set; } = Naming.InvoiceTypeDefinition.二聯式;
        public string BuyerReceiptNo { get; set; }
        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public int? SellerID { get; set; }
        public int? RegisterID { get; set; }
        public int? ProductID { get; set; }
        public int? ProductCount { get; set; } = 1;
        public int?[] VoidID { get; set; }
        public bool? Drawback { get; set; }
        public int? ShareAmount { get; set; }
        public int? CoachID { get; set; }
    }

    public class PaymentQueryViewModel : PaymentViewModel
    {
        public string UserName { get; set; }
        public int? BranchID { get; set; }
        public bool? IsCancelled { get; set; }
        public DateTime? PayoffDateFrom { get; set; }
        public DateTime? PayoffDateTo { get; set; }

    }
}