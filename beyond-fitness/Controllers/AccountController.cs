﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebHome.Models.DataEntity;
using WebHome.Models.ViewModel;
using WebHome.Helper;
using System.Threading;
using System.Text;
using WebHome.Models.Locale;
using Utility;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Web.Security;

namespace WebHome.Controllers
{
    public class AccountController : SampleController<UserProfile>
    {
        public AccountController() : base()
        {

        }
        
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sample()
        {
            return View();
        }

        
        public ActionResult Login()
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
                return View();
            else
                return processLogin(profile);
        }

        
        public ActionResult LoginByMail()
        {
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }


        private String createMemberCode()
        {
            Thread.Sleep(1);
            Random rnd = new Random();

            return (new StringBuilder()).Append((char)((int)'A' + rnd.Next(26)))
                .Append((char)((int)'A' + rnd.Next(26)))
                .Append(String.Format("{0:00000000}",rnd.Next(100000000)))
                .ToString();
        }

        
        public ActionResult CheckMemberCode(String memberCode)
        {
                try
                {
                    if (models.EntityList.Any(u => u.MemberCode == memberCode))
                    {
                        return Json(new { result = true });
                    }

                    return Json(new { result = false, message = "學員編號錯誤!!"}, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            
        }

        
        public ActionResult UpdateMemberPicture(String memberCode, String imgUrl)
        {
            try
            {
                    UserProfile item = models.EntityList.Where(u => u.MemberCode == memberCode).FirstOrDefault();

                    if (item == null)
                        return Json(new { result = false, message = "學員編號錯誤!!" }, JsonRequestBehavior.AllowGet);

                    String storePath = Path.Combine(Logger.LogDailyPath, Guid.NewGuid().ToString() + ".dat");
                    if (Request.Files.Count > 0)
                    {
                        Request.Files[0].SaveAs(storePath);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(imgUrl))
                        {
                            return Json(new { result = false, message = "來源網址錯誤!!" });
                        }
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(imgUrl, storePath);
                        }
                    }

                    item.Attachment = new Attachment
                    {
                        StoredPath = storePath
                    };

                    models.SubmitChanges();
                    return Json(new { result = true,pictureID = item.PictureID });
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FetchPicture(String imgUrl)
        {
            try
            {

                    String storePath = Path.Combine(Logger.LogDailyPath, Guid.NewGuid().ToString() + ".dat");
                    if (Request.Files.Count > 0)
                    {
                        Request.Files[0].SaveAs(storePath);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(imgUrl))
                        {
                            return Json(new { result = false, message = "來源網址錯誤!!" });
                        }
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(imgUrl, storePath);
                        }
                    }

                    Attachment item = new Attachment
                    {
                        StoredPath = storePath
                    };

                    models.GetTable<Attachment>().InsertOnSubmit(item);

                    models.SubmitChanges();
                    return Json(new { result = true, pictureID = item.AttachmentID });
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [ValidateAntiForgeryToken]
        public ActionResult RegisterByFB(FBRegisterViewModel viewModel)
        {
            
            
            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "學員編號錯誤!!";
                return View("Register");
            }

            if (item.LevelID != (int)Naming.MemberStatusDefinition.ReadyToRegister)
            {
                ViewBag.Message = "學員編號已註冊!!";
                return View("Register");
            }

            viewModel.UserID = viewModel.UserID.GetEfficientString();
            if (viewModel.UserID == null)
            {
                ViewBag.Message = "無法取得您的FaceBook帳號識別碼!!";
                return View("Register");
            }

            if (item.ExternalID != viewModel.UserID && models.EntityList.Any(u => u.ExternalID == viewModel.UserID))
            {
                ViewBag.Message = "您的FaceBook帳號已經是註冊使用者!!請直接登入系統!!";
                return View("Register");
            }
            item.ExternalID = viewModel.UserID;

            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail != null)
            {
                if(item.PID!=viewModel.EMail && models.EntityList.Any(u => u.PID == viewModel.EMail))
                {
                    ViewBag.Message = "您的FaceBook電子郵件已經是註冊使用者!!請直接登入系統!!";
                    return View("Register");
                }
                item.PID = viewModel.EMail;
            }
            item.UserName = viewModel.UserName.GetEfficientString();
            item.PictureID = viewModel.PictureID;
            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.UID, item.UID);

