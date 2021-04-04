using System;
using System.Collections.Generic;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Articles = new HashSet<Article>();
            BlogArticles = new HashSet<BlogArticle>();
            BodyDiagnosisCoaches = new HashSet<BodyDiagnosis>();
            BodyDiagnosisLearners = new HashSet<BodyDiagnosis>();
            BranchStoreManagers = new HashSet<BranchStore>();
            BranchStoreViceManagers = new HashSet<BranchStore>();
            CourseContractAgents = new HashSet<CourseContract>();
            CourseContractLevels = new HashSet<CourseContractLevel>();
            CourseContractMembers = new HashSet<CourseContractMember>();
            CourseContractOwners = new HashSet<CourseContract>();
            CourseContractSupervisors = new HashSet<CourseContract>();
            DocumentPrintLogs = new HashSet<DocumentPrintLog>();
            DocumentPrintQueues = new HashSet<DocumentPrintQueue>();
            EnterpriseCourseMembers = new HashSet<EnterpriseCourseMember>();
            FavoriteLessons = new HashSet<FavoriteLesson>();
            GroupEvents = new HashSet<GroupEvent>();
            InverseAuth = new HashSet<UserProfile>();
            InverseCreatorNavigation = new HashSet<UserProfile>();
            LearnerAwardActors = new HashSet<LearnerAward>();
            LearnerAwardUIDNavigations = new HashSet<LearnerAward>();
            LearnerFitnessAdvisors = new HashSet<LearnerFitnessAdvisor>();
            LearnerFitnessAssessments = new HashSet<LearnerFitnessAssessment>();
            LessonCommentHearers = new HashSet<LessonComment>();
            LessonCommentSpeakers = new HashSet<LessonComment>();
            LessonFitnessAssessments = new HashSet<LessonFitnessAssessment>();
            PDQQuestions = new HashSet<PDQQuestion>();
            PDQTasks = new HashSet<PDQTask>();
            PaymentAudits = new HashSet<PaymentAudit>();
            Payments = new HashSet<Payment>();
            PreferredLessonTimes = new HashSet<PreferredLessonTime>();
            QuestionnaireCoachBypasses = new HashSet<QuestionnaireCoachBypass>();
            QuestionnaireRequests = new HashSet<QuestionnaireRequest>();
            RegisterLessons = new HashSet<RegisterLesson>();
            ResetPasswords = new HashSet<ResetPassword>();
            UserEvents = new HashSet<UserEvent>();
            UserRoleAuthorizations = new HashSet<UserRoleAuthorization>();
            UserRoles = new HashSet<UserRole>();
            UserSignatures = new HashSet<UserSignature>();
            VoidPaymentLevels = new HashSet<VoidPaymentLevel>();
            VoidPayments = new HashSet<VoidPayment>();
        }

        public int UID { get; set; }
        public string UserName { get; set; }
        public string PID { get; set; }
        public string Password { get; set; }
        public string ExternalID { get; set; }
        public DateTime? Expiration { get; set; }
        public int? Creator { get; set; }
        public int? AuthID { get; set; }
        public int? LevelID { get; set; }
        public string ThemeName { get; set; }
        public string Password2 { get; set; }
        public string MemberCode { get; set; }
        public int? PictureID { get; set; }
        public string RealName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string RecentStatus { get; set; }
        public DateTime? Birthday { get; set; }
        public string Nickname { get; set; }
        public int? BirthdateIndex { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual UserProfile Auth { get; set; }
        public virtual UserProfile CreatorNavigation { get; set; }
        public virtual LevelExpression Level { get; set; }
        public virtual Attachment Picture { get; set; }
        public virtual EmployeeWelfare EmployeeWelfare { get; set; }
        public virtual ExerciseGameContestant ExerciseGameContestant { get; set; }
        public virtual PDQUserAssessment PDQUserAssessment { get; set; }
        public virtual PersonalExercisePurpose PersonalExercisePurpose { get; set; }
        public virtual ServingCoach ServingCoach { get; set; }
        public virtual UserProfileExtension UserProfileExtension { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<BlogArticle> BlogArticles { get; set; }
        public virtual ICollection<BodyDiagnosis> BodyDiagnosisCoaches { get; set; }
        public virtual ICollection<BodyDiagnosis> BodyDiagnosisLearners { get; set; }
        public virtual ICollection<BranchStore> BranchStoreManagers { get; set; }
        public virtual ICollection<BranchStore> BranchStoreViceManagers { get; set; }
        public virtual ICollection<CourseContract> CourseContractAgents { get; set; }
        public virtual ICollection<CourseContractLevel> CourseContractLevels { get; set; }
        public virtual ICollection<CourseContractMember> CourseContractMembers { get; set; }
        public virtual ICollection<CourseContract> CourseContractOwners { get; set; }
        public virtual ICollection<CourseContract> CourseContractSupervisors { get; set; }
        public virtual ICollection<DocumentPrintLog> DocumentPrintLogs { get; set; }
        public virtual ICollection<DocumentPrintQueue> DocumentPrintQueues { get; set; }
        public virtual ICollection<EnterpriseCourseMember> EnterpriseCourseMembers { get; set; }
        public virtual ICollection<FavoriteLesson> FavoriteLessons { get; set; }
        public virtual ICollection<GroupEvent> GroupEvents { get; set; }
        public virtual ICollection<UserProfile> InverseAuth { get; set; }
        public virtual ICollection<UserProfile> InverseCreatorNavigation { get; set; }
        public virtual ICollection<LearnerAward> LearnerAwardActors { get; set; }
        public virtual ICollection<LearnerAward> LearnerAwardUIDNavigations { get; set; }
        public virtual ICollection<LearnerFitnessAdvisor> LearnerFitnessAdvisors { get; set; }
        public virtual ICollection<LearnerFitnessAssessment> LearnerFitnessAssessments { get; set; }
        public virtual ICollection<LessonComment> LessonCommentHearers { get; set; }
        public virtual ICollection<LessonComment> LessonCommentSpeakers { get; set; }
        public virtual ICollection<LessonFitnessAssessment> LessonFitnessAssessments { get; set; }
        public virtual ICollection<PDQQuestion> PDQQuestions { get; set; }
        public virtual ICollection<PDQTask> PDQTasks { get; set; }
        public virtual ICollection<PaymentAudit> PaymentAudits { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PreferredLessonTime> PreferredLessonTimes { get; set; }
        public virtual ICollection<QuestionnaireCoachBypass> QuestionnaireCoachBypasses { get; set; }
        public virtual ICollection<QuestionnaireRequest> QuestionnaireRequests { get; set; }
        public virtual ICollection<RegisterLesson> RegisterLessons { get; set; }
        public virtual ICollection<ResetPassword> ResetPasswords { get; set; }
        public virtual ICollection<UserEvent> UserEvents { get; set; }
        public virtual ICollection<UserRoleAuthorization> UserRoleAuthorizations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserSignature> UserSignatures { get; set; }
        public virtual ICollection<VoidPaymentLevel> VoidPaymentLevels { get; set; }
        public virtual ICollection<VoidPayment> VoidPayments { get; set; }
    }
}
