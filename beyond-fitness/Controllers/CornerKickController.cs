﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using CommonLib.DataAccess;
using CommonLib.MvcExtension;
using EPOS;
using Newtonsoft.Json;
using Utility;
using WebHome.Helper;
using WebHome.Helper.BusinessOperation;
using WebHome.Helper.MessageOperation;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    public class CornerKickController : SampleController<UserProfile>
    {
        // GET: CornerKick
        public ActionResult Index(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.LineID = viewModel.KeyID.DecryptKey();
            }
            return View("~/Views/CornerKick/Index.cshtml");
        }

        public ActionResult TestError()
        {
            throw new Exception("Test Error...");
        }

        public ActionResult Login(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View("~/Views/CornerKick/Login.cshtml");
        }

        public ActionResult Error(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View("Error");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is CryptographicException)
            {
                Response.Redirect((new UrlHelper(filterContext.RequestContext)).Action("InvalidCrypto", "Error"));
            }
            else
            {
                if (filterContext.Exception != null)
                {
                    Logger.Error(filterContext.Exception);
                }
                Response.Redirect((new UrlHelper(filterContext.RequestContext)).Action("Error", "CornerKick"));
                //base.OnException(filterContext);
            }
        }


        public ActionResult ForgetPassword(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult AutoLogin(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = HttpContext.GetUser();
            if (item != null)
            {
                HttpContext.SignOn(item);
                return View("~/Views/CornerKick/Module/AutoLogin.cshtml", model: this.ProcessLogin(item));
            }

            return new EmptyResult();
        }

        public ActionResult Register(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = models.GetTable<UserProfileExtension>().Where(u => u.LineID == viewModel.LineID)
                    .Select(u => u.UserProfile).FirstOrDefault();

            if (item != null)
            {
                HttpContext.SignOn(item);
                return View("~/Views/CornerKick/Module/AutoLogin.cshtml", model: this.ProcessLogin(item, true));
            }
            else
            {
                return View();
            }

        }

        public ActionResult Unbind(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = models.GetTable<UserProfileExtension>().Where(u => u.LineID == viewModel.LineID)
                    .Select(u => u.UserProfile).FirstOrDefault();

            if (item != null)
            {
                return View(item);
            }
            else
            {
                return View("Index");
            }

        }

        public ActionResult CommitUnbound(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.KeyID != null)
            {
                viewModel.UID = viewModel.DecryptKeyValue();
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();

            if (item != null)
            {
                item.UserProfileExtension.LineID = null;
                models.SubmitChanges();
            }

            return View("Index");

        }


        public ActionResult BindUser(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            viewModel.LineID = viewModel.LineID.GetEfficientString();
            if (viewModel.LineID == null)
            {
                ModelState.AddModelError("PID", "您輸入的Line ID錯誤，請確認後再重新輸入!!");
            }

            viewModel.MemberCode = viewModel.MemberCode.GetEfficientString();
            if (viewModel.MemberCode == null)
            {
                ModelState.AddModelError("MemberCode", "會員編號資料錯誤，請確認後再重新輸入!!");
            }

            viewModel.PID = viewModel.PID.GetEfficientString();
            if (viewModel.PID == null || !Regex.IsMatch(viewModel.PID, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                ModelState.AddModelError("PID", "電子信箱資料錯誤，請確認後再重新輸入!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                ViewBag.SingleError = true;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode
                || u.UserProfileExtension.IDNo == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (item.UserProfileExtension.LineID != null)
            {
                ModelState.AddModelError("PID", "您的帳號已設定過其他Line ID，請確認後再重新輸入!!");
            }

            if (models.GetTable<UserProfileExtension>().Any(u => u.LineID == viewModel.LineID))
            {
                ModelState.AddModelError("PID", "您的Line ID已設定其他使用者帳號，請確認後再重新輸入!!");
            }

            if (models.EntityList.Any(u => u.PID == viewModel.PID && u.UID != item.UID))
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                ViewBag.ModelState = ModelState;
            }

            if (item.UserProfileExtension.CurrentTrial == 1 || !item.IsLearner())
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
            }

            var pwd = (viewModel.Password).MakePassword();
            if (item.LevelID != (int)Naming.MemberStatusDefinition.ReadyToRegister)
            {
                if (item.Password != pwd)
                {
                    ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                }
            }
            else if (pwd == null)
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
            }
            else
            {
                item.Password = pwd;
                item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            }


            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                ViewBag.SingleError = true;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (item.PID != viewModel.PID)
            {
                item.PID = viewModel.PID;
            }

            item.UserProfileExtension.LineID = viewModel.LineID;
            models.SubmitChanges();

            HttpContext.SignOn(item);

            //綁定Line贈點活動
            PDQTask taskItem = null;
            if (!models.GetTable<PDQTask>().Any(t => t.UID == item.UID && t.PDQQuestion.GroupID == 8))
            {
                var quest = models.GetTable<PDQQuestion>().Where(q => q.GroupID == 8).FirstOrDefault();
                if (quest != null)
                {
                    taskItem = new PDQTask
                    {
                        QuestionID = quest.QuestionID,
                        UID = item.UID,
                        TaskDate = DateTime.Now,
                        PDQTaskBonus = new PDQTaskBonus { },
                    };
                    models.GetTable<PDQTask>().InsertOnSubmit(taskItem);
                    models.SubmitChanges();
                }
            }

            return Json(new { result = true, bonus = taskItem != null }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CommitToRegister(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            viewModel.MemberCode = viewModel.MemberCode.GetEfficientString();
            if (viewModel.MemberCode == null)
            {
                ModelState.AddModelError("MemberCode", "會員編號資料錯誤，請確認後再重新輸入!!");
            }

            viewModel.PID = viewModel.PID.GetEfficientString();
            if (viewModel.PID == null || !Regex.IsMatch(viewModel.PID, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                ModelState.AddModelError("PID", "電子信箱資料錯誤，請確認後再重新輸入!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                ViewBag.SingleError = true;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode
                || u.UserProfileExtension.IDNo == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (models.EntityList.Any(u => u.PID == viewModel.PID && u.UID != item.UID))
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                ViewBag.ModelState = ModelState;
            }

            if (item.UserProfileExtension.CurrentTrial == 1 /*|| !item.IsLearner()*/)
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
            }

            var pwd = (viewModel.Password).MakePassword();
            if (item.LevelID != (int)Naming.MemberStatusDefinition.ReadyToRegister)
            {
                if (item.Password != pwd)
                {
                    ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                }
            }
            else if (pwd == null)
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
            }
            else
            {
                item.Password = pwd;
                item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            }


            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                ViewBag.SingleError = true;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (item.PID != viewModel.PID)
            {
                item.PID = viewModel.PID;
            }

            models.SubmitChanges();

            HttpContext.SignOn(item);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);

        }


        [Authorize]
        public ActionResult Settings(bool? learnerSettings, bool? bonus)
        {
            ViewBag.LearnerSettings = learnerSettings;
            ViewBag.LineBonus = bonus;
            return View();
        }

        [Authorize]
        public ActionResult CommitSettings(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            viewModel.UserName = viewModel.UserName.GetEfficientString();
            if (viewModel.UserName == null)
            {
                ModelState.AddModelError("UserName", "請輸入暱稱!!");
            }


            UserProfile item = HttpContext.GetUser().LoadInstance(models);

            if (viewModel.LearnerSettings == true)
            {
                viewModel.PID = viewModel.PID.GetEfficientString();
                if (viewModel.PID != null)
                {
                    if (item.PID != viewModel.PID && models.EntityList.Any(u => u.PID == viewModel.PID && u.UID != item.UID))
                    {
                        ModelState.AddModelError("EMail", "您的Email已經是註冊使用者!!請重新設定Email!!");
                    }
                    else
                    {
                        item.PID = viewModel.PID;
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            item.UserName = viewModel.UserName;
            models.SubmitChanges();

            return View("~/Views/Html/Module/AutoLogin.cshtml", model: this.ProcessLogin(item, true));

        }

        public ActionResult Notice(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.LineID = viewModel.KeyID.DecryptKey();
            }
            var item = models.GetTable<UserProfileExtension>().Where(u => u.LineID == viewModel.LineID)
                    .Select(u => u.UserProfile).FirstOrDefault();

            if (item != null)
            {
                HttpContext.SignOn(item);
                return View("~/Views/CornerKick/Notice.cshtml", item);
            }
            else
            {
                ViewBag.Message = "此支裝置尚未設定過專屬服務，請點選下方更多資訊/專屬服務/帳號設定才可使用！";
                return View("Index");
            }

        }

        public ActionResult NoticeNotFound(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult DataNotFound(DataItemViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }


        public ActionResult CheckAttendance(RegisterViewModel viewModel)
        {
            ViewResult result = (ViewResult)Notice(viewModel);
            UserProfile item = result.Model as UserProfile;

            if (item != null)
            {
                result.ViewName = "CheckAttendance";
            }

            return result;
        }

        [Authorize]
        public ActionResult LearnerToCheckAttendance()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View("CheckAttendance", profile);
        }


        public ActionResult AttendanceAccomplished(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        [Authorize]
        public ActionResult CommitAttendance(LearnerQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser().LoadInstance(models);

            if (viewModel.LessonID != null && viewModel.LessonID.Length > 0)
            {
                foreach (var item in models.GetTable<LessonTime>().Where(l => viewModel.LessonID.Contains(l.LessonID)))
                {
                    if (item.GroupingLesson.RegisterLesson.Any(r => r.UID == profile.UID))
                    {
                        item.LessonPlan.CommitAttendance = DateTime.Now;
                        models.SubmitChanges();
                    }
                }
            }

            return View("CheckAttendance", profile);

        }

        [Authorize]
        public ActionResult AnswerDailyQuestion()
        {
            var profile = HttpContext.GetUser();
            var item = models.PromptLearnerDailyQuestion(profile);
            return View("AnswerDailyQuestion", item);
        }

        [Authorize]
        public ActionResult MyContract()
        {
            var profile = HttpContext.GetUser();
            var items = models.PromptEffectiveContract()
                .Where(c => c.CourseContractMember.Any(m => m.UID == profile.UID));

            var lessons = models.GetTable<RegisterLesson>()
                .Where(r => r.UID == profile.UID)
                .Where(r => r.Attended != (int)Naming.LessonStatus.課程結束);

            if (items.Count() > 0 || lessons.Count() > 0)
            {
                ViewBag.DataItems = lessons;
                return View("~/Views/CornerKick/MyContract.cshtml", items);
            }
            else
            {
                ViewBag.ViewModel = new DataItemViewModel
                {
                    Title = "我的合約",
                    Message = "目前尚未規劃任何訓練，<br/>若有興趣請與您的教練一起規劃訓練內容喔！",
                };
                return View("~/Views/CornerKick/DataNotFound.cshtml");
            }
        }

        public ActionResult ToSignCourseContract(CourseContractQueryViewModel viewModel, String encUID)
        {
            int? uid = null;
            if (encUID != null)
            {
                uid = encUID.DecryptKeyValue();
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (item != null)
            {
                HttpContext.SignOn(item);
                return SignCourseContract(viewModel);
            }
            else
            {
                //ViewBag.Message = "此支裝置尚未設定過專屬服務，請點選下方更多資訊/專屬服務/帳號設定才可使用！";
                return View("Index");
            }
        }

        public ActionResult ToSignContractService(CourseContractQueryViewModel viewModel, String encUID)
        {
            int? uid = null;
            if (encUID != null)
            {
                uid = encUID.DecryptKeyValue();
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (item != null)
            {
                HttpContext.SignOn(item);
                return SignContractService(viewModel);
            }
            else
            {
                //ViewBag.Message = "此支裝置尚未設定過專屬服務，請點選下方更多資訊/專屬服務/帳號設定才可使用！";
                return View("Index");
            }
        }



        [Authorize]
        public ActionResult SignCourseContract(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser();

            var item = models.GetTable<CourseContract>()
                .Where(c => c.ContractID == viewModel.ContractID)
                .Where(c => c.CourseContractMember.Any(m => m.UID == profile.UID))
                .FirstOrDefault();

            if (item == null)
            {
                ViewBag.ViewModel = new DataItemViewModel
                {
                    Title = "我的合約",
                    Message = "目前尚未規劃任何訓練，<br/>若有興趣請與您的教練一起規劃訓練內容喔！",
                };
                return View("~/Views/CornerKick/DataNotFound.cshtml");
            }
            else if (item.Status != (int)Naming.CourseContractStatus.待簽名)
            {
                ViewBag.ViewModel = new DataItemViewModel
                {
                    Title = "我的合約",
                    Message = "合約狀態不符！",
                };
                return View("~/Views/CornerKick/ErrorMessage.cshtml");
            }

            ViewBag.DataItem = item;
            return View("~/Views/CornerKick/SignCourseContract.cshtml", profile.LoadInstance(models));
        }

        [Authorize]
        public ActionResult SignContractService(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser();

            var item = models.GetTable<CourseContract>()
                .Where(c => c.ContractID == viewModel.ContractID)
                .Where(c => c.CourseContractMember.Any(m => m.UID == profile.UID))
                .FirstOrDefault();

            if (item == null)
            {
                ViewBag.ViewModel = new DataItemViewModel
                {
                    Title = "我的合約",
                    Message = "目前尚未規劃任何訓練，<br/>若有興趣請與您的教練一起規劃訓練內容喔！",
                };
                return View("~/Views/CornerKick/DataNotFound.cshtml");
            }
            else if (item.Status != (int)Naming.CourseContractStatus.待簽名)
            {
                ViewBag.ViewModel = new DataItemViewModel
                {
                    Title = "我的合約",
                    Message = "合約狀態不符！",
                };
                return View("~/Views/CornerKick/ErrorMessage.cshtml");
            }

            ViewBag.DataItem = item;
            return View("~/Views/CornerKick/SignContractService.cshtml", profile.LoadInstance(models));
        }


        [Authorize]
        public ActionResult ConfirmSignature(CourseContractViewModel viewModel, CourseContractSignatureViewModel signatureViewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.Agree != true)
            {
                ModelState.AddModelError("Message", "請勾選同意聲明!!");
                ViewBag.AlertError = true;
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }

            CourseContract item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("Message", "合約資料錯誤!!");
                ViewBag.AlertError = true;
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (item.InstallmentID.HasValue)
            {
                foreach (var c in models.GetTable<CourseContract>().Where(c => c.InstallmentID == item.InstallmentID))
                {
                    var contract = commitSignature(viewModel, signatureViewModel, c, out String alertMessage);
                    if (contract == null)
                    {
                        ModelState.AddModelError("Message", alertMessage);
                        ViewBag.AlertError = true;
                        ViewBag.ModelState = this.ModelState;
                        return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
                    }
                }
            }
            else
            {
                item = commitSignature(viewModel, signatureViewModel, item, out String alertMessage);
                if (item == null)
                {
                    ModelState.AddModelError("Message", alertMessage);
                    ViewBag.AlertError = true;
                    ViewBag.ModelState = this.ModelState;
                    return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
                }
            }

            String jsonData = this.RenderViewToString("~/Views/LineEvents/Message/NotifyToContractPayment.cshtml", item);
            jsonData.PushLineMessage();

            ViewBag.ViewModel = new QueryViewModel
            {
                UrlAction = Url.Action("MyContract"),
            };
            return View("~/Views/CornerKick/Shared/ViewModelCommitted.cshtml");
        }

        [Authorize]
        public ActionResult ConfirmContractServiceSignature(CourseContractViewModel viewModel, CourseContractSignatureViewModel signatureViewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (viewModel.Agree != true)
            {
                ModelState.AddModelError("Message", "請勾選同意聲明!!");
                ViewBag.AlertError = true;
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            if (viewModel.KeyID != null)
            {
                viewModel.ContractID = viewModel.DecryptKeyValue();
            }

            CourseContract item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("Message", "合約資料錯誤!!");
                ViewBag.AlertError = true;
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            item = commitSignature(viewModel, signatureViewModel, item, out String alertMessage);
            if (item == null)
            {
                ModelState.AddModelError("Message", alertMessage);
                ViewBag.AlertError = true;
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            var jsonData = this.RenderViewToString("~/Views/LineEvents/Message/NotifyToCoachBook.cshtml", item);
            jsonData.PushLineMessage();

            ViewBag.ViewModel = new QueryViewModel
            {
                UrlAction = Url.Action("MyContract"),
            };
            return View("~/Views/CornerKick/Shared/ViewModelCommitted.cshtml");
        }

        private CourseContract commitSignature(CourseContractViewModel viewModel, CourseContractSignatureViewModel signatureViewModel, CourseContract item, out String alertMessage)
        {
            var profile = HttpContext.GetUser();

            for (int i = 0; i < (signatureViewModel.SignatureCount ?? 1); i++)
            {
                String signatureName = $"{signatureViewModel.SignatureName}{i:#}";
                var sigItem = models.GetTable<CourseContractSignature>()
                    .Where(s => s.ContractID == item.ContractID)
                    .Where(s => s.UID == profile.UID)
                    .Where(s => s.SignatureName == signatureName)
                    .FirstOrDefault();

                if (sigItem == null)
                {
                    sigItem = new CourseContractSignature
                    {
                        ContractID = item.ContractID,
                        UID = profile.UID,
                        SignatureName = signatureName,
                    };
                    models.GetTable<CourseContractSignature>().InsertOnSubmit(sigItem);
                }

                sigItem.Signature = signatureViewModel.Signature;
                models.SubmitChanges();
            }

            if (item.CourseContractRevision != null)
            {
                return viewModel.ConfirmContractServiceSignature(this, out alertMessage, out String pdfFile, item.CourseContractRevision);
            }
            else
            {
                return viewModel.ConfirmContractSignature(this, out alertMessage, out String pdfFile, item);
            }
        }

        [Authorize]
        public ActionResult CommitAnswerDailyQuestion(DailyQuestionViewModel viewModel)
        {

            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            if (viewModel.KeyID != null)
            {
                viewModel.QuestionID = viewModel.DecryptKeyValue();
            }

            var item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == viewModel.QuestionID).FirstOrDefault();
            if (item == null || !viewModel.SuggestionID.HasValue)
            {
                return AnswerDailyQuestion();
            }

            if (models.GetTable<PDQTask>().Any(t => t.UID == profile.UID
                 && t.TaskDate >= DateTime.Today && t.TaskDate < DateTime.Today.AddDays(1)
                 && t.PDQQuestion.GroupID == 6))
            {
                return AnswerDailyQuestion();
            }

            var taskItem = new PDQTask
            {
                QuestionID = item.QuestionID,
                SuggestionID = viewModel.SuggestionID,
                UID = profile.UID,
                TaskDate = DateTime.Now
            };
            models.GetTable<PDQTask>().InsertOnSubmit(taskItem);
            models.SubmitChanges();

            if (item.PDQSuggestion.Any(s => s.SuggestionID == viewModel.SuggestionID && s.RightAnswer == true))
            {
                if (item.PDQQuestionExtension != null)
                {
                    taskItem.PDQTaskBonus = new PDQTaskBonus { };
                    models.SubmitChanges();
                }
            }

            return AnswerDailyQuestion();
        }

        [Authorize]
        public ActionResult LearnerIndex()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View("~/Views/CornerKick/LearnerIndex.cshtml", profile);
        }

        public ActionResult Logout(RegisterViewModel viewModel, String message = null)
        {
            this.HttpContext.Logout();
            ViewBag.Message = message;
            return Index(viewModel);
        }

        [Authorize]
        public ActionResult LearnerNotice()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View("~/Views/CornerKick/Notice.cshtml", profile);
        }

        public ActionResult ResetPassword(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            UserProfile item;
            if (viewModel.UUID != null)
            {
                item = models.GetTable<ResetPassword>().Where(r => r.ResetID == viewModel.UUID)
                    .Select(r => r.UserProfile)
                    .Where(u => u.LevelID == (int)Naming.MemberStatusDefinition.Checked)
                    .FirstOrDefault();
            }
            else
            {
                item = HttpContext.GetUser()?.LoadInstance(models);
            }

            if (item == null)
            {
                return Index(viewModel);
            }

            return View(item);
        }

        public ActionResult CommitPassword(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            UserProfile item;
            if (viewModel.UUID != null)
            {
                item = models.GetTable<ResetPassword>().Where(r => r.ResetID == viewModel.UUID)
                    .Select(r => r.UserProfile)
                    .Where(u => u.LevelID == (int)Naming.MemberStatusDefinition.Checked)
                    .FirstOrDefault();
            }
            else
            {
                item = HttpContext.GetUser()?.LoadInstance(models);
            }

            if (item == null)
            {
                if (viewModel.KeyID != null)
                {
                    viewModel.UID = viewModel.DecryptKeyValue();
                }
                item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();
            }

            if (item == null)
            {
                return Login(viewModel);
            }

            var pwd = (viewModel.Password).MakePassword();
            if (pwd == null)
            {
                ModelState.AddModelError("Password", "您輸入的資料錯誤，請確認後再重新輸入!!");
            }
            else if (viewModel.Password.ToUpper() == "BEYOND")
            {
                ModelState.AddModelError("Password", "您所輸入的密碼安全性不足，請重新設定!!");
            }
            else
            {
                item.Password = pwd;
                item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                ViewBag.SingleError = true;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            models.SubmitChanges();

            //if (viewModel.UUID != null)
            //{
            HttpContext.SignOn(item);
            //}

            return View("~/Views/CornerKick/Module/CommitPassword.cshtml", item);

        }

        [Authorize]
        public ActionResult TrainingAnalysis()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View(profile);
        }

        [Authorize]
        public ActionResult LearnerTrainingGoal()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View(profile);
        }

        [Authorize]
        public ActionResult LearnerToCheckTrainingGoal()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            models.ExecuteCommand(@"UPDATE PersonalExercisePurposeItem
                            SET        NoticeStatus = {0}
                            WHERE   (UID = {1})", (int)Naming.IncommingMessageStatus.已讀, profile.UID);
            return View("LearnerTrainingGoal", profile);
        }


        [Authorize]
        public ActionResult StartNavigation(UserEventViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.EventID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser().LoadInstance(models);
            if (viewModel.EventID.HasValue)
            {
                models.ExecuteCommand("delete UserEvent where UID={0} and EventID={1}", profile.UID, viewModel.EventID);
            }

            return View(profile);
        }

        [Authorize]
        public ActionResult CheckBonusPoint(AwardQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View("~/Views/CornerKick/CheckBonusPoint2021.cshtml", profile);
        }


        public ActionResult TodayLesson(RegisterViewModel viewModel)
        {
            ViewResult result = (ViewResult)Notice(viewModel);
            UserProfile item = result.Model as UserProfile;

            if (item != null)
            {
                return View("TodayLesson", item);
            }
            else
            {
                return result;
            }
        }

        public ActionResult CheckBonusPointByLine(RegisterViewModel viewModel)
        {
            ViewResult result = (ViewResult)Notice(viewModel);
            UserProfile item = result.Model as UserProfile;

            if (item != null)
            {
                return View("CheckBonusPoint", item);
            }
            else
            {
                return result;
            }
        }

        [Authorize]
        public ActionResult ViewLesson(LessonTimeBookingViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.LessonID = viewModel.DecryptKeyValue();
            }

            ViewBag.DataItem = models.GetTable<LessonTime>().Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View("~/Views/CornerKick/TodayLesson.cshtml", profile);
        }

        [Authorize]
        public ActionResult AboutOnlineLesson(LessonTimeBookingViewModel viewModel)
        {
            ViewResult result = (ViewResult)ViewLesson(viewModel);
            result.ViewName = "~/Views/CornerKick/AboutOnlineLesson.cshtml";

            return result;
        }


        public ActionResult SignOn(LoginViewModel viewModel, string returnUrl)
        {
            ViewBag.ViewModel = viewModel;

            UserProfile item = viewModel.ValiateLogin(models, ModelState);

            if (item == null)
            {
                ViewBag.ModelState = ModelState;
                return View("Login");
            }

            if (viewModel.Password.ToUpper() == "BEYOND")
            {
                return View("ResetPassword", item);
            }

            HttpContext.SignOn(item, viewModel.RememberMe);

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return View("~/Views/Html/Module/AutoLogin.cshtml", model: returnUrl);
            }

            return View("~/Views/Html/Module/AutoLogin.cshtml", model: this.ProcessLogin(item, false));

        }

        public ActionResult CommitToForgetPassword(String email)
        {
            if (String.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "請輸入您的 email address");
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }


            UserProfile item = models.EntityList.Where(u => u.PID == email.Trim()
                && u.LevelID == (int)Naming.MemberStatusDefinition.Checked).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("email", "您輸入的資料錯誤，請確認後再重新輸入!!");
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            ResetPassword toReset = new ResetPassword
            {
                ResetID = Guid.NewGuid(),
                UserProfile = item
            };

            models.GetTable<ResetPassword>().InsertOnSubmit(toReset);
            models.SubmitChanges();

            toReset.NotifyResetPassword(notifyUrl: $"{WebHome.Properties.Settings.Default.HostDomain}{VirtualPathUtility.ToAbsolute("~/CornerKick/NotifyResetPassword")}");

            return View("~/Views/Shared/JsAlert.cshtml", model: "重設密碼通知郵件已寄出!!");

        }

        public ActionResult NotifyResetPassword(String resetID)
        {
            return View("NotifyResetPassword", model: resetID);
        }

        [Authorize]
        public ActionResult LearnerCalendar()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View(profile);
        }

        [Authorize]
        public ActionResult LearnerEvents(DateTime dateFrom, DateTime? dateTo)
        {
            ViewBag.StartDate = dateFrom;
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View("~/Views/CornerKick/Module/LearnerEvents.cshtml", profile);
        }

        [Authorize]
        public ActionResult EditUserEvent(UserEventViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser().LoadInstance(models);

            if (viewModel.KeyID != null)
            {
                viewModel.EventID = viewModel.DecryptKeyValue();
            }

            var item = models.GetTable<UserEvent>().Where(v => v.EventID == viewModel.EventID && v.UID == profile.UID).FirstOrDefault();
            if (item != null)
            {
                viewModel.UID = item.UID;
                viewModel.StartDate = item.StartDate;
                viewModel.EndDate = item.EndDate;
                viewModel.Title = item.Title;
            }

            return View("~/Views/CornerKick/EditUserEvent.cshtml", profile);
        }

        [Authorize]
        public ActionResult CommitUserEvent(UserEventViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var profile = HttpContext.GetUser();

            if (!viewModel.StartDate.HasValue)
            {
                ModelState.AddModelError("StartDate", "請選擇開始時間!!");
            }
            if (!viewModel.EndDate.HasValue)
            {
                ModelState.AddModelError("EndDate", "請選擇結束時間!!");
            }

            if (viewModel.StartTime.HasValue)
            {
                viewModel.StartDate = viewModel.StartDate.Value + viewModel.StartTime.Value;
            }

            if (viewModel.EndTime.HasValue)
            {
                viewModel.EndDate = viewModel.EndDate.Value + viewModel.EndTime.Value;
            }

            if (viewModel.StartDate > viewModel.EndDate)
            {
                ModelState.AddModelError("StartDate", "開始日期晚於結束日期!!");
            }


            if (String.IsNullOrEmpty(viewModel.Title))
            {
                ModelState.AddModelError("Title", "請輸入行事曆內容!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            var item = models.GetTable<UserEvent>().Where(u => u.EventID == viewModel.EventID).FirstOrDefault();
            if (item == null)
            {
                item = new UserEvent
                {
                    UID = profile.UID
                };
                models.GetTable<UserEvent>().InsertOnSubmit(item);
                viewModel.CreateNew = true;
            }

            item.StartDate = viewModel.StartDate.Value;
            item.EndDate = viewModel.EndDate.Value;
            item.Title = viewModel.Title;

            models.SubmitChanges();

            return View("~/Views/CornerKick/Module/CompleteUserEvent.cshtml", item);
        }

        [Authorize]
        public ActionResult ShowLearnerEvent(UserEventViewModel viewModel)
        {
            var profile = HttpContext.GetUser();

            var eventItem = models.GetTable<UserEvent>().Where(v => v.UID == profile.UID && v.EventID == viewModel.EventID).FirstOrDefault();
            if (eventItem != null)
            {
                return EditUserEvent(viewModel);
            }

            var lessonItem = models.GetTable<LessonTime>().Where(l => l.LessonID == viewModel.EventID).FirstOrDefault();
            if (lessonItem != null)
            {
                ViewBag.DataItem = lessonItem;
                return View("~/Views/CornerKick/TodayLesson.cshtml", profile.LoadInstance(models));
            }

            return View("LearnerCalendar", profile.LoadInstance(models));
        }

        [Authorize]
        public ActionResult DeleteLearnerEvent(UserEventViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var eventItem = models.DeleteAny<UserEvent>(v => v.UID == profile.UID && v.EventID == viewModel.EventID);
            return View("LearnerCalendar", profile.LoadInstance(models));
        }

        public ActionResult WriteOffBonusPoint(AwardQueryViewModel viewModel)
        {
            if (viewModel.KeyID != null)
            {
                viewModel = JsonConvert.DeserializeObject<AwardQueryViewModel>(viewModel.KeyID.DecryptKey());
            }

            ViewBag.ViewModel = viewModel;

            var profile = HttpContext.GetUser().LoadInstance(models);


            var item = models.GetTable<BonusAwardingItem>().Where(i => i.ItemID == viewModel.ItemID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "兌換商品錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            if (profile.BonusPoint(models) < item.PointValue)
            {
                return Json(new { result = false, message = "累積點數不足!!" }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.DataItem = item;

            if (viewModel.Confirmed == true)
            {
                return View("~/Views/CornerKick/WriteOffBonusPoint.cshtml", profile);
            }
            else
            {
                viewModel.Confirmed = true;
                ViewBag.ViewModel = new QueryViewModel
                {
                    UrlAction = Url.Action("WriteOffBonusPoint"),
                    KeyID = viewModel.JsonStringify().EncryptKey()
                };
                return View("~/Views/CornerKick/Shared/ViewModelCommitted.cshtml");
            }
        }

        public ActionResult ExchangeBonusPoint(AwardQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser().LoadInstance(models);

            var item = models.GetTable<BonusAwardingItem>().Where(i => i.ItemID == viewModel.ItemID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "兌換商品錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            if (profile.BonusPoint(models) < item.PointValue)
            {
                return Json(new { result = false, message = "累積點數不足!!" }, JsonRequestBehavior.AllowGet);
            }

            viewModel.WriteoffCode = viewModel.WriteoffCode.GetEfficientString();
            int? recipientID = null;
            if (item.BonusAwardingLesson != null)
            {
                viewModel.ActorID = profile.UID;

                if (item.BonusAwardingIndication != null && item.BonusAwardingIndication.Indication == "AwardingLessonGift")
                {
                    var users = models.GetTable<UserProfile>()
                        .Where(u => u.UID != profile.UID)
                        .Where(u => u.LevelID != (int)Naming.MemberStatusDefinition.Deleted
                                && u.LevelID != (int)Naming.MemberStatusDefinition.Anonymous)
                        .Where(l => l.Phone == viewModel.WriteoffCode)
                        .FilterByLearner(models)
                        .Where(u => u.UserProfileExtension != null && !u.UserProfileExtension.CurrentTrial.HasValue);

                    int count = users.Count();

                    if (count == 0)
                    {
                        ModelState.AddModelError("WriteoffCode", "受贈會員資料錯誤，請確認後再重新輸入");
                    }
                    else if (count > 1)
                    {
                        ModelState.AddModelError("WriteoffCode", "受贈學員手機號碼重複，請連絡您的體能顧問");
                    }
                    else
                    {
                        recipientID = users.First().UID;
                    }
                }
            }
            else
            {
                var coach = models.PromptEffectiveCoach(models.GetTable<UserProfile>().Where(l => l.Phone == viewModel.WriteoffCode)).FirstOrDefault();
                if (coach == null)
                {
                    ModelState.AddModelError("WriteoffCode", "兌換核銷密碼欄位資料錯誤，請確認後再重新輸入");
                }
                else
                {
                    viewModel.ActorID = coach.CoachID;
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/CornerKick/Shared/ReportInputError.cshtml");
            }

            var award = new LearnerAward
            {
                UID = profile.UID,
                AwardDate = DateTime.Now,
                ItemID = item.ItemID,
                ActorID = viewModel.ActorID.Value,
            };
            models.GetTable<LearnerAward>().InsertOnSubmit(award);

            int usedPoints = item.PointValue;
            foreach (var bounsItem in profile.BonusPointList(models))
            {
                if (usedPoints <= 0)
                    break;
                award.BonusExchange.Add(new BonusExchange
                {
                    TaskID = bounsItem.TaskID
                });
                usedPoints -= bounsItem.PDQTask.PDQQuestion.PDQQuestionExtension.BonusPoint.Value;
            }

            ///兌換課程
            ///
            if (item.BonusAwardingLesson != null)
            {
                var lesson = models.GetTable<RegisterLesson>().Where(r => r.UID == profile.UID)
                    .Join(models.GetTable<RegisterLessonContract>(), r => r.RegisterID, c => c.RegisterID, (r, c) => r)
                    .OrderByDescending(r => r.RegisterID).FirstOrDefault();

                if (item.BonusAwardingIndication != null && item.BonusAwardingIndication.Indication == "AwardingLessonGift")
                {
                    var giftLesson = new RegisterLesson
                    {
                        RegisterDate = DateTime.Now,
                        GroupingMemberCount = 1,
                        ClassLevel = item.BonusAwardingLesson.PriceID,
                        Lessons = 1,
                        UID = recipientID.Value,
                        AdvisorID = lesson?.RegisterLessonContract.CourseContract.FitnessConsultant,
                        Attended = (int)Naming.LessonStatus.準備上課,
                        GroupingLesson = new GroupingLesson { }
                    };
                    award.AwardingLessonGift = new AwardingLessonGift
                    {
                        RegisterLesson = giftLesson
                    };
                    models.GetTable<RegisterLesson>().InsertOnSubmit(giftLesson);
                }
                else
                {
                    var awardLesson = new RegisterLesson
                    {
                        RegisterDate = DateTime.Now,
                        GroupingMemberCount = 1,
                        ClassLevel = item.BonusAwardingLesson.PriceID,
                        Lessons = 1,
                        UID = profile.UID,
                        AdvisorID = lesson?.AdvisorID,
                        Attended = (int)Naming.LessonStatus.準備上課,
                        GroupingLesson = new GroupingLesson { }
                    };
                    award.AwardingLesson = new AwardingLesson
                    {
                        RegisterLesson = awardLesson
                    };
                    models.GetTable<RegisterLesson>().InsertOnSubmit(awardLesson);
                }
            }

            models.SubmitChanges();
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PayoffOnLine(PaymentViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if (viewModel.KeyID != null)
            {
                viewModel.PaymentID = viewModel.DecryptKeyValue();
            }

            var profile = HttpContext.GetUser();

            var item = models.GetTable<Payment>()
                .Where(c => c.PaymentID == viewModel.PaymentID)
                .FirstOrDefault();

            if (item == null)
            {
                ViewBag.ViewModel = new DataItemViewModel
                {
                    Title = "我的付款",
                    Message = "付款資料錯誤！",
                };
                return View("~/Views/CornerKick/DataNotFound.cshtml");
            }

            PaymentOnLine paymentItem = new PaymentOnLine
            {
                PaymentID = item.PaymentID,
                oid = Guid.NewGuid().ToString(),
            };

            models.GetTable<PaymentOnLine>().InsertOnSubmit(paymentItem);
            models.SubmitChanges();

            paymentItem.oid = $"{paymentItem.OrderID:000000000000}";
            models.SubmitChanges();

            return View("~/Views/CornerKick/PayoffOnLine.cshtml", paymentItem);
        }

        public ActionResult FrontendPayoff(PayoffViewModel viewModel)
        {
            //return CommitPayoff(viewModel);
            var path = Dump(false);
            return Content(System.IO.File.ReadAllText(path), "text/plain");
        }

        public ActionResult CommitPayoff(PayoffViewModel viewModel)
        {
            String path = Dump();

            ViewBag.ViewModel = viewModel;

            var profile = HttpContext.GetUser();

            var item = models.GetTable<PaymentOnLine>()
                .Where(c => c.oid == viewModel.oid)
                .FirstOrDefault();

            if (item != null)
            {
                item.mid = viewModel.mid;
                item.tid = viewModel.tid;
                item.oid = viewModel.oid;
                item.pan = viewModel.pan;
                item.transCode = viewModel.transCode;
                item.transMode = viewModel.transMode;
                item.transDate = viewModel.transDate;
                item.transTime = viewModel.transTime;
                item.transAmt = viewModel.transAmt;
                item.approveCode = viewModel.approveCode;
                item.responseCode = viewModel.responseCode;
                item.responseMsg = viewModel.responseMsg;
                item.installmentType = viewModel.installmentType;
                item.installment = viewModel.installment;
                item.firstAmt = viewModel.firstAmt;
                item.eachAmt = viewModel.eachAmt;
                item.fee = viewModel.fee;
                item.redeemType = viewModel.redeemType;
                item.redeemUsed = viewModel.redeemUsed;
                item.redeemBalance = viewModel.redeemBalance;
                item.creditAmt = viewModel.creditAmt;
                item.secureStatus = viewModel.secureStatus.GetEfficientString();

                models.SubmitChanges();

                if ($"{item.PaymentTransaction.Payment.PayoffAmount}" != item.transAmt)
                {
                    int rtnCode = 0;
                    ApiClient apiClient = new ApiClient();

                    apiClient.clear();
                    apiClient.setMid(item.mid);
                    apiClient.setOid(item.oid);
                    apiClient.setTransCode("01");
                    apiClient.setDoname("eposuat.sinopac.com");
                    apiClient.setSecurityId(item.PaymentTransaction.BranchStore.EPOS_SID);
                    try
                    {
                        rtnCode = apiClient.post();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }

            //return Content(System.IO.File.ReadAllText(path), "text/plain");

            var result = new
            {
                viewModel.mid,
                viewModel.tid,
                viewModel.oid,
                transCode = $"{viewModel.transCode:00}",
                viewModel.approveCode,
                viewModel.responseCode,
            };

            Logger.Info(result.JsonStringify());
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}