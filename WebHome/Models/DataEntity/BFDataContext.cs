using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#nullable disable

namespace WebHome.Models.DataEntity
{
    public partial class BFDataContext : DbContext
    {
        static readonly ILoggerFactory __ConsoleLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

        public BFDataContext()
        {
        }

        public BFDataContext(DbContextOptions<BFDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<ArticleCategoryDefinition> ArticleCategoryDefinitions { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AwardingLesson> AwardingLessons { get; set; }
        public virtual DbSet<AwardingLessonGift> AwardingLessonGifts { get; set; }
        public virtual DbSet<BlogArticle> BlogArticles { get; set; }
        public virtual DbSet<BlogCategoryDefinition> BlogCategoryDefinitions { get; set; }
        public virtual DbSet<BlogTag> BlogTags { get; set; }
        public virtual DbSet<BodyDiagnosis> BodyDiagnoses { get; set; }
        public virtual DbSet<BodyPart> BodyParts { get; set; }
        public virtual DbSet<BodySuffering> BodySufferings { get; set; }
        public virtual DbSet<BonusAwardingIndication> BonusAwardingIndications { get; set; }
        public virtual DbSet<BonusAwardingItem> BonusAwardingItems { get; set; }
        public virtual DbSet<BonusAwardingLesson> BonusAwardingLessons { get; set; }
        public virtual DbSet<BonusExchange> BonusExchanges { get; set; }
        public virtual DbSet<BranchStore> BranchStores { get; set; }
        public virtual DbSet<CoachBranchMonthlyBonu> CoachBranchMonthlyBonus { get; set; }
        public virtual DbSet<CoachCertificate> CoachCertificates { get; set; }
        public virtual DbSet<CoachMonthlySalary> CoachMonthlySalaries { get; set; }
        public virtual DbSet<CoachRating> CoachRatings { get; set; }
        public virtual DbSet<CoachWorkplace> CoachWorkplaces { get; set; }
        public virtual DbSet<ContractElement> ContractElements { get; set; }
        public virtual DbSet<ContractInstallment> ContractInstallments { get; set; }
        public virtual DbSet<ContractMonthlySummary> ContractMonthlySummaries { get; set; }
        public virtual DbSet<ContractPayment> ContractPayments { get; set; }
        public virtual DbSet<ContractTrustSettlement> ContractTrustSettlements { get; set; }
        public virtual DbSet<ContractTrustTrack> ContractTrustTracks { get; set; }
        public virtual DbSet<ContractTrustTrackBak> ContractTrustTrackBaks { get; set; }
        public virtual DbSet<CourseContract> CourseContracts { get; set; }
        public virtual DbSet<CourseContractExtension> CourseContractExtensions { get; set; }
        public virtual DbSet<CourseContractLevel> CourseContractLevels { get; set; }
        public virtual DbSet<CourseContractMember> CourseContractMembers { get; set; }
        public virtual DbSet<CourseContractRevision> CourseContractRevisions { get; set; }
        public virtual DbSet<CourseContractRevisionItem> CourseContractRevisionItems { get; set; }
        public virtual DbSet<CourseContractSignature> CourseContractSignatures { get; set; }
        public virtual DbSet<CourseContractTrust> CourseContractTrusts { get; set; }
        public virtual DbSet<CourseContractType> CourseContractTypes { get; set; }
        public virtual DbSet<DailyWorkingHour> DailyWorkingHours { get; set; }
        public virtual DbSet<DerivedDocument> DerivedDocuments { get; set; }
        public virtual DbSet<DiagnosisAssessment> DiagnosisAssessments { get; set; }
        public virtual DbSet<DiagnosisJudgement> DiagnosisJudgements { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentPrintLog> DocumentPrintLogs { get; set; }
        public virtual DbSet<DocumentPrintQueue> DocumentPrintQueues { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EmployeeWelfare> EmployeeWelfares { get; set; }
        public virtual DbSet<EnterpriseCourseContent> EnterpriseCourseContents { get; set; }
        public virtual DbSet<EnterpriseCourseContract> EnterpriseCourseContracts { get; set; }
        public virtual DbSet<EnterpriseCourseMember> EnterpriseCourseMembers { get; set; }
        public virtual DbSet<EnterpriseCoursePayment> EnterpriseCoursePayments { get; set; }
        public virtual DbSet<EnterpriseLessonType> EnterpriseLessonTypes { get; set; }
        public virtual DbSet<ExerciseGameContestant> ExerciseGameContestants { get; set; }
        public virtual DbSet<ExerciseGameItem> ExerciseGameItems { get; set; }
        public virtual DbSet<ExerciseGamePersonalRank> ExerciseGamePersonalRanks { get; set; }
        public virtual DbSet<ExerciseGameRank> ExerciseGameRanks { get; set; }
        public virtual DbSet<ExerciseGameResult> ExerciseGameResults { get; set; }
        public virtual DbSet<FavoriteLesson> FavoriteLessons { get; set; }
        public virtual DbSet<FitnessAssessment> FitnessAssessments { get; set; }
        public virtual DbSet<FitnessAssessmentGroup> FitnessAssessmentGroups { get; set; }
        public virtual DbSet<FitnessAssessmentItem> FitnessAssessmentItems { get; set; }
        public virtual DbSet<FitnessDiagnosis> FitnessDiagnoses { get; set; }
        public virtual DbSet<GoalAboutPDQ> GoalAboutPDQs { get; set; }
        public virtual DbSet<GroupEvent> GroupEvents { get; set; }
        public virtual DbSet<GroupingLesson> GroupingLessons { get; set; }
        public virtual DbSet<GroupingLessonDiscount> GroupingLessonDiscounts { get; set; }
        public virtual DbSet<IntuitionCharge> IntuitionCharges { get; set; }
        public virtual DbSet<InvoiceAllowance> InvoiceAllowances { get; set; }
        public virtual DbSet<InvoiceAllowanceBuyer> InvoiceAllowanceBuyers { get; set; }
        public virtual DbSet<InvoiceAllowanceCancellation> InvoiceAllowanceCancellations { get; set; }
        public virtual DbSet<InvoiceAllowanceDetail> InvoiceAllowanceDetails { get; set; }
        public virtual DbSet<InvoiceAllowanceDispatch> InvoiceAllowanceDispatches { get; set; }
        public virtual DbSet<InvoiceAllowanceDispatchLog> InvoiceAllowanceDispatchLogs { get; set; }
        public virtual DbSet<InvoiceAllowanceItem> InvoiceAllowanceItems { get; set; }
        public virtual DbSet<InvoiceAllowanceSeller> InvoiceAllowanceSellers { get; set; }
        public virtual DbSet<InvoiceAmountType> InvoiceAmountTypes { get; set; }
        public virtual DbSet<InvoiceBuyer> InvoiceBuyers { get; set; }
        public virtual DbSet<InvoiceCancellation> InvoiceCancellations { get; set; }
        public virtual DbSet<InvoiceCancellationDispatch> InvoiceCancellationDispatches { get; set; }
        public virtual DbSet<InvoiceCancellationDispatchLog> InvoiceCancellationDispatchLogs { get; set; }
        public virtual DbSet<InvoiceCarrier> InvoiceCarriers { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual DbSet<InvoiceDonation> InvoiceDonations { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<InvoiceItemDispatch> InvoiceItemDispatches { get; set; }
        public virtual DbSet<InvoiceItemDispatchLog> InvoiceItemDispatchLogs { get; set; }
        public virtual DbSet<InvoiceNoAssignment> InvoiceNoAssignments { get; set; }
        public virtual DbSet<InvoiceNoInterval> InvoiceNoIntervals { get; set; }
        public virtual DbSet<InvoiceNoIntervalGroup> InvoiceNoIntervalGroups { get; set; }
        public virtual DbSet<InvoiceProduct> InvoiceProducts { get; set; }
        public virtual DbSet<InvoiceProductItem> InvoiceProductItems { get; set; }
        public virtual DbSet<InvoicePurchaseOrder> InvoicePurchaseOrders { get; set; }
        public virtual DbSet<InvoiceSeller> InvoiceSellers { get; set; }
        public virtual DbSet<InvoiceTrackCode> InvoiceTrackCodes { get; set; }
        public virtual DbSet<InvoiceTrackCodeAssignment> InvoiceTrackCodeAssignments { get; set; }
        public virtual DbSet<InvoiceWinningNumber> InvoiceWinningNumbers { get; set; }
        public virtual DbSet<IsInternalLesson> IsInternalLessons { get; set; }
        public virtual DbSet<IsWelfareGiftLesson> IsWelfareGiftLessons { get; set; }
        public virtual DbSet<LearnerAward> LearnerAwards { get; set; }
        public virtual DbSet<LearnerFitnessAdvisor> LearnerFitnessAdvisors { get; set; }
        public virtual DbSet<LearnerFitnessAssessment> LearnerFitnessAssessments { get; set; }
        public virtual DbSet<LearnerFitnessAssessmentResult> LearnerFitnessAssessmentResults { get; set; }
        public virtual DbSet<LessonAttendance> LessonAttendances { get; set; }
        public virtual DbSet<LessonAttendanceDueDate> LessonAttendanceDueDates { get; set; }
        public virtual DbSet<LessonComment> LessonComments { get; set; }
        public virtual DbSet<LessonFeedBack> LessonFeedBacks { get; set; }
        public virtual DbSet<LessonFitnessAssessment> LessonFitnessAssessments { get; set; }
        public virtual DbSet<LessonFitnessAssessmentReport> LessonFitnessAssessmentReports { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonPriceProperty> LessonPriceProperties { get; set; }
        public virtual DbSet<LessonPriceSeries> LessonPriceSeries { get; set; }
        public virtual DbSet<LessonPriceType> LessonPriceTypes { get; set; }
        public virtual DbSet<LessonTime> LessonTimes { get; set; }
        public virtual DbSet<LessonTimeExpansion> LessonTimeExpansions { get; set; }
        public virtual DbSet<LessonTimeSettlement> LessonTimeSettlements { get; set; }
        public virtual DbSet<LessonTrend> LessonTrends { get; set; }
        public virtual DbSet<LevelExpression> LevelExpressions { get; set; }
        public virtual DbSet<MerchandiseTransaction> MerchandiseTransactions { get; set; }
        public virtual DbSet<MerchandiseTransactionType> MerchandiseTransactionTypes { get; set; }
        public virtual DbSet<MerchandiseWindow> MerchandiseWindows { get; set; }
        public virtual DbSet<MonthlyBranchIndicator> MonthlyBranchIndicators { get; set; }
        public virtual DbSet<MonthlyBranchRevenueGoal> MonthlyBranchRevenueGoals { get; set; }
        public virtual DbSet<MonthlyBranchRevenueIndicator> MonthlyBranchRevenueIndicators { get; set; }
        public virtual DbSet<MonthlyCoachRevenueIndicator> MonthlyCoachRevenueIndicators { get; set; }
        public virtual DbSet<MonthlyIndicator> MonthlyIndicators { get; set; }
        public virtual DbSet<MonthlyRevenueGoal> MonthlyRevenueGoals { get; set; }
        public virtual DbSet<MonthlyRevenueGrade> MonthlyRevenueGrades { get; set; }
        public virtual DbSet<MonthlyRevenueIndicator> MonthlyRevenueIndicators { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<PDQGroup> PDQGroups { get; set; }
        public virtual DbSet<PDQQuestion> PDQQuestions { get; set; }
        public virtual DbSet<PDQQuestionExtension> PDQQuestionExtensions { get; set; }
        public virtual DbSet<PDQSuggestion> PDQSuggestions { get; set; }
        public virtual DbSet<PDQTask> PDQTasks { get; set; }
        public virtual DbSet<PDQTaskBonu> PDQTaskBonus { get; set; }
        public virtual DbSet<PDQTaskItem> PDQTaskItems { get; set; }
        public virtual DbSet<PDQType> PDQTypes { get; set; }
        public virtual DbSet<PDQUserAssessment> PDQUserAssessments { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentAudit> PaymentAudits { get; set; }
        public virtual DbSet<PaymentOrder> PaymentOrders { get; set; }
        public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public virtual DbSet<PersonalExercisePurpose> PersonalExercisePurposes { get; set; }
        public virtual DbSet<PersonalExercisePurposeItem> PersonalExercisePurposeItems { get; set; }
        public virtual DbSet<PreferredLessonTime> PreferredLessonTimes { get; set; }
        public virtual DbSet<ProfessionalCertificate> ProfessionalCertificates { get; set; }
        public virtual DbSet<ProfessionalLevel> ProfessionalLevels { get; set; }
        public virtual DbSet<ProfessionalLevelReview> ProfessionalLevelReviews { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<QuestionnaireCoachBypass> QuestionnaireCoachBypasses { get; set; }
        public virtual DbSet<QuestionnaireGroup> QuestionnaireGroups { get; set; }
        public virtual DbSet<QuestionnaireRequest> QuestionnaireRequests { get; set; }
        public virtual DbSet<RegisterLesson> RegisterLessons { get; set; }
        public virtual DbSet<RegisterLessonContract> RegisterLessonContracts { get; set; }
        public virtual DbSet<RegisterLessonEnterprise> RegisterLessonEnterprises { get; set; }
        public virtual DbSet<ResetPassword> ResetPasswords { get; set; }
        public virtual DbSet<ServingCoach> ServingCoaches { get; set; }
        public virtual DbSet<Settlement> Settlements { get; set; }
        public virtual DbSet<StyleAboutPDQ> StyleAboutPDQs { get; set; }
        public virtual DbSet<SystemEventBulletin> SystemEventBulletins { get; set; }
        public virtual DbSet<TrainingAid> TrainingAids { get; set; }
        public virtual DbSet<TrainingExecution> TrainingExecutions { get; set; }
        public virtual DbSet<TrainingExecutionStage> TrainingExecutionStages { get; set; }
        public virtual DbSet<TrainingItem> TrainingItems { get; set; }
        public virtual DbSet<TrainingItemAid> TrainingItemAids { get; set; }
        public virtual DbSet<TrainingLevelAboutPDQ> TrainingLevelAboutPDQs { get; set; }
        public virtual DbSet<TrainingPlan> TrainingPlans { get; set; }
        public virtual DbSet<TrainingStage> TrainingStages { get; set; }
        public virtual DbSet<TrainingStageItem> TrainingStageItems { get; set; }
        public virtual DbSet<TrainingType> TrainingTypes { get; set; }
        public virtual DbSet<TuitionAchievement> TuitionAchievements { get; set; }
        public virtual DbSet<TuitionInstallment> TuitionInstallments { get; set; }
        public virtual DbSet<UniformInvoiceWinningNumber> UniformInvoiceWinningNumbers { get; set; }
        public virtual DbSet<UsageType> UsageTypes { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserProfileExtension> UserProfileExtensions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRoleAuthorization> UserRoleAuthorizations { get; set; }
        public virtual DbSet<UserRoleDefinition> UserRoleDefinitions { get; set; }
        public virtual DbSet<UserSignature> UserSignatures { get; set; }
        public virtual DbSet<V_BranchStaff> V_BranchStaffs { get; set; }
        public virtual DbSet<V_ContractTuition> V_ContractTuitions { get; set; }
        public virtual DbSet<V_LearnerFitenessAssessment> V_LearnerFitenessAssessments { get; set; }
        public virtual DbSet<V_LessonTime> V_LessonTimes { get; set; }
        public virtual DbSet<V_LessonUnitPrice> V_LessonUnitPrices { get; set; }
        public virtual DbSet<V_PerformanceShare> V_PerformanceShares { get; set; }
        public virtual DbSet<V_Tuition> V_Tuitions { get; set; }
        public virtual DbSet<V_WorkPlace> V_WorkPlaces { get; set; }
        public virtual DbSet<VacantInvoiceNo> VacantInvoiceNos { get; set; }
        public virtual DbSet<VoidPayment> VoidPayments { get; set; }
        public virtual DbSet<VoidPaymentLevel> VoidPaymentLevels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /***
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                **/
                IConfiguration config = new ConfigurationBuilder()
                                      .SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                      .Build();
                optionsBuilder
                    .UseLoggerFactory(__ConsoleLoggerFactory)
                    .UseSqlServer(config.GetConnectionString("BFDbConnection"));
                //optionsBuilder.UseSqlServer("Data Source=VM-Venus\\sqlexpress,1433;Initial Catalog=BeyondFitnessProd2;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.DocID);

                entity.ToTable("Article");

                entity.Property(e => e.DocID).ValueGeneratedNever();

                entity.Property(e => e.Subtitle).HasMaxLength(256);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AuthorID)
                    .HasConstraintName("FK_Article_UserProfile");

                entity.HasOne(d => d.Doc)
                    .WithOne(p => p.Article)
                    .HasForeignKey<Article>(d => d.DocID)
                    .HasConstraintName("FK_Article_Document");

                entity.HasOne(d => d.IllustrationNavigation)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.Illustration)
                    .HasConstraintName("FK_Article_Attachment");
            });

            modelBuilder.Entity<ArticleCategory>(entity =>
            {
                entity.HasKey(e => new { e.DocID, e.Category });

                entity.ToTable("ArticleCategory");

                entity.Property(e => e.Category).HasMaxLength(32);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleCategory_ArticleCategoryDefinition");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.ArticleCategories)
                    .HasForeignKey(d => d.DocID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleCategory_Article");
            });

            modelBuilder.Entity<ArticleCategoryDefinition>(entity =>
            {
                entity.HasKey(e => e.Category);

                entity.ToTable("ArticleCategoryDefinition");

                entity.Property(e => e.Category).HasMaxLength(32);

                entity.Property(e => e.Description).HasMaxLength(64);
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.StoredPath)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.DocID)
                    .HasConstraintName("FK_Attachment_Document");
            });

            modelBuilder.Entity<AwardingLesson>(entity =>
            {
                entity.HasKey(e => e.AwardID);

                entity.ToTable("AwardingLesson");

                entity.Property(e => e.AwardID).ValueGeneratedNever();

                entity.HasOne(d => d.Award)
                    .WithOne(p => p.AwardingLesson)
                    .HasForeignKey<AwardingLesson>(d => d.AwardID)
                    .HasConstraintName("FK_AwardingLesson_LearnerAward");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.AwardingLessons)
                    .HasForeignKey(d => d.RegisterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AwardingLesson_RegisterLesson");
            });

            modelBuilder.Entity<AwardingLessonGift>(entity =>
            {
                entity.HasKey(e => e.AwardID);

                entity.ToTable("AwardingLessonGift");

                entity.Property(e => e.AwardID).ValueGeneratedNever();

                entity.HasOne(d => d.Award)
                    .WithOne(p => p.AwardingLessonGift)
                    .HasForeignKey<AwardingLessonGift>(d => d.AwardID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AwardingLessonGift_LearnerAward");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.AwardingLessonGifts)
                    .HasForeignKey(d => d.RegisterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AwardingLessonGift_RegisterLesson");
            });

            modelBuilder.Entity<BlogArticle>(entity =>
            {
                entity.HasKey(e => e.DocID);

                entity.ToTable("BlogArticle");

                entity.HasIndex(e => e.BlogID, "IX_BlogArticle")
                    .IsUnique();

                entity.Property(e => e.DocID).ValueGeneratedNever();

                entity.Property(e => e.BlogID)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Subtitle).HasMaxLength(256);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BlogArticles)
                    .HasForeignKey(d => d.AuthorID)
                    .HasConstraintName("FK_BlogArticle_UserProfile");

                entity.HasOne(d => d.Doc)
                    .WithOne(p => p.BlogArticle)
                    .HasForeignKey<BlogArticle>(d => d.DocID)
                    .HasConstraintName("FK_BlogArticle_Document");
            });

            modelBuilder.Entity<BlogCategoryDefinition>(entity =>
            {
                entity.HasKey(e => e.CategoryID);

                entity.ToTable("BlogCategoryDefinition");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.CategoryIndication).HasMaxLength(256);

                entity.Property(e => e.Description).HasMaxLength(64);
            });

            modelBuilder.Entity<BlogTag>(entity =>
            {
                entity.HasKey(e => new { e.DocID, e.CategoryID });

                entity.ToTable("BlogTag");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BlogTags)
                    .HasForeignKey(d => d.CategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BlogTag_BlogCategoryDefinition");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.BlogTags)
                    .HasForeignKey(d => d.DocID)
                    .HasConstraintName("FK_BlogTag_BlogArticle");
            });

            modelBuilder.Entity<BodyDiagnosis>(entity =>
            {
                entity.HasKey(e => e.DiagnosisID);

                entity.ToTable("BodyDiagnosis");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.DiagnosisDate).HasColumnType("datetime");

                entity.Property(e => e.Goal).HasMaxLength(256);

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.BodyDiagnosisCoaches)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodyDiagnosis_UserProfile1");

                entity.HasOne(d => d.Learner)
                    .WithMany(p => p.BodyDiagnosisLearners)
                    .HasForeignKey(d => d.LearnerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodyDiagnosis_UserProfile");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.BodyDiagnoses)
                    .HasForeignKey(d => d.LevelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodyDiagnosis_LevelExpression");
            });

            modelBuilder.Entity<BodyPart>(entity =>
            {
                entity.HasKey(e => e.PartID);

                entity.Property(e => e.PartID).ValueGeneratedNever();

                entity.Property(e => e.Part).HasMaxLength(64);
            });

            modelBuilder.Entity<BodySuffering>(entity =>
            {
                entity.HasKey(e => new { e.DiagnosisID, e.PartID });

                entity.ToTable("BodySuffering");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.BodySufferings)
                    .HasForeignKey(d => d.DiagnosisID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodySuffering_BodyDiagnosis");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.BodySufferings)
                    .HasForeignKey(d => d.PartID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BodySuffering_BodyParts");
            });

            modelBuilder.Entity<BonusAwardingIndication>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("BonusAwardingIndication");

                entity.Property(e => e.ItemID).ValueGeneratedNever();

                entity.Property(e => e.Indication).HasMaxLength(32);

                entity.HasOne(d => d.Item)
                    .WithOne(p => p.BonusAwardingIndication)
                    .HasForeignKey<BonusAwardingIndication>(d => d.ItemID)
                    .HasConstraintName("FK_BonusAwardingIndication_BonusAwardingItem");
            });

            modelBuilder.Entity<BonusAwardingItem>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("BonusAwardingItem");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.SampleUrl).HasMaxLength(256);
            });

            modelBuilder.Entity<BonusAwardingLesson>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("BonusAwardingLesson");

                entity.Property(e => e.ItemID).ValueGeneratedNever();

                entity.HasOne(d => d.Item)
                    .WithOne(p => p.BonusAwardingLesson)
                    .HasForeignKey<BonusAwardingLesson>(d => d.ItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BonusAwardingLesson_BonusAwardingItem");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.BonusAwardingLessons)
                    .HasForeignKey(d => d.PriceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BonusAwardingLesson_LessonPriceType");
            });

            modelBuilder.Entity<BonusExchange>(entity =>
            {
                entity.HasKey(e => new { e.AwardID, e.TaskID });

                entity.ToTable("BonusExchange");

                entity.HasOne(d => d.Award)
                    .WithMany(p => p.BonusExchanges)
                    .HasForeignKey(d => d.AwardID)
                    .HasConstraintName("FK_BonusExchange_LearnerAward");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.BonusExchanges)
                    .HasForeignKey(d => d.TaskID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BonusExchange_PDQTaskBonus");
            });

            modelBuilder.Entity<BranchStore>(entity =>
            {
                entity.HasKey(e => e.BranchID);

                entity.ToTable("BranchStore");

                entity.Property(e => e.BranchID).ValueGeneratedNever();

                entity.Property(e => e.BranchName).HasMaxLength(32);

                entity.HasOne(d => d.Branch)
                    .WithOne(p => p.BranchStore)
                    .HasForeignKey<BranchStore>(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchStore_Organization");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.BranchStoreManagers)
                    .HasForeignKey(d => d.ManagerID)
                    .HasConstraintName("FK_BranchStore_UserProfile");

                entity.HasOne(d => d.ViceManager)
                    .WithMany(p => p.BranchStoreViceManagers)
                    .HasForeignKey(d => d.ViceManagerID)
                    .HasConstraintName("FK_BranchStore_UserProfile1");
            });

            modelBuilder.Entity<CoachBranchMonthlyBonu>(entity =>
            {
                entity.HasKey(e => new { e.CoachID, e.SettlementID, e.BranchID });

                entity.ToTable("CoachBranchMonthlyBonus", "Report");

                entity.Property(e => e.AchievementAttendanceCount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CoachBranchMonthlyBonus)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachBranchMonthlyBonus_BranchStore");

                entity.HasOne(d => d.CoachMonthlySalary)
                    .WithMany(p => p.CoachBranchMonthlyBonus)
                    .HasForeignKey(d => new { d.CoachID, d.SettlementID })
                    .HasConstraintName("FK_CoachBranchMonthlyBonus_CoachMonthlySalary");
            });

            modelBuilder.Entity<CoachCertificate>(entity =>
            {
                entity.HasKey(e => new { e.CoachID, e.CertificateID });

                entity.ToTable("CoachCertificate");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.CoachCertificates)
                    .HasForeignKey(d => d.CertificateID)
                    .HasConstraintName("FK_CoachCertificate_ProfessionalCertificate");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.CoachCertificates)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachCertificate_ServingCoach");
            });

