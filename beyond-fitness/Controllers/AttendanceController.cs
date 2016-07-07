using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;

namespace WebHome.Controllers
{
    public class AttendanceController : LessonsController
    {
        public AttendanceController() : base() { }
        // GET: Attendance

        public ActionResult SaveAssessment(TrainingAssessmentViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model = storeAssessment(viewModel, out result);

            if (result != null)
                return result;
            
            models.SubmitChanges();
            ViewBag.Message = "資料存檔完成!!";

            return View("TrainingPlan", model);

        }

        public ActionResult SaveThenAddTraining(TrainingAssessmentViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model = storeAssessment(viewModel, out result);

            if (result != null)
                return result;

            models.SubmitChanges();

            return AddTraining(viewModel);
        }

        private LessonTimeExpansion storeAssessment(TrainingAssessmentViewModel viewModel,out ActionResult result)
        {
            result = null;
            LessonTimeExpansion model = null;
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);
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

            if (model.LessonTime.TrainingPlan.Count == 0)
            {
                ViewBag.Message = "尚未編定上課內容";
                result = RedirectToAction("Coach", "Account", new { lessonDate = item.ClassDate, hour = item.Hour, registerID = item.RegisterID, lessonID = item.LessonID });
                return null;
            }

            int itemIdx = 0;
            for (int idx = 0; idx < viewModel.Conclusion.Length && idx < model.LessonTime.TrainingPlan.Count; idx++)
            {
                model.LessonTime.TrainingPlan[idx].TrainingExecution.Conclusion = viewModel.Conclusion[idx];
                foreach (var trainItem in model.LessonTime.TrainingPlan[idx].TrainingExecution.TrainingItem)
                {
                    trainItem.ActualStrength = viewModel.ActualStrength[itemIdx];
                    trainItem.ActualTurns = viewModel.ActualTurns[itemIdx];
                    itemIdx++;
                }
            }

            LessonTrend trend = model.LessonTime.LessonTrend;
            if (trend == null)
                trend = model.LessonTime.LessonTrend = new LessonTrend { };
            trend.ActionLearning = viewModel.ActionLearning;
            trend.PostureRedress = viewModel.PostureRedress;
            trend.Training = viewModel.Training;

            FitnessAssessment fitness = model.LessonTime.FitnessAssessment;
            if (fitness == null)
                fitness = model.LessonTime.FitnessAssessment = new FitnessAssessment { };
            fitness.Flexibility = viewModel.Flexibility;
            fitness.Cardiopulmonary = viewModel.Cardiopulmonary;
            fitness.Endurance = viewModel.Endurance;
            fitness.ExplosiveForce = viewModel.ExplosiveForce;
            fitness.SportsPerformance = viewModel.SportsPerformance;
            fitness.Strength = viewModel.Strength;

            model.LessonTime.AttendingCoach = viewModel.CoachID;
            model.LessonTime.RegisterLesson.Attended = (int)Naming.LessonStatus.上課中;

            LessonPlan plan = model.LessonTime.LessonPlan;
            plan.Warming = viewModel.Warming;
            plan.RecentStatus = viewModel.RecentStatus;
            model.RegisterLesson.UserProfile.RecentStatus = viewModel.RecentStatus;
            plan.EndingOperation = viewModel.EndingOperation;
            plan.Remark = viewModel.Remark;

            return model;
        }



        public ActionResult CommitAssessment(TrainingAssessmentViewModel viewModel)
        {
            ActionResult result;
            LessonTimeExpansion model = storeAssessment(viewModel, out result);

            if (result != null)
                return result;

            LessonAttendance attendance = model.LessonTime.LessonAttendance;
            if (attendance == null)
                attendance = model.LessonTime.LessonAttendance = new LessonAttendance { };
            attendance.CompleteDate = DateTime.Now;

            models.SubmitChanges();
            ViewBag.Message = "資料存檔完成!!";

            HttpContext.RemoveCache(CachingKey.Training);
            return RedirectToAction("Coach", "Account", new { lessonDate = model.ClassDate, hour = model.Hour, registerID = model.RegisterID, lessonID = model.LessonID });

        }


        public ActionResult EndAssessment()
        {
            LessonTimeExpansion item = (LessonTimeExpansion)HttpContext.GetCacheValue(CachingKey.Training);

            if (item == null)
            {
                return RedirectToAction("Coach", "Account");
            }

            HttpContext.RemoveCache(CachingKey.Training);
            return RedirectToAction("Coach", "Account", new { lessonDate = item.ClassDate, hour = item.Hour, registerID = item.RegisterID, lessonID = item.LessonID });
        }


    }
}