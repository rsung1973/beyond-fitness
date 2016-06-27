using System;
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

    }

    public class LearnerViewModel
    {
        //public LearnerViewModel()
        //{
        //    ClassLevel = 1;
        //}

        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "會員編號")]
        public string MemberCode { get; set; }

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

        [Display(Name = "會員編號")]
        public int MemberCount { get; set; }

    }


    public class CoachViewModel
    {
        public CoachViewModel()
        {
            CoachRole = (int)Naming.RoleID.Coach;
        }

        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "EMail")]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "教練身份")]
        public int CoachRole { get; set; }

        [Display(Name = "會員編號")]
        public string MemberCode { get; set; }

    }

    public class LessonTimeViewModel
    {
        public LessonTimeViewModel()
        {
            ClassDate = DateTime.Today;
            ClassTime = new TimeSpan(8, 0, 0);
            Duration = 60;
        }

        [Required]
        [Display(Name = "學員姓名")]
        public int RegisterID { get; set; }

        [Required]
        [Display(Name = "教練姓名")]
        public int CoachID { get; set; }

        [Required]
        [Display(Name = "上課日期")]
        public DateTime ClassDate { get; set; }

        [Required]
        [Display(Name = "上課時段")]
        public TimeSpan ClassTime { get; set; }

        [Required]
        [Display(Name = "上課時間")]
        public int Duration { get; set; }

    }
}