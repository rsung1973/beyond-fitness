﻿using System;
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

namespace WebHome.Controllers
{
    [Authorize]
    public class LessonsController : SampleController<UserProfile>
    {
        public LessonsController() : base() { }
        // GET: Lessons
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookingSelfTraining(int? lessonID)
        {
            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();
            LessonTimeViewModel viewModel = new LessonTimeViewModel
            {
                BranchID = 1
            };
            if(item!=null)
            {
                viewModel.LessonID = item.LessonID;
                viewModel.ClassDate = item.ClassTime.Value;
                viewModel.Duration = item.DurationInMinutes.Value;
                viewModel.BranchID = item.BranchID.Value;
            }
            return View("~/Views/Lessons/LessonTime/Module/BookingSelfTraining.ascx", viewModel);
        }

        public ActionResult CommitBookingSelfTraining(LessonTimeViewModel viewModel)
        {

            var profile = HttpContext.GetUser();

            LessonTime timeItem = models.GetTable<LessonTime>().Where(l => l.LessonID == viewModel.LessonID).FirstOrDefault();

            ViewBag.ViewModel = viewModel;

            if (!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            DateTime endTime = viewModel.ClassDate.AddMinutes(viewModel.Duration);
            IQueryable<LessonTimeExpansion> items = models.GetTable<LessonTimeExpansion>();
            if (viewModel.LessonID.HasValue)
                items = items.Where(t => t.LessonID != viewModel.LessonID);
            if (items.Any(t => t.RegisterLesson.UID == profile.UID
                && t.ClassDate == viewModel.ClassDate.Date && t.Hour >= viewModel.ClassDate.Hour && t.Hour < endTime.Hour))
            {
                ViewBag.Message = "上課時間重複!!";
                return View("~/Views/Shared/AlertMessage.ascx");
            }

            if (timeItem == null)
            {
                var priceType = models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.內部訓練).FirstOrDefault();
                if (priceType == null)
                {
                    ViewBag.Message = "內部訓練課程類別未設定!!";
                    return View("~/Views/Shared/AlertMessage.ascx");
                }

                RegisterLesson lesson = new RegisterLesson
                {
                    RegisterDate = DateTime.Now,
                    GroupingMemberCount = 1,
                    ClassLevel = priceType.PriceID,
                    Lessons = 1,
                    UID = profile.UID,
                    AdvisorID = profile.UID,
                    GroupingLesson = new GroupingLesson { }
                };

                timeItem = new LessonTime
                {
                    RegisterLesson = lesson,
                    LessonPlan = new LessonPlan
                    {

                    },
                    GroupingLesson = lesson.GroupingLesson
                };

                timeItem.LessonFitnessAssessment.Add(new LessonFitnessAssessment
                {
                    UID = lesson.UID
                });

                models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            }

            timeItem.ClassTime = viewModel.ClassDate;
            timeItem.DurationInMinutes = viewModel.Duration;
            timeItem.BranchID = viewModel.BranchID;
            timeItem.InvitedCoach =  timeItem.AttendingCoach = profile.UID;

            models.DeleteAllOnSubmit<LessonTimeExpansion>(t => t.LessonID == viewModel.LessonID);

            models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();

            for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
            {
                timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                {
                    ClassDate = timeItem.ClassTime.Value.Date,
                    LessonID = timeItem.LessonID,
                    Hour = timeItem.ClassTime.Value.Hour + i,
                    RegisterID = timeItem.RegisterID
                });
            }

            models.SubmitChanges();
            return Json(new { result = true, message = "上課時間預約完成!!" });

        }

