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
        <th>時間</th>
        <th>學員</th>
        <th>功能</th>
    </tr>
    <%  foreach (var item in _items)
        { %>
    <tr>
        <td><%= item.Hour %>:00 - <%= item.Hour+1 %>:00</td>
        <td><%= item.RegisterLesson.GroupingMemberCount > 1
                     ? String.Join("<br/>", item.RegisterLesson.GroupingLesson.RegisterLesson.Select(l=>l.UserProfile.RealName))
                     : item.RegisterLesson.UserProfile.RealName %></td>
        <td>
            <a href="before-class.htm" class="btn btn-system btn-small">預編課程 <i class="fa fa-edit" aria-hidden="true"></i></a>
            <a href="after-class.htm" class="btn btn-system btn-small">上課囉 <i class="fa fa-heartbeat" aria-hidden="true"></i></a>
            <a href="#" class="btn btn-system btn-small">取消預約 <i class="fa fa-calendar-times-o" aria-hidden="true"></i></a>
            <a href="preview-vip.htm" class="btn btn-system btn-small">檢視 <i class="fa fa-eye" aria-hidden="true"></i></a>
        </td>
    </tr>
    <%  } %>
</table>


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
        _items = (IEnumerable<LessonTimeExpansion>)this.Model;
    }

</script>
