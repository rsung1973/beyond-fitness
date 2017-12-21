using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Helper;
using WebHome.Models.ViewModel;
using Utility;
using System.Web.Security;

namespace WebHome.Controllers
{
    public class HtmlController : SampleController<UserProfile>
    {
        public ActionResult Login(bool? confirmedID)
        {
            return View("~/Views/Html/Module/Login.ascx", confirmedID);
        }
        public ActionResult ForgetPassword()
        {
            return View("~/Views/Html/Module/ForgetPassword.ascx");
        }

        public ActionResult Register()
        {
            return View("~/Views/Html/Module/Register.ascx");
        }

        public ActionResult RegisterByMail(RegisterViewModel viewModel)
        {

            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("MemberCode", "學員編號錯誤!!");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            if (item.LevelID != (int)Naming.MemberStatusDefinition.ReadyToRegister)
            {
                ModelState.AddModelError("MemberCode", "學員編號已註冊!!");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Html/Module/RegisterByMail.ascx", item);
        }

        public ActionResult CompleteRegister(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx",model:"會員編號錯誤!!");
            }

            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail == null)
            {
                this.ModelState.AddModelError("EMail", "請輸入Email");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            if (models.EntityList.Any(u => u.PID == viewModel.EMail))
            {
                ModelState.AddModelError("EMail", "您的Email已經是註冊使用者!!請直接登入系統!!");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            item.PID = viewModel.EMail;
            item.UserName = viewModel.UserName.GetEfficientString();
            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            item.UserProfileExtension.RegisterStatus = true;

            if (!this.CreatePassword(viewModel))
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            if (!String.IsNullOrEmpty(viewModel.Password))
            {
                item.Password = (viewModel.Password).MakePassword();
            }

            models.SubmitChanges();

            this.HttpContext.SignOn(item);

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Html/Module/CompleteRegister.ascx", item);
        }

        public ActionResult EditMySelf(RegisterViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            viewModel.EMail = item.PID.Contains("@") ? item.PID : null;
            viewModel.MemberCode = item.MemberCode;
            viewModel.PictureID = item.PictureID;
            viewModel.UserName = item.UserName;
            viewModel.Birthday = item.Birthday;
            viewModel.UID = item.UID;

            ViewBag.ViewModel = viewModel;
            return View("~/Views/Html/Module/EditMySelf.ascx", item);
        }

        public ActionResult CommitMySelf(RegisterViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
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
                this.ModelState.AddModelError("EMail", "請輸入Email");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            viewModel.EMail = viewModel.EMail.GetEfficientString();
            if (viewModel.EMail != null)
            {
                if (item.PID != viewModel.EMail && models.EntityList.Any(u => u.PID == viewModel.EMail))
                {
                    ModelState.AddModelError("EMail", "您的Email已經是註冊使用者!!請重新設定Email!!");
                    ViewBag.ModelState = ModelState;
                    return View("~/Views/Shared/ReportInputError.ascx");
                }
                item.PID = viewModel.EMail;
            }

            item.UserName = viewModel.UserName.GetEfficientString();
            if (viewModel.Birthday.HasValue)
                item.Birthday = viewModel.Birthday;

            if (!this.CreatePassword(viewModel))
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }
            item.Password = (viewModel.Password).MakePassword();

            models.SubmitChanges();

            this.HttpContext.SignOn(item);

            return View("~/Views/Html/Module/CompleteRegister.ascx", item);
        }


        public ActionResult CompleteRegisterReview(RegisterViewModel viewModel)
        {
            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "會員編號錯誤!!");
            }

