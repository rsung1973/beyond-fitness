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
using System.Text.RegularExpressions;

namespace WebHome.Controllers
{
    public class MemberController : SampleController<UserProfile>
    {
        public MemberController() : base()
        {

        }

        public ActionResult ListAll()
        {
            MembersQueryViewModel viewModel = (MembersQueryViewModel)HttpContext.GetCacheValue(CachingKey.MembersQuery);
            if(viewModel==null)
            {
                viewModel = new MembersQueryViewModel
                {
                    RoleID = Naming.RoleID.Coach
                };
                HttpContext.SetCacheValue(CachingKey.MembersQuery, viewModel);
            }
            return View(viewModel);
        }

        public ActionResult ListLearners(String byName)
        {

            MembersQueryViewModel viewModel = (MembersQueryViewModel)HttpContext.GetCacheValue(CachingKey.MembersQuery);
            if (viewModel == null)
            {
                viewModel = new MembersQueryViewModel
                {
                };
                HttpContext.SetCacheValue(CachingKey.MembersQuery, viewModel);
            }

            viewModel.RoleID = Naming.RoleID.Learner;
            viewModel.ByName = byName;

            models.Items = models.EntityList    //.Where(u => u.LevelID != (int)Naming.MemberStatusDefinition.Deleted)
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
        public ActionResult AddLearner()
        {



            LearnerViewModel viewModel = new LearnerViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddLearner(LearnerViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }




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
                Phone = viewModel.Phone,
                Birthday = viewModel.Birthday
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = (int)Naming.RoleID.Learner
            });


            models.EntityList.InsertOnSubmit(item);
            models.SubmitChanges();

