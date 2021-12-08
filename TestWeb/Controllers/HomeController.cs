using CustomPolicyProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestWeb.Models;
using TestWeb.Models.DataEntity;

namespace TestWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // View protected with custom parameterized authorization policy
        [MinimumAgeAuthorize(10)]
        public IActionResult MinimumAge10()
        {
            return View("MinimumAge", 10);
        }

        // View protected with custom parameterized authorization policy
        [MinimumAgeAuthorize(50)]
        //[MinimumAgeAuthorize(30)]
        public IActionResult MinimumAge50()
        {
            return View("MinimumAge", 50);
        }

        public ActionResult HandleUnknownAction(string actionName)
        {
            BFDataContext db = new BFDataContext();
            IQueryable<UserProfile> items = db.GetTable<UserProfile>();
            var count = items.Count();
            return Content(actionName);
            //this.View(actionName).ExecuteResult(this.ControllerContext);
        }

        public async Task<ActionResult> TestDownloadAsync()
        {
            var s = HttpUtility.UrlEncode("http://www.xxx.yyy.zzz?a=b+c");
            Response.ContentType = "application/octet-stream";
            Response.Headers.Add("Content-Disposition", String.Format("attachment;filename={0:yyyy-MM-dd HH-mm-ss}.dat", DateTime.Now));

            using(Stream input = System.IO.File.OpenRead("G:\\temp\\request.txt"))
            {
                await input.CopyToAsync(Response.Body);
            }
            await Response.Body.FlushAsync();

            return new EmptyResult { };
        }

        public ActionResult TestRedirect()
        {
            return Redirect("~/Account/Signin");
        }
    }
}
