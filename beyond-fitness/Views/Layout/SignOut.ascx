﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<ul id="mobile-profile-img" class="header-dropdown-list hidden-xs padding-5">
    <li class="">
        <a href="#" class="dropdown-toggle no-margin userdropdown" data-toggle="dropdown">
            <img runat="server" src="~/img/avatars/male.png" alt="親愛的" class="online" />
        </a>
        <ul class="dropdown-menu pull-right">
            <li>
                <a href="javascript:void(0);" class="padding-10 padding-top-0 padding-bottom-0"><i class="fa fa-cog"></i>Setting</a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="profile.html" class="padding-10 padding-top-0 padding-bottom-0"><i class="fa fa-user"></i><u>P</u>rofile</a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="<%= VirtualPathUtility.ToAbsolute("~/Account/Logout") %>" class="padding-10 padding-top-5 padding-bottom-5" data-action="userLogout" data-logout-msg="為了安全起見，建議您登出後將網頁關閉！"><i class="fa fa-sign-out fa-lg"></i><strong><u>L</u>ogout</strong></a>
            </li>
        </ul>
    </li>
</ul>

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