            return View("LearnerCreated", item);
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
            CoachViewModel model = new CoachViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCoach(CoachViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }




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
                Phone = viewModel.Phone,
                Birthday = viewModel.Birthday,
                ServingCoach = new ServingCoach { }
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = viewModel.CoachRole
            });


            models.EntityList.InsertOnSubmit(item);
            models.SubmitChanges();

            return View("CoachCreated", item);
        }

        public ActionResult DeleteLessons(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            RegisterLesson lesson = item.RegisterLesson.Where(r => r.RegisterID == id).FirstOrDefault();
            if (lesson == null)
            {
                ViewBag.DataItem = item;
                return View("AddLessons", new LessonViewModel { });
            }

            models.DeleteAnyOnSubmit<RegisterLesson>(l => l.RegisterID == lesson.RegisterID);

            if (lesson.GroupingMemberCount > 1)
            {
                models.DeleteAnyOnSubmit<GroupingLesson>(g => g.GroupID == lesson.RegisterGroupID);
            }

            models.SubmitChanges();

            ViewBag.Message = "資料已刪除!!";
            ViewBag.DataItem = item;

            return View("AddLessons", new LessonViewModel { });

        }

        public ActionResult Delete(int uid)
        {



            UserProfile item = models.EntityList.Where(u => u.UID == uid).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            item.LevelID = (int)Naming.MemberStatusDefinition.Deleted;
            models.SubmitChanges();

            try
            {
                models.DeleteAny<UserProfile>(u => u.UID == item.UID);
            }
            catch (Exception ex)
            {
                Logger.Warn("無法刪除使用者，因其他關聯性資料...\r\n" + ex);
            }

            ViewBag.Message = "資料已刪除!!";

            return RedirectToAction("ListAll");

        }

        public ActionResult EnableUser(int id)
        {



            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            item.LevelID = (int)Naming.MemberStatusDefinition.Checked;
            models.SubmitChanges();

            ViewBag.Message = "會員已啟用!!";

            return RedirectToAction("ListAll");

        }

        public ActionResult EditCoach(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            var model = new CoachViewModel
            {
                CoachRole = item.UserRole[0].RoleID,
                Phone = item.Phone,
                RealName = item.RealName,
                MemberCode = item.MemberCode,
                Birthday = item.Birthday,
                Email = item.PID.Contains("@") ? item.PID : null
            };

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);

            return View(model);

        }

        public ActionResult ShowMember(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            return View(item);

        }

        public ActionResult ShowLearner(int id)
        {
            return ShowMember(id);
        }


        [HttpPost]
        public ActionResult EditCoach(CoachViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }

            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)).FirstOrDefault();

            if (item == null)
            {
                HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            viewModel.Email = viewModel.Email.GetEfficientString();
            if (viewModel.Email != null)
            {
                if (item.PID != viewModel.Email && models.EntityList.Any(u => u.PID == viewModel.Email))
                {
                    ModelState.AddModelError("email", "Email已經是註冊使用者!!");
                    ViewBag.ModelState = ModelState;
                    return View(viewModel);
                }
                item.PID = viewModel.Email;
            }

            item.RealName = viewModel.RealName;
            item.Phone = viewModel.Phone;
            if (viewModel.Birthday.HasValue)
                item.Birthday = viewModel.Birthday;

            models.SubmitChanges();
            models.ExecuteCommand("update UserRole set RoleID = {0} where UID = {1}", viewModel.CoachRole, item.UID);

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);

            return View("CoachUpdated", item);
        }

        public ActionResult EditLearner(int id)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            var lesson = item.RegisterLesson.OrderByDescending(r => r.RegisterID).FirstOrDefault();

            var model = new LearnerViewModel
            {
                MemberCode = item.MemberCode,
                Phone = item.Phone,
                RealName = item.RealName,
                Birthday = item.Birthday,
                Email = item.PID.IndexOf('@') >= 0 ? item.PID : null
            };


            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);
            ViewBag.DataItem = item;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditLearner(LearnerViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }

            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID.ToString())).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);
                return RedirectToAction("ListAll");
            }

            item.RealName = viewModel.RealName;
            item.Phone = viewModel.Phone;
            item.Birthday = viewModel.Birthday;

            if (!String.IsNullOrEmpty(viewModel.Email))
            {
                if (item.PID != viewModel.Email && models.EntityList.Any(u => u.PID == viewModel.Email))
                {
                    ModelState.AddModelError("email", "Email已經是註冊使用者!!");
                    ViewBag.ModelState = ModelState;
                    return View(viewModel);
                }
                item.PID = viewModel.Email;
            }

            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);

            return View("LearnerUpdated", item);
        }

        public ActionResult GroupLessons(int id)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);

            return View(item);
        }

        public ActionResult GroupLessonUsers(int lessonId)
        {

            RegisterLesson item = models.GetTable<RegisterLesson>()
                .Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)
                    && u.RegisterID == lessonId).FirstOrDefault();

            if (item == null)
            {
                return Content("課程資料不存在!!");
            }

            return View(item);
        }

        public ActionResult GroupLessonUsersSelector(int lessonId, String userName)
        {

            RegisterLesson item = models.GetTable<RegisterLesson>()
                .Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)
                    && u.RegisterID == lessonId).FirstOrDefault();

            if (item == null)
            {
                return Content("課程資料不存在!!");
            }

            RegisterLesson[] items;
            userName = userName.GetEfficientString();
            if (userName == null)
            {
                items = models.GetTable<RegisterLesson>().Where(l => l.Attended == (int)Naming.LessonStatus.準備上課 && l.ClassLevel == item.ClassLevel
                    && l.Lessons == item.Lessons
                    && l.RegisterID != item.RegisterID
                    && l.GroupingMemberCount == item.GroupingMemberCount).ToArray();

            }
            else
            {
                items = models.GetTable<RegisterLesson>().Where(l => l.Attended == (int)Naming.LessonStatus.準備上課 && l.ClassLevel == item.ClassLevel
                    && l.Lessons == item.Lessons
                    && l.RegisterID != item.RegisterID
                    && l.GroupingMemberCount == item.GroupingMemberCount
                    && (l.UserProfile.RealName.Contains(userName) || l.RegisterGroupID == item.RegisterGroupID)).ToArray();
            }

            ViewBag.DataItems = items;

            return View(item);
        }


        public ActionResult ApplyGroupLessons(int lessonId, int[] registerID)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)).FirstOrDefault();

            RegisterLesson lesson = models.GetTable<RegisterLesson>()
                .Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)
                    && u.RegisterID == lessonId).FirstOrDefault();

            if (lesson == null)
            {
                ViewBag.Message = "課程資料不存在!!";
                return RedirectToAction("ListAll");
            }

            if (lesson.RegisterGroupID.HasValue)
            {
                models.DeleteAnyOnSubmit<GroupingLesson>(g => g.GroupID == lesson.RegisterGroupID);
                lesson.RegisterGroupID = null;
                models.SubmitChanges();
            }

            GroupingLesson currentGroup = new GroupingLesson { };
            lesson.GroupingLesson = currentGroup;
            models.SubmitChanges();

            String sql = @"UPDATE RegisterLesson
                            SET RegisterGroupID = {0}
                            WHERE(RegisterID = {1})";
            foreach (var id in registerID)
            {
                models.ExecuteCommand(sql, currentGroup.GroupID, id);
            }

            return View("GroupLessons", item);
        }

        public ActionResult RemoveGroupUser(int id)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)).FirstOrDefault();

            RegisterLesson lesson = models.GetTable<RegisterLesson>()
                .Where(u => u.RegisterID == id).FirstOrDefault();

            if (lesson == null || lesson.GroupingLesson == null
                || !lesson.GroupingLesson.RegisterLesson.Any(r => r.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)))
            {
                ViewBag.Message = "課程資料不存在!!";
                return RedirectToAction("ListAll");
            }

            models.ExecuteCommand("update RegisterLesson set RegisterGroupID = null where RegisterID = {0} ", lesson.RegisterID);

            return View("GroupLessons", item);
        }

        public ActionResult ChangeRoleList(Naming.RoleID? roleID)
        {
            if(roleID.HasValue)
            {
                MembersQueryViewModel viewModel = (MembersQueryViewModel)HttpContext.GetCacheValue(CachingKey.MembersQuery);
                if(viewModel==null)
                {
                    viewModel = new MembersQueryViewModel { };
                }
                viewModel.RoleID = roleID;
                HttpContext.SetCacheValue(CachingKey.MembersQuery, viewModel);
            }

            return Json(new { result = true });
        }




        public ActionResult AddLessons(int id)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return RedirectToAction("ListAll");
            }

            var lesson = item.RegisterLesson.OrderByDescending(r => r.RegisterID).FirstOrDefault();

            var model = new LessonViewModel
            {

            };

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);
            ViewBag.DataItem = item;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLessons(LessonViewModel viewModel)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID.ToString())).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);
                return RedirectToAction("ListAll");
            }

            item.RegisterLesson.Add(new RegisterLesson
            {
                ClassLevel = viewModel.ClassLevel,
                RegisterDate = DateTime.Now,
                Lessons = viewModel.Lessons,
                Attended = (int)Naming.LessonStatus.準備上課,
                GroupingMemberCount = String.IsNullOrEmpty(viewModel.Grouping) ? 1 : viewModel.MemberCount
            });

            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);

            return View("LessonsUpdated", item);
        }


        public ActionResult Test(String alertMsg)
        {
            ModelState.AddModelError("realName", "test error...");
            ViewBag.ModelState = this.ModelState;
            ViewBag.Message = alertMsg ?? "Test Alert Message...";
            return View();
        }

        public ActionResult PDQ(int id)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == id).FirstOrDefault();
            if(item==null)
            {
                ViewBag.Message = "學員資料不存在!!";
                return RedirectToAction("ListAll");
            }

            ViewBag.DataItems = models.GetTable<PDQQuestion>().ToArray();
            return View(item);
        }

        public ActionResult UpdatePDQ(int id,int? goalID, int? styleID, int? levelID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new { result = false, message = "您的連線已中斷，請重新登入系統!!" });
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == id).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "學員資料不存在!!" });
            }

            models.ExecuteCommand("delete PDQTask where UID = {0}", item.UID);

            foreach (var key in Request.Form.AllKeys.Where(k => Regex.IsMatch(k, "_\\d")))
            {
                savePDQ(item, key);
            }

            if (item.PDQUserAssessment == null)
                item.PDQUserAssessment = new PDQUserAssessment { };
            item.PDQUserAssessment.GoalID = goalID;
            item.PDQUserAssessment.StyleID = styleID;
            item.PDQUserAssessment.LevelID = levelID;

            models.SubmitChanges();

            return Json(new { result = true });
        }

        private void savePDQ(UserProfile item, string key)
        {
            int questionID = int.Parse(key.Substring(1));
            var quest = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == questionID).FirstOrDefault();
            if (quest == null)
                return;

            var values = Request.Form.GetValues(key);
            if (values == null)
                return;

            switch ((Naming.QuestionType)quest.QuestionType)
            {
                case Naming.QuestionType.問答題:
                    if (values.Length > 0)
                    {
                        models.GetTable<PDQTask>().InsertOnSubmit(new PDQTask
                        {
                            QuestionID = quest.QuestionID,
                            UID = item.UID,
                            PDQAnswer = values[0]
                        });
                        models.SubmitChanges();
                    }
                    break;

                case Naming.QuestionType.單選題:
                case Naming.QuestionType.單選其他:
                    foreach (var v in values)
                    {
                        int suggestID;
                        if (int.TryParse(v, out suggestID))
                        {
                            models.GetTable<PDQTask>().InsertOnSubmit(new PDQTask
                            {
                                QuestionID = quest.QuestionID,
                                UID = item.UID,
                                SuggestionID = suggestID
                            });
                        }
                        else
                        {
                            models.GetTable<PDQTask>().InsertOnSubmit(new PDQTask
                            {
                                QuestionID = quest.QuestionID,
                                UID = item.UID,
                                PDQAnswer = v
                            });
                        }
                        models.SubmitChanges();
                    }
                    break;

                case Naming.QuestionType.多重選:
                    break;

                case Naming.QuestionType.是非題:
                    if (values.Length > 0)
                    {
                        models.GetTable<PDQTask>().InsertOnSubmit(new PDQTask
                        {
                            QuestionID = quest.QuestionID,
                            UID = item.UID,
                            YesOrNo = values[0] == "1" ? true : false
                        });
                        models.SubmitChanges();
                    }
                    break;
            }
        }
    }

}