        [HttpGet]
        public ActionResult BookingByCoach(int? coachID)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = new LessonTimeViewModel
            {
                ClassDate = DateTime.Today.AddHours(8)
            };
            if (coachID.HasValue)
                ViewBag.DefaultCoach = coachID;
            return View(item);

        }

        public ActionResult BookingTrialLesson(int? coachID)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = new LessonTimeViewModel
            {
                ClassDate = DateTime.Today.AddHours(8)
            };
            if (coachID.HasValue)
                ViewBag.DefaultCoach = coachID;
            return View(item);
        }

        public ActionResult RebookingByCoach(int id, LessonTimeViewModel viewModel)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == id).FirstOrDefault();
            if (item == null)
            {
                return this.TransferToAction("Coach", "Account", new { Message = "修改上課時間資料不存在!!" });
            }

            if (Request.HttpMethod == "POST")
            {
                ViewBag.ViewModel = viewModel;
                if(!ModelState.IsValid)
                {
                    ViewBag.ModelState = ModelState;
                    return View(item);
                }

                LessonTime timeItem = new LessonTime
                {
                    InvitedCoach = viewModel.CoachID,
                    AttendingCoach = viewModel.CoachID,
                    ClassTime = viewModel.ClassDate,
                    DurationInMinutes = viewModel.Duration
                };

                var users = checkOverlapedBooking(timeItem, item.RegisterLesson, item);
                if (users.Count() > 0)
                {
                    ViewBag.Message = "學員(" + String.Join("、", users.Select(u => u.RealName)) + ")上課時間重複!!";
                    return View(item);
                }

                models.DeleteAll<LessonTimeExpansion>(t => t.LessonID == id);

                item.InvitedCoach = viewModel.CoachID;
                item.AttendingCoach = viewModel.CoachID;
                item.ClassTime = viewModel.ClassDate;
                item.DurationInMinutes = viewModel.Duration;
                item.BranchID = viewModel.BranchID;
                //item.TrainingBySelf = viewModel.TrainingBySelf;

                models.SubmitChanges();

                var timeExpansion = models.GetTable<LessonTimeExpansion>();
                if (item.RegisterLesson.GroupingMemberCount > 1)
                {
                    for (int i = 0; i <= (item.DurationInMinutes + item.ClassTime.Value.Minute - 1) / 60; i++)
                    {
                        foreach (var regles in item.RegisterLesson.GroupingLesson.RegisterLesson)
                        {
                            timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                            {
                                ClassDate = item.ClassTime.Value.Date,
                                LessonID = item.LessonID,
                                Hour = item.ClassTime.Value.Hour + i,
                                RegisterID = regles.RegisterID
                            });
                        }
                    }
                }
                else
                {
                    for (int i = 0; i <= (item.DurationInMinutes + item.ClassTime.Value.Minute - 1) / 60; i++)
                    {
                        timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                        {
                            ClassDate = item.ClassTime.Value.Date,
                            LessonID = item.LessonID,
                            Hour = item.ClassTime.Value.Hour + i,
                            RegisterID = item.RegisterID
                        });
                    }
                }

                models.SubmitChanges();

                if (item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自主訓練)
                {
                    models.ExecuteCommand("update TuitionInstallment set PayoffDate = {0} where RegisterID = {1} ", item.ClassTime, item.RegisterID);
                }

                return this.TransferToAction("Coach", "Account", new { Message = "上課時間修改完成!!", LessonDate = item.ClassTime });
                                
            }


            ViewBag.ViewModel = new LessonTimeViewModel
            {
                ClassDate = item.ClassTime.Value,
                CoachID = item.AttendingCoach.Value,
                Duration = item.DurationInMinutes.Value,
                TrainingBySelf = item.TrainingBySelf,
                BranchID = item.BranchID.Value
            };

            return View(item);

        }

        public ActionResult FreeAgentClockIn(int? coachID)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new { result = false, message = "連線已中斷，請重新登入!!" });
            }

            if (item.IsFreeAgent() && item.UID != coachID)
            {
                return Json(new { result = false, message = "自由教練資料錯誤!!" });
            }

            var items = models.GetTable<LessonTime>()
                .Where(l => l.ClassTime >= DateTime.Today && l.ClassTime < DateTime.Today.AddDays(1))
                .Where(l => l.AttendingCoach == coachID)
                .Where(l => l.LessonAttendance == null);

            if (items.Count() <= 0)
            {
                return Json(new { result = false, message = "本日無自由教練課程!!" });
            }

            foreach (var lesson in items)
            {
                lesson.LessonAttendance = new LessonAttendance
                {
                    CompleteDate = DateTime.Now
                };
            }
            models.SubmitChanges();

            return Json(new { result = true });

        }

        [HttpGet]
        public ActionResult BookingByFreeAgent(int? coachID)
        {
            return BookingByCoach(coachID);
        }


        [HttpPost]
        public ActionResult BookingByCoach(LessonTimeViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;

            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(item);
            }

            var coach = models.GetTable<ServingCoach>().Where(s => s.CoachID == viewModel.CoachID).FirstOrDefault();
            if (coach == null)
            {
                ViewBag.Message = "未指定體能顧問!!";
                return View(item);
            }

            if(coach.UserProfile.IsFreeAgent())
            {
                var result = bookingByFreeAgent(viewModel, coach);
                if(result!=null)
                {
                    return result;
                } 
                else
                {
                    return this.TransferToAction("Coach", "Account", new { lessonDate = viewModel.ClassDate, message = "上課時間預約完成!!" });
                }
            }


            RegisterLesson lesson;
            if (viewModel.TrainingBySelf == 1)
            {
                if (!viewModel.UID.HasValue)
                {
                    this.ModelState.AddModelError("queryAttendee", "請選擇上課學員!!");
                    ViewBag.ModelState = this.ModelState;
                    return View(item);
                }

                var priceType = models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.自主訓練).FirstOrDefault();

                lesson = new RegisterLesson
                {
                    UID = viewModel.UID.Value,
                    RegisterDate = DateTime.Now,
                    GroupingMemberCount = 1,
                    Lessons = 1,
                    ClassLevel = priceType != null ? priceType.PriceID : (int?)null,
                    IntuitionCharge = new IntuitionCharge
                    {
                        ByInstallments = 1,
                        Payment = "Cash",
                        FeeShared = 0
                    },
                    AdvisorID = viewModel.CoachID,
                    GroupingLesson = new GroupingLesson { }
                };

                //var installment = new TuitionInstallment
                //{
                //    PayoffDate = viewModel.ClassDate,
                //    PayoffAmount = priceType.ListPrice
                //};
                //installment.TuitionAchievement.Add(new TuitionAchievement
                //{
                //    CoachID = lesson.AdvisorID.Value,
                //    ShareAmount = installment.PayoffAmount
                //});

                //lesson.IntuitionCharge.TuitionInstallment.Add(installment);
                models.GetTable<RegisterLesson>().InsertOnSubmit(lesson);
                models.SubmitChanges();
            }
            else
            {
                lesson = models.GetTable<RegisterLesson>().Where(r => r.RegisterID == viewModel.RegisterID).FirstOrDefault();
                if (lesson == null)
                {
                    ViewBag.Message = "學員未購買課程!!";
                    return View(item);
                }

                if (lesson.Attended == (int)Naming.LessonStatus.課程結束)
                {
                    ViewBag.Message = "學員課程已結束!!";
                    return View(item);
                }

                if (lesson.LessonTime.Count + (lesson.AttendedLessons ?? 0) >= lesson.Lessons)
                {
                    ViewBag.Message = "學員上課堂數已滿!!";
                    return View(item);
                }
            }


            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = viewModel.CoachID,
                AttendingCoach = viewModel.CoachID,
                //ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration,
                TrainingBySelf = viewModel.TrainingBySelf,
                RegisterID = lesson.RegisterID,
                LessonPlan = new LessonPlan
                {

                },
                BranchID = viewModel.BranchID
            };

            if (viewModel.TrainingBySelf == 1)
            {
                timeItem.GroupID = lesson.RegisterGroupID;
            }
            else
            {
                var users = checkOverlapedBooking(timeItem, lesson);
                if (users.Count() > 0)
                {
                    ViewBag.Message = "學員(" + String.Join("、", users.Select(u => u.RealName)) + ")上課時間重複!!";
                    return View(item);
                }
                if (lesson.GroupingMemberCount > 1)
                {
                    timeItem.GroupID = lesson.RegisterGroupID;
                    timeItem.LessonFitnessAssessment.AddRange(
                        lesson.GroupingLesson.RegisterLesson.Select(
                            r => new LessonFitnessAssessment
                            {
                                UID = r.UID
                            }));
                }
                else
                {
                    timeItem.LessonFitnessAssessment.Add(new LessonFitnessAssessment
                    {
                        UID = lesson.UID
                    });
                    if(!lesson.RegisterGroupID.HasValue)
                    {
                        timeItem.GroupingLesson = lesson.GroupingLesson = new GroupingLesson { };
                    }
                    else
                    {
                        timeItem.GroupID = lesson.RegisterGroupID;
                    }
                }
            }

            models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            //models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();
            if (lesson.GroupingMemberCount > 1)
            {
                for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
                {
                    foreach (var regles in lesson.GroupingLesson.RegisterLesson)
                    {
                        timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                        {
                            ClassDate = timeItem.ClassTime.Value.Date,
                            //LessonID = timeItem.LessonID,
                            LessonTime = timeItem,
                            Hour = timeItem.ClassTime.Value.Hour + i,
                            RegisterID = regles.RegisterID
                        });
                    }
                }
            }
            else
            {
                for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
                {
                    timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                    {
                        ClassDate = timeItem.ClassTime.Value.Date,
                        //LessonID = timeItem.LessonID,
                        LessonTime = timeItem,
                        Hour = timeItem.ClassTime.Value.Hour + i,
                        RegisterID = lesson.RegisterID
                    });
                }
            }

            try
            {
                models.SubmitChanges();
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                ViewBag.Message = "預約未完成，請重新預約!!";
                return View(item);
            }

            return RedirectToAction("Coach", "Account", new { lessonDate = viewModel.ClassDate, message = "上課時間預約完成!!" });
        }

        public ActionResult CommitBookingByCoach(LessonTimeViewModel viewModel)
        {
            ViewBag.ViewModel = viewModel;

            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View("~/Views/Shared/ReportInputError.ascx");
            }

            var coach = models.GetTable<ServingCoach>().Where(s => s.CoachID == viewModel.CoachID).FirstOrDefault();
            if (coach == null)
            {
                return View("~/Views/Shared/MessageView.ascx", model: "未指定體能顧問!!");
            }

            if (coach.UserProfile.IsFreeAgent())
            {
                var result = bookingByFreeAgent(viewModel, coach);
                if (result != null)
                {
                    ((ViewResult)result).ViewName = "~/Views/Shared/MessageView.ascx";
                    return result;
                }
                else
                {
                    return Json(new { result = true, message = "上課時間預約完成!!" });
                }
            }


            RegisterLesson lesson;
            if (viewModel.TrainingBySelf == 1)
            {
                if (!viewModel.UID.HasValue)
                {
                    this.ModelState.AddModelError("userName", "請選擇上課學員!!");
                    ViewBag.ModelState = this.ModelState;
                    return View("~/Views/Shared/ReportInputError.ascx");
                }

                var priceType = models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.自主訓練).FirstOrDefault();

                lesson = new RegisterLesson
                {
                    UID = viewModel.UID.Value,
                    RegisterDate = DateTime.Now,
                    GroupingMemberCount = 1,
                    Lessons = 1,
                    ClassLevel = priceType != null ? priceType.PriceID : (int?)null,
                    IntuitionCharge = new IntuitionCharge
                    {
                        ByInstallments = 1,
                        Payment = "Cash",
                        FeeShared = 0
                    },
                    AdvisorID = viewModel.CoachID,
                    GroupingLesson = new GroupingLesson { }
                };

                //var installment = new TuitionInstallment
                //{
                //    PayoffDate = viewModel.ClassDate,
                //    PayoffAmount = priceType.ListPrice
                //};
                //installment.TuitionAchievement.Add(new TuitionAchievement
                //{
                //    CoachID = lesson.AdvisorID.Value,
                //    ShareAmount = installment.PayoffAmount
                //});

                //lesson.IntuitionCharge.TuitionInstallment.Add(installment);
                models.GetTable<RegisterLesson>().InsertOnSubmit(lesson);
                models.SubmitChanges();
            }
            else
            {
                lesson = models.GetTable<RegisterLesson>().Where(r => r.RegisterID == viewModel.RegisterID).FirstOrDefault();
                if (lesson == null)
                {
                    return View("~/Views/Shared/MessageView.ascx", model: "學員未購買課程!!");
                }

                if (lesson.Attended == (int)Naming.LessonStatus.課程結束)
                {
                    return View("~/Views/Shared/MessageView.ascx", model: "學員課程已結束!!");
                }

                if (lesson.LessonTime.Count + (lesson.AttendedLessons ?? 0) >= lesson.Lessons)
                {
                    return View("~/Views/Shared/MessageView.ascx", model: "學員上課堂數已滿!!");
                }
            }


            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = viewModel.CoachID,
                AttendingCoach = viewModel.CoachID,
                //ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration,
                TrainingBySelf = viewModel.TrainingBySelf,
                RegisterID = lesson.RegisterID,
                LessonPlan = new LessonPlan
                {

                },
                BranchID = viewModel.BranchID
            };

            if (viewModel.TrainingBySelf == 1)
            {
                timeItem.GroupID = lesson.RegisterGroupID;
            }
            else
            {
                var users = checkOverlapedBooking(timeItem, lesson);
                if (users.Count() > 0)
                {
                    ViewBag.Message = "學員(" + String.Join("、", users.Select(u => u.RealName)) + ")上課時間重複!!";
                    return View("~/Views/Shared/MessageView.ascx");
                }
                if (lesson.GroupingMemberCount > 1)
                {
                    timeItem.GroupID = lesson.RegisterGroupID;
                    timeItem.LessonFitnessAssessment.AddRange(
                        lesson.GroupingLesson.RegisterLesson.Select(
                            r => new LessonFitnessAssessment
                            {
                                UID = r.UID
                            }));
                }
                else
                {
                    timeItem.LessonFitnessAssessment.Add(new LessonFitnessAssessment
                    {
                        UID = lesson.UID
                    });
                    if (!lesson.RegisterGroupID.HasValue)
                    {
                        timeItem.GroupingLesson = lesson.GroupingLesson = new GroupingLesson { };
                    }
                    else
                    {
                        timeItem.GroupID = lesson.RegisterGroupID;
                    }
                }
            }

            models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            //models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();
            if (lesson.GroupingMemberCount > 1)
            {
                for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
                {
                    foreach (var regles in lesson.GroupingLesson.RegisterLesson)
                    {
                        timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                        {
                            ClassDate = timeItem.ClassTime.Value.Date,
                            //LessonID = timeItem.LessonID,
                            LessonTime = timeItem,
                            Hour = timeItem.ClassTime.Value.Hour + i,
                            RegisterID = regles.RegisterID
                        });
                    }
                }
            }
            else
            {
                for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
                {
                    timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                    {
                        ClassDate = timeItem.ClassTime.Value.Date,
                        //LessonID = timeItem.LessonID,
                        LessonTime = timeItem,
                        Hour = timeItem.ClassTime.Value.Hour + i,
                        RegisterID = lesson.RegisterID
                    });
                }
            }

            try
            {
                models.SubmitChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return View("~/Views/Shared/MessageView.ascx", model: "預約未完成，請重新預約!!");
            }

            return Json(new { result = true, message = "上課時間預約完成!!" });
        }

        public ActionResult CommitTrialLesson(LessonTimeViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;

            if (!this.ModelState.IsValid)
            {
                ViewBag.ModelState = this.ModelState;
                return View(item);
            }

            var coach = models.GetTable<ServingCoach>().Where(s => s.CoachID == viewModel.CoachID).FirstOrDefault();
            if (coach == null)
            {
                ViewBag.Message = "未指定體能顧問!!";
                return View(item);
            }


            RegisterLesson lesson;
            if (!viewModel.UID.HasValue)
            {
                this.ModelState.AddModelError("queryAttendee", "請選擇上課學員!!");
                ViewBag.ModelState = this.ModelState;
                return View(item);
            }

            var priceType = models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.體驗課程).FirstOrDefault();

            lesson = new RegisterLesson
            {
                UID = viewModel.UID.Value,
                RegisterDate = DateTime.Now,
                GroupingMemberCount = 1,
                Lessons = 1,
                ClassLevel = priceType != null ? priceType.PriceID : (int?)null,
                IntuitionCharge = new IntuitionCharge
                {
                    ByInstallments = 1,
                    Payment = "Cash",
                    FeeShared = 0
                },
                AdvisorID = viewModel.CoachID,
                GroupingLesson = new GroupingLesson { }
            };
            var installment = new TuitionInstallment
            {
                PayoffDate = viewModel.ClassDate,
                PayoffAmount = priceType.ListPrice
            };
            installment.TuitionAchievement.Add(new TuitionAchievement
            {
                CoachID = lesson.AdvisorID.Value,
                ShareAmount = installment.PayoffAmount
            });

            lesson.IntuitionCharge.TuitionInstallment.Add(installment);
            models.GetTable<RegisterLesson>().InsertOnSubmit(lesson);
            models.SubmitChanges();


            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = viewModel.CoachID,
                AttendingCoach = viewModel.CoachID,
                //ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration,
                TrainingBySelf = viewModel.TrainingBySelf,
                RegisterID = lesson.RegisterID,
                LessonPlan = new LessonPlan
                {

                },
                BranchID = viewModel.BranchID
            };

            timeItem.GroupID = lesson.RegisterGroupID;
            timeItem.LessonFitnessAssessment.Add(new LessonFitnessAssessment
            {
                UID = lesson.UID
            });
            models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            //models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();

            for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
            {
                timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                {
                    ClassDate = timeItem.ClassTime.Value.Date,
                    //LessonID = timeItem.LessonID,
                    LessonTime = timeItem,
                    Hour = timeItem.ClassTime.Value.Hour + i,
                    RegisterID = lesson.RegisterID
                });
            }


            try
            {
                models.SubmitChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewBag.Message = "預約未完成，請重新預約!!";
                return View(item);
            }

            return RedirectToAction("Coach", "Account", new { lessonDate = viewModel.ClassDate, message = "上課時間預約完成!!" });
        }

        public ActionResult BookingByFreeAgent(LessonTimeViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;
            var coach = models.GetTable<ServingCoach>().Where(s => s.CoachID == item.UID).FirstOrDefault();

            if(coach==null || !coach.UserProfile.IsFreeAgent())
            {
                ViewBag.Message = "未指定自由教練!!";
                return View(item);
            }

            //if (!this.ModelState.IsValid)
            //{
            //    ViewBag.ModelState = this.ModelState;
            //    return View(item);
            //}

            var result = bookingByFreeAgent(viewModel, coach);
            if(result!=null)
            {
                return result;
            }
            else
            {
                return this.TransferToAction("FreeAgent", "Account", new { lessonDate = viewModel.ClassDate, message = "上課時間預約完成!!" });
            }

        }

        private ActionResult bookingByFreeAgent(LessonTimeViewModel viewModel, ServingCoach item)
        {
            UserProfile learner = models.GetTable<UserProfile>().Where(u => u.LevelID == (int)Naming.MemberStatusDefinition.Anonymous).FirstOrDefault();
            if (learner == null)
            {
                ViewBag.Message = "自由教練學員未設定!!";
                return View(item);
            }

            var priceType = models.GetTable<LessonPriceType>().Where(p => p.Status == (int)Naming.DocumentLevelDefinition.自由教練預約).FirstOrDefault();
            if (priceType == null)
            {
                ViewBag.Message = "自由教練預約課程類別未設定!!";
                return View(item);
            }

            RegisterLesson lesson = new RegisterLesson
            {
                UID = learner.UID,
                RegisterDate = DateTime.Now,
                GroupingMemberCount = 1,
                Lessons = 1,
                ClassLevel = priceType != null ? priceType.PriceID : (int?)null,
                IntuitionCharge = new IntuitionCharge
                {
                    ByInstallments = 1,
                    Payment = "Cash",
                    FeeShared = 0
                },
                GroupingLesson = new GroupingLesson { }
            };

            models.GetTable<RegisterLesson>().InsertOnSubmit(lesson);
            models.SubmitChanges(); ;

            LessonTime timeItem = new LessonTime
            {
                InvitedCoach = item.CoachID,
                AttendingCoach = item.CoachID,
                //ClassTime = viewModel.ClassDate.Add(viewModel.ClassTime),
                ClassTime = viewModel.ClassDate,
                DurationInMinutes = viewModel.Duration,
                RegisterID = lesson.RegisterID,
                LessonPlan = new LessonPlan
                {

                },
                BranchID = viewModel.BranchID
            };

            models.GetTable<LessonTime>().InsertOnSubmit(timeItem);
            //models.SubmitChanges();

            var timeExpansion = models.GetTable<LessonTimeExpansion>();

            for (int i = 0; i <= (timeItem.DurationInMinutes + timeItem.ClassTime.Value.Minute - 1) / 60; i++)
            {
                timeExpansion.InsertOnSubmit(new LessonTimeExpansion
                {
                    ClassDate = timeItem.ClassTime.Value.Date,
                    //LessonID = timeItem.LessonID,
                    LessonTime = timeItem,
                    Hour = timeItem.ClassTime.Value.Hour + i,
                    RegisterID = lesson.RegisterID
                });
            }


            try
            {
                models.SubmitChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                ViewBag.Message = "預約未完成，請重新預約!!";
                return View(item);
            }

            return null;

        }

        private IEnumerable<UserProfile> checkOverlapedBooking(LessonTime timeItem, RegisterLesson lesson,LessonTime source = null)
        {
            int durationHours = (timeItem.ClassTime.Value.Minute + timeItem.DurationInMinutes.Value + 59) / 60;
            IQueryable<LessonTimeExpansion> items;
            if (source == null)
            {
                items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == timeItem.ClassTime.Value.Date
                    && t.Hour >= timeItem.ClassTime.Value.Hour
                    && t.Hour < (timeItem.ClassTime.Value.Hour + durationHours));
            }
            else
            {
                items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == timeItem.ClassTime.Value.Date
                    && t.Hour >= timeItem.ClassTime.Value.Hour
                    && t.Hour < (timeItem.ClassTime.Value.Hour + durationHours)
                    && t.LessonID != source.LessonID);
            }

            if (lesson.GroupingMemberCount > 1)
            {
                return items.Select(t => t.RegisterLesson)
                    .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterGroupID == lesson.RegisterGroupID),
                        t => t.UID, l => l.UID, (t, l) => l)
                    .Select(l => l.UserProfile);
            }
            else
            {
                return items.Select(t => t.RegisterLesson.UserProfile)
                    .Where(u => u.UID == lesson.UID);
            }
        }

        public ActionResult TrialLearner()
        {
            return View("~/Views/Lessons/Module/TrialLearner.ascx");
        }


        public ActionResult Attendee(String userName)
        {
            return View();
        }

        public ActionResult AttendeeByVip(String userName)
        {
            return View();
        }


        public ActionResult AttendeeSelector(String userName)
        {

            IEnumerable<RegisterLesson> items;
            userName = userName.GetEfficientString();
            if (userName == null)
            {
                items = models.GetTable<RegisterLesson>().Where(l => false);
            }
            else
            {
                items = models.GetTable<RegisterLesson>()
                    .Where(l => l.LessonPriceType.Status != (int)Naming.DocumentLevelDefinition.自主訓練)
                    .Where(l => l.Attended != (int)Naming.LessonStatus.課程結束
                        && (l.UserProfile.RealName.Contains(userName) || l.UserProfile.Nickname.Contains(userName)))
                    .Where(l => l.Lessons > l.GroupingLesson.LessonTime.Count)
                    .Where(l => l.RegisterGroupID.HasValue)
                    .OrderBy(l => l.UID).ThenBy(l => l.ClassLevel).ThenBy(l => l.Lessons);
            }

            return View(items);
        }

        public ActionResult TrialLearnerSelector(String userName)
        {
            IEnumerable<UserProfile> items = models.GetTable<UserProfile>().Where(u => u.UserProfileExtension.CurrentTrial.HasValue);

            userName = userName.GetEfficientString();
            if (userName == null)
            {
                items = items.Where(u => false);
            }
            else
            {
                items = items.Where(l => l.RealName.Contains(userName) || l.Nickname.Contains(userName))
                    .OrderBy(l => l.RealName);
            }

            return View("~/Views/Lessons/Module/TrialLearnerSelector.ascx", items);
        }

        public ActionResult VipSelector(String userName)
        {

            IEnumerable<UserProfile> items;
            userName = userName.GetEfficientString();
            if (userName == null)
            {
                items = models.GetTable<UserProfile>().Where(l => false);
            }
            else
            {
                items = models.GetTable<UserProfile>()
                    .Where(u => u.LevelID != (int)Naming.MemberStatusDefinition.Deleted
                        && u.LevelID != (int)Naming.MemberStatusDefinition.Anonymous)
                    .Where(l => l.RealName.Contains(userName) || l.Nickname.Contains(userName))
                    .Where(l => l.UserRole.Count(r => r.RoleID == (int)Naming.RoleID.Learner) > 0)
                    .OrderBy(l => l.UID);
            }

            return View(items);
        }

        public ActionResult BookingEvents(DateTime start, DateTime end)
        {

            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }

            var today = DateTime.Today;

            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);

            var dataItems = models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1));

            IQueryable<LessonTime> timeItems = models.GetTable<LessonTime>();

            if(viewModel!=null && viewModel.BranchID.HasValue)
            {
                dataItems = dataItems.Where(t => t.LessonTime.BranchID == viewModel.BranchID);
                timeItems = timeItems.Where(l => l.BranchID==viewModel.BranchID);
            }

            IEnumerable<CalendarEvent> items;
            if (profile.IsFreeAgent())
            {
                items = timeItems
                        .Where(t => t.ClassTime >= start && t.ClassTime < end.AddDays(1))
                        .Where(t => t.AttendingCoach == profile.UID)
                        .Select(t => new { t.ClassTime.Value.Date, t.RegisterLesson.GroupingMemberCount }).ToList()
                        .GroupBy(t => t.Date)
                        .Select(g => new CalendarEvent
                        {
                            id = "freeAgent",
                            title = g.Sum(v => v.GroupingMemberCount).ToString(),
                            start = g.Key.ToString("yyyy-MM-dd"),
                            description = "自由教練",
                            allDay = true,
                            className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-yellow" },
                            icon = /*g.Key < today ? "fa-check" :*/ "fa-tags"
                        });
            }
            else
            {
                items = timeItems
                        .Where(t => t.ClassTime >= start && t.ClassTime < end.AddDays(1))
                        .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自由教練預約)
                        .Select(t => new { t.ClassTime.Value.Date, t.RegisterLesson.GroupingMemberCount }).ToList()
                        .GroupBy(t => t.Date)
                        .Select(g => new CalendarEvent
                        {
                            id = "freeAgent",
                            title = g.Sum(v => v.GroupingMemberCount).ToString(),
                            start = g.Key.ToString("yyyy-MM-dd"),
                            description = "自由教練",
                            allDay = true,
                            className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-yellow" },
                            icon = /*g.Key < today ? "fa-check" :*/ "fa-tags"
                        });

                items = items.Concat(dataItems
                    .Where(t => !t.LessonTime.TrainingBySelf.HasValue || t.LessonTime.TrainingBySelf == 0)
                    .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.正常
                        || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.已刪除
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.點數兌換課程)
                    .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "course",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "P.T session",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-blue" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-clock-o"
                    }));

                items = items.Concat(dataItems
                    .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練)
                    .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "coach",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "內部訓練",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-teal" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-university"
                    }));

                items = items.Concat(dataItems
                    .Where(t => t.LessonTime.TrainingBySelf == 1)
                    .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "self",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "P.I session",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-red" },
                        icon = "fa-child" // g.Key < today ? "fa-ckeck" : "fa-clock-o"
                }));

                items = items.Concat(dataItems
                    .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程)
                    .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "trial",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "體驗課程",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-pink" },
                        icon = "fa-magic" // g.Key < today ? "fa-ckeck" : "fa-clock-o"
                    }));
            }


            return Json(items, JsonRequestBehavior.AllowGet);


        }

        [AllowAnonymous]
        public ActionResult DailyLearnerCount(DateTime date)
        {
            var items = models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate >= date.Date && t.ClassDate < date.AddDays(1))
                .Select(t => t.RegisterLesson.UID)
                .Distinct();

            return Content(items.Count().ToString());
        }

        public ActionResult VipEvents(int? id, DateTime start, DateTime end, bool? learner)
        {
            UserProfile item = models.GetTable<UserProfile>().Where(u => u.UID == id).FirstOrDefault();
            if (item == null)
                item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<CalendarEvent> items = buildVipLessonEvents(start, end, learner, item);

            var eventItems = models.GetTable<UserEvent>()
                .Where(t => t.StartDate >= start && t.EndDate < end.AddDays(1))
                .Where(t => t.UID == item.UID).ToList();

            items = items.Concat(eventItems
                .Select(g => new CalendarEvent
                {
                    id = "my",
                    title = "",
                    start = g.StartDate.ToString("yyyy-MM-dd"),
                    end = g.EndDate.AddDays(1).ToString("yyyy-MM-dd"),
                    description = g.Title,
                    lessonID = g.EventID,
                    allDay = true,
                    className = new string[] { "event", "bg-color-greenLight" },  //g.StartDate < today ? g.EndDate < today ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-pink" },
                    icon = ""   //"fa-magic"
                }));

            return Json(items, JsonRequestBehavior.AllowGet);


        }

        public ActionResult VipLessonEvents(FullCalendarViewModel viewModel)
        {
            UserProfile item = models.GetTable<UserProfile>().Where(u => u.UID == viewModel.UID).FirstOrDefault();
            if (item == null)
                item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<CalendarEvent> items = buildVipLessonEvents(viewModel.DateFrom.Value, viewModel.DateTo.Value, true, item);

            return Json(items, JsonRequestBehavior.AllowGet);


        }

        private IEnumerable<CalendarEvent> buildVipLessonEvents(DateTime start, DateTime end, bool? learner, UserProfile item)
        {
            var today = DateTime.Today;

            var dataItems = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= start && t.ClassTime < end.AddDays(1))
                .Where(t => t.RegisterLesson.UID == item.UID
                    || t.GroupingLesson.RegisterLesson.Any(r => r.UID == item.UID)).ToList();

            //var items = dataItems
            //    .Where(t => !t.TrainingBySelf.HasValue || t.TrainingBySelf == 0)
            //    .Select(g => new _calendarEvent
            //    {
            //        id = "course",
            //        title = "",
            //        start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
            //        description = "P.T session",
            //        lessonID = g.LessonID,
            //        allDay = true,
            //        className = g.ClassTime < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-blue" },
            //        icon = g.ClassTime < today ? learner == true ? "fa-anchor" : "fa-check" : "fa-clock-o"
            //    });

            var items = dataItems
                .Where(t => !t.TrainingBySelf.HasValue || t.TrainingBySelf == 0)
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.正常
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.已刪除
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.點數兌換課程)
                .Where(t => t.RegisterLesson.GroupingMemberCount == 1)
                .Select(g => new CalendarEvent
                {
                    id = "course",
                    title = "",
                    start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
                    description = "1 對 1",
                    lessonID = g.LessonID,
                    allDay = true,
                    className = g.LessonAttendance != null && learner == false ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-blue" },//g.ClassTime < today ? g.LessonAttendance == null ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-pinkDark" },
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null  // "fa -user"
                });

            items = items.Concat(dataItems
                .Where(t => !t.TrainingBySelf.HasValue || t.TrainingBySelf == 0)
                .Where(t => t.RegisterLesson.GroupingMemberCount > 1)
                .Select(g => new CalendarEvent
                {
                    id = "course",
                    title = "",
                    start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
                    description = "1 對 " + g.RegisterLesson.GroupingMemberCount,
                    lessonID = g.LessonID,
                    allDay = true,
                    className = g.LessonAttendance != null && learner == false ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-blue" },  //g.ClassTime < today ? g.LessonAttendance == null ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-pinkDark" },
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null  //"fa-users"
                }));

            items = items.Concat(dataItems
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練)
                .Select(g => new CalendarEvent
                {
                    id = "coach",
                    title = "",
                    start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
                    description = "內部訓練",
                    lessonID = g.LessonID,
                    allDay = true,
                    className = g.ClassTime < today ? g.LessonAttendance == null ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-teal" },
                    icon = "fa-university"
                }));


            items = items.Concat(dataItems
                .Where(t => t.TrainingBySelf == 1)
                .Select(g => new CalendarEvent
                {
                    id = "self",
                    title = "",
                    start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
                    description = "P.I session",
                    lessonID = g.LessonID,
                    allDay = true,
                    className = /*g.ClassTime < today ? new string[] { "event", "bg-color-yellow" } :*/ g.LessonAttendance != null && learner == false ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-red" },
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null  //"fa-child" // learner == true ? "fa-child" : g.ClassTime < today ? "fa-ckeck" : "fa-clock-o"
                }));

            items = items.Concat(dataItems
                .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程)
                .Select(g => new CalendarEvent
                {
                    id = "trial",
                    title = "",
                    start = g.ClassTime.Value.ToString("yyyy-MM-dd"),
                    description = "體驗課程",
                    lessonID = g.LessonID,
                    allDay = true,
                    className = g.LessonAttendance != null && learner == false ? new string[] { "event", "bg-color-grayDark" } : new string[] { "event", "bg-color-pink" },  //  g.ClassTime < today ? g.LessonAttendance == null ? new string[] { "event", "bg-color-red" } : new string[] { "event", "bg-color-blue" } : new string[] { "event", "bg-color-pink" },
                    icon = g.LessonPlan.CommitAttendance.HasValue ? "fa-check-square-o" : null  //"fa-magic"
                }));
            return items;
        }

        public ActionResult VipEvent(DateTime lessonDate)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }

            var items = models.GetTable<LessonTime>().Where(t => t.ClassTime >= lessonDate
                && t.ClassTime < lessonDate.AddDays(1)
                && t.RegisterLesson.UID == profile.UID);

            if (items.Count() == 0)
            {
                return Json(new { result = false, message = "課程資料不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            if(items.Count()>1)
            {
                return View("SelectVipDay", items);
            }
            else
            {
                return View("ToVipDay", items.First());
            }

        }

        public ActionResult VipDay(int id)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == id
                && t.RegisterLesson.UID == profile.UID).FirstOrDefault();

            if (item == null)
            {
                return RedirectToAction("Vip", "Account");
            }

            ViewBag.LessonTimeExpansion = item.LessonTimeExpansion.First();
            return View(item);

        }

        public ActionResult LearnerLessonRemark(int id, FeedBackViewModel viewModel)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return  Content("連線逾時，請重新登入!!");
            }

            LessonTime lesson = models.GetTable<LessonTime>().Where(l => l.LessonID == id).FirstOrDefault();
            if (lesson == null)
                return Content("課程資料不存在!!");

            LessonFeedBack item = lesson.LessonFeedBack.Where(f => f.RegisterLesson.UID == profile.UID).FirstOrDefault();
            if (item == null)
            {
                if (lesson.RegisterLesson.GroupingMemberCount > 1)
                {
                    RegisterLesson regItem = lesson.GroupingLesson.RegisterLesson.Where(r => r.UID == profile.UID).FirstOrDefault();
                    if (regItem == null)
                        return Content("團體課程資料不存在!!");

                    item = new LessonFeedBack
                    {
                        LessonID = id,
                        RegisterID = regItem.RegisterID,
                        //Status = (int)Naming.IncommingMessageStatus.未讀
                    };
                }
                else
                {
                    item = new LessonFeedBack
                    {
                        LessonID = id,
                        RegisterID = lesson.RegisterID,
                        //Status = (int)Naming.IncommingMessageStatus.未讀
                    };
                }

                lesson.LessonFeedBack.Add(item);
            }

            item.Remark = viewModel.FeedBack;
            item.RemarkDate = DateTime.Now;
            item.Status = (int)Naming.IncommingMessageStatus.未讀;
            models.SubmitChanges();

            return View("~/Views/Lessons/Feedback/LearnerLessonRemarkItem.ascx", lesson);

        }


        public ActionResult Feedback(int id,FeedBackViewModel viewModel)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new { result = false, message = "連線逾時，請重新登入!!" });
            }

            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == id
                && t.RegisterLesson.UID == profile.UID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料不存在!!" });
            }

            item.LessonPlan.FeedBack = viewModel.FeedBack;
            item.LessonPlan.FeedBackDate = DateTime.Now;
            if (viewModel.ExecutionFeedBack != null)
            {
                for (int i = 0; i < item.TrainingPlan.Count && i < viewModel.ExecutionFeedBack.Length; i++)
                {
                    item.TrainingPlan[i].TrainingExecution.ExecutionFeedBack = viewModel.ExecutionFeedBack[i];
                    item.TrainingPlan[i].TrainingExecution.ExecutionFeedBackDate = DateTime.Now;
                }
            }
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult CommitConclusion(int lessonID, String conclusion)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new { result = false, message = "連線已中斷!!" });
            }

            TrainingPlan item = models.GetTable<TrainingPlan>().Where(t => t.LessonID == lessonID).FirstOrDefault();

            if (item == null)
            {
                return Json(new { result = false, message = "課程資料不存在!!" });
            }

            item.TrainingExecution.Conclusion = conclusion;
            models.SubmitChanges();

            return Json(new { result = true });

        }


        public ActionResult DailyBookingListJson(DateTime lessonDate,DateTime? endQueryDate,String category)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new { data = new object[] { } }, JsonRequestBehavior.AllowGet);
            }

            Expression<Func<LessonTime, bool>> queryExpr = l => true;

            if (endQueryDate.HasValue)
            {
                queryExpr = queryExpr.And(l => l.ClassTime >= lessonDate && l.ClassTime < endQueryDate.Value.AddDays(1));
            }
            else
            {
                queryExpr = l => l.ClassTime >= lessonDate && l.ClassTime < lessonDate.AddDays(1);
            }

            if (category == "coach")
            {
                queryExpr = queryExpr.And(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練);
            }
            else if (category == "trial")
            {
                queryExpr = queryExpr.And(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程);
            }

            if (item.IsFreeAgent())
            {
                queryExpr = queryExpr.And(l => l.AttendingCoach == item.UID);
            }

            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);
            if (viewModel != null && viewModel.BranchID.HasValue)
            {
                queryExpr = queryExpr.And(l => l.BranchID == viewModel.BranchID);
            }

            var items = models.GetTable<LessonTime>().Where(queryExpr)
                .Select(t => t.LessonTimeExpansion.OrderBy(l => l.Hour).First());

            return getBookingListJson(items);
        }

        private ActionResult getBookingListJson(IQueryable<LessonTimeExpansion> items)
        {
            var listItems = items.GroupBy(l => new { ClassDate = l.ClassDate, Hour = l.Hour });

            return Json(new
            {
                data = listItems
                .Select(g => new
                {
                    timezone = String.Format("{0:00}:00 - {1:00}:00", g.Key.Hour, g.Key.Hour + 1),
                    count = g.Sum(t => t.RegisterLesson.GroupingMemberCount),
                    booktime = "--",
                    hour = g.Key.Hour,
                    function = "",
                    details = ""    //getDailyBookingMembers(items, g.Key.ClassDate, g.Key.Hour)
                })
                .ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingList(DateTime lessonDate, DateTime? endQueryDate)
        {
            ViewBag.EndQueryDate = endQueryDate;
            return View(lessonDate);
        }


        public ActionResult DailyTodoList(DateTime lessonDate, DateTime? endQueryDate)
        {
            ViewBag.EndQueryDate = endQueryDate;
            return View(lessonDate);
        }

        [HttpGet]
        public ActionResult QueryVip(DateTime? lessonDate, String userName, int? branchID,String category)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            if (!lessonDate.HasValue)
                HttpContext.RemoveCache(CachingKey.DailyBookingQuery);

            DailyBookingQueryViewModel viewModel = HttpContext.InitializeBookingQuery(userName, branchID, item);

            //if (lessonDate.HasValue)
            //{
            //    viewModel.DateFrom = lessonDate;
            //    viewModel.DateTo = lessonDate;
            //    viewModel.HasQuery = true;
            //}

            ViewBag.LessonDate = lessonDate.HasValue ? lessonDate.Value : new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            ViewBag.ViewModel = viewModel;
            ViewBag.Category = category;

            return View();
        }


        public ActionResult QueryVip(DailyBookingQueryViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;

            if(!ModelState.IsValid)
            {
                ViewBag.ModelState = ModelState;
                return View();
            }
            //if (!viewModel.DateFrom.HasValue)
            //{
            //    viewModel.DateFrom = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            //    viewModel.DateTo = viewModel.DateFrom.Value.AddMonths(1).AddDays(-1);
            //}
            //if (!viewModel.MonthInterval.HasValue)
            //    viewModel.MonthInterval = 1;
            viewModel.HasQuery = true;
            HttpContext.SetCacheValue(CachingKey.DailyBookingQuery, viewModel);
            return View();
        }

        public ActionResult QueryLessonList(DateTime? lessonDate,DateTime? start, DateTime? end,String category)
        {
            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);
            //if (viewModel != null)
            //{
            //    if (start.HasValue)
            //        viewModel.DateFrom = start;
            //    if (end.HasValue)
            //        viewModel.DateTo = end;
            //}

            ViewBag.ViewModel = viewModel;
            ViewBag.LessonDate = lessonDate;
            ViewBag.Category = category;
            return View("~/Views/Lessons/LessonTime/Module/QueryLessonList.ascx");
        }


        public ActionResult QueryBookingList(DateTime? lessonDate, DateTime? start, DateTime? end,String category)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTime> items = queryBookingLessons(item);
            if (start.HasValue)
                items = items.Where(t => t.ClassTime >= start);
            if (end.HasValue)
                items = items.Where(t => t.ClassTime < end.Value.AddDays(1));
            if (category == "coach")
            {
                items = items.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練);
            }
            else if (category == "trial")
            {
                items = items.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程);
            }
            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.Category = category;

            if (lessonDate.HasValue)
            {
                ViewBag.DataItems = items.Join(models.GetTable<LessonTimeExpansion>(),
                    t => t.LessonID, l => l.LessonID, (t, l) => l)
                    .Where(l => l.ClassDate == lessonDate.Value);
                ViewBag.ByQuery = true;
                return View("DailyBookingList", lessonDate);
            }
            else
            {
                ViewBag.DataItems = items;
                return View("QueryBookingList");
            }

        }

        public ActionResult DailyBookingMembersByQuery(DateTime lessonDate, int? hour,String category)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return new EmptyResult();
            }

            IQueryable<LessonTime> lessons = queryBookingLessons(item);
            if (category == "coach")
            {
                lessons = lessons.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練);
            }
            else if (category == "trial")
            {
                lessons = lessons.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程);
            }

            IQueryable<LessonTimeExpansion> items = lessons.Join(models.GetTable<LessonTimeExpansion>(),
                        n => n.LessonID, t => t.LessonID, (n, t) => t)
                    .Where(t => t.ClassDate == lessonDate)
                    .GroupBy(t => t.LessonID)
                    .Select(g => g.OrderBy(m => m.Hour).First());

            if (hour.HasValue)
            {
                items = items.Where(t => t.Hour == hour);
            }

            if (item.IsFreeAgent())
            {
                //return View("DailyBookingMembersByFreeAgent", items);
                return View("DailyBookingMembers", items);
            }
            else
            {
                return View("DailyBookingMembers", items);
            }
        }


        public ActionResult QueryBookingListJson(DateTime? start, DateTime? end)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new { data = new object[] { } }, JsonRequestBehavior.AllowGet);
            }

            IQueryable<LessonTime> lessons = queryBookingLessons(item);
            if (start.HasValue)
                lessons = lessons.Where(t => t.ClassTime >= start);
            if (end.HasValue)
                lessons = lessons.Where(t => t.ClassTime < end.Value.AddDays(1));

            var items = lessons.Select(t => t.LessonTimeExpansion.OrderBy(l => l.Hour).First());
            return getQueryBookingListJson(items);
        }

        private ActionResult getQueryBookingListJson(IQueryable<LessonTimeExpansion> items)
        {
            var listItems = items.Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                    .GroupBy(t => t.ClassDate);

            return Json(new
            {
                data = listItems
                .Select(g => new
                {
                    timezone = g.Key.ToString("yyyy/MM/dd"),
                    count = g.Distinct().Count(),
                    booktime = "--",
                    hour = g.Key.Hour,
                    function = "",
                    details = ""//getDailyBookingMembers(items, g.Key, g.Key.Hour)
                })
                .ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingListJsonByQuery(DateTime lessonDate,String category)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new { data = new object[] { } }, JsonRequestBehavior.AllowGet);
            }

            IQueryable<LessonTime> lessons = queryBookingLessons(item).Where(t => t.ClassTime >= lessonDate && t.ClassTime < lessonDate.AddDays(1));
            if (category == "coach")
            {
                lessons = lessons.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練);
            }
            else if (category == "trial")
            {
                lessons = lessons.Where(l => l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程);
            }
            var items = lessons.Select(t => t.LessonTimeExpansion.OrderBy(l => l.Hour).First());

            return getBookingListJson(items);

        }



        private IQueryable<LessonTime> queryBookingLessons(UserProfile profile)
        {
            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);

            if(viewModel==null)
            {
                return models.GetTable<LessonTime>().Where(l => false);
            }
            DataLoadOptions ops = new DataLoadOptions();
            ops.LoadWith<LessonTime>(i => i.LessonTimeExpansion);
            ops.LoadWith<LessonTime>(i => i.LessonPlan);
            ops.LoadWith<LessonTime>(i => i.RegisterLesson);
            ops.LoadWith<RegisterLesson>(i => i.UserProfile);
            ops.LoadWith<LessonTimeExpansion>(i => i.RegisterLesson);
            models.GetDataContext().LoadOptions = ops;
            IQueryable<LessonTime> items = models.GetTable<LessonTime>();

            if (viewModel.DateFrom.HasValue)
            {
                items = items.Where(l => l.ClassTime >= viewModel.DateFrom.Value);
                if (viewModel.MonthInterval.HasValue)
                {
                    items = items.Where(l => l.ClassTime < viewModel.DateFrom.Value.AddMonths(viewModel.MonthInterval.Value));
                }
            }
            if (viewModel.DateTo.HasValue)
                items = items.Where(l => l.ClassTime < viewModel.DateTo.Value.AddDays(1));

            if (viewModel.CoachID.HasValue)
                items = items.Where(l => l.AttendingCoach == viewModel.CoachID);

            if (!string.IsNullOrEmpty(viewModel.UserName))
            {
                items = items.Where(l => l.GroupingLesson.RegisterLesson.Any(r => r.UserProfile.RealName.Contains(viewModel.UserName) || r.UserProfile.Nickname.Contains(viewModel.UserName)));
            }

            if(viewModel.LessonStatus.HasValue)
            {
                switch(viewModel.LessonStatus)
                {
                    case 1:
                        items = items.Where(l => l.LessonAttendance == null);
                        break;
                    case 2:
                        items = items.Where(l => l.LessonAttendance != null);
                        break;
                    case 3:
                        items = items.Where(l => !l.LessonPlan.CommitAttendance.HasValue);
                        break;
                    case 4:
                        items = items.Where(l => l.TrainingBySelf == 1);
                        break;
                }
            }

            if(viewModel.BranchID.HasValue)
            {
                items = items.Where(l => l.BranchID == viewModel.BranchID);
            }

            if (profile.IsFreeAgent())
            {
                items = items.Where(l => l.AttendingCoach == profile.UID);
            }

            return items;
        }

        public ActionResult QueryBookingPlot()
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }

            IQueryable<LessonTime> lessons = queryBookingLessons(item);

            var items = lessons.Join(models.GetTable<LessonTimeExpansion>(),
                t => t.LessonID, l => l.LessonID, (t, l) => l)
                .Select(t => new { t.ClassDate, t.RegisterLesson.UID }).ToList()
                .GroupBy(t => t.ClassDate);

            return Json(items.Select(g => new
            {
                x = g.Key.ToString("MM/dd"),
                y = g.Distinct().Count()
            }).OrderBy(o => o.x).ToArray(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult QueryBookingEvents(DateTime start, DateTime end)
        {

            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);
            }

            IQueryable<LessonTime> lessons = queryBookingLessons(item);
            lessons = lessons.Where(t => t.ClassTime >= start && t.ClassTime < end.AddDays(1));

            var today = DateTime.Today;
            IEnumerable<CalendarEvent> items;

            if (item.IsFreeAgent())
            {
                items = lessons
                    .Select(t => new { t.ClassTime.Value.Date, t.RegisterLesson.GroupingMemberCount }).ToList()
                    .GroupBy(t => t.Date)
                    .Select(g => new CalendarEvent
                    {
                        id = "freeAgent",
                        title = g.Sum(v => v.GroupingMemberCount).ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "自由教練",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-yellow" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-tags"
                    });
            }
            else
            {
                items = lessons.Where(l=>l.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自由教練預約)
                    .Select(t => new { t.ClassTime.Value.Date, t.RegisterLesson.GroupingMemberCount }).ToList()
                    .GroupBy(t => t.Date)
                    .Select(g => new CalendarEvent
                    {
                        id = "freeAgent",
                        title = g.Sum(v => v.GroupingMemberCount).ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "自由教練",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-yellow" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-tags"
                    });

                items = items.Concat(lessons
                        .Where(n => !n.TrainingBySelf.HasValue || n.TrainingBySelf == 0)
                        .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.正常
                            || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.已刪除
                    || t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.點數兌換課程)
                    .Join(models.GetTable<LessonTimeExpansion>(), n => n.LessonID, t => t.LessonID, (n, t) => t)
                    .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1))
                    .Select(t => new { t.ClassDate, t.LessonID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "course",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "P.T session",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-blue" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-clock-o"
                    }));

                items = items.Concat(lessons
                        .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練)
                    .Join(models.GetTable<LessonTimeExpansion>(), n => n.LessonID, t => t.LessonID, (n, t) => t)
                    .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1))
                    .Select(t => new { t.ClassDate, t.LessonID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "coach",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "內部訓練",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-teal" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-university"
                    }));

                items = items.Concat(lessons.Where(n => n.TrainingBySelf == 1)
                    .Join(models.GetTable<LessonTimeExpansion>(), n => n.LessonID, t => t.LessonID, (n, t) => t)
                    .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1))
                    .Select(t => new { t.ClassDate, t.LessonID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "self",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "P.I session",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-red" },
                        icon = "fa-child"   //g.Key < today ? "fa-ckeck" : "fa-clock-o"
                }));

                items = items.Concat(lessons
                        .Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程)
                    .Join(models.GetTable<LessonTimeExpansion>(), n => n.LessonID, t => t.LessonID, (n, t) => t)
                    .Where(t => t.ClassDate >= start && t.ClassDate < end.AddDays(1))
                    .Select(t => new { t.ClassDate, t.LessonID }).ToList()
                    .GroupBy(t => t.ClassDate)
                    .Select(g => new CalendarEvent
                    {
                        id = "trial",
                        title = g.Distinct().Count().ToString(),
                        start = g.Key.ToString("yyyy-MM-dd"),
                        description = "體驗課程",
                        allDay = true,
                        className = g.Key < today ? new string[] { "event", "bg-color-yellow" } : new string[] { "event", "bg-color-pink" },
                        icon = /*g.Key < today ? "fa-check" :*/ "fa-magic"
                    }));
            }

            return Json(items, JsonRequestBehavior.AllowGet);

        }


        public ActionResult DailyBookingQuery(DailyBookingQueryViewModel viewModel)
        {
            UserProfile item = HttpContext.GetUser();
            if (item == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            IQueryable<LessonTime> items = models.GetTable<LessonTime>();

            HttpContext.SetCacheValue(CachingKey.DailyBookingQuery, viewModel);

            if (viewModel.DateFrom.HasValue)
            {
                items = items.Where(l => l.ClassTime >= viewModel.DateFrom.Value);
                if (viewModel.MonthInterval.HasValue)
                {
                    items = items.Where(l => l.ClassTime < viewModel.DateFrom.Value.AddMonths(viewModel.MonthInterval.Value));
                }
            }
            if(viewModel.DateTo.HasValue)
                items = items.Where(l => l.ClassTime < viewModel.DateTo.Value.AddDays(1));

            if (viewModel.CoachID.HasValue)
                items = items.Where(l => l.AttendingCoach == viewModel.CoachID);

            if(!string.IsNullOrEmpty(viewModel.UserName))
            {
                items = items.Where(l => l.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName) || l.RegisterLesson.UserProfile.Nickname.Contains(viewModel.UserName));
            }

            if(item.IsFreeAgent())
            {
                items = items.Where(l => l.AttendingCoach == item.UID);
            }

            ViewBag.DataItems = items;
            return View(DateTime.Today);
        }

        public ActionResult DailyBookingSummary(DateTime lessonDate)
        {
            Dictionary<int, int> index = new Dictionary<int, int>();
            foreach (int idx in Enumerable.Range(8, 15))
            {
                index[idx] = 0;
            }

            var items = models.GetTable<LessonTimeExpansion>()
                .Where(t => t.ClassDate == lessonDate)
                .Select(t => t.Hour).ToList()
                .GroupBy(t => t);

            foreach (var item in items)
            {
                index[item.Key] = item.Count();
            }

            return Json(index.Select(i => new object[] { i.Key, i.Value }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DailyBookingPlot(DateTime lessonDate)
        {
            Dictionary<int, int> index = dailyBookingHourlyCount(lessonDate);

            return Json(index.Select(i => new
            {
                x = i.Key,
                y = i.Value
            }).ToArray(), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult DailyBookingHourlyList(DateTime lessonDate)
        {
            Dictionary<int, int> index = dailyBookingHourlyCount(lessonDate);
            return Content(String.Join(",", index.Select(d => d.Value.ToString())));
        }


        private Dictionary<int, int> dailyBookingHourlyCount(DateTime lessonDate)
        {
            Dictionary<int, int> index = new Dictionary<int, int>();
            foreach (int idx in Enumerable.Range(7, 16))
            {
                index[idx] = 0;
            }

            UserProfile profile = HttpContext.GetUser();

            if (profile != null)
            {
                IQueryable<LessonTimeExpansion> lessonTime;
                //if(profile.IsFreeAgent())
                //{
                //    lessonTime = models.GetTable<LessonTimeExpansion>()
                //        .Where(t => t.LessonTime.AttendingCoach == profile.UID);
                //}
                //else
                {
                    lessonTime = models.GetTable<LessonTimeExpansion>();
                }

                DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);
                if (viewModel != null && viewModel.BranchID.HasValue)
                {
                    lessonTime = lessonTime.Where(t => t.LessonTime.BranchID == viewModel.BranchID);
                }

                var items = lessonTime
                    .Where(t => t.ClassDate == lessonDate)
                    .Select(t => t.Hour).ToList()
                    .GroupBy(t => t);

                foreach (var item in items)
                {
                    index[item.Key] = item.Count();
                }
            }

            return index;
        }

        class GraphDataItem
        {
            public DateTime? ClassTime { get; set; }
            public int ActionLearning { get; set; }
            public int PostureRedress { get; set; }
            public int Training { get; set; }
            public int Cardiopulmonary { get; set; }
            public int Endurance { get; set; }
            public int ExplosiveForce { get; set; }
            public int Flexibility { get; set; }
            public int SportsPerformance { get; set; }
            public int Strength { get; set; }
            public String ClassDate { get; set; }
        }

        GraphDataItem calcAverage(LessonTime item)
        {
            var trend = item.LessonTrend;
            decimal total = 30m;// trend.ActionLearning.Value + trend.PostureRedress.Value + trend.Training.Value;
            var r = new GraphDataItem
            {
                ClassTime = item.ClassTime.Value,
                ClassDate = String.Format("{0:yyyy-MM-dd}",item.ClassTime),
                ActionLearning = (int)Math.Round(trend.ActionLearning.Value * 100m / total),
                PostureRedress = (int)Math.Round(trend.PostureRedress.Value * 100m / total),
                Training = (int)Math.Round(trend.Training.Value * 100m / total)
            };
            //r.Training = 100 - r.ActionLearning - r.PostureRedress;
            return r;
        }

        GraphDataItem fitnessAverage(LessonTime item)
        {
            var fitness = item.FitnessAssessment;
            decimal total = 60m;
            //decimal total = (fitness.Cardiopulmonary ?? 0)
            //    + (fitness.Endurance ?? 0)
            //    + (fitness.ExplosiveForce ?? 0)
            //    + (fitness.Flexibility ?? 0)
            //    + (fitness.SportsPerformance ?? 0)
            //    + (fitness.Strength ?? 0);

            if (total == 0)
            {
                return new GraphDataItem
                {

                };
            }

            var r = new GraphDataItem
            {
                ClassTime = item.ClassTime.Value,
                ClassDate = String.Format("{0:yyyy-MM-dd}", item.ClassTime),
                Cardiopulmonary = (int)Math.Round((fitness.Cardiopulmonary ?? 0) * 100m / total),
                Endurance = (int)Math.Round((fitness.Endurance ?? 0 ) * 100m / total),
                ExplosiveForce = (int)Math.Round((fitness.ExplosiveForce ?? 0 ) * 100m / total),
                Flexibility = (int)Math.Round((fitness.Flexibility ?? 0 ) * 100m / total),
                SportsPerformance = (int)Math.Round((fitness.SportsPerformance ?? 0 ) * 100m / total),
                Strength = (int)Math.Round((fitness.Strength ?? 0) * 100m / total)
            };

            //r.Strength = 100
            //    - r.Cardiopulmonary
            //    - r.Endurance
            //    - r.ExplosiveForce
            //    - r.Flexibility
            //    - r.SportsPerformance;

            return r;
        }


        public ActionResult FitnessGraph(DateTime start, DateTime end)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { });
            }

            DateTime startDate = start < end ? new DateTime(start.Year, start.Month, 1) : new DateTime(end.Year, end.Month, 1);
            DateTime endDate = start >= end ? start : end;

            var items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= startDate && t.ClassTime < endDate.AddDays(1))
                .Where(t => t.RegisterLesson.UID == profile.UID)
                .Where(t => t.LessonAttendance != null).ToArray()
                .Select(t => fitnessAverage(t)).ToArray();

            return Json(items, JsonRequestBehavior.AllowGet);

            //var idx = Enumerable.Range(0, items.Length);
            //int section = items.Length >= 12 ? (items.Length + 11) / 12 : 1;

            //return Json(
            //    new
            //    {
            //        data = new object[]
            //        {
            //            new
            //            {
            //                label = "柔軟度",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].Flexibility
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "心肺",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].Cardiopulmonary
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "肌力",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].Strength
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "肌耐力",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].Endurance
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "爆發力",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].ExplosiveForce
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "運動表現",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].SportsPerformance
            //                }).ToArray()
            //            }
            //        },
            //        ticks = idx.Select(g => new object[]
            //            {
            //                g,
            //                //g%section==0 ? (g+1).ToString() : ""
            //                ""
            //            }).ToArray()
            //    }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TrendGraph(DateTime start, DateTime end)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { });
            }

            DateTime startDate = start < end ? new DateTime(start.Year, start.Month, 1) : new DateTime(end.Year, end.Month, 1);
            DateTime endDate = start >= end ? start : end;

            var items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= startDate && t.ClassTime < endDate.AddDays(1))
                .Where(t => t.RegisterLesson.UID == profile.UID)
                .Where(t => t.LessonAttendance != null).ToArray()
                .Select(t => calcAverage(t)).ToArray();

            return Json(items, JsonRequestBehavior.AllowGet);

            //var idx = Enumerable.Range(0, items.Length);
            //int section = items.Length >= 12 ? (items.Length + 11) / 12 : 1;

            //return Json(
            //    new
            //    {
            //        data = new object[]
            //        {
            //            new
            //            {
            //                label = "動作學習",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].ActionLearning
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "姿勢矯正",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].PostureRedress
            //                }).ToArray()
            //            },
            //            new
            //            {
            //                label = "訓練",
            //                data = idx.Select(g=>new object[]
            //                {
            //                    g,
            //                    items[g].Training
            //                }).ToArray()
            //            }
            //        },
            //        ticks = idx.Select(g => new object[]
            //            {
            //                g,
            //                //g%section==0 ? (g+1).ToString() : ""
            //                ""
            //            }).ToArray()
            //    }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TrendGraphAverage(DateTime start, DateTime end)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new object[] { });
            }

            DateTime startDate = start < end ? new DateTime(start.Year,start.Month,1) : new DateTime(end.Year, end.Month, 1);
            DateTime endDate = start >= end ? start : end;

            var items = models.GetTable<LessonTime>()
                .Where(t => t.ClassTime >= startDate && t.ClassTime < endDate.AddDays(1))
                .Where(t => t.RegisterLesson.UID == profile.UID)
                .Where(t => t.LessonAttendance != null).ToArray()
                .Select(t=> calcAverage(t))
                .GroupBy(t => new { Year = t.ClassTime.Value.Year, Month = t.ClassTime.Value.Month })
                .Select(g => new
                {
                    Key = g.Key,
                    ActonLearning = (g.Sum(v => v.ActionLearning) + g.Count() / 2) / g.Count(),
                    PostureRedress = (g.Sum(v => v.PostureRedress) + g.Count() / 2) / g.Count(),
                    Training = (g.Sum(v => v.Training) + g.Count() / 2) / g.Count()
                });

            return Json(
                new
                {
                    data = new object[]
                    {
                        new
                        {
                            label = "動作學習",
                            data = items.Select(g=>new object[]
                            {
                                (g.Key.Year-startDate.Year)*12+g.Key.Month-startDate.Month,
                                g.ActonLearning
                            }).ToArray() },
                        new
                        {
                            label = "姿勢矯正",
                            data = items.Select(g => new object[]
                            {
                                (g.Key.Year-startDate.Year)*12+g.Key.Month-startDate.Month,
                                g.PostureRedress
                            }).ToArray()
                        },
                        new
                        {
                            label = "訓練",
                            data = items.Select(g => new object[]
                            {
                                (g.Key.Year-startDate.Year)*12+g.Key.Month-startDate.Month,
                                g.Training
                            }).ToArray()
                        }
                    },
                    ticks = Enumerable.Range(0, (end.Year - start.Year) * 12 + end.Month - start.Month + 1)
                        .Select(g => new object[]
                        {
                            g,
                            String.Format("{0:00}",(start.Month-1+g)%12+1)
                        }).ToArray()
                }, JsonRequestBehavior.AllowGet);
        }

        String getDailyBookingMembers(IQueryable<LessonTimeExpansion> items, DateTime lessonDate, int? hour)
        {
            IQueryable<LessonTimeExpansion> listItems = items.GroupBy(t => t.LessonID).Select(g => g.OrderBy(m => m.Hour).First());

            if (hour.HasValue)
            {
                listItems = listItems.Where(t => t.Hour == hour);
            }

            return this.RenderViewToString("DailyBookingMembers", new ViewDataDictionary(listItems),this.TempData);

        }

        //public String getDailyBookingMembersByQuery(UserProfile item,DateTime lessonDate, int? hour)
        //{

        //    IQueryable<LessonTime> lessons = queryBookingLessons(item);
        //    IQueryable<LessonTimeExpansion> items = lessons.Join(models.GetTable<LessonTimeExpansion>(),
        //                n => n.LessonID, t => t.LessonID, (n, t) => t)
        //            .Where(t => t.ClassDate == lessonDate)
        //            .GroupBy(t => t.LessonID).Select(g => g.OrderBy(m => m.Hour).First());

        //    if (hour.HasValue)
        //    {
        //        items = items.Where(t => t.Hour == hour);
        //    }

        //    return this.RenderViewToString("DailyBookingMembers", new ViewDataDictionary(items), this.TempData);
        //}

        public ActionResult DailyBookingMembers(DateTime lessonDate, int? hour,String category)
        {
            //IQueryable<LessonTimeExpansion> items = models.GetTable<LessonTimeExpansion>().Where(t => t.ClassDate == lessonDate);
            //if(hour.HasValue)
            //{
            //    items = items.Where(t => t.Hour == hour);
            //}

            //return View(items.GroupBy(l => l.LessonID).Select(g => g.First()));

            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return new EmptyResult();
            }

            DailyBookingQueryViewModel viewModel = (DailyBookingQueryViewModel)HttpContext.GetCacheValue(CachingKey.DailyBookingQuery);

            if (profile.IsFreeAgent())
            {
                IQueryable<LessonTimeExpansion> items = models.GetTable<LessonTimeExpansion>();
                if(viewModel!=null && viewModel.BranchID.HasValue)
                {
                    items = items.Where(t => t.LessonTime.BranchID == viewModel.BranchID);
                }
                if (hour.HasValue)
                {
                    items = items.Where(t => t.Hour == hour);
                }
                if (category == "coach")
                {
                    items = items.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練);
                }
                else if (category == "trial")
                {
                    items = items.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程);
                }

                items = items.Where(t => t.LessonTime.AttendingCoach == profile.UID)
                    .Where(t => t.ClassDate == lessonDate)
                    .GroupBy(l => l.LessonID).Select(g => g.OrderBy(m => m.Hour).First());

                //return View("DailyBookingMembersByFreeAgent", items);
                return View(items);
            }
            else
            {
                IQueryable<LessonTimeExpansion> items = models.GetTable<LessonTimeExpansion>();
                if (viewModel != null && viewModel.BranchID.HasValue)
                {
                    items = items.Where(t => t.LessonTime.BranchID == viewModel.BranchID);
                }
                if (hour.HasValue)
                {
                    items = items.Where(t => t.Hour == hour);
                }
                if (category == "coach")
                {
                    items = items.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練);
                }
                else if (category == "trial")
                {
                    items = items.Where(t => t.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程);
                }


                items = items.Where(t => t.ClassDate == lessonDate)
                    .GroupBy(l => l.LessonID).Select(g => g.OrderBy(m => m.Hour).First());

                return View(items);
            }

        }

        //public ActionResult DailyBookingMembersByFreeAgent(DateTime lessonDate, int? hour)
        //{
        //    return DailyBookingMembers(lessonDate, hour);
        //}

        public ActionResult RevokeBooking(int lessonID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();
            if (item == null)
            {
                ViewBag.Message = "課程資料不存在!!";
                return RedirectToAction("Coach", "Account", new { message = "課程資料不存在!!" });
            }
            //else if (item.LessonPlan != null || item.TrainingPlan.Count > 0)
            //{
            //    ViewBag.Message = "請先刪除預編課程!!";
            //    return RedirectToAction("Coach", "Account", new { lessonDate = lessonDate, message= "請先刪除預編課程!!" });
            //}

            models.DeleteAny<LessonTime>(l => l.LessonID == lessonID);
            if (item.RegisterLesson.UserProfile.LevelID == (int)Naming.MemberStatusDefinition.Anonymous //團體課
                || item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.自主訓練  /*自主訓練*/ 
                || item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.內部訓練
                || item.RegisterLesson.LessonPriceType.Status == (int)Naming.DocumentLevelDefinition.體驗課程)
            {
                models.DeleteAny<RegisterLesson>(l => l.RegisterID == item.RegisterID);
            }

            ViewBag.Message = "課程預約已取消!!";
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassTime.Value.Date ,message = "課程預約已取消!!" });

        }

        public ActionResult RevokeBookingByFreeAgent(int lessonID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            LessonTime item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false, message = "課程資料不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            models.DeleteAny<LessonTime>(l => l.LessonID == lessonID);
            models.DeleteAny<RegisterLesson>(l => l.RegisterID == item.RegisterID);

            return Json(new { result = true });

        }

        public ActionResult PreviewLesson(LessonTimeExpansionViewModel viewModel)
        {
            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).First();

            //return View(item);
            ViewBag.PreviewLesson = true;
            return View("~/Views/Lessons/LessonTime/DataItem.aspx", item);

        }

        public ActionResult PreviewLessonTime(LessonTimeExpansionViewModel viewModel)
        {
            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).First();

            return View("~/Views/Lessons/LessonTime/DataItem.aspx", item);

        }


        public ActionResult UpdateLessonPlanRemark(int lessonID,String remark)
        {
            models.ExecuteCommand(@"
                UPDATE LessonPlan
                SET        Remark = {1}
                WHERE   (LessonID = {0})", lessonID, remark);

            return Json(new { result = true });

        }


        public ActionResult DailyTrendPie(int lessonID, bool? isLearner)
        {
            var item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();

            if (item == null || item.LessonTrend == null)
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);

            var trend = item.LessonTrend;

            if (isLearner == true)
            {
                return Json(new object[] {
                    new {
                        label = "動作學習",
                        data = trend.ActionLearning
                    },
                    new {
                        label = "姿勢矯正",
                        data = trend.PostureRedress
                    },
                    new {
                        label = "訓練",
                        data = trend.Training
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new object[] {
                    new {
                        label = "動作學習",
                        data = trend.ActionLearning
                    },
                    new {
                        label = "姿勢矯正",
                        data = trend.PostureRedress
                    },
                    new {
                        label = "訓練",
                        data = trend.Training
                    },
                    new {
                        label = "狀態溝通",
                        data = trend.Counseling
                    }
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult DailyFitnessPie(int lessonID)
        {
            var item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();

            if (item==null || item.FitnessAssessment == null)
                return Json(new object[] { }, JsonRequestBehavior.AllowGet);

            var fitness = item.FitnessAssessment;

            return Json(new object[] {
                new {
                    label = "柔軟度",
                    data = fitness.Flexibility
                },
                new {
                    label = "心肺",
                    data = fitness.Cardiopulmonary
                },
                new {
                    label = "肌力",
                    data = fitness.Strength
                },
                new {
                    label = "肌耐力",
                    data = fitness.Endurance
                },
                new {
                    label = "爆發力",
                    data = fitness.ExplosiveForce
                },
                new {
                    label = "運動表現",
                    data = fitness.SportsPerformance
                }
            }, JsonRequestBehavior.AllowGet);

        }


        [CoachOrAssistantAuthorize]
        public ActionResult TrainingPlan(LessonTimeExpansionViewModel viewModel)
        {

            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).FirstOrDefault();

            if (item == null)
            {
                return this.TransferToAction("Coach", "Account", new { Message = "上課時間資料不存在!!" });
            }

            HttpContext.SetCacheValue(CachingKey.Training, item);
            //return View(item);
            return View("~/Views/Lessons/LessonTime/EditItem.aspx", item);

        }

        public ActionResult EditLessonTime(LessonTimeExpansionViewModel viewModel)
        {

            var item = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == viewModel.ClassDate
                && l.RegisterID == viewModel.RegisterID && l.Hour == viewModel.Hour).FirstOrDefault();

            if (item == null)
            {
                return this.TransferToAction("Coach", "Account", new { Message = "上課時間資料不存在!!" });
            }

            HttpContext.SetCacheValue(CachingKey.Training, item);
            return View("~/Views/Lessons/LessonTime/EditItem.aspx", item);
        }

        public ActionResult MakeTrainingPlan(int lessonID)
        {

            var item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();

            if (item == null)
            {
                return this.TransferToAction("Coach", "Account", new { Message = "上課時間資料不存在!!" });
            }

            var model = item.LessonTimeExpansion.First();
            HttpContext.SetCacheValue(CachingKey.Training, model);
            return View("~/Views/Lessons/LessonTime/EditItem.aspx", model);
        }

        public ActionResult SingleTrainingExecutionPlan(int lessonID)
        {

            var item = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).First();

            var plan = item.TrainingPlan.FirstOrDefault();
            if (plan == null)
            {
                plan = new Models.DataEntity.TrainingPlan
                {
                    TrainingExecution = new TrainingExecution
                    {
                    }
                };

                item.TrainingPlan.Add(plan);
                models.SubmitChanges();
            }

            return View("SingleTrainingExecutionPlan", plan.TrainingExecution);
        }

        public ActionResult DeletePlan()
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            if (item == null)
            {
                ViewBag.Message = "未登記此上課時間!!";
                return RedirectToAction("Coach", "Account");
            }

            models.DeleteAllOnSubmit<TrainingPlan>(p => p.LessonID == item.LessonID);
            models.DeleteAllOnSubmit<LessonPlan>(p => p.LessonID == item.LessonID);
            models.SubmitChanges();

            HttpContext.RemoveCache(CachingKey.Training);
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassDate, hour = item.Hour, registerID = item.RegisterID, lessonID = item.LessonID });
        }

        public ActionResult CompletePlan()
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            if (item == null)
            {
                return RedirectToAction("Coach", "Account");
            }

            HttpContext.RemoveCache(CachingKey.Training);
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassDate, hour = item.Hour, registerID = item.RegisterID, lessonID = item.LessonID });
        }

        public ActionResult CommitPlan(TrainingPlanViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model;
            LessonPlan plan = prepareLessonPlan(out result, out model);

            if (result != null)
                return result;

            plan.Warming = viewModel.Warming;
            //plan.RecentStatus = viewModel.RecentStatus;
            //model.RegisterLesson.UserProfile.RecentStatus = viewModel.RecentStatus;
            plan.EndingOperation = viewModel.EndingOperation;
            plan.Remark = viewModel.Remark;


            //execution.Repeats = viewModel.Repeats;
            //execution.BreakIntervalInSecond = viewModel.BreakInterval;

            //for (int i = 0; i < execution.TrainingItem.Count && i < viewModel.TrainingID.Length; i++)
            //{
            //    var item = execution.TrainingItem[i];
            //    item.TrainingID = (viewModel.TrainingID[i]).Value;
            //    item.GoalStrength = viewModel.GoalStrength[i];
            //    item.GoalTurns = viewModel.GoalTurns[i];
            //    item.Description = viewModel.Description[i];
            //}

            //execution.TrainingPlan.PlanStatus = (int)Naming.DocumentLevelDefinition.正常;

            models.SubmitChanges();
            //HttpContext.RemoveCache(CachingKey.Training);

            //return RedirectToAction("Coach", "Account", new { lessonDate = model.ClassDate, hour = model.Hour, registerID = model.RegisterID, lessonID = model.LessonID });
            return Json(new { result = true, message = "資料更新完成!!" });

        }

        public ActionResult EditRecentStatus(int uid)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile == null)
            {
                return Content("學員資料錯誤!!");
            }

            return View(profile);
        }

        public ActionResult CommitRecentStatus(int uid,String recentStatus)
        {

            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile == null)
            {
                return Content("學員資料錯誤!!");
            }

            profile.RecentStatus = recentStatus;

            models.SubmitChanges();
            return Json(new { result = true, message = "資料更新完成!!" });

        }

        private LessonPlan prepareLessonPlan(out ActionResult result,out LessonTimeExpansion model)
        {
            result = null;
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            model = null;
            if (item != null)
            {
                model = models.GetTable<LessonTimeExpansion>()
                    .Where(l => l.ClassDate == item.ClassDate
                        && l.RegisterID == item.RegisterID
                        && l.Hour == item.Hour).FirstOrDefault();
            }

            if (model == null)
            {
                ViewBag.Message = "未登記此上課時間!!";
                result = RedirectToAction("Coach", "Account");
                return null;
            }

            LessonPlan plan = model.LessonTime.LessonPlan;
            if (plan == null)
                plan = model.LessonTime.LessonPlan = new LessonPlan { };

            return plan;
        }

        public ActionResult DeleteTraining(int id)
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            models.DeleteAny<TrainingPlan>(t => t.ExecutionID == id && t.LessonID == item.LessonID);

            LessonTimeExpansion model = models.GetTable<LessonTimeExpansion>()
                .Where(l => l.ClassDate == item.ClassDate
                    && l.RegisterID == item.RegisterID 
                    && l.Hour == item.Hour).First();

            return View("TrainingExecutionPlan", model);
        }


        public virtual ActionResult AddTraining(TrainingPlanViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model;
            LessonPlan plan = prepareLessonPlan(out result,out model);

            if (result != null)
                return result;

            plan.Warming = viewModel.Warming;
            //plan.RecentStatus = viewModel.RecentStatus;
            //model.RegisterLesson.UserProfile.RecentStatus = viewModel.RecentStatus;
            plan.EndingOperation = viewModel.EndingOperation;
            plan.Remark = viewModel.Remark;

            models.SubmitChanges();

            HttpContext.RemoveCache(CachingKey.TrainingExecution);
            return View("AddTraining",new TrainingExecution { });
        }


        public ActionResult AddTrainingItem(int id)
        {
            TrainingExecution item = models.GetTable<TrainingExecution>().Where(x => x.ExecutionID == id).First();
            ViewBag.ViewModel = new TrainingItemViewModel
            {
                ExecutionID = item.ExecutionID
            };

            return View("EditSingleTrainingItem", item);
        }

        public ActionResult AddTrainingBreakInterval(int id)
        {
            TrainingExecution item = models.GetTable<TrainingExecution>().Where(x => x.ExecutionID == id).First();
            ViewBag.ViewModel = new TrainingItemViewModel
            {
                ExecutionID = item.ExecutionID
            };

            return View("EditBreakInterval", item);
        }

        public ActionResult CloneTrainingPlan(int lessonID, int sourceID,LessonTimeExpansionViewModel viewModel)
        {
            if(lessonID==sourceID)
            {
                return Json(new { result = false, message = "複製來源與目的課程不能相同!!" });
            }

            var srcItem = models.GetTable<LessonTime>().Where(t => t.LessonID == sourceID).First();
            var targetItem = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).First();

            models.DeleteAll<TrainingPlan>(t => t.LessonID == targetItem.LessonID);

            if (srcItem.LessonPlan != null)
            {
                if (targetItem.LessonPlan == null)
                    targetItem.LessonPlan = new LessonPlan { };
                targetItem.LessonPlan.Warming = srcItem.LessonPlan.Warming;
                targetItem.LessonPlan.EndingOperation = srcItem.LessonPlan.EndingOperation;
            }


            foreach (var p in srcItem.TrainingPlan)
            {
                var newItem = new TrainingPlan
                {
                    LessonID = lessonID,
                    PlanStatus = p.PlanStatus,
                    TrainingExecution = new TrainingExecution
                    {
                    }
                };

                newItem.TrainingExecution.TrainingItem.AddRange(p.TrainingExecution.TrainingItem
                    .Select(i => new TrainingItem
                    {
                        ActualStrength = i.ActualStrength,
                        ActualTurns = i.ActualTurns,
                        BreakIntervalInSecond = i.BreakIntervalInSecond,
                        Description = i.Description,
                        GoalStrength = i.GoalStrength,
                        GoalTurns = i.GoalTurns,
                        Remark = i.Remark,
                        Repeats = i.Repeats,
                        Sequence = i.Sequence,
                        TrainingID = i.TrainingID
                    }));

                models.GetTable<TrainingPlan>().InsertOnSubmit(newItem);

            }

            models.SubmitChanges();

            return Json(new { result = true });
            
        }


        public ActionResult QueryLessonTime(DailyBookingQueryViewModel viewModel,LessonTimeExpansionViewModel timeModel)
        {
            ViewBag.ViewModel = viewModel;
            ViewBag.LessonTimeExpansion = timeModel;
            return View();
        }

        public ActionResult QueryLessonTimeList(DailyBookingQueryViewModel viewModel)
        {
            IQueryable<LessonTime> items = models.GetTable<LessonTime>()
                .Where(t => t.TrainingPlan.Any(p => p.TrainingExecution.TrainingItem.Count > 0));

            if (viewModel.CoachID.HasValue)
                items = items.Where(t => t.AttendingCoach == viewModel.CoachID.Value);

            if (viewModel.TrainingBySelf == 1)
            {
                items = items.Where(t => t.TrainingBySelf == 1);
            }
            else if (viewModel.TrainingBySelf == 0)
            {
                items = items.Where(t => !t.TrainingBySelf.HasValue);
            }

            if (!String.IsNullOrEmpty(viewModel.UserName))
            {
                items = items.Where(t => t.RegisterLesson.UserProfile.RealName.Contains(viewModel.UserName) || t.RegisterLesson.UserProfile.Nickname.Contains(viewModel.UserName));
            }

            ViewBag.ViewModel = viewModel;

            return View(items.OrderByDescending(t => t.LessonID).Take(20));
        }

        public ActionResult EditTrainingItem(int executionID,int itemID)
        {
            TrainingItem item = models.GetTable<TrainingItem>().Where(x => x.ExecutionID == executionID && x.ItemID == itemID).First();

            ViewBag.ViewModel = new TrainingItemViewModel
            {
                ExecutionID = item.ExecutionID,
                ActualStrength = item.ActualStrength,
                ActualTurns = item.ActualTurns,
                BreakInterval = item.BreakIntervalInSecond,
                Description = item.Description,
                GoalStrength = item.GoalStrength,
                GoalTurns = item.GoalTurns,
                Remark = item.Remark,
                Repeats = item.Repeats,
                TrainingID = item.TrainingID,
                ItemID = item.ItemID
            };

            ViewBag.Edit = true;

            return View("EditSingleTrainingItem", item.TrainingExecution);
        }

        public ActionResult EditTrainingBreakInterval(int executionID, int itemID)
        {
            TrainingItem item = models.GetTable<TrainingItem>().Where(x => x.ExecutionID == executionID && x.ItemID == itemID).First();

            ViewBag.ViewModel = new TrainingItemViewModel
            {
                ExecutionID = item.ExecutionID,
                BreakInterval = item.BreakIntervalInSecond,
                Remark = item.Remark,
                Repeats = item.Repeats,
                ItemID = item.ItemID
            };

            ViewBag.Edit = true;

            return View("EditBreakInterval", item.TrainingExecution);
        }

        public ActionResult EditTraining(int id)
        {

            TrainingExecution execution = models.GetTable<TrainingExecution>().Where(t => t.ExecutionID == id).FirstOrDefault();
            if(execution==null)
            {
                ViewBag.Message = "修改項目不存在!!";
                return View("AddTraining", new TrainingExecution { });
            }
            HttpContext.SetCacheValue(CachingKey.TrainingExecution, execution.ExecutionID);
            return View("AddTraining", execution);
        }

        public ActionResult UpdateTrainingItemSequence(int id,int[] itemID)
        {
            if (itemID != null && itemID.Length > 0)
            {
                for (int i = 0; i < itemID.Length; i++)
                {
                    var item = models.GetTable<TrainingItem>().Where(t => t.ItemID == itemID[i]).FirstOrDefault();
                    if (item != null)
                        item.Sequence = i;
                }
                models.SubmitChanges();
            }
            return SingleTrainingExecutionPlan(id);
        }


        public ActionResult CommitTrainingItem(TrainingItemViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }

            TrainingExecution execution = models.GetTable<TrainingExecution>().Where(t=>t.ExecutionID==viewModel.ExecutionID).FirstOrDefault();
            if(execution==null)
            {
                return Json(new { result = false, message = "預編課程項目不存在!!" });
            }

            TrainingItem item;
            if (viewModel.ItemID.HasValue)
            {
                item = execution.TrainingItem.Where(i => i.ItemID == viewModel.ItemID).FirstOrDefault();
                if(item==null)
                {
                    return Json(new { result = false, message = "修改動作項目不存在!!" });
                }
                item.ActualStrength = viewModel.ActualStrength;
                item.ActualTurns = viewModel.ActualTurns;
            }
            else
            {
                item = new TrainingItem
                {
                    ActualTurns = viewModel.GoalTurns,
                    ActualStrength = viewModel.GoalStrength,
                    Sequence = int.MaxValue
                };
                execution.TrainingItem.Add(item);
            }

            item.Description = viewModel.Description;
            item.GoalStrength = viewModel.GoalStrength;
            item.GoalTurns = viewModel.GoalTurns;
            item.TrainingID = viewModel.TrainingID;
            item.Remark = viewModel.Remark;

            models.SubmitChanges();
            return Json(new { result = true, message = "" });

        }

        public ActionResult CommitTrainingBreakInterval(TrainingItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }

            TrainingExecution execution = models.GetTable<TrainingExecution>().Where(t => t.ExecutionID == viewModel.ExecutionID).FirstOrDefault();
            if (execution == null)
            {
                return Json(new { result = false, message = "預編課程項目不存在!!" });
            }

            TrainingItem item;
            if (viewModel.ItemID.HasValue)
            {
                item = execution.TrainingItem.Where(i => i.ItemID == viewModel.ItemID).FirstOrDefault();
                if (item == null)
                {
                    return Json(new { result = false, message = "修改動作項目不存在!!" });
                }
            }
            else
            {
                item = new TrainingItem
                {
                    Sequence = int.MaxValue
                };
                execution.TrainingItem.Add(item);
            }

            item.BreakIntervalInSecond = viewModel.BreakInterval;
            item.Repeats = viewModel.Repeats;
            item.Remark = viewModel.Remark;

            models.SubmitChanges();
            return Json(new { result = true, message = "" });

        }


        public ActionResult CreateTrainingItem(TrainingExecutionViewModel viewModel)
        {
            TrainingExecution execution = prepareTrainingExecution();

            if (!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }

            execution.Repeats = viewModel.Repeats;
            execution.BreakIntervalInSecond = viewModel.BreakInterval;

            TrainingItem item = new TrainingItem
            {
                TrainingID = 1
            };
            execution.TrainingItem.Add(item);

            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.TrainingExecution, execution.ExecutionID);

            return View("EditTrainingItem", item);
        }

        private TrainingExecution prepareTrainingExecution(bool createNew = true)
        {
            TrainingExecution execution = models.GetTable<TrainingExecution>()
                .Where(t => t.ExecutionID == (int?)HttpContext.GetCacheValue(CachingKey.TrainingExecution)).FirstOrDefault();

            if (execution == null || createNew)
            {
                LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

                Models.DataEntity.TrainingPlan plan = new Models.DataEntity.TrainingPlan
                {
                    LessonID = item.LessonID.Value,
                    PlanStatus = (int)Naming.DocumentLevelDefinition.暫存
                };
                execution = new TrainingExecution
                {
                    TrainingPlan = plan
                };
                models.GetTable<Models.DataEntity.TrainingPlan>().InsertOnSubmit(plan);
            }

            return execution;
        }

        public ActionResult DeleteTrainingItem(int itemID,int executionID)
        {

            var item = models.DeleteAny<TrainingItem>(i => i.ItemID == itemID && i.ExecutionID == executionID);
            if (item == null)
            {
                return Json(new { result = false, message = "課程項目不存在!!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidateToCommitTraining(TrainingExecutionViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }
            return Json(new { result = true });
        }

        public ActionResult TrainingExecutionItem()
        {
            TrainingExecution execution = prepareTrainingExecution();
            models.SubmitChanges();

            HttpContext.SetCacheValue(CachingKey.TrainingExecution, execution.ExecutionID);
            return View(execution);
        }

        public ActionResult CommitTraining(TrainingExecutionViewModel viewModel)
        {
            TrainingExecution execution = models.GetTable<TrainingExecution>().Where(x=>x.ExecutionID==viewModel.ExecutionID).FirstOrDefault();

            if (execution == null)
            {
                return Json(new { result = false, message = "預編課程項目不存在!!" });
            }

            if (execution.TrainingItem.Count == 0)
            {
                return Json(new { result = false, message = "尚未設定上課項目!!" });
            }

            if(!ModelState.IsValid)
            {
                return Json(new { result = false, message = ModelState.ErrorMessage() });
            }

            execution.Repeats = viewModel.Repeats;
            execution.BreakIntervalInSecond = viewModel.BreakInterval;
            execution.Conclusion = viewModel.Conclusion;

            for (int i = 0; i < execution.TrainingItem.Count && i < viewModel.TrainingID.Length; i++)
            {
                var item = execution.TrainingItem[i];
                item.TrainingID = (viewModel.TrainingID[i]).Value;
                item.GoalStrength = viewModel.GoalStrength[i];
                item.GoalTurns = viewModel.GoalTurns[i];
                item.ActualStrength = viewModel.ActualStrength[i];
                item.ActualTurns = viewModel.ActualTurns[i];
                item.Description = viewModel.Description[i];
                item.Remark = viewModel.Remark[i];
            }

            execution.TrainingPlan.PlanStatus = (int)Naming.DocumentLevelDefinition.正常;
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public virtual ActionResult CompleteTraining()
        {
            LessonTimeExpansion cacheItem = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            var timeItem = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == cacheItem.ClassDate
                && l.RegisterID == cacheItem.RegisterID && l.Hour == cacheItem.Hour).First();

            ViewBag.DataItem = timeItem.LessonTime;
            return View("TrainingPlan", timeItem);

        }

        public virtual ActionResult TrainingExecutionPlan()
        {
            LessonTimeExpansion cacheItem = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
            if (cacheItem == null)
                return Content("資料錯誤!!");
            var timeItem = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == cacheItem.ClassDate
                && l.RegisterID == cacheItem.RegisterID && l.Hour == cacheItem.Hour).First();

            return View(timeItem);

        }

        public ActionResult QueryModal()
        {
            return View();
        }

        public ActionResult LessonRemark(int lessonID,int registerID)
        {
            var model = models.GetTable<LessonFeedBack>().Where(l => l.LessonID == lessonID && l.RegisterID == registerID).FirstOrDefault();

            if (model == null)
            {
                ViewBag.Message = "課程資料不存在!!";
                return View("~/Views/Shared/ShowMessage.ascx");
            }

            return View("~/Views/Lessons/Feedback/LessonRemark.ascx", model);
            

            //ActionResult result;
            //LessonTimeExpansion model;
            //LessonPlan plan = prepareLessonPlan(out result, out model);

            //if (result != null)
            //    return result;

            //plan.Warming = viewModel.Warming;
            ////plan.RecentStatus = viewModel.RecentStatus;
            ////model.RegisterLesson.UserProfile.RecentStatus = viewModel.RecentStatus;
            //plan.EndingOperation = viewModel.EndingOperation;
            //plan.Remark = viewModel.Remark;


            ////execution.Repeats = viewModel.Repeats;
            ////execution.BreakIntervalInSecond = viewModel.BreakInterval;

            ////for (int i = 0; i < execution.TrainingItem.Count && i < viewModel.TrainingID.Length; i++)
            ////{
            ////    var item = execution.TrainingItem[i];
            ////    item.TrainingID = (viewModel.TrainingID[i]).Value;
            ////    item.GoalStrength = viewModel.GoalStrength[i];
            ////    item.GoalTurns = viewModel.GoalTurns[i];
            ////    item.Description = viewModel.Description[i];
            ////}

            ////execution.TrainingPlan.PlanStatus = (int)Naming.DocumentLevelDefinition.正常;

            //models.SubmitChanges();
            ////HttpContext.RemoveCache(CachingKey.Training);

            ////return RedirectToAction("Coach", "Account", new { lessonDate = model.ClassDate, hour = model.Hour, registerID = model.RegisterID, lessonID = model.LessonID });
            //return Json(new { result = true, message = "資料更新完成!!" });

        }

        public ActionResult CommitLessonRemark(int id, String remark)
        {
            var model = models.GetTable<LessonTime>().Where(l => l.LessonID == id).FirstOrDefault();

            if (model == null)
            {
                return Json(new { result = false, message = "課程資料不存在!!" });
            }

            model.LessonPlan.Remark = remark;
            models.SubmitChanges();

            return Json(new { result = true, message = "資料更新完成!!" });

        }


    }
}