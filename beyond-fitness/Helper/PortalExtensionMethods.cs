using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using CommonLib.DataAccess;
using MessagingToolkit.QRCode.Codec;
using Utility;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;
using WebHome.Models.ViewModel;
using WebHome.Properties;

namespace WebHome.Helper
{
    public static class PortalExtensionMethods
    {
        public static String ProcessLogin(this Controller controller, UserProfile item)
        {
            UrlHelper url = new UrlHelper(controller.ControllerContext.RequestContext);
            if (item.IsAuthorizedSysAdmin())
            {
                return url.Action("Index", "CoachFacet", new { KeyID = item.UID.EncryptKey() });
            }
            else if (item.UserRoleAuthorization.Any(r => r.RoleID == (int)Naming.RoleID.Coach || r.RoleID == (int)Naming.RoleID.Manager || r.RoleID == (int)Naming.RoleID.ViceManager))
            {
                return url.Action("Index", "CoachFacet", new { KeyID = item.UID.EncryptKey() });
            }
            else if (item.IsAssistant())
            {
                return url.Action("Index", "CoachFacet", new { KeyID = item.UID.EncryptKey() });
            }
            else if (item.IsAccounting())
            {
                return url.Action("TrustIndex", "Accounting");
            }
            else if (item.IsServitor())
            {
                return url.Action("PaymentIndex", "Payment");
            }

            switch ((Naming.RoleID)item.UserRole[0].RoleID)
            {
                case Naming.RoleID.Administrator:
                    return url.Action("Index", "CoachFacet");

                case Naming.RoleID.Coach:
                case Naming.RoleID.Manager:
                case Naming.RoleID.ViceManager:
                case Naming.RoleID.Officer:
                case Naming.RoleID.Assistant:
                    return url.Action("Index", "CoachFacet", new { KeyID = item.UID.EncryptKey() });

                //case Naming.RoleID.Assistant:
                //    return url.Action("Index", "CoachFacet");

                case Naming.RoleID.Accounting:
                    return url.Action("TrustIndex", "Accounting");

                case Naming.RoleID.Learner:
                    return url.Action("LearnerIndex", "LearnerFacet");

                case Naming.RoleID.Servitor:
                    return url.Action("PaymentIndex", "Payment");

                case Naming.RoleID.FreeAgent:
                    return url.Action("FreeAgent", "Account");

            }

            return url.Action("Index", "Account"); ;
        }
        public static PDQQuestion PromptLearnerDailyQuestion<TEntity>(this ModelSource<TEntity> models, UserProfile profile)
                    where TEntity : class, new()
        {

            if (models.GetTable<PDQTask>().Any(t => t.UID == profile.UID
                 && t.TaskDate >= DateTime.Today && t.TaskDate < DateTime.Today.AddDays(1)
                 && t.PDQQuestion.GroupID == 6))
            {
                return null;
            }

            if (models.GetTable<PDQTask>().Count(t => t.UID == profile.UID && t.PDQQuestion.GroupID == 6) >=
                models.GetTable<RegisterLesson>().Where(r => r.UID == profile.UID && r.LessonPriceType.Status != (int)Naming.LessonPriceStatus.在家訓練)
                    .Select(r => r.GroupingLesson).Sum(g => g.LessonTime.Count(l => l.LessonPlan.CommitAttendance.HasValue || l.LessonAttendance != null)))
            {
                return null;
            }

            IQueryable<PDQQuestion> questItems = models.GetTable<PDQQuestion>().Where(q => q.GroupID == 6)
                .Join(models.GetTable<PDQQuestionExtension>().Where(t => !t.Status.HasValue),
                    q => q.QuestionID, t => t.QuestionID, (q, t) => q);
            int[] items = questItems
                .Select(q => q.QuestionID)
                .Where(q => !models.GetTable<PDQTask>().Where(t => t.UID == profile.UID).Select(t => t.QuestionID).Contains(q)).ToArray();

            if (items.Length == 0)
            {
                items = questItems
                .Select(q => q.QuestionID).ToArray();
            }

            profile.DailyQuestionID = items[DateTime.Now.Ticks % items.Length];

            var item = models.GetTable<PDQQuestion>().Where(q => q.QuestionID == profile.DailyQuestionID).FirstOrDefault();
            return item;
        }

        public static IQueryable<CourseContract> PromptExpiringContract<TEntity>(this ModelSource<TEntity> models)
                where TEntity : class, new()
        {
            var revisionID = models.GetTable<CourseContractRevision>().Where(r => r.Reason == "展延")
                .Select(r => r.RevisionID);
            var items = models.GetTable<CourseContract>()
                .Where(c => !c.RegisterLessonContract.Any(r => r.RegisterLesson.Attended == (int)Naming.LessonStatus.課程結束))
                .Where(c => c.Expiration < DateTime.Today.AddMonths(1))
                .Where(c => !revisionID.Any(r => r == c.ContractID));
            return items;
        }
    }


}