using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonLib.MvcExtension;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    public class MyTestController : SampleController<UserProfile>
    {
        // GET: MyTest
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RedirectTo(String url)
        {
            return Redirect(url);
        }

        public ActionResult RenderAll()
        {
            return Content("Hello...");
        }

        public ActionResult GetJson()
        {
                TempData["msg"] = "Hello...";
                ViewBag.Message = "TEST~~";
            //StringBuilder sb = new StringBuilder();
            //using (StringWriter sw = new StringWriter(sb))
            //{
            //    //Server.Execute("~/Lessons/DailyBookingMembers?lessonDate=2016-08-20&hour=8", sw);
               
            //    Server.Execute("~/MyTest/RenderAll", sw);
            //    sw.Flush();
            //}
            return Json(new { result = true, details = this.RenderViewToString("Test01", null) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Test(String view)
        {
            return View(view, null);
        }

        public ActionResult TestError()
        {
            this.ModelState.AddModelError("memberCode", "Error Member Code!!");
            ViewBag.ModelState = ModelState;
            return View("Test01", null);
        }

        public ActionResult TestPopup(String message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            return View("TestPopup", model: message);
        }




    }
}