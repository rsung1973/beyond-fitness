<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<table class="table">
    <tr class="info">
        <th>日期</th>
        <th>時段</th>
        <th>人數</th>
        <th>功能</th>
    </tr>
    <%  var items = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == _lessonDate.Value.Date)
            .GroupBy(l => l.Hour);
        if (items != null && items.Count() > 0)
        {
            foreach (var item in items)
            {%>
    <tr>
        <td><%= _lessonDate.Value.ToString("MM/dd") %></td>
        <td><%= item.Key %>:00 - <%= item.Key + 1 %>:00</td>
        <td><%= item.Count() %></td>
        <td>
            <a class="btn-system btn-small" onclick="showAttendee('<%= String.Format("{0:yyyy/MM/dd}",_lessonDate) %>',<%= item.Key %>);" >學員清單 <i class="fa fa-list-alt" aria-hidden="true"></i></a>
        </td>
    </tr>
    <%      } %>
    <%  }
        else
        { %>
    <tr>
        <td colspan="4">本日無學員上課!!</td>
    </tr>
    <%  } %>
</table>
<script>
    function showAttendee(lessonDate, hour) {
        pageParam.hour = hour;
        $('#attendeeList').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingMembers") %>', { 'lessonDate': lessonDate, 'hour': hour }, function () {
        });
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DateTime? _lessonDate;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _lessonDate = (DateTime?)this.Model;
    }

</script>
