﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
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
        || _userProfile.IsAssistant()))
    { %>
<li>
    <a href="#" title="上稿管理"><i class="fa fa-lg fa-fw fa-edit"></i><span class="menu-item-parent">上稿管理</span></a>
    <ul>
        <li>
            <a href="<%= VirtualPathUtility.ToAbsolute("~/Information/Publish") %>"><i class="fa fa-fw fa-puzzle-piece"></i>專業知識</a>
        </li>
        <li>
            <a href="<%= Url.Action("ListDailyQuestion","Activity") %>"><i class="fa fa-fw fa-question"></i>問與答</a>
        </li>
        <%  if (_userProfile.IsAuthorizedSysAdmin())
            { %>
                <li>
                    <a href="<%= Url.Action("MotivationalWords","Information") %>"><i class="fa fa-fw fa-volume-control-phone"></i>每日激勵小語</a>
                </li>
        <%  } %>
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
