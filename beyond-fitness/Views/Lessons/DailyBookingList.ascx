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
        <th class="col-xs-1 col-md-2 text-center">日期</th>
        <th class="col-xs-2 col-md-3 text-center">時段</th>
        <th class="col-xs-2 col-md-2 text-center">人數</th>
        <th class="col-xs-7 col-md-5">功能</th>
    </tr>
    <%  var items = _items.GroupBy(l => new { ClassDate = l.ClassDate, Hour = l.Hour });
        if (items != null && items.Count() > 0)
        {
            foreach (var item in items)
            {%>
    <tr>
        <td class="text-center"><%= item.Key.ClassDate.ToString("MM/dd") %></td>
        <td class="text-center"><%= item.Key.Hour %>:00 - <%= item.Key.Hour + 1 %>:00</td>
        <td class="text-center"><%= item.Count() %></td>
        <td>
            <a class="btn-system btn-small" onclick="showAttendee('<%= String.Format("{0:yyyy/MM/dd}", item.Key.ClassDate) %>',<%= item.Key.Hour %>);" >學員清單 <i class="fa fa-list-alt" aria-hidden="true"></i></a>
        </td>
    </tr>
    <%      } %>
    <%  }
        else
        { %>
    <tr>
        <td class="text-center" colspan="4">本日無學員上課!!</td>
    </tr>
    <%  } %>
</table>
<script>
    function showAttendee(lessonDate, hour) {
        pageParam.hour = hour;
        $('#loading').css('display', 'table');
        $('#attendeeList').load('<%= VirtualPathUtility.ToAbsolute("~/Lessons/DailyBookingMembers") %>', { 'lessonDate': lessonDate, 'hour': hour }, function () {
            $('#loading').css('display', 'none');
        });
    }
</script>

<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    DateTime? _lessonDate;
    IEnumerable<LessonTimeExpansion> _items;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _lessonDate = (DateTime?)this.Model;
        if(ViewBag.DataItems!=null)
        {
            _items = (IEnumerable<LessonTimeExpansion>)ViewBag.DataItems;
        }
        else if (ViewBag.EndQueryDate == null)
        {
            _items = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate == _lessonDate.Value.Date);
        }
        else
        {
            DateTime? endDate = (DateTime?)ViewBag.EndQueryDate;
            _items = models.GetTable<LessonTimeExpansion>().Where(l => l.ClassDate >= _lessonDate.Value.Date
                && l.ClassDate <= endDate.Value);
        }
    }

</script>