            return View(item);
        }


        [ValidateAntiForgeryToken]
        public ActionResult RegisterByMail(String memberCode)
        {
            
            

            UserProfile item = models.EntityList.Where(u => u.MemberCode == memberCode).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "學員編號錯誤!!";
                return View("Register");
            }

            if (item.LevelID != (int)Naming.MemberStatusDefinition.ReadyToRegister)
            {
                ViewBag.Message = "學員編號已註冊!!";
                return View("Register");
            }

            HttpContext.SetCacheValue(CachingKey.UID, item.UID);

            return View(item);
        }

        
        public ActionResult CompleteRegister(RegisterViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("RegisterByMail");
            }

            
            
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.UID)).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "會員編號錯誤!!";
                HttpContext.SetCacheValue(CachingKey.UID, null);
                return View("Register");
            }

            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail == null)
            {
                this.ModelState.AddModelError("email", "請輸入Email");
                ViewBag.ModelState = ModelState;
                return View("RegisterByMail");
            }

            if (models.EntityList.Any(u => u.PID == viewModel.EMail))
            {
                ViewBag.Message = "您的Email已經是註冊使用者!!請直接登入系統!!";
                HttpContext.SetCacheValue(CachingKey.UID, null);
                return View("Register");
            }

            item.PID = viewModel.EMail;
            item.UserName = viewModel.UserName.GetEfficientString();
            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;

            createPassword(viewModel);

            if(!String.IsNullOrEmpty(viewModel.Password))
            {
                item.Password = (viewModel.Password).MakePassword();
            }

            models.SubmitChanges();

            this.HttpContext.SignOn(item);
            
            return View(item);
        }

        public ActionResult FBCompleteRegister(String email)
        {
            email = email.GetEfficientString();
            if (email==null)
            {
                ModelState.AddModelError("email", "請輸入Email");
                ViewBag.ModelState = ModelState;
                return View("RegisterByFB");
            }

            
            
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.UID)).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "會員編號錯誤!!";
                HttpContext.SetCacheValue(CachingKey.UID, null);
                return View("Register");
            }

            if (item.PID != email && models.EntityList.Any(u => u.PID == email))
            {
                ViewBag.Message = "您的FaceBook電子郵件已經是註冊使用者!!請直接登入系統!!";
                return View("Register");
            }
            item.PID = email;
            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            models.SubmitChanges();

            this.HttpContext.SignOn(item);

            return View("CompleteRegister",item);
        }


        [Authorize]
        public ActionResult CreateNew()
        {
            

                try
                {
                    String memberCode;

                    while (true)
                    {
                        memberCode = createMemberCode();
                        if (!models.EntityList.Any(u => u.MemberCode == memberCode))
                        {
                            break;
                        }
                    }

                    UserProfile item = new UserProfile
                    {
                        PID = memberCode,
                        MemberCode = memberCode,
                        LevelID = (int)Naming.MemberStatusDefinition.ReadyToRegister
                    };

                    models.EntityList.InsertOnSubmit(item);
                    models.SubmitChanges();
                    return Json(new { result = true, message = "新增完成!!", memberCode = memberCode }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            
        }

        public ActionResult LoginByFB(String accessToken,String userId)
        {
                UserProfile item = models.EntityList.Where(u => u.ExternalID == userId
                    && u.LevelID == (int)Naming.MemberStatusDefinition.Checked).FirstOrDefault();

                if (item != null)
                {
                    HttpContext.SignOn(item);
                    return processLogin(item, true);
                }

                return Json(new { result = false, message = "登入資料錯誤!!" });
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new
            //    {
            //        result = false,
            //        errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0)
            //                .Select(k => new { name = k, message = ModelState[k].Errors.Select(r => r.ErrorMessage) }).ToArray()
            //    });
            //}

            

            
            UserProfile item = models.EntityList.Where(u => u.PID == viewModel.PID
                && u.LevelID == (int)Naming.MemberStatusDefinition.Checked).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "登入資料錯誤!!";
                return View("LoginByMail");
            }

            if (item.Password != (viewModel.Password).MakePassword())
            {
                ViewBag.Message = "登入資料錯誤!!";
                return View("LoginByMail");
            }

            HttpContext.SignOn(item);

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return processLogin(item);

        }

        private ActionResult processLogin(UserProfile item,bool isJson = false)
        {
            switch ((Naming.RoleID)item.UserRole[0].RoleID)
            {
                case Naming.RoleID.Administrator:
                case Naming.RoleID.Coach:
                case Naming.RoleID.FreeAgent:
                    if (isJson)
                        return Json(new { result = true, url = VirtualPathUtility.ToAbsolute("~/Account/Coach") });
                    else
                        return RedirectToAction("Coach", "Account");

                case Naming.RoleID.Learner:
                    if (isJson)
                        return Json(new { result = true, url = VirtualPathUtility.ToAbsolute("~/Account/Vip") });
                    else
                        return RedirectToAction("Vip", "Account");
            }

            return View();
        }

        public ActionResult Logout()
        {
            this.HttpContext.Logout();
            return View();
        }

        public ActionResult ForgetPassword(String email)
        {
            if(String.IsNullOrEmpty(email))
            {
                return View();
            }

            

            
            UserProfile item = models.EntityList.Where(u => u.PID == email.Trim()
                && u.LevelID == (int)Naming.MemberStatusDefinition.Checked).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("email", "您提供的email資料不存在!!");
                ViewBag.ModelState = this.ModelState;
                return View();
            }

            ResetPassword toReset = new ResetPassword
            {
                ResetID = Guid.NewGuid(),
                UserProfile = item
            };

            models.GetTable<ResetPassword>().InsertOnSubmit(toReset);
            models.SubmitChanges();

            toReset.NotifyResetPassword();

            ViewBag.Success = "重設密碼通知郵件已寄出。";

            return View();

        }

        [HttpGet]
        public ActionResult ResetPass(Guid id)
        {

            

            
            UserProfile item = models.GetTable<ResetPassword>().Where(r => r.ResetID == id)
                .Select(r => r.UserProfile)
                .Where(u => u.LevelID == (int)Naming.MemberStatusDefinition.Checked)
                .FirstOrDefault();

            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            HttpContext.SetCacheValue(CachingKey.UID, item.UID);
            return View(item);

        }

        [HttpPost]
        public ActionResult ResetPass(PasswordViewModel viewModel)
        {

            

            
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.UID)
                && u.LevelID == (int)Naming.MemberStatusDefinition.Checked)
                .FirstOrDefault();

            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            ViewBag.ModelState = this.ModelState;

            if (!createPassword(viewModel))
                return View(item);

            item.Password = (viewModel.Password).MakePassword();
            //models.DeleteAllOnSubmit<ResetPassword>(r => r.UID == item.UID);
            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.UID, null);

            return View("CompleteResetPass", item);

        }

        private bool createPassword(PasswordViewModel viewModel)
        {
            if (String.IsNullOrEmpty(viewModel.lockPattern))
            {
                if (String.IsNullOrEmpty(viewModel.Password))
                {
                    ModelState.AddModelError("password", "請輸入密碼!!");
                    return false;
                }
                else if (viewModel.Password != viewModel.Password2)
                {
                    ModelState.AddModelError("password2", "密碼確認錯誤!!");
                    return false;
                }
            }
            else
            {
                viewModel.Password = viewModel.lockPattern;
            }
            return true;
        }

        public ActionResult Vip(DateTime? lessonDate,DateTime? endQueryDate)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            if (!lessonDate.HasValue)
                lessonDate = DateTime.Today.AddYears(-1);
            if (!endQueryDate.HasValue)
                endQueryDate = lessonDate.Value.AddYears(1);

            ViewBag.LessonDate = lessonDate;
            ViewBag.EndQueryDate = endQueryDate;

            return View(item);
        }

        public ActionResult EditVip(int id,DateTime? lessonDate)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == id).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "學員資料不存在!!";
                return RedirectToAction("Coach");
            }

            RegisterViewModel model = new RegisterViewModel
            {
                EMail = item.PID.Contains("@") ? item.PID : null,
                MemberCode = item.MemberCode,
                PictureID = item.PictureID,
                UserName = item.UserName
            };

            ViewBag.LessonDate = lessonDate.HasValue ? lessonDate : DateTime.Today;
            return View("EditMySelf", model);
        }



        public ActionResult Coach(DateTime? lessonDate,DateTime? endQueryDate)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            if (!lessonDate.HasValue)
                lessonDate = DateTime.Today;

            ViewBag.LessonDate = lessonDate;
            ViewBag.EndQueryDate = endQueryDate;

            return View(item);
        }


        [HttpGet]
        public ActionResult EditMySelf()
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            RegisterViewModel model = new RegisterViewModel
            {
                EMail = item.PID.Contains("@") ? item.PID : null,
                MemberCode = item.MemberCode,
                PictureID = item.PictureID,
                UserName = item.UserName
            };

            return View(model);
        }


        public ActionResult EditMySelf(RegisterViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("EditMySelf");
            }

            int uid = item.UID;
            item = models.EntityList.Where(u => u.UID == uid).FirstOrDefault();

            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            viewModel.PictureID = item.PictureID;
            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail == null)
            {
                this.ModelState.AddModelError("email", "請輸入Email");
                ViewBag.ModelState = ModelState;
                return View(viewModel);
            }

            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail != null)
            {
                if (item.PID != viewModel.EMail && models.EntityList.Any(u => u.PID == viewModel.EMail))
                {
                    ViewBag.Message = "您的Email已經是註冊使用者!!請重新設定Email!!";
                    return View(viewModel);
                }
                item.PID = viewModel.EMail;
            }

            item.UserName = viewModel.UserName.GetEfficientString();

            if (!createPassword(viewModel))
                return View(viewModel);
            item.Password = (viewModel.Password).MakePassword();

            models.SubmitChanges();

            this.HttpContext.SignOn(item);

            return View("CompleteEditMySelf", item);
        }



    }
}