﻿using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

using CommonLib.MvcExtension;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;
using WebHome.Security.Authorization;
using WebHome.Properties;

namespace WebHome.Controllers
{
    [Authorize]
    public class CourseContractController : SampleController<UserProfile>
    {
        // GET: CourseContract
        public ActionResult ContractIndex(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult ApplyAmendment(CourseContractQueryViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View();
        }

        public ActionResult CreateContract()
        {
            var profile = HttpContext.GetUser();
            var item = profile.LoadInstance(models);

            return View(item);
        }

        [CoachOrAssistantAuthorize]
        public ActionResult EditCourseContract(CourseContractViewModel viewModel)
        {

            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                viewModel.ContractType = item.ContractType;
                viewModel.ContractDate = item.ContractDate;
                viewModel.Subject = item.Subject;
                viewModel.ValidFrom = item.ValidFrom;
                viewModel.Expiration = item.Expiration;
                viewModel.OwnerID = item.OwnerID;
                viewModel.SequenceNo = item.SequenceNo;
                viewModel.Lessons = item.Lessons;
                viewModel.PriceID = item.PriceID;
                viewModel.Remark = item.Remark;
                viewModel.FitnessConsultant = item.FitnessConsultant;
                viewModel.Status = item.Status;
                viewModel.UID = item.CourseContractMember.Select(m => m.UID).ToArray();
                viewModel.BranchID = item.LessonPriceType.BranchID;
            }

            ViewBag.ViewModel = viewModel;
            return View("~/Views/CourseContract/Module/EditCourseContract.ascx", item);
        }

