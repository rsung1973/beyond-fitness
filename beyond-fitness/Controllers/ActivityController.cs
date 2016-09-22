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

using CommonLib.MvcExtension;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.Timeline;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    [Authorize]
    public class ActivityController : SampleController<UserProfile>
    {
        // GET: Activity
        public ActionResult TimeLine(int uid)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            List<TimelineEvent> items = new List<TimelineEvent>();
            if (profile != null)
            {
                contructTimeline(profile, items);
            }

            return View(items);
        }

        private void contructTimeline(UserProfile profile, List<TimelineEvent> items)
        {
            ///1. fetch all reserved lessons
            ///
            var lessons = models.GetTable<LessonTime>().Where(t => t.LessonAttendance == null)
                    .Where(t => t.RegisterLesson.UID == profile.UID
                        || t.GroupingLesson.RegisterLesson.Any(r => r.UID == profile.UID));
            items.AddRange(lessons.Select(t => new LessonEvent
            {
                EventTime = t.ClassTime.Value,
                Lesson = t
            }));

            ///2. fetch top 5 attended lessons
            ///
            lessons = models.GetTable<LessonTime>().Where(t => t.LessonAttendance != null)
                    .Where(t => t.RegisterLesson.UID == profile.UID
                        || t.GroupingLesson.RegisterLesson.Any(r => r.UID == profile.UID))
                    .OrderByDescending(t => t.LessonID).Take(5);
            items.AddRange(lessons.Select(t => new LessonEvent
            {
                EventTime = t.ClassTime.Value,
                Lesson = t
            }));

            ///3. add monthly review
            ///
            items.Add(new LearnerMonthlyReviewEvent
            {
                EventTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            });

            ///4. add birthday
            ///
            if (profile.Birthday.HasValue)
            {
                DateTime birth = new DateTime(DateTime.Today.Year, profile.Birthday.Value.Month, 1);
                DateTime birthDate = birth.AddDays(profile.Birthday.Value.Day - 1);

                if (birth.Month == birthDate.Month && birthDate.Month == DateTime.Today.Month)
                {
                    items.Add(new BirthdayEvent
                    {
                        EventTime = birthDate
                    });
                }
            }
        }

        public ActionResult UpdateLessonFeedBack(int lessonID, String feedBack)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            LessonTime lesson = models.GetTable<LessonTime>().Where(l => l.LessonID == lessonID).FirstOrDefault();
            if (lesson == null)
                return new EmptyResult();

            LessonFeedBack item = lesson.LessonFeedBack.Where(f => f.RegisterLesson.UID == profile.UID).FirstOrDefault();
            if (item == null)
            {
                if (lesson.GroupID.HasValue)
                {
                    RegisterLesson regItem = lesson.GroupingLesson.RegisterLesson.Where(r => r.UID == profile.UID).FirstOrDefault();
                    if (regItem == null)
                        return new EmptyResult();

                    item = new LessonFeedBack
                    {
                        LessonID = lessonID,
                        RegisterID = regItem.RegisterID
                    };
                }
                else
                {
                    item = new LessonFeedBack
                    {
                        LessonID = lessonID,
                        RegisterID = lesson.RegisterID
                    };
                }

                models.GetTable<LessonFeedBack>().InsertOnSubmit(item);
            }

            item.FeedBack = feedBack;
            item.FeedBackDate = DateTime.Now;
            models.SubmitChanges();

            return View("LessonFeedBackItem", item);

        }

        public ActionResult LearnerLesson(int lessonID, bool? attendance)
        {
            var item = models.GetTable<LessonTime>().Where(t => t.LessonID == lessonID).FirstOrDefault();

            if (item == null)
            {
                return Content("學員未建立上課資料!!");
            }

            ViewBag.LearnerAttendance = attendance;
            return View(item);
        }

        public ActionResult ListReservedLessons(int uid)
        {
            var items = models.GetTable<LessonTime>().Where(t => t.LessonAttendance == null)
                .Where(t => t.RegisterLesson.UID == uid);

            return View(items);
        }

        public ActionResult ListDailyQuestion()
        {
            var items = models.GetTable<PDQQuestion>().Where(g => g.GroupID == 6)
                .OrderByDescending(q => q.QuestionID);
            return View("ListDailyQuestion", items);
        }

        public ActionResult DeleteQuestion(int id)
        {
            var item = models.DeleteAny<PDQQuestion>(q => q.QuestionID == id && q.GroupID == 6);
            if (item != null)
            {
                ViewBag.Message = "資料已刪除!!";
            }

            return ListDailyQuestion();
        }

        public ActionResult LearnerDailyQuestion()
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            if (models.GetTable<PDQTask>().Any(t => t.UID == profile.UID
                 && t.TaskDate >= DateTime.Today && t.TaskDate < DateTime.Today.AddDays(1)
                 && t.PDQQuestion.GroupID == 6))
            {
                return new EmptyResult();
            }


            int[] items = models.GetTable<PDQQuestion>().Where(q => q.GroupID == 6)
                .Select(q => q.QuestionID)
                .Where(q => !models.GetTable<PDQTask>().Where(t => t.UID == profile.UID).Select(t => t.QuestionID).Contains(q)).ToArray();

            if (items.Length == 0)
            {
                items = models.GetTable<PDQQuestion>().Where(q => q.GroupID == 6)
                .Select(q => q.QuestionID).ToArray();
            }

            profile.DailyQuestionID = items[DateTime.Now.Ticks % items.Length];

            var item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == profile.DailyQuestionID).FirstOrDefault();
            return View(item);
        }

        public ActionResult AnswerDailyQuestion(int? suggestionID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Json(new { result = false });
            }

            if (profile.DailyQuestionID == null)
            {
                return Json(new { result = false });
            }

            var item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == profile.DailyQuestionID).FirstOrDefault();
            if (item == null)
            {
                return Json(new { result = false });
            }

            models.GetTable<PDQTask>().InsertOnSubmit(new PDQTask
            {
                QuestionID = item.QuestionID,
                SuggestionID = suggestionID,
                UID = profile.UID,
                TaskDate = DateTime.Now
            });

            models.SubmitChanges();
            if (item.PDQSuggestion.Any(s => s.SuggestionID == suggestionID && s.RightAnswer == true))
            {
                if (item.PDQQuestionExtension != null)
                {
                    return Json(new { result = true, message = item.PDQQuestionExtension.BonusPoint.ToString() });
                }
            }

            return Json(new { result = false });

        }


        [HttpGet]
        public ActionResult EditDailyQuestion(int? questionID)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            var viewModel = new PDQQuestionViewModel
            {
                AskerID = profile.UID
            };
            var item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == questionID).FirstOrDefault();
            if (item != null)
            {
                viewModel.QuestionID = item.QuestionID;
                viewModel.Question = item.Question;
                viewModel.QuestionType = item.QuestionType;
                viewModel.GroupID = item.GroupID;
                viewModel.AskerID = item.AskerID;
                var rightAns = item.PDQSuggestion.Where(s => s.RightAnswer == true).FirstOrDefault();
                viewModel.RightAnswerIndex = rightAns != null ? item.PDQSuggestion.IndexOf(rightAns) : 0;
                viewModel.Suggestion = item.PDQSuggestion.Select(s => s.Suggestion).ToArray();
                viewModel.BonusPoint = item.PDQQuestionExtension.BonusPoint;
            }

            ViewBag.ViewModel = viewModel;
            return View("EditDailyQuestion", profile);

        }

        public ActionResult EditDailyQuestion(PDQQuestionViewModel viewModel)
        {
            UserProfile profile = HttpContext.GetUser();
            if (profile == null)
            {
                return Redirect(FormsAuthentication.LoginUrl);
            }

            ViewBag.ViewModel = viewModel;
            if (!viewModel.AskerID.HasValue)
            {
                ModelState.AddModelError("AskerID", "請選擇提問者!!");
                ViewBag.ModelState = ModelState;
                return View("EditDailyQuestion", profile);
            }
            if (!viewModel.BonusPoint.HasValue)
            {
                ModelState.AddModelError("BonusPoint", "請輸入回饋點數!!");
                ViewBag.ModelState = ModelState;
                return View("EditDailyQuestion", profile);
            }
            if (viewModel.Suggestion == null || viewModel.Suggestion.Length < 1)
            {
                ModelState.AddModelError("Suggestion", "請輸入選項!!");
                ViewBag.ModelState = ModelState;
                return View("EditDailyQuestion", profile);
            }

            PDQQuestion item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == viewModel.QuestionID).FirstOrDefault();
            if (item == null)
            {
                item = new PDQQuestion
                {
                    PDQQuestionExtension = new PDQQuestionExtension { }
                };

                models.GetTable<PDQQuestion>().InsertOnSubmit(item);

                for (int i = 0; i < viewModel.Suggestion.Length; i++)
                {
                    item.PDQSuggestion.Add(new PDQSuggestion { });
                }
            }


            item.Question = viewModel.Question;
            item.QuestionType = viewModel.QuestionType;
            item.GroupID = viewModel.GroupID;
            item.AskerID = viewModel.AskerID;

            for (int i = 0; i < viewModel.Suggestion.Length; i++)
            {
                item.PDQSuggestion[i].Suggestion = viewModel.Suggestion[i];
                item.PDQSuggestion[i].RightAnswer = i == viewModel.RightAnswerIndex;
            }

            item.PDQQuestionExtension.BonusPoint = viewModel.BonusPoint;

            models.SubmitChanges();
            ViewBag.Message = "資料已儲存!!";

            return ListDailyQuestion();

        }

        public ActionResult LearnerFitness(int uid)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile == null)
            {
                return this.TransferToAction("ListLearners", "Member", new { Message = "學員資料不存在!!" });
            }
            ViewBag.Profile = profile;

            var items = models.GetTable<LearnerFitnessAssessment>().Where(f => f.UID == uid);
            ViewBag.FitnessItems = models.GetTable<FitnessAssessmentItem>().ToArray();

            return View(items);
        }

        public ActionResult FitnessAssessmentList(int uid)
        {
            return LearnerFitness(uid);
        }

        public ActionResult LearnerFitnessItem(int uid)
        {
            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile == null)
            {
                return this.TransferToAction("ListLearners", "Member", new { Message = "學員資料不存在!!" });
            }
            ViewBag.Profile = profile;
            return View(models.GetTable<FitnessAssessmentItem>());
        }

        public ActionResult EditFitnessAssessment(int assessmentID)
        {
            var item = models.GetTable<LearnerFitnessAssessment>().Where(f => f.AssessmentID == assessmentID).FirstOrDefault();
            if (item == null)
                return new EmptyResult();

            return View(item);
        }

        public ActionResult DeleteFitnessAssessment(int assessmentID,int?[] itemID)
        {
            if (itemID == null)
                return EditFitnessAssessment(assessmentID);

            if (itemID.Length == 0)
                return Json(new { result = false });

            foreach(var item in itemID)
            {
                models.DeleteAny<LearnerFitnessAssessmentResult>(r => r.AssessmentID == assessmentID && r.ItemID == item);
            }

            return Json(new { result = true });

        }

        public ActionResult CommitFitnessAssessment(int assessmentID, FitnessAssessmentViewModel[] fitnessItem)
        {
            var fitnessAssessment = models.GetTable<LearnerFitnessAssessment>().Where(f => f.AssessmentID == assessmentID).FirstOrDefault();
            if (fitnessAssessment == null || fitnessItem==null)
            {
                return Json(new { result = false , message="檢測資料項目不存在!!" });
            }

            foreach(var item in fitnessItem)
            {
                var assessment = fitnessAssessment.LearnerFitnessAssessmentResult.Where(f => f.ItemID == item.ItemID).FirstOrDefault();
                if (assessment != null && item.Assessment.HasValue)
                    assessment.Assessment = item.Assessment.Value;
            }

            models.SubmitChanges();
            return Json(new { result = true });

        }


        public ActionResult UpdateFitnessAssessment(int uid, int itemID, DateTime assessmentDate, decimal assessment)
        {
            var fitnessAssessment = models.GetTable<LearnerFitnessAssessment>().Where(f => f.UID == uid && f.AssessmentDate == assessmentDate).FirstOrDefault();
            if (fitnessAssessment == null)
            {
                fitnessAssessment = new LearnerFitnessAssessment
                {
                    UID = uid,
                    AssessmentDate = assessmentDate
                };
                models.GetTable<LearnerFitnessAssessment>().InsertOnSubmit(fitnessAssessment);
            }

            var item = fitnessAssessment.LearnerFitnessAssessmentResult.Where(r => r.ItemID == itemID).FirstOrDefault();
            if (item == null)
            {
                item = new LearnerFitnessAssessmentResult
                {
                    ItemID = itemID
                };
                fitnessAssessment.LearnerFitnessAssessmentResult.Add(item);
            }

            item.Assessment = assessment;
            models.SubmitChanges();

            return Json(new { result = true });

        }

        public ActionResult AverageFitnessAssessment(int? uid)
        {
            IQueryable<V_LearnerFitenessAssessment> items = models.GetTable<V_LearnerFitenessAssessment>();

            UserProfile profile = models.GetTable<UserProfile>().Where(u => u.UID == uid).FirstOrDefault();
            if (profile != null)
            {
                ViewBag.Profile = profile;
                UserProfileExtension extension = profile.UserProfileExtension;
                items = items.Where(v => v.Gender == extension.Gender
                    && v.AthleticLevel == extension.AthleticLevel);
            }

            ViewBag.FitnessItems = models.GetTable<FitnessAssessmentItem>().ToArray();
            return View(items);
        }

        public ActionResult AverageFitness()
        {
            return View();
        }



    }
}