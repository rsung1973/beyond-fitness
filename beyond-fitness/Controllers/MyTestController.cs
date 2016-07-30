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
using System.Web.Security;

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
        public ActionResult GetJson()
        {
            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
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


    }
}