﻿using System;
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
using WebHome.Security.Authorization;

namespace WebHome.Controllers
{
    [Authorize]
    public class MemberController : SampleController<UserProfile>
    {
        public MemberController() : base()
        {

        }

        [AssistantOrSysAdminAuthorizeAttribute]
        public ActionResult ListCoaches()
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

            return View("ListCoaches",viewModel);
        }

        [CoachOrAssistantAuthorize]
        public ActionResult ListLearners(String byName, String message = null)
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
                u => u.UID, r => r.UID, (u, r) => u)
                .OrderByDescending(u => u.UID);

            if (!String.IsNullOrEmpty(byName))
            {
                models.Items = models.Items.Where(u => u.UserName.Contains(byName) || u.RealName.Contains(byName) || u.Nickname.Contains(byName));
            }

            ViewBag.Message = message;
            return View("ListLearners", models.Items);
        }


        [CoachOrAssistantAuthorize]
        public ActionResult AddLearner(LearnerViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CommitLearner(LearnerViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }

            CreateLearner(viewModel);

            ViewResult result = (ViewResult)ListLearners(null);
            ViewBag.TabIndex = viewModel.CurrentTrial.HasValue ? 1 : 0;
            return result;
        }

        private void CreateLearner(LearnerViewModel viewModel)
        {
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
                UserProfileExtension = new UserProfileExtension
                {
                    Gender = viewModel.Gender,
                    AthleticLevel = viewModel.AthleticLevel,
                    CurrentTrial = viewModel.CurrentTrial
                }
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = (int)Naming.RoleID.Learner
            });


            models.EntityList.InsertOnSubmit(item);
            models.SubmitChanges();
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

        public ActionResult AddCoach(CoachViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult CommitCoach(CoachViewModel viewModel)
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
                UserProfileExtension = new UserProfileExtension { }
            };

            item.UserRole.Add(new UserRole
            {
                RoleID = viewModel.CoachRole.Value
            });

            if (viewModel.IsCoach == true)
            {
                item.ServingCoach = new ServingCoach
                {
                    Description = viewModel.Description,
                    LevelID = viewModel.LevelID
                };
            }

            models.EntityList.InsertOnSubmit(item);
            models.SubmitChanges();

            ViewBag.Message = "員工資料新增完成!!";

            return ListCoaches();
        }

        public ActionResult CommitMember(CoachViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();
            if (item == null)
            {
                String memberCode;
                while (true)
                {
                    memberCode = createMemberCode();
                    if (!models.EntityList.Any(u => u.MemberCode == memberCode))
                    {
                        break;
                    }
                }

                item = new UserProfile
                {
                    PID = memberCode,
                    MemberCode = memberCode,
                    LevelID = (int)Naming.MemberStatusDefinition.ReadyToRegister,
                    Birthday = viewModel.Birthday,
                    UserProfileExtension = new UserProfileExtension { }
                };

                models.GetTable<UserProfile>().InsertOnSubmit(item);

            }
            else
            {
                var email = viewModel.Email.GetEfficientString();
                if (email == null)
                {
                    ModelState.AddModelError("Email", "請輸入Email");
                }
                else
                {
                    if (models.EntityList.Any(u => u.PID == email && u.UID != item.UID))
                    {
                        ModelState.AddModelError("Email", "Email已經是註冊使用者!!");
                    }
                }
            }

            if(!viewModel.CoachRole.HasValue)
            {
                ModelState.AddModelError("CoachRole", "請選擇適用角色!!");
            }

            if (viewModel.CoachRole == (int)Naming.RoleID.Coach)
            {
                if (!viewModel.BranchID.HasValue)
                {
                    ModelState.AddModelError("BranchID", "請選擇隸屬分店!!");
                }
                if (!viewModel.LevelID.HasValue)
                {
                    ModelState.AddModelError("LevelID", "請選擇Level!!");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            models.DeleteAllOnSubmit<UserRole>(r => r.UID == item.UID);
            item.UserRole.Add(new UserRole
            {
                RoleID = viewModel.CoachRole.Value
            });

            if (viewModel.CoachRole == (int)Naming.RoleID.Coach)
            {
                if (item.ServingCoach == null)
                {
                    item.ServingCoach = new ServingCoach
                    {
                        Description = viewModel.Description
                    };
                }
                models.DeleteAllOnSubmit<CoachWorkplace>(c => c.CoachID == item.UID);
                item.ServingCoach.CoachWorkplace.Add(new CoachWorkplace
                {
                    BranchID = viewModel.BranchID.Value
                });
                if (item.ServingCoach.LevelID != viewModel.LevelID)
                {
                    item.ServingCoach.LevelID = viewModel.LevelID;
                    item.ServingCoach.CoachRating.Add(new CoachRating
                    {
                        CoachID = item.ServingCoach.CoachID,
                        LevelID = viewModel.LevelID.Value,
                        RatingDate = DateTime.Now
                    });
                }
            }

            item.RealName = viewModel.RealName;
            item.Phone = viewModel.Phone;

            models.SubmitChanges();
            return Json(new { result = true });

        }

        public ActionResult EditCoachCertificate(int uid)
        {

            ServingCoach item = models.GetTable<ServingCoach>().Where(u => u.CoachID == uid).FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            return View("~/Views/Member/Module/EditCoachCertificate.ascx", item);

        }

        public ActionResult AddCoachCertificate(int uid)
        {

            ServingCoach item = models.GetTable<ServingCoach>().Where(u => u.CoachID == uid).FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            return View("~/Views/Member/Module/AddCoachCertificate.ascx", item);

        }

        public ActionResult CommitCoachCertificate(CoachCertificateViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var coach = models.GetTable<ServingCoach>().Where(u => u.CoachID == viewModel.UID).FirstOrDefault();
            if (coach == null)
            {
                return View("~/Views/Shared/JsAlert.ascx", model: "資料錯誤!!");
            }

            if (!viewModel.CertificateID.HasValue)
            {
                ModelState.AddModelError("CertificateID", "請選證照!!");
            }

            if (!viewModel.Expiration.HasValue)
            {
                ModelState.AddModelError("Expiration", "請選到期日!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var item = coach.CoachCertificate.Where(c => c.CertificateID == viewModel.CertificateID).FirstOrDefault();
            if (item == null)
            {
                item = new CoachCertificate
                {
                    CertificateID = viewModel.CertificateID.Value
                };
                coach.CoachCertificate.Add(item);
            }

            item.Expiration = viewModel.Expiration;

            models.SubmitChanges();
            return Json(new { result = true });

        }

        public ActionResult DeleteCoachCertificate(CoachCertificateViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var item = models.DeleteAny<CoachCertificate>(c => c.CoachID == viewModel.UID && c.CertificateID == viewModel.CertificateID);

            if (item == null)
            {
                return Json(new { result = false,message = "資料錯誤!!" });
            }

            return Json(new { result = true });

        }


        public ActionResult CoachCertificateList(int uid)
        {
            var items = models.GetTable<CoachCertificate>().Where(c => c.CoachID == uid);
            return View("~/Views/Member/Module/CoachCertificateList.ascx", items);
        }



        public ActionResult DeleteLessons(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            RegisterLesson lesson = item.RegisterLesson.Where(r => r.RegisterID == id).FirstOrDefault();
            if (lesson == null)
            {
                ViewBag.ViewModel = new LessonViewModel
                {
                    AdvisorID = profile.UID
                };
                return View("AddLessons", item);
            }

            models.DeleteAnyOnSubmit<RegisterLesson>(l => l.RegisterID == lesson.RegisterID);

            if (lesson.GroupingMemberCount > 1)
            {
                models.DeleteAnyOnSubmit<GroupingLesson>(g => g.GroupID == lesson.RegisterGroupID);
            }

            models.SubmitChanges();

            ViewBag.Message = "資料已刪除!!";
            ViewBag.ViewModel = new LessonViewModel
            {
                AdvisorID = profile.UID
            };
            return View("AddLessons", item);
        }

        public ActionResult Delete(int uid)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == uid).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
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

            return ListLearners(null);

        }
        public ActionResult DeleteCoach(int uid)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == uid).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListCoaches();
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

            return ListCoaches();

        }

        public ActionResult EnableUser(int id)
        {



            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            item.LevelID = item.UserProfileExtension.RegisterStatus == true ? (int)Naming.MemberStatusDefinition.Checked : (int)Naming.MemberStatusDefinition.ReadyToRegister;
            models.SubmitChanges();

            ViewBag.Message = "會員已啟用!!";

            return ListLearners(null);

        }

        public ActionResult ApplyToFormal(int uid)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == uid).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
            }
            else
            {
                item.UserProfileExtension.CurrentTrial = null;
                models.SubmitChanges();

                ViewBag.Message = "學員資料轉至完成!!";
            }

            ViewBag.TabIndex = 1;
            return ListLearners(null);

        }

        public ActionResult EnableCoach(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListCoaches();
            }

            item.LevelID = item.UserProfileExtension.RegisterStatus == true ? (int)Naming.MemberStatusDefinition.Checked : (int)Naming.MemberStatusDefinition.ReadyToRegister;
            models.SubmitChanges();

            ViewBag.Message = "會員已啟用!!";

            return ListCoaches();

        }


        [CoachOrAssistantAuthorize]
        public ActionResult EditCoach(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListCoaches();
            }

            var model = new CoachViewModel
            {
                CoachRole = item.UserRole[0].RoleID,
                Phone = item.Phone,
                RealName = item.RealName,
                MemberCode = item.MemberCode,
                Birthday = item.Birthday,
                Email = item.PID.Contains("@") ? item.PID : null,
                IsCoach = item.ServingCoach != null,
                Description = item.ServingCoach != null ? item.ServingCoach.Description : null,
                LevelID = item.ServingCoach != null ? item.ServingCoach.LevelID : null
            };

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);
            ViewBag.Profile = item;

            return View(model);

        }

        [CoachOrAssistantAuthorize]
        public ActionResult EditMember(CoachViewModel viewModel)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == viewModel.UID 
                && !u.UserRole.Any(r=>r.RoleID==(int)Naming.RoleID.Learner)).FirstOrDefault();

            if (item != null)
            {
                viewModel.Birthday = item.Birthday;
                viewModel.CoachRole = item.UserRole.First().RoleID;
                viewModel.Phone = item.Phone;
                viewModel.Email = item.PID;
                viewModel.RealName = item.RealName;
                if(item.IsCoach())
                {
                    viewModel.IsCoach = true;
                    if(item.ServingCoach.CoachWorkplace.Count>0)
                    {
                        viewModel.BranchID = item.ServingCoach.CoachWorkplace.First().BranchID;
                    }
                    viewModel.LevelID = item.ServingCoach.ProfessionalLevel.LevelID;
                    viewModel.LevelCategory = item.ServingCoach.ProfessionalLevel.CategoryID;
                }
                else
                {
                    viewModel.IsCoach = false;
                }
            }

            ViewBag.ViewModel = viewModel;

            return View("~/Views/Member/Module/EditMember.ascx");

        }


        public ActionResult ShowMember(int id)
        {

            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            return View(item);

        }

        [CoachOrAssistantAuthorize]
        public ActionResult ShowLearner(int id)
        {
            return ShowMember(id);
        }


        [HttpPost]
        [CoachOrAssistantAuthorize]
        public ActionResult EditCoach(CoachViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(viewModel);
            }

            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            UserProfile item = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)).FirstOrDefault();

            if (item == null)
            {
                HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);
                ViewBag.Message = "資料錯誤!!";
                return ListCoaches();
            }

            viewModel.Email = viewModel.Email.GetEfficientString();
            if (viewModel.Email != null)
            {
                if (item.PID != viewModel.Email && models.EntityList.Any(u => u.PID == viewModel.Email))
                {
                    ModelState.AddModelError("email", "Email已經是註冊使用者!!");
                    ViewBag.ModelState = ModelState;
                    return View("EditCoach",viewModel);
                }
                item.PID = viewModel.Email;
            }

            item.RealName = viewModel.RealName;
            item.Phone = viewModel.Phone;
            if (viewModel.Birthday.HasValue)
                item.Birthday = viewModel.Birthday;

            if (viewModel.IsCoach == true)
            {
                item.ServingCoach.Description = viewModel.Description;
                if (profile.IsOfficer())
                {
                    if (item.ServingCoach.LevelID != viewModel.LevelID)
                    {
                        item.ServingCoach.LevelID = viewModel.LevelID;
                        item.ServingCoach.CoachRating.Add(new CoachRating
                        {
                            CoachID = item.ServingCoach.CoachID,
                            LevelID = viewModel.LevelID.Value,
                            RatingDate = DateTime.Now
                        });
                    }
                }
            }

            models.SubmitChanges();
            models.ExecuteCommand("update UserRole set RoleID = {0} where UID = {1}", viewModel.CoachRole, item.UID);

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);

            ViewBag.Message = "資料更新完成!!";

            return ListCoaches();
        }

        [CoachOrAssistantAuthorize]
        public ActionResult EditLearner(int id)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            if (item.UserProfileExtension == null)
                item.UserProfileExtension = new UserProfileExtension { };

            var model = new LearnerViewModel
            {
                MemberCode = item.MemberCode,
                Phone = item.Phone,
                RealName = item.RealName,
                Birthday = item.Birthday,
                Email = item.PID.IndexOf('@') >= 0 ? item.PID : null,
                MemberStatus = (Naming.MemberStatusDefinition?)item.LevelID,
                Gender = item.UserProfileExtension.Gender,
                AthleticLevel = item.UserProfileExtension.AthleticLevel
            };


            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);
            ViewBag.DataItem = item;
            ViewBag.Profile = item;

            return View(model);
        }

        [HttpPost]
        [CoachOrAssistantAuthorize]
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
                return ListLearners(null);
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

            if (item.UserProfileExtension == null)
                item.UserProfileExtension = new UserProfileExtension { };
            item.UserProfileExtension.Gender = viewModel.Gender;
            item.UserProfileExtension.AthleticLevel = viewModel.AthleticLevel;

            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);
            ViewBag.Message = "資料更新完成!!";

            return ListLearners(null);
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

        public ActionResult PayInstallment(int registerID)
        {
            RegisterLesson item = models.GetTable<RegisterLesson>()
                .Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)
                    && u.RegisterID == registerID).FirstOrDefault();

            if (item == null)
            {
                //ViewBag.Message = "課程資料不存在!!";
                return ListLearners(null);
            }

            if (item.IntuitionCharge == null)
            {
                item.IntuitionCharge = new IntuitionCharge
                {
                    ByInstallments = 1,
                    Payment = "Cash",
                    FeeShared = 0
                };
                models.SubmitChanges();
            }

            var viewModel = new InstallmentViewModel
            {
                RegisterID = item.RegisterID
            };

            var itemCount = Math.Max(item.IntuitionCharge.ByInstallments.Value, item.IntuitionCharge.TuitionInstallment.Count);
            viewModel.PayoffAmount = new int?[itemCount];
            viewModel.PayoffDate = new DateTime?[itemCount];

            if (item.IntuitionCharge.TuitionInstallment.Count > 0)
            {
                Array.Copy(item.IntuitionCharge.TuitionInstallment.Select(t => t.PayoffAmount).ToArray(), viewModel.PayoffAmount, item.IntuitionCharge.TuitionInstallment.Count);
                Array.Copy(item.IntuitionCharge.TuitionInstallment.Select(t => t.PayoffDate).ToArray(), viewModel.PayoffDate, item.IntuitionCharge.TuitionInstallment.Count);
            }

            ViewBag.ViewModel = viewModel;
            return View(item);
        }

        public ActionResult PaySingleInstallment(int registerID)
        {
            RegisterLesson item = models.GetTable<RegisterLesson>()
                .Where(u => u.RegisterID == registerID).FirstOrDefault();

            if (item == null)
            {
                //ViewBag.Message = "課程資料不存在!!";
                ViewBag.Message = "付款課程資料錯誤!!";
                return View("~/Views/Shared/MessageModal.ascx");
            }

            if (item.IntuitionCharge == null)
            {
                item.IntuitionCharge = new IntuitionCharge
                {
                    ByInstallments = 1,
                    Payment = "Cash",
                    FeeShared = 0
                };
                models.SubmitChanges();
            }

            return View("~/Views/Member/Tuition/Module/PaySingleInstallment.ascx", item.IntuitionCharge);
        }

        public ActionResult CommitSinglePayment(SingleInstallmentViewModel viewModel)
        {

            IntuitionCharge item = models.GetTable<IntuitionCharge>()
                .Where(u => u.RegisterID == viewModel.RegisterID).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "課程資料不存在!!";
                return ListLearners(null);
            }

            if(!viewModel.PayoffAmount.HasValue || viewModel.PayoffAmount<=0)
            {
                ModelState.AddModelError("PayoffAmount", "請輸入正確付款金額!!");
            }

            if (!viewModel.PayoffDate.HasValue)
            {
                ModelState.AddModelError("PayoffDate", "請輸入付款日期!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var installment = new TuitionInstallment
            {
                PayoffAmount = viewModel.PayoffAmount,
                PayoffDate = viewModel.PayoffDate,
                RegisterID = item.RegisterID
            };
            installment.TuitionAchievement.Add(new TuitionAchievement
            {
                CoachID = item.RegisterLesson.AdvisorID.Value,
                ShareAmount = installment.PayoffAmount
            });

            item.TuitionInstallment.Add(installment);
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult DeleteTuitionPayment(int installmentID)
        {

            var item = models.DeleteAny<TuitionInstallment>(t => t.InstallmentID == installmentID);

            if(item==null)
            {
                return Json(new { result = false, message = "付款資料錯誤!!" });
            }
            else
            {
                return Json(new { result = true });
            }

        }

        public ActionResult TuitionAchievementShare(int installmentID)
        {
            TuitionInstallment item = models.GetTable<TuitionInstallment>()
                .Where(u => u.InstallmentID == installmentID).FirstOrDefault();

            if (item == null)
            {
                //ViewBag.Message = "課程資料不存在!!";
                return View("~/Views/Shared/MessageView.ascx", model: "付款資料錯誤!!");
            }

            return View("~/Views/Member/Tuition/Module/TuitionAchievementShare.ascx", item);
        }

        public ActionResult CommitAchievementShare(AchievementShareViewModel viewModel)
        {
            TuitionInstallment item = models.GetTable<TuitionInstallment>()
                .Where(u => u.InstallmentID == viewModel.InstallmentID).FirstOrDefault();

            if (item == null)
            {
                return View("~/Views/Shared/MessageView.ascx", model: "付款資料不存在!!");
            }

            if (!viewModel.ShareAmount.HasValue || viewModel.ShareAmount <= 0)
            {
                ModelState.AddModelError("ShareAmount", "請輸入業績金額!!");
            }

            if(item.TuitionAchievement.Where(t=>t.CoachID!=viewModel.CoachID).Sum(t=>t.ShareAmount) + viewModel.ShareAmount > item.PayoffAmount)
            {
                ModelState.AddModelError("ShareAmount", "所屬體能顧問業績總額大於付款金額!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var shareItem = item.TuitionAchievement.Where(t => t.CoachID == viewModel.CoachID).FirstOrDefault();
            if(shareItem==null)
            {
                shareItem = new TuitionAchievement
                {
                    CoachID = viewModel.CoachID
                };
                item.TuitionAchievement.Add(shareItem);
            }

            shareItem.ShareAmount = viewModel.ShareAmount;
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult DeleteAchievementShare(int installmentID,int coachID)
        {

            var item = models.DeleteAny<TuitionAchievement>(t => t.InstallmentID == installmentID
                && t.CoachID==coachID);

            if (item == null)
            {
                return Json(new { result = false, message = "業績資料錯誤!!" });
            }
            else
            {
                return Json(new { result = true });
            }

        }


        public ActionResult CommitPayment(int registerID,InstallmentViewModel viewModel)
        {
            RegisterLesson item = models.GetTable<RegisterLesson>()
                .Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID)
                    && u.RegisterID == registerID).FirstOrDefault();

            if (item == null)
            {
                //ViewBag.Message = "課程資料不存在!!";
                return ListLearners(null);
            }

            List<TuitionInstallment> items = new List<TuitionInstallment>();
            for(int i=0;i< viewModel.PayoffAmount.Length;i++)
            {
                if(viewModel.PayoffAmount[i].HasValue && viewModel.PayoffDate[i].HasValue)
                {
                    var installment = new TuitionInstallment
                    {
                        PayoffAmount = viewModel.PayoffAmount[i],
                        PayoffDate = viewModel.PayoffDate[i],
                        RegisterID = item.RegisterID
                    };
                    installment.TuitionAchievement.Add(new TuitionAchievement
                    {
                        CoachID = item.AdvisorID.Value,
                        ShareAmount = installment.PayoffAmount
                    });
                    items.Add(installment);
                }
            }

            models.ExecuteCommand(@"DELETE FROM TuitionAchievement
                    FROM     TuitionInstallment INNER JOIN
                                    TuitionAchievement ON TuitionInstallment.InstallmentID = TuitionAchievement.InstallmentID
                    WHERE   (TuitionInstallment.RegisterID = {0}) ", item.RegisterID);
            models.ExecuteCommand("delete from TuitionInstallment where RegisterID = {0} ", item.RegisterID);
            models.GetTable<TuitionInstallment>().InsertAllOnSubmit(items);
            models.SubmitChanges();

            ViewBag.Message = "資料已儲存!!";

            return AddLessons((int)HttpContext.GetCacheValue(CachingKey.EditMemberUID));

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
                    && (l.UserProfile.RealName.Contains(userName) || l.UserProfile.Nickname.Contains(userName) || l.RegisterGroupID == item.RegisterGroupID)).ToArray();
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
                return ListLearners(null);
            }

            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
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
            String clearGroup = @"DELETE FROM GroupingLesson
                                    FROM     GroupingLesson INNER JOIN
                                                   RegisterLesson ON GroupingLesson.GroupID = RegisterLesson.RegisterGroupID
                                    WHERE   (RegisterLesson.RegisterID = {0})";
            foreach (var id in registerID)
            {
                models.ExecuteCommand(clearGroup, id);
                models.ExecuteCommand(sql, currentGroup.GroupID, id);
            }

            ViewBag.Message = "團體課學員設定完成!!";
            ViewBag.ViewModel = new LessonViewModel
            {
                AdvisorID = profile.UID
            };
            return View("AddLessons", item);
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

        public ActionResult RegisterLessonForm(int? registerID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Content("連線已逾時，請重新登入系統!!");
            }

            var viewModel = new LessonViewModel
            {
                AdvisorID = profile.UID
            };

            var item = models.GetTable<RegisterLesson>().Where(r => r.RegisterID == registerID).FirstOrDefault();
            if (item != null)
            {
                viewModel.RegisterID = item.RegisterID;
                viewModel.Lessons = item.Lessons;
                viewModel.ClassLevel = item.ClassLevel.Value;
                viewModel.Grouping = item.GroupingMemberCount > 1 ? "Y" : "N";
                viewModel.MemberCount = item.GroupingMemberCount;
                viewModel.AdvisorID = item.AdvisorID;
                viewModel.ByInstallments = item.IntuitionCharge.ByInstallments;
                viewModel.FeeShared = item.IntuitionCharge.FeeShared;
                viewModel.Installments = item.IntuitionCharge.ByInstallments.HasValue && item.IntuitionCharge.ByInstallments > 1 ? "Y" : "N";
                viewModel.Payment = item.IntuitionCharge.Payment;
            }

            return View(viewModel);

        }

        [AssistantOrSysAdminAuthorize]
        public ActionResult AddLessons(int id)
        {
            UserProfile item = models.EntityList.Where(u => u.UID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            //var lesson = item.RegisterLesson.OrderByDescending(r => r.RegisterID).FirstOrDefault();

            var model = new LessonViewModel
            {
                AdvisorID = profile.UID
            };

            HttpContext.SetCacheValue(CachingKey.EditMemberUID, item.UID);
            ViewBag.ViewModel = model;

            return View("AddLessons", item);
        }

        [AssistantOrSysAdminAuthorize]
        public ActionResult LessonTuitionInstallment(int id)
        {
            var item =  models.GetTable<RegisterLesson>().Where(u => u.RegisterID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            if (item.IntuitionCharge == null)
            {
                item.IntuitionCharge = new IntuitionCharge
                {
                    ByInstallments = 1,
                    Payment = "Cash",
                    FeeShared = 0
                };
                models.SubmitChanges();
            }

            ViewBag.DataItem = item.IntuitionCharge;
            return View("~/Views/Member/Tuition/InstallmentPayment.aspx", item.UserProfile);
        }

        [AssistantOrSysAdminAuthorize]
        public ActionResult TuitionCharge(int id)
        {
            var item = models.GetTable<IntuitionCharge>().Where(u => u.RegisterID == id).FirstOrDefault();

            if (item == null)
            {
                ViewBag.Message = "資料錯誤!!";
                return ListLearners(null);
            }

            return View("~/Views/Member/Tuition/Module/TuitionCharge.ascx", item);
        }

        [HttpPost]
        public ActionResult CommitLessons(LessonViewModel viewModel)
        {

            UserProfile learner = models.EntityList.Where(u => u.UID == (int?)HttpContext.GetCacheValue(CachingKey.EditMemberUID.ToString())).FirstOrDefault();

            if (learner == null)
            {
                ViewBag.Message = "資料錯誤!!";
                HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);
                return ListLearners(null);
            }

            UserProfile coach = HttpContext.GetUser();
            if (coach == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;
            if (viewModel.Lessons < 1)
            {
                ModelState.AddModelError("Lessons", "購買堂數錯誤!!");
            }

            if (!viewModel.AdvisorID.HasValue || viewModel.AdvisorID <= 1)
            {
                ModelState.AddModelError("AdvisorID", "請選擇體能顧問!!");
            }

            if (viewModel.Grouping == "Y" && viewModel.MemberCount < 2)
            {
                ModelState.AddModelError("MemberCount", "請選擇團體上課人數!!");
            }

            if (viewModel.Installments == "Y" && (!viewModel.ByInstallments.HasValue || viewModel.ByInstallments < 2))
            {
                ModelState.AddModelError("ByInstallments", "請輸入分期期數!!");
            }


            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("AddLessons", learner);
            }

            var item = models.GetTable<RegisterLesson>().Where(r => r.RegisterID == viewModel.RegisterID).FirstOrDefault();

            if (item == null)
            {
                item = new RegisterLesson
                {
                    ClassLevel = viewModel.ClassLevel,
                    RegisterDate = DateTime.Now,
                    Lessons = viewModel.Lessons,
                    Attended = (int)Naming.LessonStatus.準備上課,
                    GroupingMemberCount = viewModel.Grouping == "Y" ? viewModel.MemberCount : 1,
                    IntuitionCharge = new IntuitionCharge
                    {
                        Payment = viewModel.Payment,
                        FeeShared = viewModel.Payment == "CreditCard" ? viewModel.FeeShared : 0,
                        ByInstallments = viewModel.Installments == "Y" ? viewModel.ByInstallments : 1
                    },
                    AdvisorID = viewModel.AdvisorID,
                    AttendedLessons = 0
                };

                item.GroupingLesson = new GroupingLesson { };
                learner.RegisterLesson.Add(item);

            }
            else
            {
                item.Lessons = viewModel.Lessons;
                item.ClassLevel = viewModel.ClassLevel;
                item.GroupingMemberCount = viewModel.Grouping == "Y" ? viewModel.MemberCount : 1;
                //if (item.GroupingMemberCount != 1 && item.RegisterGroupID.HasValue)
                //{
                //    item.RegisterGroupID = null;
                //}
                item.AdvisorID = viewModel.AdvisorID;
                item.IntuitionCharge.Payment = viewModel.Payment;
                item.IntuitionCharge.FeeShared = viewModel.Payment == "CreditCard" ? viewModel.FeeShared : 0;
                item.IntuitionCharge.ByInstallments = viewModel.Installments == "Y" ? viewModel.ByInstallments : 1;
                //models.DeleteAllOnSubmit<TuitionInstallment>(t => t.RegisterID == item.RegisterID);
            }

            models.SubmitChanges();
            ViewBag.ViewModel = new LessonViewModel
            {
                AdvisorID = coach.UID
            };
            ViewBag.Message = "資料已儲存!!";
            //HttpContext.SetCacheValue(CachingKey.EditMemberUID, null);

            return View("AddLessons", learner);
        }


        public ActionResult Test(String alertMsg)
        {
            ModelState.AddModelError("realName", "test error...");
            ViewBag.ModelState = this.ModelState;
            ViewBag.Message = alertMsg ?? "Test Alert Message...";
            return View();
        }

        [CoachOrAssistantAuthorize]
        public ActionResult PDQ(int id, int? groupID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == id).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "學員資料不存在!!";
                return ListLearners(null);
            }

            ViewBag.GroupID = groupID;
            switch(groupID)
            {
                case 1:
                    ViewBag.Percent = "20%";
                    return View(item);
                case 2:
                    ViewBag.Percent = "40%";
                    return View("PDQ_All", item);
                case 3:
                    ViewBag.Percent = "60%";
                    return View("PDQ_All", item);
                case 4:
                    ViewBag.Percent = "80%";
                    return View("PDQ_All", item);
                case 5:
                    ViewBag.Percent = "95%";
                    return View("PDQ_All", item);
                case 6:
                    return View("PDQ_Final", item);
                default:
                    return View(item);
            }
        }

        public ActionResult UpdatePDQ(int id,int groupID,int? goalID, int? styleID, int? levelID)
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

            models.ExecuteCommand(@"
                DELETE FROM PDQTask
                FROM     PDQTask INNER JOIN
                                PDQQuestion ON PDQTask.QuestionID = PDQQuestion.QuestionID
                WHERE   (PDQTask.UID = {0}) AND (PDQQuestion.GroupID = {1})", item.UID, groupID);

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

            IQueryable<PDQQuestion> questItems = models.GetTable<PDQQuestion>().Where(q => q.GroupID == groupID);
            var voidAns = questItems
                .Where(q => !q.PDQTask.Any(t => t.UID == id)
                    || q.PDQTask.Count(t => t.UID == id && !t.SuggestionID.HasValue && t.PDQAnswer == "") == 1)
                .OrderBy(q => q.QuestionNo);
            if(voidAns.Count()>0)
            {
                return Json(new { result = false, message = "請填選第" + String.Join("、", voidAns.Select(q => q.QuestionNo)) + "題答案!!" });
            }

            return Json(new { result = true });
        }

        protected void savePDQ(UserProfile item, string key)
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