            modelBuilder.Entity<CoachMonthlySalary>(entity =>
            {
                entity.HasKey(e => new { e.CoachID, e.SettlementID })
                    .HasName("PK_CoachMonthlySalary_1");

                entity.ToTable("CoachMonthlySalary", "Report");

                entity.Property(e => e.AchievementShareRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GradeIndex).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.CoachMonthlySalaries)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachMonthlySalary_ServingCoach");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.CoachMonthlySalaries)
                    .HasForeignKey(d => d.LevelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachMonthlySalary_ProfessionalLevel");

                entity.HasOne(d => d.Settlement)
                    .WithMany(p => p.CoachMonthlySalaries)
                    .HasForeignKey(d => d.SettlementID)
                    .HasConstraintName("FK_CoachMonthlySalary_Settlement");

                entity.HasOne(d => d.WorkPlaceNavigation)
                    .WithMany(p => p.CoachMonthlySalaries)
                    .HasForeignKey(d => d.WorkPlace)
                    .HasConstraintName("FK_CoachMonthlySalary_BranchStore");
            });

            modelBuilder.Entity<CoachRating>(entity =>
            {
                entity.HasKey(e => e.RatingID);

                entity.ToTable("CoachRating");

                entity.Property(e => e.RatingDate).HasColumnType("datetime");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.CoachRatings)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachRating_ServingCoach");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.CoachRatings)
                    .HasForeignKey(d => d.LevelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachRating_ProfessionalLevel");
            });

            modelBuilder.Entity<CoachWorkplace>(entity =>
            {
                entity.HasKey(e => new { e.CoachID, e.BranchID });

                entity.ToTable("CoachWorkplace");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CoachWorkplaces)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoachWorkplace_BranchStore");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.CoachWorkplaces)
                    .HasForeignKey(d => d.CoachID)
                    .HasConstraintName("FK_CoachWorkplace_ServingCoach");
            });

            modelBuilder.Entity<ContractElement>(entity =>
            {
                entity.HasKey(e => e.ElementID);

                entity.ToTable("ContractElement");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractElements)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_ContractElement_CourseContract");

                entity.HasOne(d => d.OriginalFitnessConsultantNavigation)
                    .WithMany(p => p.ContractElements)
                    .HasForeignKey(d => d.OriginalFitnessConsultant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractElement_ServingCoach");

                entity.HasOne(d => d.Revision)
                    .WithMany(p => p.ContractElements)
                    .HasForeignKey(d => d.RevisionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractElement_CourseContractRevision");
            });

            modelBuilder.Entity<ContractInstallment>(entity =>
            {
                entity.HasKey(e => e.InstallmentID);

                entity.ToTable("ContractInstallment");
            });

            modelBuilder.Entity<ContractMonthlySummary>(entity =>
            {
                entity.HasKey(e => new { e.ContractID, e.SettlementDate });

                entity.ToTable("ContractMonthlySummary");

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractMonthlySummaries)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_ContractMonthlySummary_CourseContract");
            });

