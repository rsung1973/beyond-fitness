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

        
        public ActionResult RegisterByFB(RegisterViewModel viewModel)
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

            if (String.IsNullOrEmpty(viewModel.UserID))
            {
                ViewBag.Message = "無法取得您的FaceBook帳號識別碼!!";
                return View("Register");
            }

            if (models.EntityList.Any(u=>u.PID==viewModel.UserID))
            {
                ViewBag.Message = "您的FaceBook帳號已經是註冊使用者!!\\r\\n請直接登入系統!!";
                return View("Register");
            }

            ViewBag.ViewModel = viewModel;

            return View(item);
        }

        
        public ActionResult RegisterByMail(RegisterViewModel viewModel)
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

            ViewBag.ViewModel = viewModel;

            return View(item);
        }

        
        public ActionResult CompleteRegister(RegisterViewModel viewModel)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "學員編號錯誤!!";
                return View("Register");
            }

            viewModel.UserID = viewModel.UserID.GetEfficientString();
            if (viewModel.UserID == null)
            {
                ViewBag.Message = "無法取得您的帳號識別碼!!";
                return View("Register");
            }

            if (models.EntityList.Any(u => u.PID == viewModel.UserID))
            {
                ViewBag.Message = "您的帳號已經是註冊使用者!!\\r\\n請直接登入系統!!";
                return View("Register");
            }

            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail == null)
            {
                ViewBag.Message = "email錯誤!!";
                return View("Register");
            }


            item.EMail = viewModel.EMail;
            item.UserName = viewModel.UserName;
            item.PID = viewModel.UserID;
            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            if(!String.IsNullOrEmpty(viewModel.Password))
            {
                item.Password = (viewModel.UserID + viewModel.Password).MakePassword();
            }

            models.SubmitChanges();

            this.HttpContext.SignOn(item);
            
            return View(item);
        }

        
        public ActionResult RegisterComplete(int id)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
                return Json(new { result = false, message = "學員資料錯誤!!" }, JsonRequestBehavior.AllowGet);

            return View(item);
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

            if(item.Password != (viewModel.PID+viewModel.Password).MakePassword())
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
    }
}