            return View("~/Views/Account/CompleteRegister.aspx", item);
        }

        public ActionResult AutoLogin(long? timeTicks)
        {
            //HttpCookie cookie = Request.Cookies["loginToken"];
            //if (cookie != null && !String.IsNullOrEmpty(cookie.Value))
            //{
            //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            //    FormsIdentity identity = new FormsIdentity(ticket);
            //    Context.User = new ClaimsPrincipal(identity);
            //}

            var item = HttpContext.GetUser();
            if (item != null)
            {
                HttpContext.SignOn(item);
                return View("~/Views/Html/Module/AutoLogin.ascx", model: processLogin(item));
            }

            return new EmptyResult();
        }

        private String processLogin(UserProfile item)
        {
            UrlHelper url = new UrlHelper(ControllerContext.RequestContext);
            if (item.IsAuthorizedSysAdmin())
            {
                return url.Action("Index", "CoachFacet");
            }
            else if (item.UserRoleAuthorization.Any(r => r.RoleID == (int)Naming.RoleID.Coach || r.RoleID == (int)Naming.RoleID.Manager || r.RoleID == (int)Naming.RoleID.ViceManager))
            {
                return url.Action("Index", "CoachFacet", new { CoachID = item.UID });
            }
            else if(item.IsAssistant())
            {
                return url.Action("Index", "CoachFacet");
            }
            else if(item.IsAccounting())
            {
                return url.Action("TrustIndex", "Accounting");
            }

            switch ((Naming.RoleID)item.UserRole[0].RoleID)
            {
                case Naming.RoleID.Administrator:
                    return url.Action("Index", "CoachFacet");

                case Naming.RoleID.Coach:
                case Naming.RoleID.Manager:
                case Naming.RoleID.ViceManager:
                    return url.Action("Index", "CoachFacet", new { CoachID = item.UID });

                case Naming.RoleID.Assistant:
                    return url.Action("Index", "CoachFacet");

                case Naming.RoleID.Accounting:
                    return url.Action("TrustIndex", "Accounting");

                case Naming.RoleID.FreeAgent:
                    return url.Action("FreeAgent", "Account");

                case Naming.RoleID.Learner:
                    return url.Action("LearnerIndex", "LearnerFacet");
            }

            return url.Action("Index", "Account"); ;
        }

        public ActionResult LoginByForm(LoginViewModel viewModel, string returnUrl)
        {
            UserProfile item = models.EntityList.Where(u => u.PID == viewModel.PID
                && u.LevelID == (int)Naming.MemberStatusDefinition.Checked).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("PID", "登入資料錯誤!!");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            if (item.Password != (viewModel.Password).MakePassword())
            {
                ModelState.AddModelError("PID", "登入資料錯誤!!");
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            HttpContext.SignOn(item, viewModel.RememberMe);

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return View("~/Views/Html/Module/AutoLogin.ascx", model: returnUrl);

            }

            return View("~/Views/Html/Module/AutoLogin.ascx", model: processLogin(item));

        }

        public ActionResult LearnerPrompt()
        {
            var result = PromptQuestionnaire();
            if (result is EmptyResult)
            {
                result = LearnerDailyQuestion();
            }
            return result;
        }

        public ActionResult PromptQuestionnaire()
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return new EmptyResult();
            }

            profile = models.GetTable<UserProfile>().Where(u => u.UID == profile.UID).First();

            var item = models.GetQuestionnaireRequest(profile).FirstOrDefault();

            if (item == null)
            {
                return new EmptyResult();
            }
            else
            {
                return View("~/Views/Html/Module/PromptQuestionnaire.ascx", item);
            }

        }

        public ActionResult LearnerDailyQuestion()
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return new EmptyResult();
            }

            if (models.GetTable<PDQTask>().Any(t => t.UID == profile.UID
                 && t.TaskDate >= DateTime.Today && t.TaskDate < DateTime.Today.AddDays(1)
                 && t.PDQQuestion.GroupID == 6))
            {
                return new EmptyResult();
            }

            if (models.GetTable<PDQTask>().Count(t => t.UID == profile.UID && t.PDQQuestion.GroupID == 6) >=
                models.GetTable<RegisterLesson>().Where(r => r.UID == profile.UID)
                    .Select(r => r.GroupingLesson).Sum(g => g.LessonTime.Count(l => l.LessonPlan.CommitAttendance.HasValue || l.LessonAttendance != null)))
            {
                return new EmptyResult();
            }


            int[] items = models.GetTable<PDQQuestion>().Where(q => q.GroupID == 6)
                .Select(q => q.QuestionID)
                .Where(q => !models.GetTable<PDQTask>().Where(t => t.UID == profile.UID).Select(t => t.QuestionID).Contains(q)).ToArray();

            if (items.Length == 0)
            {
                items = models.GetTable<PDQQuestion>().Where(q => q.GroupID == 6)
                .Select(q => q.QuestionID).ToArray();
            }

            profile.DailyQuestionID = items[DateTime.Now.Ticks % items.Length];

            var item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == profile.DailyQuestionID).FirstOrDefault();
            return View("~/Views/Html/Module/LearnerDailyQuestion.ascx", item);
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

            PasswordViewModel viewModel = new PasswordViewModel
            {
                PID = item.PID
            };

            ViewBag.ViewModel = viewModel;
            return View(item);

        }

        [HttpPost]
        public ActionResult ResetPass(PasswordViewModel viewModel)
        {

            ViewBag.ViewModel = viewModel;

            UserProfile item = models.EntityList.Where(u => u.PID == viewModel.PID
                && u.LevelID == (int)Naming.MemberStatusDefinition.Checked)
                .FirstOrDefault();

            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }
            ViewBag.ModelState = this.ModelState;

            if (!this.CreatePassword(viewModel))
                return View(item);

            item.Password = (viewModel.Password).MakePassword();
            //models.DeleteAllOnSubmit<ResetPassword>(r => r.UID == item.UID);
            models.SubmitChanges();

            HttpContext.SignOn(item);

            return Redirect(processLogin(item));

        }


    }
}