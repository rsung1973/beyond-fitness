﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebHome.Models.Locale;

namespace WebHome.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "PID")]
        //[EmailAddress]
        public string PID { get; set; }

        //[Display(Name = "validCode")]
        //[CaptchaValidation("EncryptedCode", ErrorMessage = "驗證碼錯誤!!")]
        //public string ValidCode { get; set; }

        //[Display(Name = "encryptedCode")]
        //public string EncryptedCode { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class FBRegisterViewModel
    {
        [Required]
        [Display(Name = "會員編號")]
        public string MemberCode { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress]
        public string EMail { get; set; }

        [Display(Name = "暱稱")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "UserID")]
        public string UserID { get; set; }

        public int? PictureID { get; set; }

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }


    }

    public class RegisterViewModel : PasswordViewModel
    {

        [Display(Name = "EMail")]
        [EmailAddress]
        public string EMail { get; set; }

        [Display(Name = "暱稱")]
        public string UserName { get; set; }

        [Display(Name = "會員編號")]
        public string MemberCode { get; set; }

        public int? PictureID { get; set; }

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }

        public int? UID;

    }

    public class PasswordViewModel
    {

        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        public string Password2 { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "圖形密碼")]
        public string lockPattern { get; set; }

        public String PID { get; set; }

    }

    public class LearnerViewModel
    {
        //public LearnerViewModel()
        //{
        //    ClassLevel = 1;
        //}

        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "會員編號")]
        public string MemberCode { get; set; }

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }

        public String Gender { get; set; }
        public int? AthleticLevel { get; set; }
        public int? CurrentTrial { get; set; }

        public Naming.MemberStatusDefinition? MemberStatus { get; set; }
        public String Address { get; set; }
        public Naming.RoleID? RoleID { get; set; }
    }

    public class LessonViewModel
    {
        public LessonViewModel()
        {
            ClassLevel = 1;
            MemberCount = 1;
        }

        [Display(Name = "上課堂數")]
        public int Lessons { get; set; }

        [Display(Name = "課程類別")]
        public int ClassLevel { get; set; }

        public string Grouping { get; set; }

        public int MemberCount { get; set; }

        public String Payment { get; set; }

        public int? FeeShared { get; set; }

        public string Installments { get; set; }

        public int? ByInstallments { get; set; }

        public int? AdvisorID { get; set; }

        public int? RegisterID { get; set; }
    }


    public class CoachViewModel
    {
        public CoachViewModel()
        {
            //CoachRole = (int)Naming.RoleID.Coach;
        }

        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Display(Name = "體能顧問身份")]
        public int? CoachRole
        {
            get
            {
                return AuthorizedRole != null && AuthorizedRole.Length > 0 ? AuthorizedRole[0] : null;
            }
            set
            {
                if(AuthorizedRole != null && AuthorizedRole.Length > 0)
                {
                    AuthorizedRole[0] = value;
                }
                else
                {
                    AuthorizedRole = new int?[] { value };
                }
            }
        }

        [Display(Name = "會員編號")]
        public string MemberCode { get; set; }

        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }

        public String Description { get; set; }

        public int? LevelID { get; set; } 

        public bool? IsCoach { get; set; }
        public int? UID { get; set; }
        public int? BranchID { get; set; }
        public int? LevelCategory { get; set; } = (int)Naming.ProfessionalCategory.新制;
        public int?[] AuthorizedRole { get; set; }
        public bool? HasGiftLessons { get; set; }
        public int? MonthlyGiftLessons { get; set; }

    }

    public class LessonTimeViewModel
    {
        public LessonTimeViewModel()
        {
            ClassDate = DateTime.Today;
            //ClassTime = new TimeSpan(8, 0, 0);
            //Duration = 60;
        }

        [Display(Name = "學員姓名")]
        public int? RegisterID { get; set; }

        [Required]
        [Display(Name = "體能顧問姓名")]
        public int CoachID { get; set; }

        [Required]
        [Display(Name = "上課日期")]
        public DateTime ClassDate { get; set; }

        //[Required]
        //[Display(Name = "上課時段")]
        //public TimeSpan ClassTime { get; set; }

        //[Required]
        //[Display(Name = "上課時間")]
        //public int Duration { get; set; }

        public int? UID { get; set; }

        public int? TrainingBySelf { get; set; }

        [Required]
        [Display(Name = "上課地點")]
        public int BranchID { get; set; }

        public int? LessonID { get; set; }
        public int? CurrentTrial { get; set; }

    }

    public class LessonTimeExpansionViewModel
    {
        public DateTime? ClassDate { get; set; }
        public int Hour { get; set; }
        public int RegisterID { get; set; }
        public int LessonID { get; set; }
    }

    public class TrainingExecutionViewModel
    {

        [Display(Name = "組數")]
        public String Repeats { get; set; }

        [Display(Name = "休息秒數")]
        public String BreakInterval { get; set; }

        [Display(Name = "肌力訓練")]
        public int?[] TrainingID { get; set; }

        public String[] Description { get; set; }

        [Display(Name = "目標次數")]
        public String[] GoalTurns { get; set; }

        [Display(Name = "目標強度")]
        public String[] GoalStrength { get; set; }

        [Display(Name = "備註")]
        public String[] Remark { get; set; }

        [Display(Name = "實際次數")]
        public String[] ActualTurns { get; set; }

        [Display(Name = "實際強度")]
        public String[] ActualStrength { get; set; }

        [Display(Name = "評論")]
        public String Conclusion { get; set; }

        public int ExecutionID { get; set; }
    }

    public class TrainingItemViewModel
    {

        [Display(Name = "肌力訓練")]
        public int? TrainingID { get; set; }

        public String Description { get; set; }

        [Display(Name = "目標次數")]
        public String GoalTurns { get; set; }

        [Display(Name = "目標強度")]
        public String GoalStrength { get; set; }

        [Display(Name = "備註")]
        public String Remark { get; set; }
        public int ExecutionID { get; set; }

        [Display(Name = "實際次數")]
        public String ActualTurns { get; set; }

        [Display(Name = "實際強度")]
        public String ActualStrength { get; set; }

        [Display(Name = "組數")]
        public String Repeats { get; set; }

        [Display(Name = "休息秒數")]
        public String BreakInterval { get; set; }

        public int? ItemID { get; set; }
    }

    public class TrainingPlanViewModel
    {
        [Display(Name = "目前近況")]
        public String RecentStatus { get; set; }

        [Display(Name = "暖身")]
        public String Warming { get; set; }

        [Display(Name = "收操")]
        public String EndingOperation { get; set; }

        [Display(Name = "體能顧問評鑑")]
        public String Remark { get; set; }
    }

    public class TrainingAssessmentViewModel : TrainingPlanViewModel
    {

        [Display(Name = "體能顧問")]
        public int CoachID { get; set; }

        [Display(Name = "實際次數")]
        public String[] ActualTurns { get; set; }

        [Display(Name = "實際強度")]
        public String[] ActualStrength { get; set; }

        [Display(Name = "評論")]
        public String[] Conclusion { get; set; }

        [Display(Name = "動作學習")]
        public int? ActionLearning { get; set; }

        [Display(Name = "姿勢矯正")]
        public int? PostureRedress { get; set; }

        [Display(Name = "訓練")]
        public int? Training { get; set; }

        [Display(Name = "狀態溝通")]
        public int? Counseling { get; set; }

        [Display(Name = "柔軟度")]
        public int? Flexibility { get; set; }

        [Display(Name = "心肺")]
        public int? Cardiopulmonary { get; set; }

        [Display(Name = "肌力")]
        public int? Strength { get; set; }

        [Display(Name = "肌耐力")]
        public int? Endurance { get; set; }

        [Display(Name = "爆發力")]
        public int? ExplosiveForce { get; set; }

        [Display(Name = "運動表現")]
        public int? SportsPerformance { get; set; }
    }

    public class FeedBackViewModel
    {
        [Display(Name = "迴響")]
        public String[] ExecutionFeedBack { get; set; }

        [Display(Name = "學員意見反饋")]
        public String FeedBack { get; set; }

        public String[] Conclusion { get; set; }


    }

    public class DailyBookingQueryViewModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public String UserName { get; set; }
        public int? CoachID { get; set; }
        public int? MonthInterval { get; set; }
        public bool? HasQuery { get; set; }
        public int? TrainingBySelf { get; set; }
        public int? LessonID { get; set; }
        public int? LessonStatus { get; set; }
        public int? BranchID { get; set; }
    }

    public class LessonPriceViewModel
    {
        public int? PriceID { get; set; }
        public String Description { get; set; }
        public int? ListPrice { get; set; }
        public int? Status { get; set; }
        public int? UsageType { get; set; }
        public int? CoachPayoff { get; set; }
        public int? CoachPayoffCreditCard { get; set; }
    }

    public class MembersQueryViewModel
    {
        public String ByName { get; set; }
        public Naming.RoleID? RoleID { get; set; }
    }

    public class InstallmentViewModel
    {
        public int RegisterID { get; set; }
        public int?[] PayoffAmount { get; set; }
        public DateTime?[] PayoffDate { get; set; }
    }

    public class SingleInstallmentViewModel
    {
        public int RegisterID { get; set; }
        public int CoachID { get; set; }
        public int? PayoffAmount { get; set; }
        public DateTime? PayoffDate { get; set; }
    }

    public class AchievementShareViewModel
    {
        public int InstallmentID { get; set; }
        public int CoachID { get; set; }
        public int? ShareAmount { get; set; }
    }

    public class LearnerPaymentViewModel
    {
        public int? CoachID { get; set; }
        public bool? Payoff { get; set; }
        public String UserName { get; set; }
        public bool? HasQuery { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class PDQQuestionViewModel
    {
        public PDQQuestionViewModel()
        {
            QuestionType = (int)Naming.QuestionType.單選題;
            GroupID = 6;
            BonusPoint = 1;
        }

        public int? QuestionID { get; set; }
        public String Question { get; set; }
        public int? QuestionType { get; set; }
        public int QuestionNo { get; set; }
        public int? GroupID { get; set; }
        public int? AskerID { get; set; }

        public String[] Suggestion { get; set; }
        public int? RightAnswerIndex { get; set; }
        public int? BonusPoint { get; set; }
    }

    public class FitnessAssessmentViewModel
    {
        public int? ItemID { get; set; }
        public decimal? Assessment { get; set; }
    }

    public class FitnessAssessmentReportViewModel
    {
        public int AssessmentID { get; set; }
        public int TrendItem { get; set; }
        public decimal? TrendAssessment { get; set; }
        public int? ItemID { get; set; }
        public decimal? TotalAssessment { get; set; }
        public String Calc { get; set; }
        public int? ByTimes { get; set; }
        public decimal? SingleAssessment { get; set; }
        public bool? BySingleSide { get; set; }
        public String ByCustom { get; set; }
    }


    public class ArgumentModel
    {
        public String PartialViewName { get; set; }
        public object Model { get; set; }
    }

    public class MotivationalWordsViewModel
    {
        public int? DocID { get; set; }
        public String Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class FullCalendarViewModel : DailyBookingQueryViewModel
    {
        public DateTime? DefaultDate { get; set; }
        public String DefaultView { get; set; }
        public String Category { get; set; }
        public DateTime? LessonDate { get; set; }
        public int? Duration { get; set; }
        public int? UID { get; set; }
        public String QueryType { get; set; } = "default";
    }

    public class LessonQueryViewModel
    {
        public int? CoachID { get; set; }
        public DateTime? QueryStart { get; set; }
    }
    public class LessonTimeBookingViewModel
    {

        public int? LessonID { get; set; }
        public DateTime? ClassTimeStart { get; set; }
        public DateTime? ClassTimeEnd { get; set; }

    }

    public class TrialLearnerViewModel
    {
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }
        [Display(Name = "電話")]
        public string Phone { get; set; }
        public String Gender { get; set; }

    }

    public class UserEventViewModel
    {
        public int? EventID { get; set; }
        public int? UID { get; set; }
        public String Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public String ActivityProgram { get; set; }
        public int[] MemberID { get; set; }
        public String Accompanist { get; set; }
        public int? BranchID { get; set; }

    }

    public class FitnessDiagnosisViewModel
    {
        public int? DiagnosisID { get; set; }
        public int? UID { get; set; }
        public String Goal { get; set; }
        public String Description { get; set; }
        public int? ItemID { get; set; }
        public Decimal? Assessment { get; set; }
        public Decimal? AdditionalAssessment { get; set; }
        public String Judgement { get; set; }
        public String DiagnosisAction { get; set; }
    }

    public class UserEventBookingViewModel
    {
        public int? EventID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UID { get; set; }

    }

    public class CoachCertificateViewModel
    {
        public int? CertificateID { get; set; }
        public DateTime? Expiration { get; set; }
        public int? UID { get; set; }

    }



}