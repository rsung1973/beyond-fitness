using System;
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

namespace WebHome.Controllers
{
    public class AccountController : Controller
    {
        public AccountController() : base()
        {

        }
        
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Login()
        {
            if (HttpContext.GetUser() == null)
                return View();
            else
                return Redirect("~/Information/Vip");
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
                .Append(rnd.Next(100000000))
                .ToString();
        }

        
        public ActionResult CheckMemberCode(String memberCode)
        {
            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                TempData.SetModelSource(models);

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
        }

        
        public ActionResult UpdateMemberPicture(String memberCode, String imgUrl)
        {
            try
            {
                using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
                {
                    TempData.SetModelSource(models);
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
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Json(new { result = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        
        public ActionResult RegisterByFB(FBRegisterViewModel viewModel)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);
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

            if (item.PID != viewModel.UserID && models.EntityList.Any(u => u.PID == viewModel.UserID))
            {
                ViewBag.Message = "您的FaceBook帳號已經是註冊使用者!!請直接登入系統!!";
                return View("Register");
            }

            item.PID = viewModel.UserID;
            item.UserName = viewModel.UserName.GetEfficientString();
            item.EMail = viewModel.EMail.GetEfficientString();
            models.SubmitChanges();

            HttpContext.SetCacheValue("uid", item.UID);

            return View(item);
        }

        
        public ActionResult RegisterByMail(String memberCode)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);

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

            HttpContext.SetCacheValue("uid", item.UID);

            return View(item);
        }

        
        public ActionResult CompleteRegister(RegisterViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("RegisterByMail");
            }

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue("uid")).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "會員編號錯誤!!";
                HttpContext.SetCacheValue("uid", null);
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
                HttpContext.SetCacheValue("uid", null);
                return View("Register");
            }

            item.PID = item.EMail = viewModel.EMail;
            item.UserName = viewModel.UserName.GetEfficientString();
            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            if(!String.IsNullOrEmpty(viewModel.Password))
            {
                item.Password = (item.PID.ToUpper() + viewModel.Password).MakePassword();
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

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue("uid")).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "會員編號錯誤!!";
                HttpContext.SetCacheValue("uid", null);
                return View("Register");
            }

            item.EMail = email;
            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            models.SubmitChanges();

            this.HttpContext.SignOn(item);

            return View("CompleteRegister",item);
        }


        [Authorize]
        public ActionResult CreateNew()
        {
            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                TempData.SetModelSource(models);

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
        }

        public ActionResult LoginByFB(String accessToken,String userId)
        {
            using (ModelSource<UserProfile> models = new ModelSource<UserProfile>())
            {
                TempData.SetModelSource(models);
                UserProfile item = models.EntityList.Where(u => u.PID == userId).FirstOrDefault();

                if (item != null)
                {
                    HttpContext.SignOn(item);
                    return Json(new { result = true, url = VirtualPathUtility.ToAbsolute("~/Information/Vip") });
                }

                return Json(new { result = false, message = "登入資料錯誤!!" });
            }
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

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.PID == viewModel.PID).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "學員資料錯誤!!";
                return View("LoginByMail");
            }

            if (item.Password != (viewModel.PID.ToUpper() + viewModel.Password).MakePassword())
            {
                ViewBag.Message = "學員資料錯誤!!";
                return View("LoginByMail");
            }

            HttpContext.SignOn(item);

            if(!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/Information/Vip");
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

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.PID == email.Trim()).FirstOrDefault();

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

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.GetTable<ResetPassword>().Where(r => r.ResetID == id)
                .Select(r => r.UserProfile).FirstOrDefault();

            if (item == null)
            {
                return Redirect("~/Account/Login");
            }

            HttpContext.SetCacheValue("uid", item.UID);
            return View(item);

        }

        [HttpPost]
        public ActionResult ResetPass(PasswordViewModel viewModel)
        {

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue("uid")).FirstOrDefault();

            if (item == null)
            {
                return Redirect("~/Account/Login");
            }
            ViewBag.ModelState = this.ModelState;

            if (String.IsNullOrEmpty(viewModel.lockPattern))
            {
                if (String.IsNullOrEmpty(viewModel.Password))
                {
                    ModelState.AddModelError("password", "請輸入密碼!!");
                    return View(item);
                }
                else if (viewModel.Password != viewModel.Password2)
                {
                    ModelState.AddModelError("password2", "密碼確認錯誤!!");
                    return View(item);
                }
            }
            else
            {
                viewModel.Password = viewModel.lockPattern;
            }

            item.Password = (item.PID.ToUpper() + viewModel.Password).MakePassword();
            //models.DeleteAllOnSubmit<ResetPassword>(r => r.UID == item.UID);
            models.SubmitChanges();

            HttpContext.SetCacheValue("uid", null);

            return View("CompleteResetPass",item);

        }


    }
}