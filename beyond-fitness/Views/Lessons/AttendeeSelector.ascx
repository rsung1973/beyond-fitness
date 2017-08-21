<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

    <% if (_items != null && _items.Count() > 0)
        {   %>
            <label class="label">依您輸入的關鍵字，搜尋結果如下：</label>
        <%  foreach (var item in _items)
            {
                var lessonCount = item.Lessons - (item.AttendedLessons ?? 0) - (item.RegisterGroupID.HasValue ? item.GroupingLesson.LessonTime.Count : item.LessonTime.Count);
                bool pdqStatus = completePDQ(item);
                bool paymentStatus = hasPayment(item);
                bool groupComplete = item.GroupingMemberCount == item.GroupingLesson.RegisterLesson.Count ? true : false;
                bool questionnaireStatus = item.QuestionnaireRequest.Any(q => q.Status!=(int)Naming.IncommingMessageStatus.拒答 && q.PDQTask.Count == 0);
                var pastReserved = item.LessonTime.Count(l => l.ClassTime < DateTime.Today.AddDays(1) && l.LessonAttendance == null);
                var incomingReserved = item.LessonTime.Count(l => l.ClassTime >= DateTime.Today);
                %>
                <label class="<%= lessonCount>0 && pdqStatus && paymentStatus && groupComplete && !questionnaireStatus ? "radio" : "radio state-disabled" %>">
                <input type="radio" name="registerID" value="<%= item.RegisterID %>" <%= lessonCount>0 && pdqStatus && paymentStatus && groupComplete && !questionnaireStatus ? null : "disabled" %> />
                <i></i>
                    <%  if (item.RegisterLessonContract != null)
                        {
                            var contract = item.RegisterLessonContract.CourseContract;   %>
                            <%= String.Join("｜", contract.CourseContractMember.Select(m=>m.UserProfile).ToArray()
                                     .Select(u=>u.FullName())) %>【<%= contract.CourseContractType.TypeName %>(<%= contract.LessonPriceType.DurationInMinutes %>分鐘)】/ 剩餘<%= lessonCount %>堂
                            <%  if (pastReserved > 0)
                                {   %>
                                    （已預約未完成上課<%= pastReserved %>堂）   
                            <%  } %>
                    <%  }
                        else
                        {   %>
                            <%= item.UserProfile.RealName %>「<%= item.Lessons %>堂-<%= item.LessonPriceType.Description %>」
                        <%  if( item.GroupingMemberCount>1)
                            {   %>
                            <li class="fa fa-group"></li>
                            團體《<%= String.Join("·", models.GetTable<GroupingLesson>().Where(g => g.GroupID == item.RegisterGroupID)
                                                        .Join(models.GetTable<RegisterLesson>().Where(r => r.RegisterID != item.RegisterID),
                                                            g => g.GroupID, r => r.RegisterGroupID, (g, r) => r)
                                                        .Select(r => r.UserProfile.RealName)) %>》
                        <%  }
                            else
                            {   %>
                            <li class="fa fa-child"></li>
                            個人
                        <%  } %>(剩餘<%= lessonCount %>堂
                            <%  if (incomingReserved > 0)
                                { %>
                                    [已預約<%= incomingReserved %>堂]
                            <%  } %>
                            <%  if (pastReserved > 0)
                                { %>
                                    [已預約未完成上課<%= pastReserved %>堂]
                            <%  } %>
                            )
                    <%  } %>
                    <%  if (!pdqStatus)
                        { %>
                    <span class="label label-danger">
                        <li class="fa fa-exclamation-triangle"></li> PDQ尚未登打或登打不完全!!!</span>
                    <%  }
                        if (!paymentStatus)
                        {
                            if(item.GroupingMemberCount>1)
                            {
                                foreach(var r in item.GroupingLesson.RegisterLesson)
                                {
                                    if (r.IntuitionCharge == null || r.IntuitionCharge.TuitionInstallment.Count() == 0)
                                    {%>
                                        <span class="label label-danger">
                                            <li class="fa fa-exclamation-triangle"></li> <%= r.UserProfile.RealName %>未有付款紀錄!!!</span>
                        <%  
                                    }
                                }
                            }
                            else
                            {   %>
                                <span class="label label-danger">
                                    <li class="fa fa-exclamation-triangle"></li> 未有付款紀錄!!!</span>
                    <%      }
                        }
                        if (!groupComplete)
                        { %>
                    <span class="label label-danger">
                        <li class="fa fa-exclamation-triangle"></li> 請設定團體學員!!!</span>
                    <%  }
                        if (questionnaireStatus)
                        { %>
                    <span class="label label-warning">
                        <li class="fa fa-exclamation-triangle"></li>
                        階段性調整計劃未填寫，請通知學員登入系統完成階段性調整計劃。
                    </span>
                    <%  } %>
                </label>

    <%      }
        }
        else
        { %>
            <span>查無相符條件的上課資料!!</span>
    <%  } %>


<script runat="server">

    ModelStateDictionary _modelState;
    IQueryable<RegisterLesson> _items;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _items = (IQueryable<RegisterLesson>)this.Model;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

    bool completePDQ(RegisterLesson lesson)
    {
        return lesson.UserProfile.PDQTask
            .Select(t => t.PDQQuestion)
            .Where(q => q.PDQQuestionExtension == null).Count() >= 36;
    }

    bool hasPayment(RegisterLesson lesson)
    {
        if (lesson.LessonPriceType.ListPrice > 0)
        {
            if (lesson.GroupingMemberCount > 1)
            {
                foreach (var r in lesson.GroupingLesson.RegisterLesson)
                {
                    if (r.IntuitionCharge == null || r.IntuitionCharge.TuitionInstallment.Count() == 0)
                        return false;
                }
                return true;
            }
            else
            {
                return lesson.IntuitionCharge != null
                    && lesson.IntuitionCharge.TuitionInstallment.Count() > 0;
            }
        }
        return true;
    }

</script>
