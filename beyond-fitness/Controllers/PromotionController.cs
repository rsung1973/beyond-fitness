using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Mvc.Html;

using CommonLib.MvcExtension;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;
using System.Data.Linq;
using WebHome.Security.Authorization;
using System.Data;

namespace WebHome.Controllers
{
    [Authorize]
    public class PromotionController : SampleController<UserProfile>
    {
        // GET: Promotion
        public ActionResult BonusPromotionIndex(PromotionQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            return View();
        }
    }
}