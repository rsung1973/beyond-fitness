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
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult ListAll()
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);

            return View();
        }

        public ActionResult ListLearners(String byName)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);

            models.Items = models.EntityList.Where(u => u.LevelID != (int)Naming.MemberStatusDefinition.Deleted)
                .Join(models.GetTable<UserRole>()
                    .Where(r => r.RoleID == (int)Naming.RoleID.Learner),
                u => u.UID, r => r.UID, (u, r) => u);

            if (!String.IsNullOrEmpty(byName))
            {
                models.Items = models.Items.Where(u => u.UserName.Contains(byName) || u.RealName.Contains(byName));
            }

            return View(models.Items);
        }


        [HttpGet]
        public ActionResult AddLeaner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLeaner(LeanerViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View();
            }

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);

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
                LevelID = (int)Naming.MemberStatusDefinition.ReadyToRegister,
                RealName = viewModel.RealName,
                Phone = viewModel.Phone
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = (int)Naming.RoleID.Learner
            });

            item.RegisterLesson.Add(new RegisterLesson
            {
                ClassLevel = viewModel.ClassLevel,
                RegisterDate = DateTime.Now,
                Lessons = viewModel.Lessons
            });

            models.EntityList.InsertOnSubmit(item);
            models.SubmitChanges();

            return View("VipPDQ_1", item);
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

        [HttpGet]
        public ActionResult AddCoach()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCoach(CoachViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }

            ModelSource<UserProfile> models = new ModelSource<UserProfile>();
            TempData.SetModelSource(models);

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
                LevelID = (int)Naming.MemberStatusDefinition.ReadyToRegister,
                RealName = viewModel.RealName,
                Phone = viewModel.Phone
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = viewModel.CoachRole
            });


            models.EntityList.InsertOnSubmit(item);
            models.SubmitChanges();

            return View("CoachCreated", item);
        }

        public ActionResult Delete(int uid)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == uid).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return View("ListAll");
            }

            item.LevelID = (int)Naming.MemberStatusDefinition.Deleted;
            models.SubmitChanges();

            ViewBag.Message = "資料已刪除!!";

            return View("ListAll");

        }

        public ActionResult EditCoach(int id)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return View("ListAll");
            }

            return View(item);

        }

        [HttpPost]
        public ActionResult EditCoach(CoachViewModel viewModel)
        {
            ModelSource<UserProfile> models = new ModelSource<UserProfile>();

            TempData.SetModelSource(models);
            UserProfile item = models.EntityList.Where(u => u.UID == viewModel.UID).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return View("ListAll");
            }

            item.RealName = viewModel.RealName;
            item.Phone = viewModel.Phone;
            item.UserRole[0].RoleID = viewModel.CoachRole;
            models.SubmitChanges();

            return View("CoachUpdated", item);
        }



        public ActionResult Test()
        {
            ModelState.AddModelError("realName", "test error...");
            ViewBag.ModelState = this.ModelState;
            return View();
        }


    }

}