            modelBuilder.Entity<ContractPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentID);

                entity.ToTable("ContractPayment");

                entity.Property(e => e.PaymentID).ValueGeneratedNever();

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractPayments)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_ContractPayment_CourseContract");

                entity.HasOne(d => d.Payment)
                    .WithOne(p => p.ContractPayment)
                    .HasForeignKey<ContractPayment>(d => d.PaymentID)
                    .HasConstraintName("FK_ContractPayment_Payment");
            });

            modelBuilder.Entity<ContractTrustSettlement>(entity =>
            {
                entity.HasKey(e => new { e.ContractID, e.SettlementID });

                entity.ToTable("ContractTrustSettlement");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractTrustSettlements)
                    .HasForeignKey(d => d.ContractID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContractTrustSettlement_CourseContract");

                entity.HasOne(d => d.Settlement)
                    .WithMany(p => p.ContractTrustSettlements)
                    .HasForeignKey(d => d.SettlementID)
                    .HasConstraintName("FK_ContractTrustSettlement_Settlement");
            });

            modelBuilder.Entity<ContractTrustTrack>(entity =>
            {
                entity.HasKey(e => e.TrustID);

                entity.ToTable("ContractTrustTrack");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.TrustType)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.ContractTrustTracks)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_ContractTrustTrack_CourseContract");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.ContractTrustTracks)
                    .HasForeignKey(d => d.LessonID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ContractTrustTrack_LessonTime");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.ContractTrustTracks)
                    .HasForeignKey(d => d.PaymentID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ContractTrustTrack_Payment");

                entity.HasOne(d => d.Settlement)
                    .WithMany(p => p.ContractTrustTracks)
                    .HasForeignKey(d => d.SettlementID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ContractTrustTrack_Settlement");

                entity.HasOne(d => d.Void)
                    .WithMany(p => p.ContractTrustTracks)
                    .HasForeignKey(d => d.VoidID)
                    .HasConstraintName("FK_ContractTrustTrack_VoidPayment");
            });

            modelBuilder.Entity<ContractTrustTrackBak>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ContractTrustTrackBak");

                entity.Property(e => e.EventDate).HasColumnType("datetime");

                entity.Property(e => e.TrustID).ValueGeneratedOnAdd();

                entity.Property(e => e.TrustType)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<CourseContract>(entity =>
            {
                entity.HasKey(e => e.ContractID);

                entity.ToTable("CourseContract");

                entity.HasIndex(e => e.ContractNo, "IX_CourseContract");

                entity.Property(e => e.ContractDate).HasColumnType("datetime");

                entity.Property(e => e.ContractNo).HasMaxLength(64);

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.PayoffDue).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(256);

                entity.Property(e => e.Subject).HasMaxLength(8);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.Property(e => e.ValidTo).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CourseContractAgents)
                    .HasForeignKey(d => d.AgentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContract_UserProfile1");

                entity.HasOne(d => d.ContractTypeNavigation)
                    .WithMany(p => p.CourseContracts)
                    .HasForeignKey(d => d.ContractType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContract_CourseContractType");

                entity.HasOne(d => d.FitnessConsultantNavigation)
                    .WithMany(p => p.CourseContracts)
                    .HasForeignKey(d => d.FitnessConsultant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContract_ServingCoach");

                entity.HasOne(d => d.InstallmentNavigation)
                    .WithMany(p => p.CourseContracts)
                    .HasForeignKey(d => d.InstallmentID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CourseContract_ContractInstallment");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.CourseContractOwners)
                    .HasForeignKey(d => d.OwnerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContract_UserProfile");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.CourseContracts)
                    .HasForeignKey(d => d.PriceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContract_LessonPriceType");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.CourseContracts)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContract_LevelExpression");

                entity.HasOne(d => d.Supervisor)
                    .WithMany(p => p.CourseContractSupervisors)
                    .HasForeignKey(d => d.SupervisorID)
                    .HasConstraintName("FK_CourseContract_UserProfile2");
            });

            modelBuilder.Entity<CourseContractExtension>(entity =>
            {
                entity.HasKey(e => e.ContractID);

                entity.ToTable("CourseContractExtension");

                entity.Property(e => e.ContractID).ValueGeneratedNever();

                entity.Property(e => e.PaymentMethod).HasMaxLength(64);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CourseContractExtensions)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractExtension_BranchStore");

                entity.HasOne(d => d.Contract)
                    .WithOne(p => p.CourseContractExtension)
                    .HasForeignKey<CourseContractExtension>(d => d.ContractID)
                    .HasConstraintName("FK_CourseContractExtension_CourseContract");

                entity.HasOne(d => d.RevisionTracking)
                    .WithMany(p => p.CourseContractExtensions)
                    .HasForeignKey(d => d.RevisionTrackingID)
                    .HasConstraintName("FK_CourseContractExtension_CourseContractRevision");
            });

            modelBuilder.Entity<CourseContractLevel>(entity =>
            {
                entity.HasKey(e => e.LogID);

                entity.ToTable("CourseContractLevel");

                entity.Property(e => e.LevelDate).HasColumnType("datetime");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CourseContractLevels)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_CourseContractLevel_CourseContract");

                entity.HasOne(d => d.Executor)
                    .WithMany(p => p.CourseContractLevels)
                    .HasForeignKey(d => d.ExecutorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractLevel_UserProfile");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.CourseContractLevels)
                    .HasForeignKey(d => d.LevelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractLevel_LevelExpression");
            });

            modelBuilder.Entity<CourseContractMember>(entity =>
            {
                entity.HasKey(e => new { e.ContractID, e.UID });

                entity.ToTable("CourseContractMember");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.CourseContractMembers)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_CourseContractMember_CourseContract");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.CourseContractMembers)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractMember_UserProfile");
            });

            modelBuilder.Entity<CourseContractRevision>(entity =>
            {
                entity.HasKey(e => e.RevisionID);

                entity.ToTable("CourseContractRevision");

                entity.HasIndex(e => e.Reason, "IX_CourseContractRevision");

                entity.Property(e => e.RevisionID).ValueGeneratedNever();

                entity.Property(e => e.Reason).HasMaxLength(16);

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.CourseContractRevisions)
                    .HasForeignKey(d => d.AttachmentID)
                    .HasConstraintName("FK_CourseContractRevision_Attachment");

                entity.HasOne(d => d.OriginalContractNavigation)
                    .WithMany(p => p.CourseContractRevisionOriginalContractNavigations)
                    .HasForeignKey(d => d.OriginalContract)
                    .HasConstraintName("FK_CourseContractRevision_CourseContract1");

                entity.HasOne(d => d.Revision)
                    .WithOne(p => p.CourseContractRevisionRevision)
                    .HasForeignKey<CourseContractRevision>(d => d.RevisionID)
                    .HasConstraintName("FK_CourseContractRevision_CourseContract");
            });

            modelBuilder.Entity<CourseContractRevisionItem>(entity =>
            {
                entity.HasKey(e => e.RevisionID);

                entity.ToTable("CourseContractRevisionItem");

                entity.Property(e => e.RevisionID).ValueGeneratedNever();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CourseContractRevisionItems)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractRevisionItem_BranchStore");

                entity.HasOne(d => d.FitnessConsultantNavigation)
                    .WithMany(p => p.CourseContractRevisionItems)
                    .HasForeignKey(d => d.FitnessConsultant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractRevisionItem_ServingCoach");

                entity.HasOne(d => d.Revision)
                    .WithOne(p => p.CourseContractRevisionItem)
                    .HasForeignKey<CourseContractRevisionItem>(d => d.RevisionID)
                    .HasConstraintName("FK_CourseContractRevisionItem_CourseContractRevision");
            });

            modelBuilder.Entity<CourseContractSignature>(entity =>
            {
                entity.HasKey(e => e.SignatureID);

                entity.ToTable("CourseContractSignature");

                entity.Property(e => e.Signature).IsUnicode(false);

                entity.Property(e => e.SignatureName).HasMaxLength(64);

                entity.HasOne(d => d.CourseContractMember)
                    .WithMany(p => p.CourseContractSignatures)
                    .HasForeignKey(d => new { d.ContractID, d.UID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseContractSignature_CourseContractMember");
            });

            modelBuilder.Entity<CourseContractTrust>(entity =>
            {
                entity.HasKey(e => e.ContractID);

                entity.ToTable("CourseContractTrust");

                entity.Property(e => e.ContractID).ValueGeneratedNever();

                entity.HasOne(d => d.Contract)
                    .WithOne(p => p.CourseContractTrust)
                    .HasForeignKey<CourseContractTrust>(d => d.ContractID)
                    .HasConstraintName("FK_CourseContractTrust_CourseContract");

                entity.HasOne(d => d.C)
                    .WithMany(p => p.CourseContractTrusts)
                    .HasForeignKey(d => new { d.ContractID, d.CurrentSettlement })
                    .HasConstraintName("FK_CourseContractTrust_ContractTrustSettlement");
            });

            modelBuilder.Entity<CourseContractType>(entity =>
            {
                entity.HasKey(e => e.TypeID);

                entity.ToTable("CourseContractType");

                entity.Property(e => e.TypeID).ValueGeneratedNever();

                entity.Property(e => e.ContractCode).HasMaxLength(8);

                entity.Property(e => e.TypeName).HasMaxLength(32);

                entity.HasOne(d => d.GroupingMemberCountNavigation)
                    .WithMany(p => p.CourseContractTypes)
                    .HasForeignKey(d => d.GroupingMemberCount)
                    .HasConstraintName("FK_CourseContractType_GroupingLessonDiscount");
            });

            modelBuilder.Entity<DailyWorkingHour>(entity =>
            {
                entity.HasKey(e => e.Hour);

                entity.ToTable("DailyWorkingHour");

                entity.Property(e => e.Hour).ValueGeneratedNever();
            });

            modelBuilder.Entity<DerivedDocument>(entity =>
            {
                entity.HasKey(e => e.DocID);

                entity.ToTable("DerivedDocument");

                entity.Property(e => e.DocID).ValueGeneratedNever();

                entity.HasOne(d => d.Doc)
                    .WithOne(p => p.DerivedDocumentDoc)
                    .HasForeignKey<DerivedDocument>(d => d.DocID)
                    .HasConstraintName("FK_DerivedDocument_Document");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.DerivedDocumentSources)
                    .HasForeignKey(d => d.SourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DerivedDocument_Document1");
            });

            modelBuilder.Entity<DiagnosisAssessment>(entity =>
            {
                entity.HasKey(e => new { e.DiagnosisID, e.ItemID });

                entity.ToTable("DiagnosisAssessment");

                entity.Property(e => e.AdditionalAssessment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Assessment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.DiagnosisAction).HasMaxLength(256);

                entity.Property(e => e.Judgement).HasMaxLength(64);

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.DiagnosisAssessments)
                    .HasForeignKey(d => d.DiagnosisID)
                    .HasConstraintName("FK_DiagnosisAssessment_BodyDiagnosis");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.DiagnosisAssessments)
                    .HasForeignKey(d => d.ItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisAssessment_FitnessAssessmentItem");
            });

            modelBuilder.Entity<DiagnosisJudgement>(entity =>
            {
                entity.HasKey(e => e.JudgementID);

                entity.ToTable("DiagnosisJudgement");

                entity.Property(e => e.Judgement).HasMaxLength(64);

                entity.Property(e => e.RangeEnd).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RangeStart).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Fitness)
                    .WithMany(p => p.DiagnosisJudgements)
                    .HasForeignKey(d => d.FitnessID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DiagnosisJudgement_FitnessDiagnosis");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.DocID);

                entity.ToTable("Document");

                entity.HasComment("系統文件主檔");

                entity.Property(e => e.DocDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("文件建立時間");

                entity.HasOne(d => d.CurrentStepNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CurrentStep)
                    .HasConstraintName("FK_Document_LevelExpression");

                entity.HasOne(d => d.DocTypeNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocType)
                    .HasConstraintName("FK_Document_DocumentType");
            });

            modelBuilder.Entity<DocumentPrintLog>(entity =>
            {
                entity.HasKey(e => e.LogID);

                entity.ToTable("DocumentPrintLog");

                entity.Property(e => e.PrintDate).HasColumnType("datetime");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.DocumentPrintLogs)
                    .HasForeignKey(d => d.DocID)
                    .HasConstraintName("FK_DocumentPrintLog_Document");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.DocumentPrintLogs)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DocumentPrintLog_UserProfile");
            });

            modelBuilder.Entity<DocumentPrintQueue>(entity =>
            {
                entity.HasKey(e => e.DocID);

                entity.ToTable("DocumentPrintQueue");

                entity.Property(e => e.DocID).ValueGeneratedNever();

                entity.Property(e => e.SubmitDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Doc)
                    .WithOne(p => p.DocumentPrintQueue)
                    .HasForeignKey<DocumentPrintQueue>(d => d.DocID)
                    .HasConstraintName("FK_DocumentPrintQueue_Document");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.DocumentPrintQueues)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DocumentPrintQueue_UserProfile");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.TypeID);

                entity.ToTable("DocumentType");

                entity.HasComment("文件定對檔");

                entity.Property(e => e.TypeID)
                    .ValueGeneratedNever()
                    .HasComment("主鍵");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(128)
                    .HasComment("文件名稱");
            });

            modelBuilder.Entity<EmployeeWelfare>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("EmployeeWelfare");

                entity.Property(e => e.UID).ValueGeneratedNever();

                entity.HasOne(d => d.UIDNavigation)
                    .WithOne(p => p.EmployeeWelfare)
                    .HasForeignKey<EmployeeWelfare>(d => d.UID)
                    .HasConstraintName("FK_EmployeeWelfare_UserProfile");
            });

            modelBuilder.Entity<EnterpriseCourseContent>(entity =>
            {
                entity.HasKey(e => new { e.ContractID, e.TypeID });

                entity.ToTable("EnterpriseCourseContent");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.EnterpriseCourseContents)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_EnterpriseCourseContent_EnterpriseCourseContract1");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.EnterpriseCourseContents)
                    .HasForeignKey(d => d.TypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnterpriseCourseContent_EnterpriseLessonType");
            });

            modelBuilder.Entity<EnterpriseCourseContract>(entity =>
            {
                entity.HasKey(e => e.ContractID)
                    .HasName("PK_EnterpriseCourseContract_1");

                entity.ToTable("EnterpriseCourseContract");

                entity.Property(e => e.CompanyID).HasComment("主鍵");

                entity.Property(e => e.ContractNo).HasMaxLength(64);

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.GroupingMemberCount).HasDefaultValueSql("((1))");

                entity.Property(e => e.Remark).HasMaxLength(64);

                entity.Property(e => e.Subject).HasMaxLength(64);

                entity.Property(e => e.ValidFrom).HasColumnType("datetime");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.EnterpriseCourseContracts)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_EnterpriseCourseContract_BranchStore");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.EnterpriseCourseContracts)
                    .HasForeignKey(d => d.CompanyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnterpriseCourseContract_Organization");
            });

            modelBuilder.Entity<EnterpriseCourseMember>(entity =>
            {
                entity.HasKey(e => new { e.ContractID, e.UID });

                entity.ToTable("EnterpriseCourseMember");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.EnterpriseCourseMembers)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_EnterpriseCourseMember_EnterpriseCourseContract");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.EnterpriseCourseMembers)
                    .HasForeignKey(d => d.GroupID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_EnterpriseCourseMember_GroupingLesson");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.EnterpriseCourseMembers)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnterpriseCourseMember_UserProfile");
            });

            modelBuilder.Entity<EnterpriseCoursePayment>(entity =>
            {
                entity.HasKey(e => e.PaymentID);

                entity.ToTable("EnterpriseCoursePayment");

                entity.Property(e => e.PaymentID).ValueGeneratedNever();

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.EnterpriseCoursePayments)
                    .HasForeignKey(d => d.ContractID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnterpriseCoursePayment_EnterpriseCourseContract");

                entity.HasOne(d => d.Payment)
                    .WithOne(p => p.EnterpriseCoursePayment)
                    .HasForeignKey<EnterpriseCoursePayment>(d => d.PaymentID)
                    .HasConstraintName("FK_EnterpriseCoursePayment_Payment");
            });

            modelBuilder.Entity<EnterpriseLessonType>(entity =>
            {
                entity.HasKey(e => e.TypeID);

                entity.ToTable("EnterpriseLessonType");

                entity.Property(e => e.TypeID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(64);
            });

            modelBuilder.Entity<ExerciseGameContestant>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("ExerciseGameContestant");

                entity.Property(e => e.UID).ValueGeneratedNever();

                entity.HasOne(d => d.UIDNavigation)
                    .WithOne(p => p.ExerciseGameContestant)
                    .HasForeignKey<ExerciseGameContestant>(d => d.UID)
                    .HasConstraintName("FK_ExerciseGameContestant_UserProfile");
            });

            modelBuilder.Entity<ExerciseGameItem>(entity =>
            {
                entity.HasKey(e => e.ExerciseID);

                entity.ToTable("ExerciseGameItem");

                entity.Property(e => e.ExerciseID).ValueGeneratedNever();

                entity.Property(e => e.Exercise)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.Unit).HasMaxLength(16);
            });

            modelBuilder.Entity<ExerciseGamePersonalRank>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("ExerciseGamePersonalRank");

                entity.Property(e => e.UID).ValueGeneratedNever();

                entity.HasOne(d => d.UIDNavigation)
                    .WithOne(p => p.ExerciseGamePersonalRank)
                    .HasForeignKey<ExerciseGamePersonalRank>(d => d.UID)
                    .HasConstraintName("FK_ExerciseGamePersonalRank_ExerciseGameContestant");
            });

            modelBuilder.Entity<ExerciseGameRank>(entity =>
            {
                entity.HasKey(e => new { e.UID, e.ExerciseID });

                entity.ToTable("ExerciseGameRank");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.ExerciseGameRanks)
                    .HasForeignKey(d => d.ExerciseID)
                    .HasConstraintName("FK_ExerciseGameRank_ExerciseGameItem");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.ExerciseGameRanks)
                    .HasForeignKey(d => d.RecordID)
                    .HasConstraintName("FK_ExerciseGameRank_ExerciseGameResult");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.ExerciseGameRanks)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_ExerciseGameRank_ExerciseGameContestant");
            });

            modelBuilder.Entity<ExerciseGameResult>(entity =>
            {
                entity.HasKey(e => e.TestID);

                entity.ToTable("ExerciseGameResult");

                entity.Property(e => e.Score).HasColumnType("decimal(12, 4)");

                entity.Property(e => e.TestDate).HasColumnType("datetime");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.ExerciseGameResults)
                    .HasForeignKey(d => d.ExerciseID)
                    .HasConstraintName("FK_ExerciseGameResult_ExerciseGameItem");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.ExerciseGameResults)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_ExerciseGameResult_ExerciseGameContestant");
            });

            modelBuilder.Entity<FavoriteLesson>(entity =>
            {
                entity.HasKey(e => new { e.ExecutionID, e.UID });

                entity.ToTable("FavoriteLesson");

                entity.HasOne(d => d.Execution)
                    .WithMany(p => p.FavoriteLessons)
                    .HasForeignKey(d => d.ExecutionID)
                    .HasConstraintName("FK_FavoriteLesson_TrainingPlan");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.FavoriteLessons)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteLesson_UserProfile");
            });

            modelBuilder.Entity<FitnessAssessment>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("FitnessAssessment");

                entity.Property(e => e.LessonID).ValueGeneratedNever();

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.FitnessAssessment)
                    .HasForeignKey<FitnessAssessment>(d => d.LessonID)
                    .HasConstraintName("FK_FitnessAssessment_LessonTime");
            });

            modelBuilder.Entity<FitnessAssessmentGroup>(entity =>
            {
                entity.HasKey(e => e.GroupID);

                entity.ToTable("FitnessAssessmentGroup");

                entity.Property(e => e.GroupID).ValueGeneratedNever();

                entity.Property(e => e.GroupName).HasMaxLength(32);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.FitnessAssessmentGroups)
                    .HasForeignKey(d => d.MajorID)
                    .HasConstraintName("FK_FitnessAssessmentGroup_FitnessAssessmentItem");
            });

            modelBuilder.Entity<FitnessAssessmentItem>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("FitnessAssessmentItem");

                entity.Property(e => e.ItemID).ValueGeneratedNever();

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FitnessAssessmentItems)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_FitnessAssessmentItem_FitnessAssessmentGroup");
            });

            modelBuilder.Entity<FitnessDiagnosis>(entity =>
            {
                entity.HasKey(e => e.FitnessID);

                entity.ToTable("FitnessDiagnosis");

                entity.Property(e => e.Gender).HasMaxLength(8);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.FitnessDiagnoses)
                    .HasForeignKey(d => d.ItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FitnessDiagnosis_FitnessAssessmentItem");
            });

            modelBuilder.Entity<GoalAboutPDQ>(entity =>
            {
                entity.HasKey(e => e.GoalID);

                entity.ToTable("GoalAboutPDQ");

                entity.Property(e => e.GoalID).ValueGeneratedNever();

                entity.Property(e => e.Goal).HasMaxLength(64);
            });

            modelBuilder.Entity<GroupEvent>(entity =>
            {
                entity.HasKey(e => new { e.EventID, e.UID });

                entity.ToTable("GroupEvent");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.GroupEvents)
                    .HasForeignKey(d => d.EventID)
                    .HasConstraintName("FK_GroupEvent_UserEvent");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.GroupEvents)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupEvent_UserProfile");
            });

            modelBuilder.Entity<GroupingLesson>(entity =>
            {
                entity.HasKey(e => e.GroupID);

                entity.ToTable("GroupingLesson");
            });

            modelBuilder.Entity<GroupingLessonDiscount>(entity =>
            {
                entity.HasKey(e => e.GroupingMemberCount);

                entity.ToTable("GroupingLessonDiscount");

                entity.Property(e => e.GroupingMemberCount).ValueGeneratedNever();
            });

            modelBuilder.Entity<IntuitionCharge>(entity =>
            {
                entity.HasKey(e => e.RegisterID);

                entity.ToTable("IntuitionCharge");

                entity.Property(e => e.RegisterID).ValueGeneratedNever();

                entity.Property(e => e.Payment)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.HasOne(d => d.Register)
                    .WithOne(p => p.IntuitionCharge)
                    .HasForeignKey<IntuitionCharge>(d => d.RegisterID)
                    .HasConstraintName("FK_IntuitionCharge_RegisterLesson");
            });

            modelBuilder.Entity<InvoiceAllowance>(entity =>
            {
                entity.HasKey(e => e.AllowanceID);

                entity.ToTable("InvoiceAllowance");

                entity.Property(e => e.AllowanceID).ValueGeneratedNever();

                entity.Property(e => e.AllowanceDate)
                    .HasColumnType("datetime")
                    .HasComment("折讓證明單日期");

                entity.Property(e => e.AllowanceNumber)
                    .HasMaxLength(16)
                    .HasComment("折讓證明單號碼");

                entity.Property(e => e.AllowanceType).HasComment("折讓種類\r\n1:買方開立折讓證明單\r\n2:賣方折讓證明單通知\r\n");

                entity.Property(e => e.BuyerId).HasMaxLength(10);

                entity.Property(e => e.SellerId).HasMaxLength(10);

                entity.Property(e => e.TaxAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("營業稅額合計");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("金額(不含稅之進貨額)合計");

                entity.HasOne(d => d.Allowance)
                    .WithOne(p => p.InvoiceAllowance)
                    .HasForeignKey<InvoiceAllowance>(d => d.AllowanceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceAllowance_Document");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceAllowances)
                    .HasForeignKey(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceAllowance_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceAllowanceBuyer>(entity =>
            {
                entity.HasKey(e => e.AllowanceID);

                entity.ToTable("InvoiceAllowanceBuyer");

                entity.Property(e => e.AllowanceID).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.ContactName).HasMaxLength(64);

                entity.Property(e => e.CustomerID).HasMaxLength(64);

                entity.Property(e => e.CustomerName).HasMaxLength(64);

                entity.Property(e => e.EMail).HasMaxLength(512);

                entity.Property(e => e.Fax).HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.PersonInCharge).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.ReceiptNo).HasMaxLength(10);

                entity.Property(e => e.RoleRemark).HasMaxLength(64);

                entity.HasOne(d => d.Allowance)
                    .WithOne(p => p.InvoiceAllowanceBuyer)
                    .HasForeignKey<InvoiceAllowanceBuyer>(d => d.AllowanceID)
                    .HasConstraintName("FK_InvoiceAllowanceBuyer_InvoiceAllowance");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.InvoiceAllowanceBuyers)
                    .HasForeignKey(d => d.BuyerID)
                    .HasConstraintName("FK_InvoiceAllowanceBuyer_Organization");
            });

            modelBuilder.Entity<InvoiceAllowanceCancellation>(entity =>
            {
                entity.HasKey(e => e.AllowanceID);

                entity.ToTable("InvoiceAllowanceCancellation");

                entity.Property(e => e.AllowanceID).ValueGeneratedNever();

                entity.Property(e => e.CancelDate)
                    .HasColumnType("datetime")
                    .HasComment("作廢日期");

                entity.Property(e => e.CancelReason).HasMaxLength(256);

                entity.Property(e => e.Remark)
                    .HasMaxLength(256)
                    .HasComment("作廢折讓備註\r\n作廢折讓時必填，填寫作廢原因");

                entity.HasOne(d => d.Allowance)
                    .WithOne(p => p.InvoiceAllowanceCancellation)
                    .HasForeignKey<InvoiceAllowanceCancellation>(d => d.AllowanceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceAllowanceCancellation_InvoiceAllowance");
            });

            modelBuilder.Entity<InvoiceAllowanceDetail>(entity =>
            {
                entity.HasKey(e => new { e.AllowanceID, e.ItemID });

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.InvoiceAllowanceDetails)
                    .HasForeignKey(d => d.AllowanceID)
                    .HasConstraintName("FK_InvoiceAllowanceDetails_InvoiceAllowance");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.InvoiceAllowanceDetails)
                    .HasForeignKey(d => d.ItemID)
                    .HasConstraintName("FK_InvoiceAllowanceDetails_InvoiceAllowanceItem");
            });

            modelBuilder.Entity<InvoiceAllowanceDispatch>(entity =>
            {
                entity.HasKey(e => e.AllowanceID);

                entity.ToTable("InvoiceAllowanceDispatch");

                entity.Property(e => e.AllowanceID).ValueGeneratedNever();

                entity.HasOne(d => d.Allowance)
                    .WithOne(p => p.InvoiceAllowanceDispatch)
                    .HasForeignKey<InvoiceAllowanceDispatch>(d => d.AllowanceID)
                    .HasConstraintName("FK_InvoiceAllowanceDispatch_InvoiceAllowance");
            });

            modelBuilder.Entity<InvoiceAllowanceDispatchLog>(entity =>
            {
                entity.HasKey(e => e.AllowanceID);

                entity.ToTable("InvoiceAllowanceDispatchLog");

                entity.Property(e => e.AllowanceID).ValueGeneratedNever();

                entity.Property(e => e.DispatchDate).HasColumnType("datetime");

                entity.HasOne(d => d.Allowance)
                    .WithOne(p => p.InvoiceAllowanceDispatchLog)
                    .HasForeignKey<InvoiceAllowanceDispatchLog>(d => d.AllowanceID)
                    .HasConstraintName("FK_InvoiceAllowanceDispatchLog_InvoiceAllowance");
            });

            modelBuilder.Entity<InvoiceAllowanceItem>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("InvoiceAllowanceItem");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Amount2).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNo).HasMaxLength(16);

                entity.Property(e => e.ItemNo).HasMaxLength(16);

                entity.Property(e => e.OriginalDescription).HasMaxLength(256);

                entity.Property(e => e.Piece).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Piece2).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PieceUnit).HasMaxLength(16);

                entity.Property(e => e.PieceUnit2).HasMaxLength(16);

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UnitCost2).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.ProductItem)
                    .WithMany(p => p.InvoiceAllowanceItems)
                    .HasForeignKey(d => d.ProductItemID)
                    .HasConstraintName("FK_InvoiceAllowanceItem_InvoiceProductItem");
            });

            modelBuilder.Entity<InvoiceAllowanceSeller>(entity =>
            {
                entity.HasKey(e => e.AllowanceID);

                entity.ToTable("InvoiceAllowanceSeller");

                entity.Property(e => e.AllowanceID).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.ContactName).HasMaxLength(64);

                entity.Property(e => e.CustomerID).HasMaxLength(64);

                entity.Property(e => e.CustomerName).HasMaxLength(64);

                entity.Property(e => e.EMail).HasMaxLength(512);

                entity.Property(e => e.Fax).HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.PersonInCharge).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.ReceiptNo).HasMaxLength(10);

                entity.Property(e => e.RoleRemark).HasMaxLength(64);

                entity.HasOne(d => d.Allowance)
                    .WithOne(p => p.InvoiceAllowanceSeller)
                    .HasForeignKey<InvoiceAllowanceSeller>(d => d.AllowanceID)
                    .HasConstraintName("FK_InvoiceAllowanceSeller_InvoiceAllowance");

                entity.HasOne(d => d.AllowanceNavigation)
                    .WithOne(p => p.InvoiceAllowanceSeller)
                    .HasForeignKey<InvoiceAllowanceSeller>(d => d.AllowanceID)
                    .HasConstraintName("FK_InvoiceAllowanceSeller_Document");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.InvoiceAllowanceSellers)
                    .HasForeignKey(d => d.SellerID)
                    .HasConstraintName("FK_InvoiceAllowanceSeller_Organization");
            });

            modelBuilder.Entity<InvoiceAmountType>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceAmountType");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.Property(e => e.Adjustment)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("角分調整");

                entity.Property(e => e.DiscountAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("扣抵金額");

                entity.Property(e => e.ExchangeRate)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("匯率");

                entity.Property(e => e.OriginalCurrencyAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("原幣金額");

                entity.Property(e => e.SalesAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("應稅銷售額合計(新台幣)");

                entity.Property(e => e.TaxAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("營業稅額");

                entity.Property(e => e.TaxRate)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("稅率");

                entity.Property(e => e.TaxType).HasComment("課稅別\r\n1：應稅\r\n2：零稅率\r\n3：免稅\r\n9：混合應稅與免稅或零稅率 (限收銀機發票無法分辨時使用)");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 5)")
                    .HasComment("總計\r\n整數\r\n(應稅銷售額合計+免稅銷售額合計+零稅率銷售額合計+營業稅額=此總計欄位) ，可為負數");

                entity.Property(e => e.TotalAmountInChinese)
                    .HasMaxLength(32)
                    .HasComment("中文國字大寫金額");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceAmountType)
                    .HasForeignKey<InvoiceAmountType>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceAmountType_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceBuyer>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceBuyer");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.ContactName).HasMaxLength(64);

                entity.Property(e => e.CustomerID).HasMaxLength(64);

                entity.Property(e => e.CustomerName).HasMaxLength(64);

                entity.Property(e => e.CustomerNumber).HasMaxLength(20);

                entity.Property(e => e.EMail).HasMaxLength(512);

                entity.Property(e => e.Fax).HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.PersonInCharge).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.ReceiptNo).HasMaxLength(10);

                entity.Property(e => e.RoleRemark).HasMaxLength(64);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.InvoiceBuyers)
                    .HasForeignKey(d => d.BuyerID)
                    .HasConstraintName("FK_InvoiceBuyer_Organization");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceBuyer)
                    .HasForeignKey<InvoiceBuyer>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceBuyer_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceCancellation>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceCancellation");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.Property(e => e.CancelDate)
                    .HasColumnType("datetime")
                    .HasComment("作廢日期");

                entity.Property(e => e.CancelReason).HasMaxLength(256);

                entity.Property(e => e.CancellationNo)
                    .HasMaxLength(16)
                    .HasComment("作廢發票號碼");

                entity.Property(e => e.Remark)
                    .HasMaxLength(256)
                    .HasComment("作廢備註\r\n作廢發票時必填，填寫作廢原因");

                entity.Property(e => e.ReturnTaxDocumentNo)
                    .HasMaxLength(64)
                    .HasComment("專案作廢核准文號\r\n若發票的作廢時間超過申報期間，則此欄位為必填欄位。若不填寫由上傳營業人自行負責。");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceCancellation)
                    .HasForeignKey<InvoiceCancellation>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceCancellation_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceCancellationDispatch>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceCancellationDispatch");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceCancellationDispatch)
                    .HasForeignKey<InvoiceCancellationDispatch>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceCancellationDispatch_InvoiceCancellation");
            });

            modelBuilder.Entity<InvoiceCancellationDispatchLog>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceCancellationDispatchLog");

                entity.Property(e => e.InvoiceID)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.Property(e => e.DispatchDate).HasColumnType("datetime");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceCancellationDispatchLog)
                    .HasForeignKey<InvoiceCancellationDispatchLog>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceCancellationDispatchLog_InvoiceCancellation");
            });

            modelBuilder.Entity<InvoiceCarrier>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceCarrier");

                entity.Property(e => e.InvoiceID)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.Property(e => e.CarrierNo)
                    .HasMaxLength(64)
                    .HasComment("載具卡號");

                entity.Property(e => e.CarrierNo2).HasMaxLength(64);

                entity.Property(e => e.CarrierType)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasComment("載具類別\r\n1：悠遊卡\r\n2：UXB2B條碼卡");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceCarrier)
                    .HasForeignKey<InvoiceCarrier>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceCarrier_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceID, e.ProductID });

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceDetails_InvoiceItem");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceDetails_InvoiceProduct");
            });

            modelBuilder.Entity<InvoiceDonation>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceDonation");

                entity.Property(e => e.InvoiceID)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.Property(e => e.AgencyCode)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("機構代碼");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceDonation)
                    .HasForeignKey<InvoiceDonation>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceDonation_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceItem");

                entity.Property(e => e.InvoiceID)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.Property(e => e.BuyerRemark)
                    .HasMaxLength(1)
                    .HasComment("買受人註記欄\r\n1：得抵扣之進貨及費用；\r\n2：得抵扣之固定資產；\r\n3：不得抵扣之進貨及費用；\r\n4：不得抵扣之固定資產\r\n");

                entity.Property(e => e.Category)
                    .HasMaxLength(2)
                    .HasComment("沖帳別");

                entity.Property(e => e.CheckNo)
                    .HasMaxLength(10)
                    .HasComment("發票檢查碼");

                entity.Property(e => e.CustomsClearanceMark)
                    .HasMaxLength(1)
                    .HasComment("通關方式註記\r\n1：非經海關出口;\r\n2：經海關出口(零稅率時，為必要欄位)\r\n");

                entity.Property(e => e.DonateMark)
                    .HasMaxLength(1)
                    .HasComment("捐贈註記\r\n以”0”表示 非捐贈發票\r\n以”1”表示 為捐贈發票\r\n");

                entity.Property(e => e.GroupMark)
                    .HasMaxLength(2)
                    .HasComment("彙開註記\r\n以”*”表示 彙開");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasComment("發票日期");

                entity.Property(e => e.InvoiceType).HasComment("發票類別\r\n1: 三聯式;\r\n2: 二聯式;\r\n3: 二聯式收銀機;\r\n4. 特種稅額;\r\n5: 電子計算機;\r\n6: 三聯式收銀機\r\n");

                entity.Property(e => e.No)
                    .HasMaxLength(16)
                    .HasComment("發票號碼");

                entity.Property(e => e.PermitDate)
                    .HasColumnType("datetime")
                    .HasComment("核准日");

                entity.Property(e => e.PermitNumber)
                    .HasMaxLength(20)
                    .HasComment("核准號");

                entity.Property(e => e.PermitWord)
                    .HasMaxLength(40)
                    .HasComment("核准文");

                entity.Property(e => e.PrintMark).HasMaxLength(1);

                entity.Property(e => e.RandomNo)
                    .HasMaxLength(10)
                    .HasComment("發票防偽隨機碼\r\n前端隨機產生");

                entity.Property(e => e.RelateNumber)
                    .HasMaxLength(20)
                    .HasComment("相關號碼");

                entity.Property(e => e.Remark)
                    .HasMaxLength(256)
                    .HasComment("總備註");

                entity.Property(e => e.TaxCenter)
                    .HasMaxLength(40)
                    .HasComment("稅捐稽徵處名稱");

                entity.Property(e => e.TrackCode).HasMaxLength(2);

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceItem)
                    .HasForeignKey<InvoiceItem>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceItem_Document");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.SellerID)
                    .HasConstraintName("FK_InvoiceItem_Organization");
            });

            modelBuilder.Entity<InvoiceItemDispatch>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceItemDispatch");

                entity.Property(e => e.InvoiceID)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceItemDispatch)
                    .HasForeignKey<InvoiceItemDispatch>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceItemDispatch_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceItemDispatchLog>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceItemDispatchLog");

                entity.Property(e => e.InvoiceID)
                    .ValueGeneratedNever()
                    .HasComment("Primary Key");

                entity.Property(e => e.DispatchDate).HasColumnType("datetime");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceItemDispatchLog)
                    .HasForeignKey<InvoiceItemDispatchLog>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceItemDispatchLog_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceNoAssignment>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceNoAssignment");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.HasOne(d => d.Interval)
                    .WithMany(p => p.InvoiceNoAssignments)
                    .HasForeignKey(d => d.IntervalID)
                    .HasConstraintName("FK_InvoiceNoAssignment_InvoiceNoInterval");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceNoAssignment)
                    .HasForeignKey<InvoiceNoAssignment>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceNoAssignment_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceNoInterval>(entity =>
            {
                entity.HasKey(e => e.IntervalID);

                entity.ToTable("InvoiceNoInterval");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.InvoiceNoIntervals)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_InvoiceNoInterval_InvoiceNoIntervalGroup");

                entity.HasOne(d => d.InvoiceTrackCodeAssignment)
                    .WithMany(p => p.InvoiceNoIntervals)
                    .HasForeignKey(d => new { d.TrackID, d.SellerID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceNoInterval_InvoiceTrackCodeAssignment");
            });

            modelBuilder.Entity<InvoiceNoIntervalGroup>(entity =>
            {
                entity.HasKey(e => e.GroupID);

                entity.ToTable("InvoiceNoIntervalGroup");
            });

            modelBuilder.Entity<InvoiceProduct>(entity =>
            {
                entity.HasKey(e => e.ProductID);

                entity.ToTable("InvoiceProduct");

                entity.Property(e => e.Brief).HasMaxLength(256);
            });

            modelBuilder.Entity<InvoiceProductItem>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("InvoiceProductItem");

                entity.Property(e => e.CostAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.CostAmount2).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.FreightAmount).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.ItemNo).HasMaxLength(16);

                entity.Property(e => e.OriginalPrice).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Piece).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Piece2).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.PieceUnit).HasMaxLength(16);

                entity.Property(e => e.PieceUnit2).HasMaxLength(16);

                entity.Property(e => e.RelateNumber).HasMaxLength(64);

                entity.Property(e => e.Remark).HasMaxLength(128);

                entity.Property(e => e.Spec).HasMaxLength(128);

                entity.Property(e => e.UnitCost).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UnitCost2).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.UnitFreight).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 5)");

                entity.Property(e => e.WeightUnit).HasMaxLength(16);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceProductItems)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK_InvoiceProductItem_InvoiceProduct");
            });

            modelBuilder.Entity<InvoicePurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoicePurchaseOrder");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.Property(e => e.OrderNo).HasMaxLength(64);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoicePurchaseOrder)
                    .HasForeignKey<InvoicePurchaseOrder>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoicePurchaseOrder_InvoiceItem");
            });

            modelBuilder.Entity<InvoiceSeller>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceSeller");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.ContactName).HasMaxLength(64);

                entity.Property(e => e.CustomerID).HasMaxLength(64);

                entity.Property(e => e.CustomerName).HasMaxLength(64);

                entity.Property(e => e.EMail).HasMaxLength(512);

                entity.Property(e => e.Fax).HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.PersonInCharge).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.ReceiptNo).HasMaxLength(10);

                entity.Property(e => e.RoleRemark).HasMaxLength(64);

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceSeller)
                    .HasForeignKey<InvoiceSeller>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceSeller_InvoiceItem");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.InvoiceSellers)
                    .HasForeignKey(d => d.SellerID)
                    .HasConstraintName("FK_InvoiceSeller_Organization");
            });

            modelBuilder.Entity<InvoiceTrackCode>(entity =>
            {
                entity.HasKey(e => e.TrackID);

                entity.ToTable("InvoiceTrackCode");

                entity.Property(e => e.TrackCode)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<InvoiceTrackCodeAssignment>(entity =>
            {
                entity.HasKey(e => new { e.TrackID, e.SellerID });

                entity.ToTable("InvoiceTrackCodeAssignment");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.InvoiceTrackCodeAssignments)
                    .HasForeignKey(d => d.SellerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceTrackCodeAssignment_Organization");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.InvoiceTrackCodeAssignments)
                    .HasForeignKey(d => d.TrackID)
                    .HasConstraintName("FK_InvoiceTrackCodeAssignment_InvoiceTrackCode");
            });

            modelBuilder.Entity<InvoiceWinningNumber>(entity =>
            {
                entity.HasKey(e => e.InvoiceID);

                entity.ToTable("InvoiceWinningNumber");

                entity.Property(e => e.InvoiceID).ValueGeneratedNever();

                entity.Property(e => e.BonusDescription).HasMaxLength(32);

                entity.Property(e => e.DataType).HasMaxLength(1);

                entity.Property(e => e.DownloadDate).HasColumnType("datetime");

                entity.Property(e => e.TrackCode).HasMaxLength(2);

                entity.Property(e => e.WinningNO)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasComment("中獎號碼");

                entity.Property(e => e.WinningType).HasMaxLength(64);

                entity.HasOne(d => d.Invoice)
                    .WithOne(p => p.InvoiceWinningNumber)
                    .HasForeignKey<InvoiceWinningNumber>(d => d.InvoiceID)
                    .HasConstraintName("FK_InvoiceWinningNumber_InvoiceItem");

                entity.HasOne(d => d.Winning)
                    .WithMany(p => p.InvoiceWinningNumbers)
                    .HasForeignKey(d => d.WinningID)
                    .HasConstraintName("FK_InvoiceWinningNumber_UniformInvoiceWinningNumber");
            });

            modelBuilder.Entity<IsInternalLesson>(entity =>
            {
                entity.HasKey(e => e.PriceID);

                entity.ToTable("IsInternalLesson");

                entity.Property(e => e.PriceID).ValueGeneratedNever();

                entity.HasOne(d => d.Price)
                    .WithOne(p => p.IsInternalLesson)
                    .HasForeignKey<IsInternalLesson>(d => d.PriceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IsInternalLesson_LessonPriceType");
            });

            modelBuilder.Entity<IsWelfareGiftLesson>(entity =>
            {
                entity.HasKey(e => e.PriceID);

                entity.ToTable("IsWelfareGiftLesson");

                entity.Property(e => e.PriceID).ValueGeneratedNever();

                entity.HasOne(d => d.Price)
                    .WithOne(p => p.IsWelfareGiftLesson)
                    .HasForeignKey<IsWelfareGiftLesson>(d => d.PriceID)
                    .HasConstraintName("FK_IsWelfareGiftLesson_LessonPriceType");
            });

            modelBuilder.Entity<LearnerAward>(entity =>
            {
                entity.HasKey(e => e.AwardID);

                entity.ToTable("LearnerAward");

                entity.Property(e => e.AwardDate).HasColumnType("datetime");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.LearnerAwardActors)
                    .HasForeignKey(d => d.ActorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerAward_UserProfile1");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.LearnerAwards)
                    .HasForeignKey(d => d.ItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerAward_BonusAwardingItem");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.LearnerAwardUIDNavigations)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerAward_UserProfile");
            });

            modelBuilder.Entity<LearnerFitnessAdvisor>(entity =>
            {
                entity.HasKey(e => new { e.UID, e.CoachID });

                entity.ToTable("LearnerFitnessAdvisor");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.LearnerFitnessAdvisors)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerFitnessAdvisor_ServingCoach");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.LearnerFitnessAdvisors)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_LearnerFitnessAdvisor_UserProfile");
            });

            modelBuilder.Entity<LearnerFitnessAssessment>(entity =>
            {
                entity.HasKey(e => e.AssessmentID);

                entity.ToTable("LearnerFitnessAssessment");

                entity.Property(e => e.AssessmentDate).HasColumnType("datetime");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.LearnerFitnessAssessments)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerFitnessAssessment_UserProfile");
            });

            modelBuilder.Entity<LearnerFitnessAssessmentResult>(entity =>
            {
                entity.HasKey(e => new { e.AssessmentID, e.ItemID });

                entity.ToTable("LearnerFitnessAssessmentResult");

                entity.Property(e => e.Assessment).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.AssessmentNavigation)
                    .WithMany(p => p.LearnerFitnessAssessmentResults)
                    .HasForeignKey(d => d.AssessmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerFitnessAssessmentResult_LearnerFitnessAssessment");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.LearnerFitnessAssessmentResults)
                    .HasForeignKey(d => d.ItemID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LearnerFitnessAssessmentResult_FitnessAssessmentItem");
            });

            modelBuilder.Entity<LessonAttendance>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("LessonAttendance");

                entity.Property(e => e.LessonID).ValueGeneratedNever();

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.LessonAttendance)
                    .HasForeignKey<LessonAttendance>(d => d.LessonID)
                    .HasConstraintName("FK_LessonAttendance_LessonTime");
            });

            modelBuilder.Entity<LessonAttendanceDueDate>(entity =>
            {
                entity.HasKey(e => e.DueDate);

                entity.ToTable("LessonAttendanceDueDate");

                entity.Property(e => e.DueDate).HasColumnType("date");
            });

            modelBuilder.Entity<LessonComment>(entity =>
            {
                entity.HasKey(e => e.CommentID);

                entity.ToTable("LessonComment");

                entity.HasIndex(e => e.CommentDate, "IX_LessonComment");

                entity.Property(e => e.CommentDate).HasColumnType("datetime");

                entity.HasOne(d => d.Hearer)
                    .WithMany(p => p.LessonCommentHearers)
                    .HasForeignKey(d => d.HearerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonComment_UserProfile1");

                entity.HasOne(d => d.Speaker)
                    .WithMany(p => p.LessonCommentSpeakers)
                    .HasForeignKey(d => d.SpeakerID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonComment_UserProfile");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.LessonComments)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_LessonComment_LevelExpression");
            });

            modelBuilder.Entity<LessonFeedBack>(entity =>
            {
                entity.HasKey(e => new { e.LessonID, e.RegisterID });

                entity.ToTable("LessonFeedBack");

                entity.Property(e => e.FeedBackDate).HasColumnType("datetime");

                entity.Property(e => e.RemarkDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonFeedBacks)
                    .HasForeignKey(d => d.LessonID)
                    .HasConstraintName("FK_LessonFeedBack_LessonTime");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.LessonFeedBacks)
                    .HasForeignKey(d => d.RegisterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonFeedBack_RegisterLesson");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.LessonFeedBacks)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_LessonFeedBack_LevelExpression");
            });

            modelBuilder.Entity<LessonFitnessAssessment>(entity =>
            {
                entity.HasKey(e => e.AssessmentID);

                entity.ToTable("LessonFitnessAssessment");

                entity.Property(e => e.AssessmentDate).HasColumnType("datetime");

                entity.Property(e => e.FeedBackDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonFitnessAssessments)
                    .HasForeignKey(d => d.LessonID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LessonFitnessAssessment_LessonTime");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.LessonFitnessAssessments)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonFitnessAssessment_UserProfile");
            });

            modelBuilder.Entity<LessonFitnessAssessmentReport>(entity =>
            {
                entity.HasKey(e => new { e.AssessmentID, e.ItemID });

                entity.ToTable("LessonFitnessAssessmentReport");

                entity.Property(e => e.ByCustom).HasMaxLength(64);

                entity.Property(e => e.SingleAssessment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TotalAssessment).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Assessment)
                    .WithMany(p => p.LessonFitnessAssessmentReports)
                    .HasForeignKey(d => d.AssessmentID)
                    .HasConstraintName("FK_LessonFitnessAssessmentReport_LessonFitnessAssessment");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.LessonFitnessAssessmentReports)
                    .HasForeignKey(d => d.ItemID)
                    .HasConstraintName("FK_LessonFitnessAssessmentReport_FitnessAssessmentItem");
            });

            modelBuilder.Entity<LessonPlan>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("LessonPlan");

                entity.Property(e => e.LessonID).ValueGeneratedNever();

                entity.Property(e => e.CommitAttendance).HasColumnType("datetime");

                entity.Property(e => e.FeedBackDate).HasColumnType("datetime");

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.LessonPlan)
                    .HasForeignKey<LessonPlan>(d => d.LessonID)
                    .HasConstraintName("FK_LessonPlan_LessonTime");
            });

            modelBuilder.Entity<LessonPriceProperty>(entity =>
            {
                entity.HasKey(e => new { e.PriceID, e.PropertyID });

                entity.ToTable("LessonPriceProperty");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.LessonPriceProperties)
                    .HasForeignKey(d => d.PriceID)
                    .HasConstraintName("FK_LessonPriceProperty_LessonPriceType");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.LessonPriceProperties)
                    .HasForeignKey(d => d.PropertyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonPriceProperty_LevelExpression");
            });

            modelBuilder.Entity<LessonPriceSeries>(entity =>
            {
                entity.HasKey(e => e.PriceID);

                entity.Property(e => e.PriceID).ValueGeneratedNever();

                entity.HasOne(d => d.Price)
                    .WithOne(p => p.LessonPriceSeries)
                    .HasForeignKey<LessonPriceSeries>(d => d.PriceID)
                    .HasConstraintName("FK_LessonPriceSeries_LessonPriceType");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.LessonPriceSeries)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_LessonPriceSeries_LevelExpression");
            });

            modelBuilder.Entity<LessonPriceType>(entity =>
            {
                entity.HasKey(e => e.PriceID);

                entity.ToTable("LessonPriceType");

                entity.Property(e => e.PriceID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(64);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.LessonPriceTypes)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_LessonPriceType_BranchStore");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.LessonPriceTypes)
                    .HasForeignKey(d => d.SeriesID)
                    .HasConstraintName("FK_LessonPriceType_LessonPriceSeries");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.LessonPriceTypes)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_LessonPriceType_LevelExpression");

                entity.HasOne(d => d.UsageTypeNavigation)
                    .WithMany(p => p.LessonPriceTypes)
                    .HasForeignKey(d => d.UsageType)
                    .HasConstraintName("FK_LessonPriceType_UsageType");
            });

            modelBuilder.Entity<LessonTime>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("LessonTime");

                entity.HasIndex(e => new { e.AttendingCoach, e.BranchID, e.ClassTime }, "IX_LessonTime_AttendingCoach_BranchID_ClassTime");

                entity.HasIndex(e => e.GroupID, "IX_LessonTime_GroupID");

                entity.HasIndex(e => e.RegisterID, "IX_LessonTime_RegisterID");

                entity.Property(e => e.ClassTime).HasColumnType("datetime");

                entity.Property(e => e.Place).HasMaxLength(64);

                entity.HasOne(d => d.AttendingCoachNavigation)
                    .WithMany(p => p.LessonTimeAttendingCoachNavigations)
                    .HasForeignKey(d => d.AttendingCoach)
                    .HasConstraintName("FK_LessonTime_ServingCoach1");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.LessonTimes)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_LessonTime_BranchStore");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.LessonTimes)
                    .HasForeignKey(d => d.GroupID)
                    .HasConstraintName("FK_LessonTime_GroupingLesson");

                entity.HasOne(d => d.HourOfClassTimeNavigation)
                    .WithMany(p => p.LessonTimes)
                    .HasForeignKey(d => d.HourOfClassTime)
                    .HasConstraintName("FK_LessonTime_DailyWorkingHour");

                entity.HasOne(d => d.InvitedCoachNavigation)
                    .WithMany(p => p.LessonTimeInvitedCoachNavigations)
                    .HasForeignKey(d => d.InvitedCoach)
                    .HasConstraintName("FK_LessonTime_ServingCoach");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.LessonTimes)
                    .HasForeignKey(d => d.RegisterID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonTime_RegisterLesson");
            });

            modelBuilder.Entity<LessonTimeExpansion>(entity =>
            {
                entity.HasKey(e => new { e.ClassDate, e.Hour, e.RegisterID });

                entity.ToTable("LessonTimeExpansion");

                entity.Property(e => e.ClassDate).HasColumnType("date");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonTimeExpansions)
                    .HasForeignKey(d => d.LessonID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LessonTimeExpansion_LessonTime");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.LessonTimeExpansions)
                    .HasForeignKey(d => d.RegisterID)
                    .HasConstraintName("FK_LessonTimeExpansion_RegisterLesson");
            });

            modelBuilder.Entity<LessonTimeSettlement>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("LessonTimeSettlement");

                entity.Property(e => e.LessonID).ValueGeneratedNever();

                entity.Property(e => e.MarkedGradeIndex).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.CoachWorkPlaceNavigation)
                    .WithMany(p => p.LessonTimeSettlements)
                    .HasForeignKey(d => d.CoachWorkPlace)
                    .HasConstraintName("FK_LessonTimeSettlement_BranchStore");

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.LessonTimeSettlement)
                    .HasForeignKey<LessonTimeSettlement>(d => d.LessonID)
                    .HasConstraintName("FK_LessonTimeSettlement_LessonTime");

                entity.HasOne(d => d.ProfessionalLevel)
                    .WithMany(p => p.LessonTimeSettlements)
                    .HasForeignKey(d => d.ProfessionalLevelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LessonTimeSettlement_ProfessionalLevel");

                entity.HasOne(d => d.Settlement)
                    .WithMany(p => p.LessonTimeSettlements)
                    .HasForeignKey(d => d.SettlementID)
                    .HasConstraintName("FK_LessonTimeSettlement_Settlement");
            });

            modelBuilder.Entity<LessonTrend>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("LessonTrend");

                entity.Property(e => e.LessonID).ValueGeneratedNever();

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.LessonTrend)
                    .HasForeignKey<LessonTrend>(d => d.LessonID)
                    .HasConstraintName("FK_LessonTrend_LessonTime");
            });

            modelBuilder.Entity<LevelExpression>(entity =>
            {
                entity.HasKey(e => e.LevelID);

                entity.ToTable("LevelExpression");

                entity.Property(e => e.LevelID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Expression)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MerchandiseTransaction>(entity =>
            {
                entity.HasKey(e => new { e.TransactionID, e.ProductID });

                entity.ToTable("MerchandiseTransaction");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MerchandiseTransactions)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchandiseTransaction_MerchandiseWindow");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.MerchandiseTransactions)
                    .HasForeignKey(d => d.TransactionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchandiseTransaction_MerchandiseTransactionType");
            });

            modelBuilder.Entity<MerchandiseTransactionType>(entity =>
            {
                entity.HasKey(e => e.TransactionID);

                entity.ToTable("MerchandiseTransactionType");

                entity.Property(e => e.TransactionID).ValueGeneratedNever();

                entity.Property(e => e.TransactionType).HasMaxLength(16);

                entity.HasOne(d => d.CategorySource)
                    .WithMany(p => p.InverseCategorySource)
                    .HasForeignKey(d => d.CategorySourceID)
                    .HasConstraintName("FK_MerchandiseTransactionType_MerchandiseTransactionType");
            });

            modelBuilder.Entity<MerchandiseWindow>(entity =>
            {
                entity.HasKey(e => e.ProductID);

                entity.ToTable("MerchandiseWindow");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.SampleUrl).HasMaxLength(256);

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.MerchandiseWindows)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_MerchandiseWindow_LevelExpression");
            });

            modelBuilder.Entity<MonthlyBranchIndicator>(entity =>
            {
                entity.HasKey(e => new { e.PeriodID, e.BranchID });

                entity.ToTable("MonthlyBranchIndicator", "KPI");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MonthlyBranchIndicators)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyBranchIndicator_BranchStore");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.MonthlyBranchIndicators)
                    .HasForeignKey(d => d.PeriodID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyBranchIndicator_MonthlyIndicator");
            });

            modelBuilder.Entity<MonthlyBranchRevenueGoal>(entity =>
            {
                entity.HasKey(e => new { e.PeriodID, e.BranchID, e.GradeID });

                entity.ToTable("MonthlyBranchRevenueGoal", "KPI");

                entity.Property(e => e.CustomIndicatorPercentage).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MonthlyBranchRevenueIndicator)
                    .WithOne(p => p.MonthlyBranchRevenueGoal)
                    .HasForeignKey<MonthlyBranchRevenueGoal>(d => new { d.PeriodID, d.BranchID, d.GradeID })
                    .HasConstraintName("FK_MonthlyBranchRevenueGoal_MonthlyBranchRevenueIndicator");
            });

            modelBuilder.Entity<MonthlyBranchRevenueIndicator>(entity =>
            {
                entity.HasKey(e => new { e.PeriodID, e.BranchID, e.GradeID });

                entity.ToTable("MonthlyBranchRevenueIndicator", "KPI");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MonthlyBranchRevenueIndicators)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyBranchRevenueIndicator_BranchStore");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.MonthlyBranchRevenueIndicators)
                    .HasForeignKey(d => d.GradeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyBranchRevenueIndicator_MonthlyRevenueGrade");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.MonthlyBranchRevenueIndicators)
                    .HasForeignKey(d => d.PeriodID)
                    .HasConstraintName("FK_MonthlyBranchRevenueIndicator_MonthlyIndicator");

                entity.HasOne(d => d.MonthlyBranchIndicator)
                    .WithMany(p => p.MonthlyBranchRevenueIndicators)
                    .HasForeignKey(d => new { d.PeriodID, d.BranchID })
                    .HasConstraintName("FK_MonthlyBranchRevenueIndicator_MonthlyBranchIndicator");
            });

            modelBuilder.Entity<MonthlyCoachRevenueIndicator>(entity =>
            {
                entity.HasKey(e => new { e.PeriodID, e.CoachID })
                    .HasName("PK_MonthlyCoachRevenuIndicator");

                entity.ToTable("MonthlyCoachRevenueIndicator", "KPI");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.MonthlyCoachRevenueIndicators)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_MonthlyCoachRevenuIndicator_BranchStore");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.MonthlyCoachRevenueIndicators)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyCoachRevenuIndicator_ServingCoach");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.MonthlyCoachRevenueIndicators)
                    .HasForeignKey(d => d.LevelID)
                    .HasConstraintName("FK_MonthlyCoachRevenuIndicator_ProfessionalLevel");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.MonthlyCoachRevenueIndicators)
                    .HasForeignKey(d => d.PeriodID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyCoachRevenuIndicator_MonthlyIndicator");
            });

            modelBuilder.Entity<MonthlyIndicator>(entity =>
            {
                entity.HasKey(e => e.PeriodID);

                entity.ToTable("MonthlyIndicator", "KPI");

                entity.HasIndex(e => new { e.Year, e.Month }, "IX_MonthlyIndicator")
                    .IsUnique();

                entity.Property(e => e.EndExclusiveDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MonthlyRevenueGoal>(entity =>
            {
                entity.HasKey(e => new { e.PeriodID, e.GradeID });

                entity.ToTable("MonthlyRevenueGoal", "KPI");

                entity.Property(e => e.CustomIndicatorPercentage).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MonthlyRevenueIndicator)
                    .WithOne(p => p.MonthlyRevenueGoal)
                    .HasForeignKey<MonthlyRevenueGoal>(d => new { d.PeriodID, d.GradeID })
                    .HasConstraintName("FK_MonthlyRevenueGoal_MonthlyRevenueIndicator");
            });

            modelBuilder.Entity<MonthlyRevenueGrade>(entity =>
            {
                entity.HasKey(e => e.GradeID);

                entity.ToTable("MonthlyRevenueGrade", "KPI");

                entity.Property(e => e.GradeID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(16);
            });

            modelBuilder.Entity<MonthlyRevenueIndicator>(entity =>
            {
                entity.HasKey(e => new { e.PeriodID, e.GradeID });

                entity.ToTable("MonthlyRevenueIndicator", "KPI");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.MonthlyRevenueIndicators)
                    .HasForeignKey(d => d.GradeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyRevenueIndicator_MonthlyRevenueGrade");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.MonthlyRevenueIndicators)
                    .HasForeignKey(d => d.PeriodID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MonthlyRevenueIndicator_MonthlyIndicator");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.CompanyID);

                entity.ToTable("Organization");

                entity.Property(e => e.CompanyID).HasComment("主鍵");

                entity.Property(e => e.Addr)
                    .HasMaxLength(256)
                    .HasComment("地址");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(128)
                    .HasComment("機關名稱");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(512)
                    .HasComment("連絡人電子郵件");

                entity.Property(e => e.ContactFax).HasMaxLength(20);

                entity.Property(e => e.ContactMobilePhone).HasMaxLength(20);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .HasComment("連絡人");

                entity.Property(e => e.ContactPhone).HasMaxLength(20);

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(16)
                    .HasComment("連絡人職稱");

                entity.Property(e => e.EnglishAddr).HasMaxLength(256);

                entity.Property(e => e.EnglishName).HasMaxLength(50);

                entity.Property(e => e.EnglishRegAddr).HasMaxLength(256);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .HasComment("傳真");

                entity.Property(e => e.InvoiceSignature).HasMaxLength(64);

                entity.Property(e => e.LogoURL).HasMaxLength(200);

                entity.Property(e => e.Phone)
                    .HasMaxLength(64)
                    .HasComment("電話");

                entity.Property(e => e.ReceiptNo)
                    .HasMaxLength(10)
                    .HasComment("統一編號");

                entity.Property(e => e.RegAddr).HasMaxLength(256);

                entity.Property(e => e.TaxNo).HasMaxLength(16);

                entity.Property(e => e.UndertakerFax).HasMaxLength(20);

                entity.Property(e => e.UndertakerID).HasMaxLength(16);

                entity.Property(e => e.UndertakerMobilePhone).HasMaxLength(20);

                entity.Property(e => e.UndertakerName)
                    .HasMaxLength(50)
                    .HasComment("負責人姓名");

                entity.Property(e => e.UndertakerPhone).HasMaxLength(20);
            });

            modelBuilder.Entity<PDQGroup>(entity =>
            {
                entity.HasKey(e => e.GroupID);

                entity.ToTable("PDQGroup");

                entity.Property(e => e.GroupID).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Conclusion)
                    .WithMany(p => p.PDQGroups)
                    .HasForeignKey(d => d.ConclusionID)
                    .HasConstraintName("FK_PDQGroup_PDQQuestion");
            });

            modelBuilder.Entity<PDQQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionID);

                entity.ToTable("PDQQuestion");

                entity.Property(e => e.Question).HasMaxLength(1024);

                entity.HasOne(d => d.Asker)
                    .WithMany(p => p.PDQQuestions)
                    .HasForeignKey(d => d.AskerID)
                    .HasConstraintName("FK_PDQQuestion_UserProfile");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.PDQQuestions)
                    .HasForeignKey(d => d.GroupID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PDQQuestion_PDQGroup");

                entity.HasOne(d => d.QuestionTypeNavigation)
                    .WithMany(p => p.PDQQuestions)
                    .HasForeignKey(d => d.QuestionType)
                    .HasConstraintName("FK_PDQQuestion_LevelExpression");
            });

            modelBuilder.Entity<PDQQuestionExtension>(entity =>
            {
                entity.HasKey(e => e.QuestionID);

                entity.ToTable("PDQQuestionExtension");

                entity.HasIndex(e => e.AwardingAction, "IX_PDQQuestionExtension");

                entity.Property(e => e.QuestionID).ValueGeneratedNever();

                entity.Property(e => e.CreationTime).HasColumnType("datetime");

                entity.HasOne(d => d.Question)
                    .WithOne(p => p.PDQQuestionExtension)
                    .HasForeignKey<PDQQuestionExtension>(d => d.QuestionID)
                    .HasConstraintName("FK_PDQQuestionExtension_PDQQuestion");
            });

            modelBuilder.Entity<PDQSuggestion>(entity =>
            {
                entity.HasKey(e => e.SuggestionID);

                entity.ToTable("PDQSuggestion");

                entity.Property(e => e.Suggestion).HasMaxLength(256);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.PDQSuggestions)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_PDQSuggestion_PDQQuestion");
            });

            modelBuilder.Entity<PDQTask>(entity =>
            {
                entity.HasKey(e => e.TaskID);

                entity.ToTable("PDQTask");

                entity.Property(e => e.TaskDate).HasColumnType("datetime");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.PDQTasks)
                    .HasForeignKey(d => d.QuestionID)
                    .HasConstraintName("FK_PDQTask_PDQQuestion");

                entity.HasOne(d => d.Questionnaire)
                    .WithMany(p => p.PDQTasks)
                    .HasForeignKey(d => d.QuestionnaireID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PDQTask_QuestionnaireRequest");

                entity.HasOne(d => d.Suggestion)
                    .WithMany(p => p.PDQTasks)
                    .HasForeignKey(d => d.SuggestionID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PDQTask_PDQSuggestion");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.PDQTasks)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PDQTask_UserProfile");
            });

            modelBuilder.Entity<PDQTaskBonu>(entity =>
            {
                entity.HasKey(e => e.TaskID);

                entity.Property(e => e.TaskID).ValueGeneratedNever();

                entity.HasOne(d => d.Task)
                    .WithOne(p => p.PDQTaskBonu)
                    .HasForeignKey<PDQTaskBonu>(d => d.TaskID)
                    .HasConstraintName("FK_PDQTaskBonus_PDQTask");
            });

            modelBuilder.Entity<PDQTaskItem>(entity =>
            {
                entity.HasKey(e => new { e.TaskID, e.SuggestionID });

                entity.ToTable("PDQTaskItem");

                entity.HasOne(d => d.Suggestion)
                    .WithMany(p => p.PDQTaskItems)
                    .HasForeignKey(d => d.SuggestionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PDQTaskItem_PDQSuggestion");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.PDQTaskItems)
                    .HasForeignKey(d => d.TaskID)
                    .HasConstraintName("FK_PDQTaskItem_PDQTask");
            });

            modelBuilder.Entity<PDQType>(entity =>
            {
                entity.HasKey(e => e.QuestionID);

                entity.ToTable("PDQType");

                entity.Property(e => e.QuestionID).ValueGeneratedNever();

                entity.HasOne(d => d.Question)
                    .WithOne(p => p.PDQType)
                    .HasForeignKey<PDQType>(d => d.QuestionID)
                    .HasConstraintName("FK_PDQType_PDQQuestion");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PDQTypes)
                    .HasForeignKey(d => d.TypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PDQType_LevelExpression");
            });

            modelBuilder.Entity<PDQUserAssessment>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("PDQUserAssessment");

                entity.Property(e => e.UID).ValueGeneratedNever();

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.PDQUserAssessments)
                    .HasForeignKey(d => d.GoalID)
                    .HasConstraintName("FK_PDQUserAssessment_GoalAboutPDQ");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.PDQUserAssessments)
                    .HasForeignKey(d => d.LevelID)
                    .HasConstraintName("FK_PDQUserAssessment_TrainingLevelAboutPDQ");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.PDQUserAssessments)
                    .HasForeignKey(d => d.StyleID)
                    .HasConstraintName("FK_PDQUserAssessment_StyleAboutPDQ");

                entity.HasOne(d => d.UIDNavigation)
                    .WithOne(p => p.PDQUserAssessment)
                    .HasForeignKey<PDQUserAssessment>(d => d.UID)
                    .HasConstraintName("FK_PDQUserAssessment_UserProfile");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.InvoiceID).HasComment("Primary Key");

                entity.Property(e => e.PaymentType).HasMaxLength(32);

                entity.Property(e => e.PayoffDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(256);

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AllowanceID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Payment_InvoiceAllowance");

                entity.HasOne(d => d.Handler)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.HandlerID)
                    .HasConstraintName("FK_Payment_UserProfile");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.InvoiceID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Payment_InvoiceItem");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Payment_LevelExpression");
            });

            modelBuilder.Entity<PaymentAudit>(entity =>
            {
                entity.HasKey(e => e.PaymentID)
                    .HasName("PK_PaymentAudit_1");

                entity.ToTable("PaymentAudit");

                entity.Property(e => e.PaymentID).ValueGeneratedNever();

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.HasOne(d => d.Auditor)
                    .WithMany(p => p.PaymentAudits)
                    .HasForeignKey(d => d.AuditorID)
                    .HasConstraintName("FK_PaymentAudit_UserProfile");

                entity.HasOne(d => d.Payment)
                    .WithOne(p => p.PaymentAudit)
                    .HasForeignKey<PaymentAudit>(d => d.PaymentID)
                    .HasConstraintName("FK_PaymentAudit_Payment");
            });

            modelBuilder.Entity<PaymentOrder>(entity =>
            {
                entity.HasKey(e => new { e.PaymentID, e.ProductID });

                entity.ToTable("PaymentOrder");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PaymentOrders)
                    .HasForeignKey(d => d.PaymentID)
                    .HasConstraintName("FK_PaymentOrder_PaymentTransaction");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PaymentOrders)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentOrder_MerchandiseWindow");
            });

            modelBuilder.Entity<PaymentTransaction>(entity =>
            {
                entity.HasKey(e => e.PaymentID);

                entity.ToTable("PaymentTransaction");

                entity.Property(e => e.PaymentID).ValueGeneratedNever();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.PaymentTransactions)
                    .HasForeignKey(d => d.BranchID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentTransaction_BranchStore");

                entity.HasOne(d => d.Payment)
                    .WithOne(p => p.PaymentTransaction)
                    .HasForeignKey<PaymentTransaction>(d => d.PaymentID)
                    .HasConstraintName("FK_PaymentTransaction_Payment");
            });

            modelBuilder.Entity<PersonalExercisePurpose>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("PersonalExercisePurpose");

                entity.Property(e => e.UID).ValueGeneratedNever();

                entity.Property(e => e.AbilityStyle).HasMaxLength(16);

                entity.Property(e => e.PowerAbility).HasMaxLength(16);

                entity.Property(e => e.Purpose).HasMaxLength(16);

                entity.HasOne(d => d.UIDNavigation)
                    .WithOne(p => p.PersonalExercisePurpose)
                    .HasForeignKey<PersonalExercisePurpose>(d => d.UID)
                    .HasConstraintName("FK_PersonalExercisePurpose_UserProfile");
            });

            modelBuilder.Entity<PersonalExercisePurposeItem>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("PersonalExercisePurposeItem");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.InitialDate).HasColumnType("datetime");

                entity.Property(e => e.PurposeItem)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.PersonalExercisePurposeItems)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_PersonalExercisePurposeItem_PersonalExercisePurpose");
            });

            modelBuilder.Entity<PreferredLessonTime>(entity =>
            {
                entity.HasKey(e => e.LessonID);

                entity.ToTable("PreferredLessonTime");

                entity.Property(e => e.LessonID).ValueGeneratedNever();

                entity.Property(e => e.ApprovalDate).HasColumnType("datetime");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.PreferredLessonTimes)
                    .HasForeignKey(d => d.ApproverID)
                    .HasConstraintName("FK_PreferredLessonTime_UserProfile");

                entity.HasOne(d => d.Lesson)
                    .WithOne(p => p.PreferredLessonTime)
                    .HasForeignKey<PreferredLessonTime>(d => d.LessonID)
                    .HasConstraintName("FK_PreferredLessonTime_LessonTime");
            });

            modelBuilder.Entity<ProfessionalCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateID);

                entity.ToTable("ProfessionalCertificate");

                entity.Property(e => e.CertificateID).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(64);
            });

            modelBuilder.Entity<ProfessionalLevel>(entity =>
            {
                entity.HasKey(e => e.LevelID);

                entity.ToTable("ProfessionalLevel");

                entity.Property(e => e.LevelID).ValueGeneratedNever();

                entity.Property(e => e.DisplayName).HasMaxLength(16);

                entity.Property(e => e.GradeIndex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProfessionalLevels)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_ProfessionalLevel_LevelExpression");
            });

            modelBuilder.Entity<ProfessionalLevelReview>(entity =>
            {
                entity.HasKey(e => e.LevelID);

                entity.ToTable("ProfessionalLevelReview");

                entity.Property(e => e.LevelID).ValueGeneratedNever();

                entity.HasOne(d => d.Demotion)
                    .WithMany(p => p.ProfessionalLevelReviewDemotions)
                    .HasForeignKey(d => d.DemotionID)
                    .HasConstraintName("FK_ProfessionalLevelReview_ProfessionalLevel2");

                entity.HasOne(d => d.Level)
                    .WithOne(p => p.ProfessionalLevelReviewLevel)
                    .HasForeignKey<ProfessionalLevelReview>(d => d.LevelID)
                    .HasConstraintName("FK_ProfessionalLevelReview_ProfessionalLevel");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.ProfessionalLevelReviewPromotions)
                    .HasForeignKey(d => d.PromotionID)
                    .HasConstraintName("FK_ProfessionalLevelReview_ProfessionalLevel1");
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.HasKey(e => e.DocID);

                entity.ToTable("Publication");

                entity.Property(e => e.DocID).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Doc)
                    .WithOne(p => p.Publication)
                    .HasForeignKey<Publication>(d => d.DocID)
                    .HasConstraintName("FK_Publication_Article");
            });

            modelBuilder.Entity<QuestionnaireCoachBypass>(entity =>
            {
                entity.HasKey(e => e.QuestionnaireID);

                entity.ToTable("QuestionnaireCoachBypass");

                entity.Property(e => e.QuestionnaireID).ValueGeneratedNever();

                entity.HasOne(d => d.Questionnaire)
                    .WithOne(p => p.QuestionnaireCoachBypass)
                    .HasForeignKey<QuestionnaireCoachBypass>(d => d.QuestionnaireID)
                    .HasConstraintName("FK_QuestionnaireCoachBypass_QuestionnaireRequest");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.QuestionnaireCoachBypasses)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnaireCoachBypass_UserProfile");
            });

            modelBuilder.Entity<QuestionnaireGroup>(entity =>
            {
                entity.HasKey(e => e.GroupID);

                entity.ToTable("QuestionnaireGroup");

                entity.Property(e => e.GroupID).ValueGeneratedNever();

                entity.HasOne(d => d.Group)
                    .WithOne(p => p.QuestionnaireGroup)
                    .HasForeignKey<QuestionnaireGroup>(d => d.GroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnaireGroup_PDQGroup");
            });

            modelBuilder.Entity<QuestionnaireRequest>(entity =>
            {
                entity.HasKey(e => e.QuestionnaireID);

                entity.ToTable("QuestionnaireRequest");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.QuestionnaireRequests)
                    .HasForeignKey(d => d.GroupID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnaireRequest_PDQGroup");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.QuestionnaireRequests)
                    .HasForeignKey(d => d.RegisterID)
                    .HasConstraintName("FK_QuestionnaireRequest_RegisterLesson");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.QuestionnaireRequests)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_QuestionnaireRequest_LevelExpression");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.QuestionnaireRequests)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnaireRequest_UserProfile");
            });

            modelBuilder.Entity<RegisterLesson>(entity =>
            {
                entity.HasKey(e => e.RegisterID);

                entity.ToTable("RegisterLesson");

                entity.HasIndex(e => new { e.Attended, e.RegisterGroupID }, "IX_RegisterLesson_G1");

                entity.HasIndex(e => new { e.UID, e.RegisterGroupID }, "IX_RegisterLesson_G2");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Advisor)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.AdvisorID)
                    .HasConstraintName("FK_RegisterLesson_ServingCoach");

                entity.HasOne(d => d.AttendedNavigation)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.Attended)
                    .HasConstraintName("FK_RegisterLesson_LevelExpression");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_RegisterLesson_BranchStore");

                entity.HasOne(d => d.ClassLevelNavigation)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.ClassLevel)
                    .HasConstraintName("FK_RegisterLesson_LessonPriceType");

                entity.HasOne(d => d.GroupingMemberCountNavigation)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.GroupingMemberCount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisterLesson_GroupingLessonDiscount");

                entity.HasOne(d => d.RegisterGroup)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.RegisterGroupID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_RegisterLesson_GroupingLesson");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.RegisterLessons)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_RegisterLesson_UserProfile");
            });

            modelBuilder.Entity<RegisterLessonContract>(entity =>
            {
                entity.HasKey(e => e.RegisterID);

                entity.ToTable("RegisterLessonContract");

                entity.Property(e => e.RegisterID).ValueGeneratedNever();

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.RegisterLessonContracts)
                    .HasForeignKey(d => d.ContractID)
                    .HasConstraintName("FK_RegisterLessonContract_CourseContract");

                entity.HasOne(d => d.Register)
                    .WithOne(p => p.RegisterLessonContract)
                    .HasForeignKey<RegisterLessonContract>(d => d.RegisterID)
                    .HasConstraintName("FK_RegisterLessonContract_RegisterLesson");
            });

            modelBuilder.Entity<RegisterLessonEnterprise>(entity =>
            {
                entity.HasKey(e => e.RegisterID);

                entity.ToTable("RegisterLessonEnterprise");

                entity.Property(e => e.RegisterID).ValueGeneratedNever();

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.RegisterLessonEnterprises)
                    .HasForeignKey(d => d.ContractID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_RegisterLessonEnterprise_EnterpriseCourseContract");

                entity.HasOne(d => d.Register)
                    .WithOne(p => p.RegisterLessonEnterprise)
                    .HasForeignKey<RegisterLessonEnterprise>(d => d.RegisterID)
                    .HasConstraintName("FK_RegisterLessonEnterprise_RegisterLesson");

                entity.HasOne(d => d.EnterpriseCourseContent)
                    .WithMany(p => p.RegisterLessonEnterprises)
                    .HasForeignKey(d => new { d.ContractID, d.TypeID })
                    .HasConstraintName("FK_RegisterLessonEnterprise_EnterpriseCourseContent");
            });

            modelBuilder.Entity<ResetPassword>(entity =>
            {
                entity.HasKey(e => e.ResetID);

                entity.ToTable("ResetPassword");

                entity.Property(e => e.ResetID).ValueGeneratedNever();

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.ResetPasswords)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_ResetPassword_UserProfile");
            });

            modelBuilder.Entity<ServingCoach>(entity =>
            {
                entity.HasKey(e => e.CoachID);

                entity.ToTable("ServingCoach");

                entity.Property(e => e.CoachID).ValueGeneratedNever();

                entity.Property(e => e.EmploymentDate).HasColumnType("date");

                entity.HasOne(d => d.Coach)
                    .WithOne(p => p.ServingCoach)
                    .HasForeignKey<ServingCoach>(d => d.CoachID)
                    .HasConstraintName("FK_ServingCoach_UserProfile");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.ServingCoaches)
                    .HasForeignKey(d => d.LevelID)
                    .HasConstraintName("FK_ServingCoach_ProfessionalLevel");
            });

            modelBuilder.Entity<Settlement>(entity =>
            {
                entity.ToTable("Settlement");

                entity.Property(e => e.EndExclusiveDate).HasColumnType("datetime");

                entity.Property(e => e.SettlementDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<StyleAboutPDQ>(entity =>
            {
                entity.HasKey(e => e.StyleID);

                entity.ToTable("StyleAboutPDQ");

                entity.Property(e => e.StyleID).ValueGeneratedNever();

                entity.Property(e => e.Style).HasMaxLength(64);
            });

            modelBuilder.Entity<SystemEventBulletin>(entity =>
            {
                entity.HasKey(e => e.EventID);

                entity.ToTable("SystemEventBulletin");

                entity.Property(e => e.ActionName).HasMaxLength(32);

                entity.Property(e => e.ControllerName).HasMaxLength(32);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(256);
            });

            modelBuilder.Entity<TrainingAid>(entity =>
            {
                entity.HasKey(e => e.AidID);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.TrainingAids)
                    .HasForeignKey(d => d.StageID)
                    .HasConstraintName("FK_TrainingAids_TrainingStage");
            });

            modelBuilder.Entity<TrainingExecution>(entity =>
            {
                entity.HasKey(e => e.ExecutionID);

                entity.ToTable("TrainingExecution");

                entity.Property(e => e.ExecutionID).ValueGeneratedNever();

                entity.Property(e => e.BreakIntervalInSecond).HasMaxLength(32);

                entity.Property(e => e.Emphasis).HasMaxLength(32);

                entity.Property(e => e.ExecutionFeedBackDate).HasColumnType("datetime");

                entity.Property(e => e.Repeats).HasMaxLength(32);

                entity.HasOne(d => d.Execution)
                    .WithOne(p => p.TrainingExecution)
                    .HasForeignKey<TrainingExecution>(d => d.ExecutionID)
                    .HasConstraintName("FK_TrainingExecution_TrainingPlan");
            });

            modelBuilder.Entity<TrainingExecutionStage>(entity =>
            {
                entity.HasKey(e => new { e.ExecutionID, e.StageID });

                entity.ToTable("TrainingExecutionStage");

                entity.Property(e => e.TotalMinutes).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Execution)
                    .WithMany(p => p.TrainingExecutionStages)
                    .HasForeignKey(d => d.ExecutionID)
                    .HasConstraintName("FK_TrainingExecutionStage_TrainingExecution");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.TrainingExecutionStages)
                    .HasForeignKey(d => d.StageID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainingExecutionStage_TrainingStage");
            });

            modelBuilder.Entity<TrainingItem>(entity =>
            {
                entity.HasKey(e => e.ItemID);

                entity.ToTable("TrainingItem");

                entity.Property(e => e.ActualStrength).HasMaxLength(512);

                entity.Property(e => e.ActualTurns).HasMaxLength(512);

                entity.Property(e => e.BreakIntervalInSecond).HasMaxLength(32);

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.DurationInMinutes).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ExecutionFeedBackDate).HasColumnType("datetime");

                entity.Property(e => e.GoalStrength).HasMaxLength(512);

                entity.Property(e => e.GoalTurns).HasMaxLength(512);

                entity.Property(e => e.Remark).HasMaxLength(512);

                entity.Property(e => e.Repeats).HasMaxLength(32);

                entity.HasOne(d => d.Execution)
                    .WithMany(p => p.TrainingItems)
                    .HasForeignKey(d => d.ExecutionID)
                    .HasConstraintName("FK_TrainingItem_TrainingExecution");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.TrainingItems)
                    .HasForeignKey(d => d.PurposeID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TrainingItem_PersonalExercisePurposeItem");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.TrainingItems)
                    .HasForeignKey(d => d.TrainingID)
                    .HasConstraintName("FK_TrainingItem_TrainingType");
            });

            modelBuilder.Entity<TrainingItemAid>(entity =>
            {
                entity.HasKey(e => new { e.ItemID, e.AidID });

                entity.HasOne(d => d.Aid)
                    .WithMany(p => p.TrainingItemAids)
                    .HasForeignKey(d => d.AidID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainingItemAids_TrainingAids");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TrainingItemAids)
                    .HasForeignKey(d => d.ItemID)
                    .HasConstraintName("FK_TrainingItemAids_TrainingItem");
            });

            modelBuilder.Entity<TrainingLevelAboutPDQ>(entity =>
            {
                entity.HasKey(e => e.LevelID);

                entity.ToTable("TrainingLevelAboutPDQ");

                entity.Property(e => e.LevelID).ValueGeneratedNever();

                entity.Property(e => e.TrainingLevel).HasMaxLength(64);
            });

            modelBuilder.Entity<TrainingPlan>(entity =>
            {
                entity.HasKey(e => e.ExecutionID)
                    .HasName("PK_TrainingPlan_1");

                entity.ToTable("TrainingPlan");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.LessonID)
                    .HasConstraintName("FK_TrainingPlan_LessonTime");

                entity.HasOne(d => d.PlanStatusNavigation)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.PlanStatus)
                    .HasConstraintName("FK_TrainingPlan_LevelExpression");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.TrainingPlans)
                    .HasForeignKey(d => d.RegisterID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TrainingPlan_RegisterLesson");
            });

            modelBuilder.Entity<TrainingStage>(entity =>
            {
                entity.HasKey(e => e.StageID);

                entity.ToTable("TrainingStage");

                entity.Property(e => e.StageID).ValueGeneratedNever();

                entity.Property(e => e.Stage)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<TrainingStageItem>(entity =>
            {
                entity.HasKey(e => e.TrainingID);

                entity.ToTable("TrainingStageItem");

                entity.Property(e => e.TrainingID).ValueGeneratedNever();

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.TrainingStageItems)
                    .HasForeignKey(d => d.StageID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrainingStageItem_TrainingStage");

                entity.HasOne(d => d.Training)
                    .WithOne(p => p.TrainingStageItem)
                    .HasForeignKey<TrainingStageItem>(d => d.TrainingID)
                    .HasConstraintName("FK_TrainingStageItem_TrainingType");
            });

            modelBuilder.Entity<TrainingType>(entity =>
            {
                entity.HasKey(e => e.TrainingID);

                entity.ToTable("TrainingType");

                entity.Property(e => e.BodyParts).HasMaxLength(16);
            });

            modelBuilder.Entity<TuitionAchievement>(entity =>
            {
                entity.HasKey(e => new { e.InstallmentID, e.CoachID });

                entity.ToTable("TuitionAchievement");

                entity.Property(e => e.CommitShare).HasColumnType("datetime");

                entity.HasOne(d => d.Coach)
                    .WithMany(p => p.TuitionAchievements)
                    .HasForeignKey(d => d.CoachID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TuitionAchievement_ServingCoach");

                entity.HasOne(d => d.CoachWorkPlaceNavigation)
                    .WithMany(p => p.TuitionAchievements)
                    .HasForeignKey(d => d.CoachWorkPlace)
                    .HasConstraintName("FK_TuitionAchievement_BranchStore");

                entity.HasOne(d => d.Installment)
                    .WithMany(p => p.TuitionAchievements)
                    .HasForeignKey(d => d.InstallmentID)
                    .HasConstraintName("FK_TuitionAchievement_Payment");
            });

            modelBuilder.Entity<TuitionInstallment>(entity =>
            {
                entity.HasKey(e => e.InstallmentID);

                entity.ToTable("TuitionInstallment");

                entity.Property(e => e.InstallmentID).ValueGeneratedNever();

                entity.Property(e => e.PayoffDate).HasColumnType("datetime");

                entity.HasOne(d => d.Installment)
                    .WithOne(p => p.TuitionInstallment)
                    .HasForeignKey<TuitionInstallment>(d => d.InstallmentID)
                    .HasConstraintName("FK_TuitionInstallment_Payment");

                entity.HasOne(d => d.Register)
                    .WithMany(p => p.TuitionInstallments)
                    .HasForeignKey(d => d.RegisterID)
                    .HasConstraintName("FK_TuitionInstallment_IntuitionCharge");
            });

            modelBuilder.Entity<UniformInvoiceWinningNumber>(entity =>
            {
                entity.HasKey(e => e.WinningID);

                entity.ToTable("UniformInvoiceWinningNumber");

                entity.Property(e => e.PrizeType).HasMaxLength(16);

                entity.Property(e => e.WinningNO)
                    .IsRequired()
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<UsageType>(entity =>
            {
                entity.HasKey(e => e.UsageID);

                entity.ToTable("UsageType");

                entity.Property(e => e.UsageID).ValueGeneratedNever();

                entity.Property(e => e.Usage).HasMaxLength(64);
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.HasKey(e => e.EventID);

                entity.ToTable("UserEvent");

                entity.Property(e => e.Accompanist).HasMaxLength(256);

                entity.Property(e => e.ActivityProgram).HasMaxLength(256);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Place).HasMaxLength(64);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.BranchID)
                    .HasConstraintName("FK_UserEvent_BranchStore");

                entity.HasOne(d => d.SystemEvent)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.SystemEventID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_UserEvent_SystemEventBulletin");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_UserEvent_UserProfile");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UID)
                    .HasName("PK_Customers");

                entity.ToTable("UserProfile");

                entity.HasIndex(e => e.PID, "IX_UserProfile")
                    .IsUnique();

                entity.HasIndex(e => e.ExternalID, "IX_UserProfile_1");

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.ExternalID).HasMaxLength(64);

                entity.Property(e => e.MemberCode).HasMaxLength(16);

                entity.Property(e => e.Nickname).HasMaxLength(40);

                entity.Property(e => e.PID)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(64);

                entity.Property(e => e.Password2).HasMaxLength(64);

                entity.Property(e => e.Phone).HasMaxLength(32);

                entity.Property(e => e.RealName).HasMaxLength(40);

                entity.Property(e => e.ThemeName).HasMaxLength(16);

                entity.Property(e => e.UserName).HasMaxLength(40);

                entity.HasOne(d => d.Auth)
                    .WithMany(p => p.InverseAuth)
                    .HasForeignKey(d => d.AuthID)
                    .HasConstraintName("FK_UserProfile_UserProfile1");

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.InverseCreatorNavigation)
                    .HasForeignKey(d => d.Creator)
                    .HasConstraintName("FK_UserProfile_UserProfile");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.LevelID)
                    .HasConstraintName("FK_UserProfile_LevelExpression");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.PictureID)
                    .HasConstraintName("FK_UserProfile_Attachment");
            });

            modelBuilder.Entity<UserProfileExtension>(entity =>
            {
                entity.HasKey(e => e.UID);

                entity.ToTable("UserProfileExtension");

                entity.HasIndex(e => e.IDNo, "IX_UserProfileExtension");

                entity.HasIndex(e => e.LineID, "IX_UserProfileExtension_1");

                entity.Property(e => e.UID).ValueGeneratedNever();

                entity.Property(e => e.AdministrativeArea).HasMaxLength(16);

                entity.Property(e => e.EmergencyContactPerson).HasMaxLength(16);

                entity.Property(e => e.EmergencyContactPhone).HasMaxLength(32);

                entity.Property(e => e.Gender).HasMaxLength(8);

                entity.Property(e => e.IDNo).HasMaxLength(16);

                entity.Property(e => e.LineID).HasMaxLength(64);

                entity.Property(e => e.Relationship).HasMaxLength(16);

                entity.HasOne(d => d.UIDNavigation)
                    .WithOne(p => p.UserProfileExtension)
                    .HasForeignKey<UserProfileExtension>(d => d.UID)
                    .HasConstraintName("FK_UserProfileExtension_UserProfile");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UID, e.RoleID });

                entity.ToTable("UserRole");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_UserRoleDefinition");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_UserRole_UserProfile");
            });

            modelBuilder.Entity<UserRoleAuthorization>(entity =>
            {
                entity.HasKey(e => new { e.UID, e.RoleID });

                entity.ToTable("UserRoleAuthorization");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleAuthorizations)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleAuthorization_UserRoleDefinition");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.UserRoleAuthorizations)
                    .HasForeignKey(d => d.UID)
                    .HasConstraintName("FK_UserRoleAuthorization_UserProfile");
            });

            modelBuilder.Entity<UserRoleDefinition>(entity =>
            {
                entity.HasKey(e => e.RoleID);

                entity.ToTable("UserRoleDefinition");

                entity.HasIndex(e => e.Role, "IX_UserRoleDefinition")
                    .IsUnique();

                entity.Property(e => e.RoleID).ValueGeneratedNever();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.SiteMenu).HasMaxLength(64);
            });

            modelBuilder.Entity<UserSignature>(entity =>
            {
                entity.HasKey(e => e.SignatureID);

                entity.ToTable("UserSignature");

                entity.HasOne(d => d.UIDNavigation)
                    .WithMany(p => p.UserSignatures)
                    .HasForeignKey(d => d.UID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSignature_UserProfile");
            });

            modelBuilder.Entity<V_BranchStaff>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_BranchStaff");
            });

            modelBuilder.Entity<V_ContractTuition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_ContractTuition");

                entity.Property(e => e.AchievementIndex).HasColumnType("numeric(2, 1)");

                entity.Property(e => e.BranchName).HasMaxLength(32);

                entity.Property(e => e.ClassTime).HasColumnType("datetime");

                entity.Property(e => e.CommitAttendance).HasColumnType("datetime");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.MarkedGradeIndex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OfficeLocation)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.TuitionIndex).HasColumnType("numeric(2, 1)");
            });

            modelBuilder.Entity<V_LearnerFitenessAssessment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_LearnerFitenessAssessment");

                entity.Property(e => e.Assessment).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.AssessmentDate).HasColumnType("datetime");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Gender).HasMaxLength(8);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<V_LessonTime>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_LessonTime");

                entity.Property(e => e.ClassTime).HasColumnType("datetime");

                entity.Property(e => e.CommitAttendance).HasColumnType("datetime");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<V_LessonUnitPrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_LessonUnitPrice");

                entity.Property(e => e.Description).HasMaxLength(64);
            });

            modelBuilder.Entity<V_PerformanceShare>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_PerformanceShare");

                entity.Property(e => e.PayoffDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<V_Tuition>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Tuition");

                entity.Property(e => e.AchievementIndex).HasColumnType("numeric(11, 1)");

                entity.Property(e => e.BranchName).HasMaxLength(32);

                entity.Property(e => e.ClassTime).HasColumnType("datetime");

                entity.Property(e => e.CommitAttendance).HasColumnType("datetime");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.MarkedGradeIndex).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OfficeLocation)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.TuitionIndex).HasColumnType("numeric(11, 1)");
            });

            modelBuilder.Entity<V_WorkPlace>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_WorkPlace");

                entity.Property(e => e.OfficeLocation)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<VacantInvoiceNo>(entity =>
            {
                entity.HasKey(e => e.VacancyID);

                entity.ToTable("VacantInvoiceNo");

                entity.HasOne(d => d.Interval)
                    .WithMany(p => p.VacantInvoiceNos)
                    .HasForeignKey(d => d.IntervalID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VacantInvoiceNo_InvoiceNoInterval");

                entity.HasOne(d => d.Next)
                    .WithMany(p => p.InverseNext)
                    .HasForeignKey(d => d.NextID)
                    .HasConstraintName("FK_VacantInvoiceNo_VacantInvoiceNo1");

                entity.HasOne(d => d.Prev)
                    .WithMany(p => p.InversePrev)
                    .HasForeignKey(d => d.PrevID)
                    .HasConstraintName("FK_VacantInvoiceNo_VacantInvoiceNo");
            });

            modelBuilder.Entity<VoidPayment>(entity =>
            {
                entity.HasKey(e => e.VoidID);

                entity.ToTable("VoidPayment");

                entity.Property(e => e.VoidID).ValueGeneratedNever();

                entity.Property(e => e.Remark).HasMaxLength(256);

                entity.Property(e => e.VoidDate).HasColumnType("datetime");

                entity.HasOne(d => d.Handler)
                    .WithMany(p => p.VoidPayments)
                    .HasForeignKey(d => d.HandlerID)
                    .HasConstraintName("FK_VoidPayment_UserProfile");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.VoidPayments)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_VoidPayment_LevelExpression");

                entity.HasOne(d => d.Void)
                    .WithOne(p => p.VoidPayment)
                    .HasForeignKey<VoidPayment>(d => d.VoidID)
                    .HasConstraintName("FK_VoidPayment_Payment");
            });

            modelBuilder.Entity<VoidPaymentLevel>(entity =>
            {
                entity.HasKey(e => new { e.VoidID, e.LevelDate });

                entity.ToTable("VoidPaymentLevel");

                entity.Property(e => e.LevelDate).HasColumnType("datetime");

                entity.HasOne(d => d.Executor)
                    .WithMany(p => p.VoidPaymentLevels)
                    .HasForeignKey(d => d.ExecutorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoidPaymentLevel_UserProfile");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.VoidPaymentLevels)
                    .HasForeignKey(d => d.LevelID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VoidPaymentLevel_LevelExpression");

                entity.HasOne(d => d.Void)
                    .WithMany(p => p.VoidPaymentLevels)
                    .HasForeignKey(d => d.VoidID)
                    .HasConstraintName("FK_VoidPaymentLevel_VoidPayment");
            });

            modelBuilder.HasSequence("CourseContractNoSeq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
