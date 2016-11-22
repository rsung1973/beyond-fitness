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
        {
            foreach (var item in _items)
            {
                var lessonCount = item.Lessons - (item.AttendedLessons ?? 0) - (item.RegisterGroupID.HasValue ? item.GroupingLesson.LessonTime.Count : item.LessonTime.Count);
                bool pdqStatus = completePDQ(item);
                var pastReserved = item.LessonTime.Count(l => l.ClassTime < DateTime.Today.AddDays(1) && l.LessonAttendance == null);
                var incomingReserved = item.LessonTime.Count(l => l.ClassTime >= DateTime.Today);
                %>
                <label class="<%= lessonCount>0 && pdqStatus ? "radio" : "radio state-disabled" %>">
                <input type="radio" name="registerID" value="<%= item.RegisterID %>" <%= lessonCount>0 && pdqStatus ? null : "disabled" %> />
                <i></i><%= item.UserProfile.RealName %>「<%= item.Lessons %>堂-<%= item.LessonPriceType.Description %>」
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
                    <%  if (!pdqStatus)
                        { %>
                    <span class="label label-danger">
                        <li class="fa fa-exclamation-triangle"></li>PDQ尚未登打或登打不完全!!!</span>
                    <%  } %>
                </label>

    <%      }
        }
        else
        { %>
            查無相符條件的上課資料!!
    <%  } %>


<script runat="server">

    ModelStateDictionary _modelState;
    IEnumerable<RegisterLesson> _items;
    ModelSource<UserProfile> models;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _items = (IEnumerable<RegisterLesson>)this.Model;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
    }

    bool completePDQ(RegisterLesson lesson)
    {
        return lesson.UserProfile.PDQTask
            .Select(t => t.PDQQuestion)
            .Where(q => q.PDQQuestionExtension == null).Count() >= 36;
    }

</script>
