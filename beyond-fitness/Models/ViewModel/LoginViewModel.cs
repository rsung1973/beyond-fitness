using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

    }

    public class RegisterViewModel : PasswordViewModel
    {

        [Display(Name = "EMail")]
        [EmailAddress]
        public string EMail { get; set; }

        [Display(Name = "暱稱")]
        public string UserName { get; set; }

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
        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Display(Name = "上課總次數")]
        public int Lessons { get; set; }

        [Display(Name = "課程類別")]
        public int ClassLevel { get; set; }

        [Display(Name = "EMail")]
        public string Email { get; set; }

        public int? UID { get; set; }

    }

    public class CoachViewModel
    {
        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "課程類別")]
        public int CoachRole { get; set; }

        public int? UID { get; set; }

    }
}