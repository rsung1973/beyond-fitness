<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_userProfile != null && _userProfile.CurrentUserRole.RoleID != (int)Naming.RoleID.Learner)
    { %>
<ul id="sparks">
    <li class="sparks-info">
        <h5>今日上課人數 <span class="txt-color-blue"><% Html.RenderAction("DailyLearnerCount", "Lessons", new { date = DateTime.Today }); %>人</span></h5>
        <div class="sparkline txt-color-blue">
            <% Html.RenderAction("DailyBookingHourlyList", "Lessons", new { lessonDate = DateTime.Today }); %>
        </div>
    </li>
</ul>
<%  } %>

<script runat="server">

    ModelStateDictionary _modelState;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        _userProfile = Context.GetUser();
    }

</script>
