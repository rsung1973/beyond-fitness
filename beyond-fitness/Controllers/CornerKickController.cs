using System;
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
using Newtonsoft.Json;
using Utility;
using WebHome.Helper;
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
        public ActionResult AG001_Index(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }
        public ActionResult AG001_Register(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = models.GetTable<UserProfileExtension>().Where(u => u.LineID == viewModel.LineID)
                    .Select(u => u.UserProfile).FirstOrDefault();

            if (item != null)
            {
                HttpContext.SignOn(item);
                return View("~/Views/Html/Module/AutoLogin.ascx", model: this.ProcessLogin(item));
            }
            else
            {
                return View();
            }

        }

        public ActionResult AG001_Unbind(RegisterViewModel viewModel)
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
                return View("AG001_Index");
            }

        }

        public ActionResult AG001_CommitUnbound(RegisterViewModel viewModel)
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

            return View("AG001_Index");

        }


        public ActionResult AG001_BindUser(RegisterViewModel viewModel)
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
            if (viewModel.PID == null)
            {
                ModelState.AddModelError("PID", "電子信箱資料錯誤，請確認後再重新輸入!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                ViewBag.SingleError = true;
                return View("~/Views/Shared/Materialize/ReportInputError.ascx");
            }

            UserProfile item = models.EntityList.Where(u => u.MemberCode == viewModel.MemberCode
                || u.UserProfileExtension.IDNo == viewModel.MemberCode).FirstOrDefault();

            if (item == null)
            {
                ModelState.AddModelError("PID", "您輸入的資料錯誤，請確認後再重新輸入!!");
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/Materialize/ReportInputError.ascx");
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

            if (!item.IsLearner())
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
                return View("~/Views/Shared/Materialize/ReportInputError.ascx");
            }

            if (item.PID != viewModel.PID)
            {
                item.PID = viewModel.PID;
            }

            item.UserProfileExtension.LineID = viewModel.LineID;
            models.SubmitChanges();

            HttpContext.SignOn(item);

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        public ActionResult AG001_Settings()
        {
            return View();
        }

        [Authorize]
        public ActionResult AG001_CommitSettings(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            viewModel.UserName = viewModel.UserName.GetEfficientString();
            if (viewModel.UserName == null)
            {
                ModelState.AddModelError("UserName", "請輸入暱稱!!");
            }


            UserProfile item = HttpContext.GetUser().LoadInstance(models);

            item.UserName = viewModel.UserName;
            models.SubmitChanges();

            return View("~/Views/Html/Module/AutoLogin.ascx", model: this.ProcessLogin(item));

        }

        public ActionResult AG001_Notice(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = models.GetTable<UserProfileExtension>().Where(u => u.LineID == viewModel.LineID)
                    .Select(u => u.UserProfile).FirstOrDefault();

            if (item != null)
            {
                HttpContext.SignOn(item);
                List<TimelineEvent> events = new List<TimelineEvent>();

                return View(item);
            }
            else
            {
                ViewBag.Message = "您的Line ID尚未設定，請先進行首次設定!!";
                return View("AG001_Index");
            }

        }

        public ActionResult AG001_NoticeNotFound(RegisterViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

    }
}