        [CoachOrAssistantAuthorize]
        public ActionResult DeleteCourseContract(int contractID)
        {
            try
            {
                var item = models.DeleteAny<CourseContract>(d => d.ContractID == contractID);
                if (item != null)
                {
                    models.ExecuteCommand(@"
                        DELETE FROM UserProfile
                        FROM     UserProfile INNER JOIN
                                       UserRole ON UserProfile.UID = UserRole.UID
                        WHERE   (UserRole.RoleID = 11) AND (UserProfile.UID NOT IN
                           (SELECT  UID
                           FROM     CourseContractMember))");

                    return Json(new { result = true });
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }
            return Json(new { result = false });
        }

        public ActionResult ClearPreliminaryMember()
        {
            models.ExecuteCommand(@"
                        DELETE FROM UserProfile
                        FROM     UserProfile INNER JOIN
                                       UserRole ON UserProfile.UID = UserRole.UID
                        WHERE   (UserRole.RoleID = 11) AND (UserProfile.UID NOT IN
                           (SELECT  UID
                           FROM     CourseContractMember))");

            return Json(new { result = true });

        }



        public ActionResult ListContractMember(int[] uid,int? contractType,int? ownerID)
        {
            IQueryable<UserProfile> items = models.GetTable<UserProfile>();
            if (uid != null)
            {
                items = items.Where(c => uid.Contains(c.UID));
                ViewBag.UseLearnerDiscount = models.CheckLearnerDiscount(uid);
            }
            else
            {
                items = items.Where(u => false);
                ViewBag.UseLearnerDiscount = false;
            }
            ViewBag.ContractType = contractType;
            ViewBag.OwnerID = ownerID;
            return View("~/Views/CourseContract/Module/ContractMemberList.ascx", items);
        }

        public ActionResult SelectContractMember(CourseContractViewModel viewModel, int? referenceUID)
        {
            ViewBag.ViewModel = viewModel;
            ViewBag.ReferenceUID = referenceUID;
            return View("~/Views/CourseContract/Module/SelectContractMember.ascx");
        }


        public ActionResult InquireContractMember(String userName, int? referenceUID)
        {
            IEnumerable<UserProfile> items;
            userName = userName.GetEfficientString();
            if (userName == null)
            {
                this.ModelState.AddModelError("userName", "請輸學員名稱!!");
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }
            else
            {
                items = models.GetTable<UserProfile>()
                    .Where(l => l.RealName.Contains(userName) || l.Nickname.Contains(userName))
                    .Where(l => l.UserRole.Any(r=>r.RoleID==(int)Naming.RoleID.Learner 
                            || r.RoleID==(int)Naming.RoleID.Preliminary))
                    .OrderBy(l => l.RealName);
            }

            ViewBag.ReferenceUID = referenceUID;
            return View("~/Views/CourseContract/Module/MemberSelector.ascx", items);
        }

        public ActionResult EditContractMember(ContractMemberViewModel viewModel,int? referenceUID)
        {
            ViewBag.ViewModel = viewModel;

            UserProfile item;
            if (referenceUID.HasValue)
            {
                item = models.GetTable<UserProfile>().Where(u => u.UID == referenceUID).FirstOrDefault();
                if (item != null)
                {
                    viewModel.Address = item.Address;
                    viewModel.EmergencyContactPhone = item.UserProfileExtension.EmergencyContactPhone;
                    viewModel.EmergencyContactPerson = item.UserProfileExtension.EmergencyContactPerson;
                    viewModel.Relationship = item.UserProfileExtension.Relationship;
                    viewModel.AdministrativeArea = item.UserProfileExtension.AdministrativeArea;
                }
            }
            else
            {
                item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();

                if (item != null)
                {
                    viewModel.Gender = item.UserProfileExtension.Gender;
                    viewModel.EmergencyContactPhone = item.UserProfileExtension.EmergencyContactPhone;
                    viewModel.EmergencyContactPerson = item.UserProfileExtension.EmergencyContactPerson;
                    viewModel.Relationship = item.UserProfileExtension.Relationship;
                    viewModel.AdministrativeArea = item.UserProfileExtension.AdministrativeArea;
                    viewModel.IDNo = item.UserProfileExtension.IDNo;
                    viewModel.Phone = item.Phone;
                    viewModel.Birthday = item.Birthday;
                    viewModel.AthleticLevel = item.UserProfileExtension.AthleticLevel;
                    viewModel.RealName = item.RealName;
                    viewModel.Address = item.Address;
                    viewModel.Nickname = item.Nickname;

                }
            }

            return View("~/Views/CourseContract/Module/EditContractMember.ascx");
        }

        public ActionResult CommitContractMember(ContractMemberViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            viewModel.IDNo = viewModel.IDNo.GetEfficientString();
            if(viewModel.IDNo==null)
            {
                ModelState.AddModelError("IDNo", "請輸入身份證字號/護照號碼!!");
            }
            else if(viewModel.IDNo.Length== 10 && !viewModel.IDNo.CheckIDNo())
            {
                ModelState.AddModelError("IDNo", "身份證字號格式錯誤!!");
            }
            else if(viewModel.UID.HasValue)
            {
                if(models.GetTable<UserProfileExtension>().Any(u=>u.IDNo==viewModel.IDNo && u.UID!=viewModel.UID))
                {
                    ModelState.AddModelError("IDNo", "身份證字號/護照號碼已存在!!");
                }
            }
            else
            {
                if (models.GetTable<UserProfileExtension>().Any(u => u.IDNo == viewModel.IDNo))
                {
                    ModelState.AddModelError("IDNo", "身份證字號/護照號碼已存在!!");
                }
            }


            if (String.IsNullOrEmpty(viewModel.RealName))
            {
                ModelState.AddModelError("RealName", "請輸入學員姓名!!");
            }
            if (!viewModel.Birthday.HasValue)
            {
                ModelState.AddModelError("Birthday", "請輸入生日!!");
            }
            if (String.IsNullOrEmpty(viewModel.Phone))
            {
                ModelState.AddModelError("Phone", "請輸聯絡電話!!");
            }
            if (String.IsNullOrEmpty(viewModel.Gender))
            {
                ModelState.AddModelError("Gender", "請選擇性別!!");
            }
            if (!viewModel.AthleticLevel.HasValue)
            {
                ModelState.AddModelError("AthleticLevel", "請選擇是否為運動員!!");
            }
            if (String.IsNullOrEmpty(viewModel.AdministrativeArea))
            {
                ModelState.AddModelError("AdministrativeArea", "請選擇縣市!!");
            }
            if (String.IsNullOrEmpty(viewModel.Address))
            {
                ModelState.AddModelError("Address", "請輸入居住地址!!");
            }
            if (String.IsNullOrEmpty(viewModel.EmergencyContactPerson))
            {
                ModelState.AddModelError("EmergencyContactPerson", "請輸入緊急聯絡人!!");
            }
            if (String.IsNullOrEmpty(viewModel.Relationship))
            {
                ModelState.AddModelError("Relationship", "請選擇緊急聯絡人關係!!");
            }
            if (String.IsNullOrEmpty(viewModel.EmergencyContactPhone))
            {
                ModelState.AddModelError("EmergencyContactPhone", "請輸入緊急聯絡人電話!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();
            if (item == null)
            {
                item = models.CreateLearner(viewModel, Naming.RoleID.Preliminary);
                viewModel.UID = item.UID;
            }

            item.RealName = viewModel.RealName;
            item.Phone = viewModel.Phone;
            item.Address = viewModel.Address;
            item.Birthday = viewModel.Birthday;
            item.Nickname = viewModel.Nickname;
            item.UserProfileExtension.Gender = viewModel.Gender;
            item.UserProfileExtension.AthleticLevel = viewModel.AthleticLevel;

            item.UserProfileExtension.EmergencyContactPhone = viewModel.EmergencyContactPhone;
            item.UserProfileExtension.EmergencyContactPerson = viewModel.EmergencyContactPerson;
            item.UserProfileExtension.Relationship = viewModel.Relationship;
            item.UserProfileExtension.AdministrativeArea = viewModel.AdministrativeArea;
            item.UserProfileExtension.IDNo = viewModel.IDNo.ToUpper();

            models.SubmitChanges();

            return Json(new
            {
                result = true,
                UID = item.UID,
                RealName = item.RealName,
                OwnerID = viewModel.OwnerID == -1
                            ? item.UID
                            : viewModel.OwnerID.HasValue
                                ? viewModel.OwnerID
                                : (int?)null
            });
        }

        public ActionResult GetLessonPriceList(int branchID,int? duration)
        {
            var items = models.GetTable<LessonPriceType>().Where(l => l.BranchID == branchID);
            if (duration.HasValue)
                items = items.Where(l => !l.DurationInMinutes.HasValue || l.DurationInMinutes == duration);
            return Json(items.Select(l => new
            {
                l.PriceID,
                l.Description,
                l.LowerLimit,
                l.UpperBound,
                l.BranchID,
                l.ListPrice,
                l.DiscountedPrice,
                l.DurationInMinutes
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListLessonPrice(int branchID, int? duration, Naming.LessonPriceFeature? feature)
        {
            var items = models.GetTable<LessonPriceType>().Where(l => l.Status == (int)Naming.LessonSeriesStatus.已啟用)
                .Where(l => l.BranchID == branchID
                && (l.LowerLimit.HasValue && (!l.SeriesID.HasValue || l.CurrentPriceSeries.Status == (int)Naming.LessonSeriesStatus.已啟用)));
            if (duration.HasValue)
                items = items.Where(l => !l.DurationInMinutes.HasValue || l.DurationInMinutes == duration);
            if(!feature.HasValue)
            {
                items = items.Where(l => !l.LessonPriceProperty.Any());
            }
            else
            {
                items = items.Where(l => !l.LessonPriceProperty.Any() || l.LessonPriceProperty.Any(p => p.PropertyID == (int)feature));
            }

            var profile = HttpContext.GetUser();
            if(profile.IsAssistant() || profile.IsManager())
            {

            }
            else
            {
                items = items.Where(l => l.UsageType != 0);
            }

            items = items.OrderBy(l => l.LowerLimit).ThenBy(l => l.ListPrice);
            return View("~/Views/CourseContract/Module/LessonPriceList.ascx", items);
        }

        private Naming.CourseContractStatus checkInitialStatus(CourseContractViewModel viewModel,UserProfile profile)
        {
            if (profile.IsManager())
                return Naming.CourseContractStatus.待簽名;
            var item = models.GetTable<LessonPriceType>().Where(p => p.PriceID == viewModel.PriceID).First();
            if(item.LowerLimit.HasValue && viewModel.Lessons>=item.LowerLimit
                && (!item.UpperBound.HasValue || viewModel.Lessons<item.UpperBound)
                && String.IsNullOrEmpty(viewModel.Remark))
            {
                return Naming.CourseContractStatus.待簽名;
            }
            return Naming.CourseContractStatus.待審核;
        }

        public ActionResult CommitContract(CourseContractViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            LessonPriceType lessonPrice;
            validateContractApplication(viewModel, out lessonPrice);
            if (!viewModel.Lessons.HasValue || viewModel.Lessons < 1)
            {
                ModelState.AddModelError("Lessons", "請輸入購買堂數!!");
            }

            if(lessonPrice!=null && !lessonPrice.BranchStore.ManagerID.HasValue)
            {
                ModelState.AddModelError("BranchID", "該分店未指定店長!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item == null)
            {
                item = new CourseContract
                {
                    AgentID = lessonPrice.BranchStore.ManagerID.Value
                };
                models.GetTable<CourseContract>().InsertOnSubmit(item);
            }

            item.Status = (int)checkInitialStatus(viewModel, profile);
            item.CourseContractLevel.Add(new CourseContractLevel
            {
                LevelDate = DateTime.Now,
                ExecutorID = profile.UID,
                LevelID = item.Status
            });

            item.ContractType = viewModel.ContractType.Value;
            item.ContractDate = DateTime.Now;
            item.Subject = viewModel.Subject;
            item.ValidFrom = DateTime.Today;
            item.Expiration = DateTime.Today.AddDays(viewModel.Lessons.Value * 7 + 1);
            item.OwnerID = viewModel.OwnerID.Value;
            item.SequenceNo = viewModel.SequenceNo;
            item.Lessons = viewModel.Lessons;
            item.PriceID = viewModel.PriceID.Value;
            item.Remark = viewModel.Remark;
            item.FitnessConsultant = viewModel.FitnessConsultant.Value;
            //item.Status = viewModel.Status;
            if (viewModel.UID != null && viewModel.UID.Length > 0)
            {
                models.DeleteAllOnSubmit<CourseContractMember>(m => m.ContractID == item.ContractID);
                item.CourseContractMember.AddRange(viewModel.UID.Select(u => new CourseContractMember
                {
                    UID = u
                }));
            }
            models.SubmitChanges();

            item.TotalCost = item.Lessons * item.LessonPriceType.ListPrice;
            if(item.CourseContractType.GroupingLessonDiscount!=null)
            {
                item.TotalCost = item.TotalCost * item.CourseContractType.GroupingLessonDiscount.GroupingMemberCount * item.CourseContractType.GroupingLessonDiscount.PercentageOfDiscount / 100;
            }
            models.SubmitChanges();

            foreach(var uid in viewModel.UID)
            {
                models.ExecuteCommand("update UserProfileExtension set CurrentTrial = null where UID = {0}", uid);
            }

            return Json(new { result = true, status = item.Status });
        }

        private void markContractNo(CourseContract item)
        {
            if (item.ContractNo == null)
            {
                var items = models.GetTable<CourseContract>().Where(c => c.ContractDate >= DateTime.Today
                        && c.Status >= (int)Naming.CourseContractStatus.待生效);
                int seqNo = 1 + items.Count();
                item.ContractNo = item.CourseContractType.ContractCode + String.Format("{0:yyyyMMdd}", item.ContractDate) + String.Format("{0:0000}", seqNo);
                models.SubmitChanges();
            }
        }

        public ActionResult SaveContract(CourseContractViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            LessonPriceType lessonPrice;
            validateContractApplication(viewModel,out lessonPrice);

            if (lessonPrice != null && !lessonPrice.BranchStore.ManagerID.HasValue)
            {
                ModelState.AddModelError("BranchID", "該分店未指定店長!!");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item == null)
            {
                item = new CourseContract
                {
                    Status = (int)Naming.CourseContractStatus.草稿,
                    AgentID = lessonPrice.BranchStore.ManagerID.Value
                };
                item.CourseContractLevel.Add(new CourseContractLevel
                {
                    LevelDate = DateTime.Now,
                    ExecutorID = profile.UID,
                    LevelID = (int)Naming.CourseContractStatus.草稿
                });
                models.GetTable<CourseContract>().InsertOnSubmit(item);
            }

            item.ContractType = viewModel.ContractType.Value;
            item.ContractDate = DateTime.Now;
            item.Subject = viewModel.Subject;
            item.ValidFrom = DateTime.Today;
            //item.Expiration = viewModel.Expiration;
            item.OwnerID = viewModel.OwnerID.Value;
            item.SequenceNo = viewModel.SequenceNo;
            item.Lessons = viewModel.Lessons;
            item.PriceID = viewModel.PriceID.Value;
            item.Remark = viewModel.Remark;
            item.FitnessConsultant = viewModel.FitnessConsultant.Value;
            //item.Status = viewModel.Status;
            if (viewModel.UID != null && viewModel.UID.Length > 0)
            {
                models.DeleteAllOnSubmit<CourseContractMember>(m => m.ContractID == item.ContractID);
                item.CourseContractMember.AddRange(viewModel.UID.Select(u => new CourseContractMember
                {
                    UID = u
                }));
            }
            models.SubmitChanges();

            item.TotalCost = item.Lessons * item.LessonPriceType.ListPrice;
            if (item.CourseContractType.GroupingLessonDiscount != null)
            {
                item.TotalCost = item.TotalCost * item.CourseContractType.GroupingLessonDiscount.GroupingMemberCount * item.CourseContractType.GroupingLessonDiscount.PercentageOfDiscount / 100;
            }
            models.SubmitChanges();

            return Json(new { result = true });
        }

        private void validateContractApplication(CourseContractViewModel viewModel,out LessonPriceType lessonPrice)
        {
            lessonPrice = null;
            if (!viewModel.ContractType.HasValue)
            {
                ModelState.AddModelError("ContractType", "請選擇合約名稱!!");
            }
            if (!viewModel.PriceID.HasValue)
            {
                ModelState.AddModelError("PriceID", "請選擇課程單價!!");
            }
            else
            {
                lessonPrice = models.GetTable<LessonPriceType>().Where(l => l.PriceID == viewModel.PriceID).FirstOrDefault();
            }
            if (!viewModel.FitnessConsultant.HasValue)
            {
                ModelState.AddModelError("FitnessConsultant", "請選擇體能顧問!!");
            }

            if (!viewModel.OwnerID.HasValue)
            {
                ModelState.AddModelError("OwnerID", "請設定主簽約人!!");
            }
            else if (viewModel.UID == null || viewModel.UID.Length < 1)
            {
                ModelState.AddModelError("OwnerID", "請設定學員!!");
            }
            else if ((viewModel.ContractType == 1 && viewModel.UID.Length != 1)
                || (viewModel.ContractType == 3 && viewModel.UID.Length != 2))
            {
                ModelState.AddModelError("OwnerID", "學員數與合約不符!!");
            }
        }

        public ActionResult CourseContractSummary()
        {
            var item = HttpContext.GetUser().LoadInstance(models);
            //IQueryable<LessonTime> items = models.GetTable<LessonTime>();
            //if (viewModel.CoachID.HasValue)
            //    items = items.Where(t => t.AttendingCoach == viewModel.CoachID);
            //if (viewModel.QueryStart.HasValue)
            //    items = items.Where(t => t.ClassTime >= viewModel.QueryStart && t.ClassTime < viewModel.QueryStart.Value.AddMonths(1));

            return View("~/Views/CourseContract/Module/CourseContractSummary.ascx",item);
        }

        public ActionResult ContractTodoList(CourseContractViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            IQueryable<CourseContract> items;
            UserProfile agent = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.AgentID).First();
            switch ((Naming.CourseContractStatus)viewModel.Status)
            {
                case Naming.CourseContractStatus.草稿:
                    items = models.GetContractInEditingByAgent(agent);
                    break;
                case Naming.CourseContractStatus.待審核:
                    items = models.GetContractToAllowByAgent(agent);
                    break;
                case Naming.CourseContractStatus.待生效:
                    items = models.GetContractToConfirmByAgent(agent);
                    break;
                case Naming.CourseContractStatus.待簽名:
                    items = models.GetContractToSignByAgent(agent);
                    break;
                default:
                    items = models.GetTable<CourseContract>().Where(c => false);
                    break;
            }

            return View("~/Views/CourseContract/Module/ContractTodoList.ascx", items);
        }

        public ActionResult ContractAmendmentTodoList(CourseContractViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            IQueryable<CourseContractRevision> items;
            UserProfile agent = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.AgentID).First();
            switch ((Naming.CourseContractStatus)viewModel.Status)
            {
                //case Naming.CourseContractStatus.草稿:
                //    items = models.GetContractInEditingByAgent(agent);
                //    break;
                case Naming.CourseContractStatus.待審核:
                    items = models.GetAmendmentToAllowByAgent(agent);
                    break;
                case Naming.CourseContractStatus.待生效:
                    items = models.GetAmendmentToConfirmByAgent(agent);
                    break;
                case Naming.CourseContractStatus.待簽名:
                    items = models.GetAmendmentToSignByAgent(agent);
                    break;
                default:
                    items = models.GetTable<CourseContractRevision>().Where(c => false);
                    break;
            }

            return View("~/Views/CourseContract/Module/ContractAmendmentTodoList.ascx", items);
        }

        [AllowAnonymous]
        public ActionResult ViewContract(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
                return View("CourseContractView", item);
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        [AllowAnonymous]
        public ActionResult ViewContractAmendment(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContractRevision>().Where(c => c.RevisionID == viewModel.RevisionID).FirstOrDefault();
            if (item != null)
                return View("CourseContractAmendmentView", item);
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }


        public ActionResult ContractSignatureView(CourseContractViewModel viewModel)
        {
            ViewResult result = (ViewResult)ViewContract(viewModel);
            if(result.Model is CourseContract)
            {
                ViewBag.ContractAction = "~/Views/CourseContract/Module/ConfirmSignature.ascx";
            }
            return result;
        }

        public ActionResult ContractAmendmentSignatureView(CourseContractViewModel viewModel)
        {
            ViewResult result = (ViewResult)ViewContractAmendment(viewModel);
            if (result.Model is CourseContractRevision)
            {
                ViewBag.ContractAction = "~/Views/CourseContract/Module/ConfirmAmendmentSignature.ascx";
            }
            return result;
        }


        public ActionResult SignContract(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
                return View("~/Views/CourseContract/Module/SignCourseContract.ascx", item);
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        public ActionResult ApproveContract(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
                return View("~/Views/CourseContract/Module/ApproveCourseContract.ascx", item);
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        public ActionResult ContractApprovalView(CourseContractViewModel viewModel)
        {
            ViewResult result = (ViewResult)ViewContract(viewModel);
            if (result.Model is CourseContract)
            {
                ViewBag.ContractAction = "~/Views/CourseContract/Module/ContractApproval.ascx";
            }
            return result;
        }

        public ActionResult ContractAmendmentApprovalView(CourseContractViewModel viewModel)
        {
            ViewResult result = (ViewResult)ViewContractAmendment(viewModel);
            if (result.Model is CourseContractRevision)
            {
                ViewBag.ContractAction = "~/Views/CourseContract/Module/ContractAmendmentApproval.ascx";
            }
            return result;
        }

        public ActionResult EnableContract(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
                return View("~/Views/CourseContract/Module/EnableCourseContract.ascx", item);
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }


        public ActionResult ExecuteContractStatus(CourseContractViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                executeContractStatus(profile, item,(Naming.CourseContractStatus)viewModel.Status.Value);
                models.SubmitChanges();

                return Json(new { result = true });
            }
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        private void executeContractStatus(UserProfile profile, CourseContract item,Naming.CourseContractStatus status)
        {
            item.CourseContractLevel.Add(new CourseContractLevel
            {
                ExecutorID = profile.UID,
                LevelDate = DateTime.Now,
                LevelID = (int)status
            });
            item.Status = (int)status;
        }

        public ActionResult EnableContractStatus(CourseContractViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                executeContractStatus(profile, item, (Naming.CourseContractStatus)viewModel.Status.Value);

                var groupLesson = new GroupingLesson { };
                models.GetTable<GroupingLesson>().InsertOnSubmit(groupLesson);

                foreach(var m in item.CourseContractMember)
                {
                    var lesson = new RegisterLesson
                    {
                        ClassLevel = item.PriceID,
                        RegisterDate = DateTime.Now,
                        Lessons = item.Lessons.Value,
                        Attended = (int)Naming.LessonStatus.準備上課,
                        GroupingMemberCount = item.CourseContractMember.Count,
                        IntuitionCharge = new IntuitionCharge
                        {
                            Payment = "CreditCard",
                            FeeShared = 0,
                            ByInstallments = 1
                        },
                        AdvisorID = item.FitnessConsultant,
                        AttendedLessons = 0,
                        UID = m.UID,
                        RegisterLessonContract = new RegisterLessonContract
                        {
                            ContractID = item.ContractID
                        }
                    };

                    groupLesson.RegisterLesson.Add(lesson);
                }
                
                models.SubmitChanges();

                foreach (var m in item.CourseContractMember)
                {
                    models.ExecuteCommand(@"
                        UPDATE UserRole
                        SET        RoleID = {2}
                        WHERE   (UID = {0}) AND (RoleID = {1})", m.UID, (int)Naming.RoleID.Preliminary, (int)Naming.RoleID.Learner);
                }

                return Json(new { result = true });
            }
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        public ActionResult ConfirmSignature(CourseContractViewModel viewModel, bool? extension, bool? booking, bool? cancel)
        {
            var profile = HttpContext.GetUser();
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                var owner = item.CourseContractMember.Where(m => m.UID == item.OwnerID).First();

                if (item.CourseContractType.ContractCode == "CFA")
                {
                    if (owner.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Signature") && s.Signature != null) < 3)
                    {
                        return View("~/Views/Shared/JsAlert.ascx", model: "未完成簽名!!");
                    }
                }
                else
                {

                    if (owner.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Signature") && s.Signature != null) < 2)
                    {
                        return View("~/Views/Shared/JsAlert.ascx", model: "未完成簽名!!");
                    }

                    if (item.CourseContractType.IsGroup == true)
                    {
                        if (item.CourseContractMember.Any(m => m.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Signature") && s.Signature != null) < 2))
                        {
                            return View("~/Views/Shared/JsAlert.ascx", model: "未完成簽名!!");
                        }

                        foreach (var m in item.CourseContractMember)
                        {
                            if (m.UserProfile.CurrentYearsOld() < 18 && owner.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Guardian") && s.Signature != null) < 2)
                            {
                                return View("~/Views/Shared/JsAlert.ascx", model: "家長/監護人未完成簽名!!");
                            }
                        }
                    }
                }

                if (extension != true || booking != true || cancel != true)
                {
                    return View("~/Views/Shared/JsAlert.ascx", model: "請勾選合約聲明!!");
                }


                item.CourseContractLevel.Add(new CourseContractLevel
                {
                    ExecutorID = profile.UID,
                    LevelDate = DateTime.Now,
                    LevelID = (int)Naming.CourseContractStatus.待生效
                });

                item.Status = (int)Naming.CourseContractStatus.待生效;
                models.SubmitChanges();

                markContractNo(item);

                var pdfFile = item.CreateContractPDF();
                
                //ThreadPool.QueueUserWorkItem(t =>
                //{
                //    try
                //    {
                //        item.CreateContractPDF();
                //    }
                //    catch (Exception ex)
                //    {
                //        Logger.Error(ex);
                //    }
                //});

                String contractPDF = pdfFile.Replace(HttpRuntime.AppDomainAppPath, "");
                return Json(new { result = true, pdf = VirtualPathUtility.ToAbsolute("~/" + contractPDF) });
            }
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        public ActionResult ConfirmSignatureForAmendment(CourseContractViewModel viewModel, bool? extension)
        {
            var profile = HttpContext.GetUser();
            var item = models.GetTable<CourseContractRevision>().Where(c => c.RevisionID == viewModel.RevisionID).FirstOrDefault();
            if (item != null)
            {
                CourseContract contract = item.CourseContract;
                var owner = contract.CourseContractMember.Where(m => m.UID == contract.OwnerID).First();

                if (contract.CourseContractType.ContractCode == "CFA")
                {
                    if (owner.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Signature") && s.Signature != null) < 1)
                    {
                        return View("~/Views/Shared/JsAlert.ascx", model: "未完成簽名!!");
                    }
                }
                else
                {

                    if (owner.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Signature") && s.Signature != null) < 1)
                    {
                        return View("~/Views/Shared/JsAlert.ascx", model: "未完成簽名!!");
                    }

                    if (contract.CourseContractType.IsGroup == true)
                    {
                        if (contract.CourseContractMember.Any(m => m.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Signature") && s.Signature != null) < 1))
                        {
                            return View("~/Views/Shared/JsAlert.ascx", model: "未完成簽名!!");
                        }

                        foreach (var m in contract.CourseContractMember)
                        {
                            if (m.UserProfile.CurrentYearsOld() < 18 && owner.CourseContractSignature.Count(s => s.SignatureName.StartsWith("Guardian") && s.Signature != null) < 1)
                            {
                                return View("~/Views/Shared/JsAlert.ascx", model: "家長/監護人未完成簽名!!");
                            }
                        }
                    }
                }

                if (extension != true )
                {
                    return View("~/Views/Shared/JsAlert.ascx", model: "請勾選合約聲明!!");
                }


                contract.CourseContractLevel.Add(new CourseContractLevel
                {
                    ExecutorID = profile.UID,
                    LevelDate = DateTime.Now,
                    LevelID = (int)Naming.CourseContractStatus.待生效
                });

                contract.Status = (int)Naming.CourseContractStatus.待生效;
                models.SubmitChanges();

                var pdfFile = item.CreateContractAmendmentPDF(true);

                String contractPDF = pdfFile.Replace(HttpRuntime.AppDomainAppPath, "");
                return Json(new { result = true, pdf = VirtualPathUtility.ToAbsolute("~/" + contractPDF) });
            }
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }

        public ActionResult EnableContractAmendment(CourseContractViewModel viewModel)
        {
            var profile = HttpContext.GetUser();
            var item = models.GetTable<CourseContractRevision>().Where(c => c.RevisionID == viewModel.RevisionID).FirstOrDefault();
            if (item != null)
            {
                executeContractStatus(profile, item.CourseContract, (Naming.CourseContractStatus)viewModel.Status.Value);

                switch(item.Reason)
                {
                    case "展延":
                        break;
                    case "轉點":
                        break;
                    case "轉讓":
                        break;
                    case "終止":
                        break;
                }

                models.SubmitChanges();

                foreach (var m in item.CourseContract.CourseContractMember)
                {
                    models.ExecuteCommand(@"
                        UPDATE UserRole
                        SET        RoleID = {2}
                        WHERE   (UID = {0}) AND (RoleID = {1})", m.UID, (int)Naming.RoleID.Preliminary, (int)Naming.RoleID.Learner);
                }

                return Json(new { result = true });
            }
            else
                return View("~/Views/Shared/JsAlert.ascx", model: "合約資料錯誤!!");
        }


        public ActionResult SignaturePanel(CourseContractSignatureViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            return View("~/Views/CourseContract/Module/SignaturePanel.ascx");
        }

        public ActionResult CommitSignature(CourseContractSignatureViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            var item = models.GetTable<CourseContractSignature>().Where(s => s.ContractID == viewModel.ContractID
                && s.UID == viewModel.UID && s.SignatureName == viewModel.SignatureName).FirstOrDefault();

            if (item == null)
            {
                item = new CourseContractSignature
                {
                    ContractID = viewModel.ContractID.Value,
                    UID = viewModel.UID.Value,
                    SignatureName = viewModel.SignatureName
                };
                models.GetTable<CourseContractSignature>().InsertOnSubmit(item);
            }

            item.Signature = viewModel.Signature;
            models.SubmitChanges();

            return Json(new { result = true });
        }

        public ActionResult CreateContractPdf(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                item.CreateContractPDF(true);
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { result = false, message = "合約資料錯誤!!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CalcTotalCost(CourseContractViewModel viewModel)
        {
            var price = models.GetTable<LessonPriceType>().Where(p => p.PriceID == viewModel.PriceID).FirstOrDefault();
            if (price == null)
            {
                return Json(new { result = false,message = "請選擇課程單價" }, JsonRequestBehavior.AllowGet);
            }

            var typeItem = models.GetTable<CourseContractType>().Where(c => c.TypeID == viewModel.ContractType).FirstOrDefault();
            if (typeItem == null)
            {
                return Json(new { result = false, message = "請選擇合約名稱" }, JsonRequestBehavior.AllowGet);
            }

            if (viewModel.Lessons.HasValue)
            {
                GroupingLessonDiscount discount = typeItem.GroupingLessonDiscount;

                var totalCost = viewModel.Lessons * price.ListPrice;
                if(discount!=null)
                {
                    totalCost = totalCost * discount.GroupingMemberCount * discount.PercentageOfDiscount / 100;
                }

                return Json(new { result = true, totalCost = String.Format("{0:##,###,###,###}",totalCost) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = false, message = "" }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetContractPdf(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                String pdfFile = item.CreateContractPDF();
                return File(pdfFile, "application/pdf");
            }
            else
                return Json(new { result = false, message = "合約資料錯誤!!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContractAmendmentPdf(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContractRevision>().Where(c => c.RevisionID == viewModel.RevisionID).FirstOrDefault();
            if (item != null)
            {
                String pdfFile = item.CreateContractAmendmentPDF();
                return File(pdfFile, "application/pdf");
            }
            else
                return Json(new { result = false, message = "合約資料錯誤!!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InquireContract(CourseContractQueryViewModel viewModel)
        {
            IQueryable<CourseContract> items = models.GetTable<CourseContract>();

            var profile = HttpContext.GetUser();
            if (profile.IsManager() || profile.IsViceManager())
            {
                var coaches = profile.GetServingCoachInSameStore(models);
                items = items.Join(coaches, c => c.FitnessConsultant, h => h.CoachID, (c, h) => c);
            }

            Expression<Func<CourseContract, bool>> queryExpr = c => false;
            bool hasConditon = false;

            if (viewModel.BranchID.HasValue)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.LessonPriceType.BranchID == viewModel.BranchID);
            }

            if (viewModel.FitnessConsultant.HasValue)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.FitnessConsultant == viewModel.FitnessConsultant);
            }

            viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
            if (viewModel.ContractNo != null)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.ContractNo.StartsWith(viewModel.ContractNo));
            }

            viewModel.RealName = viewModel.RealName.GetEfficientString();
            if (viewModel.RealName != null)
            {
                hasConditon = true;
                queryExpr = queryExpr.Or(c => c.CourseContractMember.Any(m => m.UserProfile.RealName.Contains(viewModel.RealName) || m.UserProfile.Nickname.Contains(viewModel.RealName)));
            }

            if(hasConditon)
            {
                items = items.Where(queryExpr);
            }

            if (viewModel.Status.HasValue)
                items = items.Where(c => c.Status == viewModel.Status);

            if(viewModel.ContractType.HasValue)
                items = items.Where(c => c.ContractType == viewModel.ContractType);

            if (viewModel.ContractDateFrom.HasValue)
                items = items.Where(c => c.ContractDate >= viewModel.ContractDateFrom);

            if (viewModel.ContractDateTo.HasValue)
                items = items.Where(c => c.ContractDate < viewModel.ContractDateTo.Value.AddDays(1));

            return View("~/Views/CourseContract/Module/ContractQueryList.ascx", items);
        }

        public ActionResult InquireContractForAmendment(CourseContractQueryViewModel viewModel)
        {
            IQueryable<CourseContract> items = models.GetTable<CourseContract>()
                    .Where(c => c.Status == (int)Naming.CourseContractStatus.已開立);

            bool hasCondition = false;

            viewModel.RealName = viewModel.RealName.GetEfficientString();
            if (viewModel.RealName != null)
            {
                items = items.Where(c => c.CourseContractMember.Any(m => m.UserProfile.RealName.Contains(viewModel.RealName) || m.UserProfile.Nickname.Contains(viewModel.RealName)));
                hasCondition = true;
            }

            viewModel.ContractNo = viewModel.ContractNo.GetEfficientString();
            if (viewModel.ContractNo != null)
            {
                items = items.Where(c => c.ContractNo.StartsWith(viewModel.ContractNo));
                hasCondition = true;
            }

            if(!hasCondition)
            {
                return View("~/Views/Shared/JsAlert.ascx",model:"請輸入查詢條件!!");
            }

            return View("~/Views/CourseContract/Module/ContractQueryAmendmentList.ascx", items);

        }

        public ActionResult ListAmendment(CourseContractViewModel viewModel)
        {
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                return View("~/Views/CourseContract/Module/ContractAmendment.ascx", item);
            }
            else
                return Json(new { result = false, message = "合約資料錯誤!!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AmendContract(CourseContractViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item != null)
            {
                return View("~/Views/CourseContract/Module/ApplyAmendment.ascx", item);
            }
            else
                return Json(new { result = false, message = "合約資料錯誤!!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CommitAmendment(CourseContractViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;
            var profile = HttpContext.GetUser();

            var item = models.GetTable<CourseContract>().Where(c => c.ContractID == viewModel.ContractID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "合約資料錯誤!!" }, JsonRequestBehavior.AllowGet);
            }

            CourseContract newItem;

            if(String.IsNullOrEmpty(viewModel.Reason))
            {
                ModelState.AddModelError("Reason", "請選擇申請項目!!");
            }
            else
            {
                newItem = new CourseContract
                {
                    AgentID = item.LessonPriceType.BranchStore.ManagerID.Value
                };
                models.GetTable<CourseContract>().InsertOnSubmit(newItem);

                newItem.ContractType = item.ContractType;
                newItem.ContractDate = DateTime.Now;
                newItem.Subject = item.Subject;
                newItem.ValidFrom = item.ValidFrom;
                newItem.Expiration = item.Expiration;
                newItem.OwnerID = item.OwnerID;
                newItem.SequenceNo = item.SequenceNo;
                newItem.Lessons = item.Lessons;
                newItem.PriceID = item.PriceID;
                newItem.Remark = viewModel.Remark;
                newItem.FitnessConsultant = profile.IsCoach() ? profile.UID : item.FitnessConsultant;
                newItem.TotalCost = item.TotalCost;
                newItem.CourseContractRevision = new CourseContractRevision
                {
                    OriginalContract = item.ContractID,
                    Reason = viewModel.Reason,
                    RevisionNo = item.RevisionList.Count + 1
                };
                newItem.ContractNo = item.ContractNo + "-" + String.Format("{0:00}", newItem.CourseContractRevision.RevisionNo);

                newItem.Status = (int)Naming.CourseContractStatus.待審核;
                item.CourseContractLevel.Add(new CourseContractLevel
                {
                    LevelDate = DateTime.Now,
                    ExecutorID = profile.UID,
                    LevelID = newItem.Status
                });

                newItem.CourseContractMember.AddRange(item.CourseContractMember.Select(u => new CourseContractMember
                {
                    UID = u.UID
                }));

                switch (viewModel.Reason)
                {
                    case "展延":
                        if(viewModel.MonthExtension.HasValue)
                        {
                            newItem.Expiration = newItem.Expiration.Value.AddMonths(viewModel.MonthExtension.Value);
                        }
                        else
                        {
                            ModelState.AddModelError("MonthExtension", "請選擇展延期間!!");
                        }
                        break;
                    case "轉點":
                        if (viewModel.PriceID.HasValue)
                        {
                            newItem.PriceID = viewModel.PriceID.Value;
                        }
                        else
                        {
                            ModelState.AddModelError("PriceID", "請選擇課程單價!!");
                        }
                        break;
                    case "轉讓":
                        if (viewModel.UID!=null && viewModel.UID.Length>0 )
                        {
                            newItem.OwnerID = viewModel.UID[0];
                            newItem.CourseContractMember.Clear();
                            newItem.CourseContractMember.Add(new CourseContractMember
                            {
                                UID = viewModel.UID[0]
                            });
                        }
                        else
                        {
                            ModelState.AddModelError("UID", "請選擇上課學員!!");
                        }
                        break;
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            models.SubmitChanges();

            return Json(new { result = true, status = item.Status });
        }


    }
}