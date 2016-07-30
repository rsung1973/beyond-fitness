<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<%  if (_userProfile != null
    && (_userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Administrator
        || _userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.Coach
        || _userProfile.CurrentUserRole.RoleID == (int)Naming.RoleID.FreeAgent))
    { %>
<li>
    <a href="classlist.html" title="課程管理"><i class="fa fa-lg fa-fw fa-calendar"></i><span class="menu-item-parent">課程管理</span></a>
    <ul>
        <li>
            <a href="classlist.html"><i class="fa fa-fw fa-dashboard"></i>我的課程總覽</a>
        </li>
        <li>
            <a href="searchvip.html"><i class="fa fa-fw fa-search"></i>我的VIP</a>
        </li>
        <li>
            <a href="bookingclass.html"><i class="fa fa-fw fa-bookmark"></i>登記上課時間</a>
        </li>
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
        _userProfile = this.Model as UserProfile;
    }

</script>
