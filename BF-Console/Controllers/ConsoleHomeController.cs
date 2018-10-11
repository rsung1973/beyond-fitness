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
using WebHome.Controllers;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Properties;
using WebHome.Security.Authorization;

namespace BFConsole.Controllers
{
    [Authorize]
    public class ConsoleHomeController : SampleController<UserProfile>
    {
        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach })]
        public ActionResult Index()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View(profile);
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach })]
        public ActionResult ExerciseBillboard()
        {
            var profile = HttpContext.GetUser().LoadInstance(models);
            return View(profile);
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach })]
        public ActionResult InquireExerciseBillboard(ExerciseBillboardQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            if(!viewModel.StartDate.HasValue)
            {
                viewModel.StartDate = DateTime.Today.FirstDayOfMonth();
            }
            if(!viewModel.EndDate.HasValue)
            {
                viewModel.EndDate = DateTime.Today;
            }

            IQueryable<RegisterLesson> lessons = models.GetTable<RegisterLesson>();
            if(viewModel.BranchID.HasValue)
            {
                lessons = lessons.Join(models.GetTable<CoachWorkplace>().Where(w => w.BranchID == viewModel.BranchID), 
                    r => r.UID, w => w.CoachID, (r, w) => r);
            }

            IQueryable<LessonTime> items = models.PromptMemberExerciseLessons(lessons)
                    .Where(l => l.ClassTime >= viewModel.StartDate)
                    .Where(l => l.ClassTime < viewModel.EndDate.Value.AddDays(1));

            return View("~/Views/ConsoleHome/Module/CurrentExerciseBillboard.ascx", items);
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach })]
        public ActionResult InquireExerciseBillboardLastMonth(ExerciseBillboardQueryViewModel viewModel)
        {
            var queryDate = DateTime.Today.FirstDayOfMonth();
            viewModel.StartDate = queryDate.AddMonths(-1);
            viewModel.EndDate = queryDate.AddDays(-1);
            ViewResult result = (ViewResult)InquireExerciseBillboard(viewModel);
            result.ViewName = "~/Views/ConsoleHome/Module/ExerciseBillboard.ascx";
            return result;
        }

        [RoleAuthorize(RoleID = new int[] { (int)Naming.RoleID.Administrator, (int)Naming.RoleID.Assistant, (int)Naming.RoleID.Officer, (int)Naming.RoleID.Coach })]
        public ActionResult GetExerciseBillboardDetails(ExerciseBillboardQueryViewModel viewModel)
        {
            ViewResult result = (ViewResult)InquireExerciseBillboard(viewModel);

            IQueryable<LessonTime> items = result.Model as IQueryable<LessonTime>;
            if (items == null)
                return new EmptyResult { };

            Response.ContentType = "application/json";
            return View("~/Views/ConsoleHome/Module/ExerciseBillboardDetails.ascx", items);
        }



    }
}