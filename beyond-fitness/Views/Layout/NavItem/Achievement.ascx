﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Linq.Expressions" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="WebHome.Helper" %>
<%@ Import Namespace="WebHome.Models.Locale" %>
<%@ Import Namespace="WebHome.Models.ViewModel" %>
<%@ Import Namespace="WebHome.Models.DataEntity" %>
<%@ Import Namespace="WebHome.Controllers" %>

<li>
    <a href="#" title="績效管理"><i class="fas fa-lg fa-fw fa-chart-bar fa-spin"></i><span class="menu-item-parent">績效管理</span></a>
    <ul>
<%--        <li>
            <a href="<%= Url.Action("AchievementIndex", "Accounting") %>"><i class="fa fa-fw fa-trophy"></i>業績統計表</a>
        </li>
        <li>
            <a href="achievementchartbybranch.html"><i class="fa fa-fw fa-chart-pie"></i>分店業績總覽</a>
        </li>--%>
        <li>
            <a href="<%= Url.Action("LessonIndex","Achievement") %>"><i class="fas fa-fw fa-clipboard-list"></i>上課統計表</a>
        </li>
<%--        <li>
            <a href="lessonbarchartbybranch.html"><i class="far fa-fw fa-chart-bar"></i>分店上課總覽</a>
        </li>
        <li>
            <a href="salarylist.html"><i class="fas fa-fw fa-hand-holding-heart fa-spin"></i>每月薪資統計表</a>
        </li>--%>
    </ul>
</li>

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
