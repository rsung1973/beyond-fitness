<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<header id="header">
    <div id="logo-group">

        <!-- PLACE YOUR LOGO HERE -->
        <span id="logo">
            <img src="~/img/logo.png" alt="SmartAdmin" runat="server" />
        </span>
        <!-- END LOGO PLACEHOLDER -->
        <%  Html.RenderPartial("~/Views/BulletinBoard/ActivityNotification.ascx"); %>

    </div>

    <!-- pulled right: nav area -->
    <div class="pull-right">

        <!-- collapse menu button -->
        <div id="hide-menu" class="btn-header pull-right">
            <span><a href="javascript:void(0);" data-action="toggleMenu" title="Collapse Menu"><i class="fa fa-reorder"></i></a></span>
        </div>
        <!-- end collapse menu -->

        <!-- #MOBILE -->
        <!-- Top menu profile link : this shows only when top menu is active -->
        <%  if (_userProfile == null || _userProfile.LevelID == (int)Naming.MemberStatusDefinition.ReadyToRegister)
            {
                Html.RenderPartial("~/Views/Layout/SignIn.ascx");
            }
            else
            {
                Html.RenderPartial("~/Views/Layout/SignOut.ascx",_userProfile);
            }   %>

        <!-- logout button -->
        <div id="logout" class="btn-header transparent pull-right">
            <span><a href="login.html" title="Sign Out" data-action="userLogout" data-logout-msg="為了安全起見，建議您登出後將網頁關閉！"><i class="fa fa-sign-out"></i></a></span>
        </div>
        <!-- end logout button -->

        <!-- FB mobile button (this is hidden till mobile view port) -->
        <div class="btn-header transparent pull-right">
            <span><a href="https://www.facebook.com/beyond.fitness.pro/" target="_blank" title="Facebook"><i class="fa fa-facebook"></i></a></span>
        </div>
        <!-- end FB mobile button -->

        <!-- fullscreen button -->
        <div id="fullscreen" class="btn-header transparent pull-right">
            <span><a href="javascript:void(0);" data-action="launchFullscreen" title="Full Screen"><i class="fa fa-arrows-alt"></i></a></span>
        </div>
        <!-- end fullscreen button -->

        <!-- multiple lang dropdown : find all flags in the flags page -->
        <ul class="header-dropdown-list hidden-xs">
            <li>
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                    <img runat="server" src="~/img/blank.gif" class="flag flag-tw" alt="United States">
                    <span>中文 </span><i class="fa fa-angle-down"></i></a>
            </li>
        </ul>
        <!-- end multiple lang -->
    </div>
    <!-- end pulled right: nav area -->

</header>
<!-- END HEADER -->


<script runat="server">

    ModelStateDictionary _modelState;
    ModelSource<UserProfile> models;
    UserProfile _userProfile;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        _modelState = (ModelStateDictionary)ViewBag.ModelState;
        models = ((SampleController<UserProfile>)ViewContext.Controller).DataSource;
        _userProfile = Context.GetUser();
    }

</script>
