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

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "memberCode")]
        //[EmailAddress]
        public string MemberCode { get; set; }

        [Display(Name = "email")]
        [EmailAddress]
        public string EMail { get; set; }

        [Display(Name = "userName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "userID")]
        public string UserID { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }

    public class LeanerViewModel
    {
        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [Display(Name = "電話")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "上課總次數")]
        public int Lessons { get; set; }

        [Required]
        [Display(Name = "課程類別")]
        public int ClassLevel { get; set; }
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