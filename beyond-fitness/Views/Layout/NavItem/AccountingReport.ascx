<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_userProfile.IsAccounting())
    { %>
<li>
    <a href="#" title="報表管理"><i class="fa fa-lg fa-fw fa-calculator"></i><span class="menu-item-parent">報表管理</span></a>
    <ul>
        <li>
            <a href="<%= Url.Action("StaffAchievement", "Report") %>"><i class="fa fa-fw fa-trophy"></i>業績統計表</a>
        </li>
    <%  Html.RenderPartial("~/Views/Layout/NavItem/PaymentList.ascx", _userProfile);       %>
    </ul>
</li>
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
