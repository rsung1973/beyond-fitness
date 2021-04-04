using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class LevelExpression
    {
        public LevelExpression()
        {
            BodyDiagnoses = new HashSet<BodyDiagnosis>();
            CourseContractLevels = new HashSet<CourseContractLevel>();
            CourseContracts = new HashSet<CourseContract>();
            Documents = new HashSet<Document>();
            LessonComments = new HashSet<LessonComment>();
            LessonFeedBacks = new HashSet<LessonFeedBack>();
            LessonPriceProperties = new HashSet<LessonPriceProperty>();
            LessonPriceSeries = new HashSet<LessonPriceSeries>();
            LessonPriceTypes = new HashSet<LessonPriceType>();
            MerchandiseWindows = new HashSet<MerchandiseWindow>();
            PDQQuestions = new HashSet<PDQQuestion>();
            PDQTypes = new HashSet<PDQType>();
            Payments = new HashSet<Payment>();
            ProfessionalLevels = new HashSet<ProfessionalLevel>();
            QuestionnaireRequests = new HashSet<QuestionnaireRequest>();
            RegisterLessons = new HashSet<RegisterLesson>();
            TrainingPlans = new HashSet<TrainingPlan>();
            UserProfiles = new HashSet<UserProfile>();
            VoidPaymentLevels = new HashSet<VoidPaymentLevel>();
            VoidPayments = new HashSet<VoidPayment>();
        }

        public int LevelID { get; set; }
        public string Expression { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BodyDiagnosis> BodyDiagnoses { get; set; }
        public virtual ICollection<CourseContractLevel> CourseContractLevels { get; set; }
        public virtual ICollection<CourseContract> CourseContracts { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<LessonComment> LessonComments { get; set; }
        public virtual ICollection<LessonFeedBack> LessonFeedBacks { get; set; }
        public virtual ICollection<LessonPriceProperty> LessonPriceProperties { get; set; }
        public virtual ICollection<LessonPriceSeries> LessonPriceSeries { get; set; }
        public virtual ICollection<LessonPriceType> LessonPriceTypes { get; set; }
        public virtual ICollection<MerchandiseWindow> MerchandiseWindows { get; set; }
        public virtual ICollection<PDQQuestion> PDQQuestions { get; set; }
        public virtual ICollection<PDQType> PDQTypes { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<ProfessionalLevel> ProfessionalLevels { get; set; }
        public virtual ICollection<QuestionnaireRequest> QuestionnaireRequests { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
        public virtual ICollection<TrainingPlan> TrainingPlans { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<VoidPaymentLevel> VoidPaymentLevels { get; set; }
        public virtual ICollection<VoidPayment> VoidPayments { get; set; }
    }
}
